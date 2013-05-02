using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TTAPI_Sample_Console_OrderRouting
{
    using TradingTechnologies.TTAPI;
    using TradingTechnologies.TTAPI.Tradebook;

    /// <summary>
    /// Main TT API class
    /// </summary>
    class TTAPIFunctions : IDisposable
    {
        /// <summary>
        /// Declare the TTAPI objects
        /// </summary>
        private UniversalLoginTTAPI m_apiInstance = null;
        private WorkerDispatcher m_disp = null;
        private bool m_disposed = false;
        private InstrumentLookupSubscription m_req = null;
        private PriceSubscription m_ps = null;
        private InstrumentTradeSubscription m_ts = null;
        private string m_orderKey = "";


        /// <summary>
        /// Default constructor
        /// </summary>
        public TTAPIFunctions()
        {
        }

        /// <summary>
        /// Create and start the Dispatcher
        /// </summary>
        public void Start()
        {
            // Attach a WorkerDispatcher to the current thread
            m_disp = Dispatcher.AttachWorkerDispatcher();
            m_disp.BeginInvoke(new Action(Init));
            m_disp.Run();
        }

        /// <summary>
        /// Initialize TT API
        /// </summary>
        public void Init()
        {
            // Use "Universal Login" Login Mode
            TTAPI.UniversalLoginModeDelegate ulDelegate = new TTAPI.UniversalLoginModeDelegate(ttApiInitComplete);
            TTAPI.CreateUniversalLoginTTAPI(Dispatcher.Current, ulDelegate);
        }

        /// <summary>
        /// Event notification for status of TT API initialization
        /// </summary>
        public void ttApiInitComplete(UniversalLoginTTAPI api, Exception ex)
        {
            if (ex == null)
            {
                // Authenticate your credentials
                m_apiInstance = api;
                m_apiInstance.AuthenticationStatusUpdate += new EventHandler<AuthenticationStatusUpdateEventArgs>(apiInstance_AuthenticationStatusUpdate);
                m_apiInstance.Authenticate("USERNAME", "PASSWORD");
            }
            else
            {
                Console.WriteLine("TT API Initialization Failed: {0}", ex.Message);
                Dispose();
            }
        }

        /// <summary>
        /// Event notification for status of authentication
        /// </summary>
        public void apiInstance_AuthenticationStatusUpdate(object sender, AuthenticationStatusUpdateEventArgs e)
        {
            if (e.Status.IsSuccess)
            {
                // lookup an instrument
                m_req = new InstrumentLookupSubscription(m_apiInstance.Session, Dispatcher.Current,
                    new ProductKey(MarketKey.Cme, ProductType.Future, "ES"),
                    "Mar13");
                m_req.Update += new EventHandler<InstrumentLookupSubscriptionEventArgs>(m_req_Update);
                m_req.Start();
            }
            else
            {
                Console.WriteLine("TT Login failed: {0}", e.Status.StatusMessage);
                Dispose();
            }
        }

        /// <summary>
        /// Event notification for instrument lookup
        /// </summary>
        void m_req_Update(object sender, InstrumentLookupSubscriptionEventArgs e)
        {
            if (e.Instrument != null && e.Error == null)
            {
                // Instrument was found
                Console.WriteLine("Found: {0}", e.Instrument.Name);

                // Subscribe for Inside Market Data
                m_ps = new PriceSubscription(e.Instrument, Dispatcher.Current);
                m_ps.Settings = new PriceSubscriptionSettings(PriceSubscriptionType.InsideMarket);
                m_ps.FieldsUpdated += new FieldsUpdatedEventHandler(m_ps_FieldsUpdated);
                m_ps.Start();

                // Create a TradeSubscription to listen for order / fill events only for orders submitted through it
                m_ts = new InstrumentTradeSubscription(m_apiInstance.Session, Dispatcher.Current, e.Instrument, true, true, false, false);
                m_ts.OrderUpdated += new EventHandler<OrderUpdatedEventArgs>(m_ts_OrderUpdated);
                m_ts.OrderAdded += new EventHandler<OrderAddedEventArgs>(m_ts_OrderAdded);
                m_ts.OrderDeleted += new EventHandler<OrderDeletedEventArgs>(m_ts_OrderDeleted);
                m_ts.OrderFilled += new EventHandler<OrderFilledEventArgs>(m_ts_OrderFilled);
                m_ts.OrderRejected += new EventHandler<OrderRejectedEventArgs>(m_ts_OrderRejected);
                m_ts.Start();
            }
            else if (e.IsFinal)
            {
                // Instrument was not found and TT API has given up looking for it
                Console.WriteLine("Cannot find instrument: {0}", e.Error.Message);
                Dispose();
            }
        }

        /// <summary>
        /// Event notification for price update
        /// </summary>
        void m_ps_FieldsUpdated(object sender, FieldsUpdatedEventArgs e)
        {
            if (e.Error == null)
            {
                // Make sure that there is a valid bid
                if (e.Fields.GetBestBidPriceField().HasValidValue)
                {
                    if (m_orderKey == "")
                    {
                        // If there is no order working, submit one through the first valid order feed.
                        // You should use the order feed that is valid for your purposes.
                        OrderProfile op = new OrderProfile(e.Fields.Instrument.GetValidOrderFeeds()[0], e.Fields.Instrument);
                        op.BuySell = BuySell.Buy;
                        op.AccountName = "12345678";
                        op.AccountType = AccountType.Agent1;
                        op.OrderQuantity = Quantity.FromInt(e.Fields.Instrument, 10);
                        op.OrderType = OrderType.Limit;
                        op.LimitPrice = e.Fields.GetBestBidPriceField().Value;

                        if (!m_ts.SendOrder(op))
                        {
                            Console.WriteLine("Send new order failed.  {0}", op.RoutingStatus.Message);
                            Dispose();
                        }
                        else
                        {
                            m_orderKey = op.SiteOrderKey;
                            Console.WriteLine("Send new order succeeded.");
                        }
                    }
                    else if(m_ts.Orders.ContainsKey(m_orderKey) && 
                        m_ts.Orders[m_orderKey].LimitPrice != e.Fields.GetBestBidPriceField().Value)
                    {
                        // If there is a working order, reprice it if its price is not the same as the bid
                        OrderProfileBase op = m_ts.Orders[m_orderKey].GetOrderProfile();
                        op.LimitPrice = e.Fields.GetBestBidPriceField().Value;
                        op.Action = OrderAction.Change;

                        if (!m_ts.SendOrder(op))
                        {
                            Console.WriteLine("Send change order failed.  {0}", op.RoutingStatus.Message);
                        }
                        else
                        {
                            Console.WriteLine("Send change order succeeded.");
                        }
                    }
                }
            }
            else
            {
                if (e.Error.IsRecoverableError == false)
                {
                    Console.WriteLine("Unrecoverable price subscription error: {0}", e.Error.Message);
                    Dispose();
                }
            }
        }

        /// <summary>
        /// Event notification for order rejected
        /// </summary>
        void m_ts_OrderRejected(object sender, OrderRejectedEventArgs e)
        {
            Console.WriteLine("Order was rejected.");
        }

        /// <summary>
        /// Event notification for order filled
        /// </summary>
        void m_ts_OrderFilled(object sender, OrderFilledEventArgs e)
        {
            if (e.FillType == FillType.Full)
            {
                Console.WriteLine("Order was fully filled for {0} at {1}.", e.Fill.Quantity, e.Fill.MatchPrice);
            }
            else
            {
                Console.WriteLine("Order was partially filled for {0} at {1}.", e.Fill.Quantity, e.Fill.MatchPrice);
            }

            Console.WriteLine("Average Buy Price = {0} : Net Position = {1} : P&L = {2}", m_ts.ProfitLossStatistics.BuyAveragePrice,
                m_ts.ProfitLossStatistics.NetPosition, m_ts.ProfitLoss.AsPrimaryCurrency);
        }

        /// <summary>
        /// Event notification for order deleted
        /// </summary>
        void m_ts_OrderDeleted(object sender, OrderDeletedEventArgs e)
        {
            Console.WriteLine("Order was deleted.");
        }

        /// <summary>
        /// Event notification for order added
        /// </summary>
        void m_ts_OrderAdded(object sender, OrderAddedEventArgs e)
        {
            Console.WriteLine("Order was added with price of {0}.", e.Order.LimitPrice);
        }

        /// <summary>
        /// Event notification for order update
        /// </summary>
        void m_ts_OrderUpdated(object sender, OrderUpdatedEventArgs e)
        {
            Console.WriteLine("Order was updated with price of {0}.", e.NewOrder.LimitPrice);
        }

        /// <summary>
        /// Shuts down the TT API
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposing pattern implementation
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!m_disposed)
            {
                if (disposing)
                {
                    // Shutdown all subscriptions
                    if (m_req != null)
                    {
                        m_req.Dispose();
                        m_req = null;
                    }
                    if (m_ps != null)
                    {
                        m_ps.Dispose();
                        m_ps = null;
                    }
                    if (m_ts != null)
                    {
                        m_ts.Dispose();
                        m_ts = null;
                    }

                    // Shutdown the Dispatcher
                    if (m_disp != null)
                    {
                        m_disp.BeginInvokeShutdown();
                        m_disp = null;
                    }

                    // Shutdown the TT API
                    if (m_apiInstance != null)
                    {
                        m_apiInstance.Shutdown();
                        m_apiInstance = null;
                    }
                }
            }

            m_disposed = true;
        }
    }
}
