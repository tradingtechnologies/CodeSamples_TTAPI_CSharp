using System;
using System.Collections.Generic;
using System.Text;

namespace TTAPI_Sample_OrderRouting
{
    using TradingTechnologies.TTAPI;
    using TradingTechnologies.TTAPI.Tradebook;
    using TradingTechnologies.TTAPI.CustomerDefaults;

    class TTAPIFunctions : IDisposable
    {
        private UniversalLoginTTAPI apiInstance = null;
        private string orderKey = null;
        private InstrumentLookupSubscription req = null;
        private TradeSubscription ts = null;
        private PriceSubscription ps = null;
        private WorkerDispatcher disp = null;
        private bool disposed = false;

        public TTAPIFunctions()
        {
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {

                    // Shutdown all subscriptions
                    if (req != null)
                    {
                        req.Dispose();
                        req = null;
                    }
                    if (ps != null)
                    {
                        ps.Dispose();
                        ps = null;
                    }
                    if (ts != null)
                    {
                        ts.Dispose();
                        ts = null;
                    }

                    // Shutdown the Dispatcher
                    if (disp != null)
                    {
                        disp.BeginInvokeShutdown();
                        disp = null;
                    }

                    // Shutdown the TT API
                    TTAPI.ShutdownCompleted += new EventHandler(TTAPI_ShutdownCompleted);
                    TTAPI.Shutdown();
                }
            }

            disposed = true;
        }

        public void TTAPI_ShutdownCompleted(object sender, EventArgs e)
        {
            // Dispose of any other objects / resources
        }

        public void Start()
        {
            // Attach a WorkerDispatcher to the current thread
            disp = Dispatcher.AttachWorkerDispatcher();
            disp.BeginInvoke(new Action(Init));
            disp.Run();
        }

        public void Init()
        {
            // Use "Universal Login" Login Mode
            ApiInitializeHandler h = new ApiInitializeHandler(ttApiInitHandler);
            TTAPI.CreateUniversalLoginTTAPI(Dispatcher.Current, "B1_TRADER1", "12345678", h);
        }

        public void ttApiInitHandler(TTAPI api, ApiCreationException ex)
        {
            if (ex == null)
            {
                apiInstance = (UniversalLoginTTAPI)api;
                apiInstance.AuthenticationStatusUpdate += new EventHandler<AuthenticationStatusUpdateEventArgs>(apiInstance_AuthenticationStatusUpdate);
                apiInstance.Start();
            }
            else if (!ex.IsRecoverable)
            {
                Console.WriteLine("API Initialization Failed: " + ex.Message);
                Dispose();
            }
        }

        public void apiInstance_AuthenticationStatusUpdate(object sender, AuthenticationStatusUpdateEventArgs e)
        {
            if (e.Status.IsSuccess)
            {
                req = new InstrumentLookupSubscription(apiInstance.Session, Dispatcher.Current,
                    new ProductKey(MarketKey.Cme, ProductType.Future, "ES"),
                    "Sep13");
                req.Update += new EventHandler<InstrumentLookupSubscriptionEventArgs>(req_Update);
                req.Start();
            }
            else
            {
                Console.WriteLine("Login Failed: " + e.Status.StatusMessage);
                Dispose();
            }
        }

        public void req_Update(object sender, InstrumentLookupSubscriptionEventArgs e)
        {
            if (e.Instrument != null && e.Error == null)
            {
                // Create a TradeSubscription with an Instrument filter
                ts = new TradeSubscription(apiInstance.Session, Dispatcher.Current);

                TradeSubscriptionInstrumentFilter tsIF =
                       new TradeSubscriptionInstrumentFilter(apiInstance.Session, e.Instrument.Key, false, "instr");

                ts.SetFilter(tsIF);
                ts.OrderAdded += new EventHandler<OrderAddedEventArgs>(ts_OrderAdded);
                ts.OrderFilled += new EventHandler<OrderFilledEventArgs>(ts_OrderFilled);
                ts.OrderRejected += new EventHandler<OrderRejectedEventArgs>(ts_OrderRejected);
                ts.OrderDeleted += new EventHandler<OrderDeletedEventArgs>(ts_OrderDeleted);
                ts.OrderUpdated += new EventHandler<OrderUpdatedEventArgs>(ts_OrderUpdated);
                ts.Start();

                // Subscribe for Inside Market Data
                ps = new PriceSubscription(e.Instrument, Dispatcher.Current);
                ps.Settings = new PriceSubscriptionSettings(PriceSubscriptionType.InsideMarket);
                ps.FieldsUpdated += new FieldsUpdatedEventHandler(priceSub_FieldsUpdated);
                ps.Start();
            }
            else if (e.IsFinal)
            {
                // Instrument was not found and TT API has given up looking for it
                Console.WriteLine("Cannot find instrument: " + e.Error.Message);
                Dispose();
            }
        }

        public void ts_OrderUpdated(object sender, OrderUpdatedEventArgs e)
        {
            if (e.OldOrder.SiteOrderKey == orderKey)
            {
                // Our order has been updated
                Console.WriteLine("Our order has been updated: " + e.Message);
            }
        }

        public void ts_OrderDeleted(object sender, OrderDeletedEventArgs e)
        {
            if (e.DeletedUpdate.SiteOrderKey == orderKey)
            {
                // Our order has been deleted
                Console.WriteLine("Our order has been deleted: " + e.Message);
                Dispose();
            }
        }

        public void ts_OrderRejected(object sender, OrderRejectedEventArgs e)
        {
            if (e.Order.SiteOrderKey == orderKey)
            {
                // Our order has been rejected
                Console.WriteLine("Our order has been rejected: " + e.Message);
                Dispose();
            }
        }

        public void ts_OrderFilled(object sender, OrderFilledEventArgs e)
        {
            if (e.Fill.SiteOrderKey == orderKey)
            {
                // Our order has been filled
                Console.WriteLine("Our order has been " + (e.Fill.FillType == FillType.Full ? "fully" : "partially") + " filled");

                if (e.Fill.FillType == FillType.Full)
                {
                    Dispose();
                }
            }
        }

        public void ts_OrderAdded(object sender, OrderAddedEventArgs e)
        {
            if (e.Order.SiteOrderKey == orderKey)
            {
                // Our order has been added
                Console.WriteLine("Our order has been added: " + e.Message);
                Console.WriteLine(e.Order.BrokerKey.Name + " " + e.Order.BuySell.ToString() + " " + e.Order.InstrumentKey.MarketKey.Name
                    + " " + e.Order.InstrumentKey.ProductKey.Name);
            }
        }

        public void submitMarketOrder(OrderFeed of, Instrument instr, BuySell bs, int qty, TradeSubscription ts, string cust)
        {
            OrderProfile prof = new OrderProfile(of, instr, cust);

            prof.BuySell = bs;
            prof.OrderQuantity = Quantity.FromInt(instr, qty);
            prof.OrderType = OrderType.Market;

            if (!ts.SendOrder(prof))
            {
                Console.WriteLine("Send Order failed : {0}", prof.RoutingStatus.Message);
            }
            else
            {
                Console.WriteLine("Order sent with price = {0}", prof.LimitPrice);
            }
        }

        public void priceSub_FieldsUpdated(object sender, FieldsUpdatedEventArgs e)
        {
            if (e.Error == null)
            {
                if (orderKey == null)
                {
                    // In this example, the order is routed to the first order feed in the list of valid order feeds.
                    // You should use the order feed that is appropriate for your purposes.
                    foreach (OrderFeed off in e.Fields.Instrument.GetValidOrderFeeds())
                    {
                        Console.WriteLine(off.ConnectionKey.BrokerKey.ToString());
                    }
                    OrderProfile prof = new OrderProfile(e.Fields.Instrument.GetValidOrderFeeds()[0], e.Fields.Instrument, "<Default>");
                    prof.BuySell = BuySell.Buy;
                    //prof.AccountType = AccountType.Agent1;
                    //prof.AccountName = "fg006001";
                    prof.OrderQuantity = Quantity.FromInt(e.Fields.Instrument, 10);
                    prof.OrderType = OrderType.Limit;
                    prof.LimitPrice = e.Fields.GetBestBidPriceField().Value;

                    if (!ts.SendOrder(prof))
                    {
                        Console.WriteLine("Send Order failed : {0}", prof.RoutingStatus.Message);
                        Dispose();
                    }
                    else
                    {
                        orderKey = prof.SiteOrderKey;
                        Console.WriteLine("Order sent with price = {0}", prof.LimitPrice);
                    }
                }
                else if (ts.Orders.Keys.Contains(orderKey) &&
                         e.Fields.GetBestBidPriceField().HasValueChanged &&
                         e.Fields.GetBestBidPriceField().HasValidValue)
                {
                    OrderProfileBase prof = ts.Orders[orderKey].GetOrderProfile();
                    prof.LimitPrice = e.Fields.GetBestBidPriceField().Value;
                    prof.Action = OrderAction.Change;

                    if (!ts.SendOrder(prof))
                    {
                        Console.WriteLine("Send Order failed : {0}", prof.RoutingStatus.Message);
                        Dispose();
                    }
                    else
                    {
                        Console.WriteLine("Order Re-priced to {0}", prof.LimitPrice);
                    }
                }
            }
            else
            {
                if (e.Error.IsRecoverableError == false)
                {
                    Console.WriteLine("Unrecoverable price subscription error: " + e.Error.Message);
                    Dispose();
                }
            }
        }
    }
}
