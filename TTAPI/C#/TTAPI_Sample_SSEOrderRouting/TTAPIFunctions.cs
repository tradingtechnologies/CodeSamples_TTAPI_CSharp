using System;
using System.Collections.Generic;
using System.Text;

namespace TTAPI_Sample_SSEOrderRouting
{
    using TradingTechnologies.TTAPI;
    using TradingTechnologies.TTAPI.Tradebook;

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
                    if (apiInstance != null)
                    {
                        apiInstance.Shutdown();
                        apiInstance = null;
                    }
                }
            }

            disposed = true;
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
            TTAPI.UniversalLoginModeDelegate ulDelegate = new TTAPI.UniversalLoginModeDelegate(ttApiInitComplete);
            TTAPI.CreateUniversalLoginTTAPI(Dispatcher.Current, ulDelegate);
        }

        public void ttApiInitComplete(UniversalLoginTTAPI api, Exception ex)
        {
            // Login
            if (ex == null)
            {
                apiInstance = api;
                apiInstance.AuthenticationStatusUpdate += new EventHandler<AuthenticationStatusUpdateEventArgs>(apiInstance_AuthenticationStatusUpdate);
                apiInstance.Authenticate("JSMITH", "12345678");
            }
            else
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
                    new ProductKey(MarketKey.Cbot, ProductType.Future, "ZN"),
                    "Sep12");
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
                // Our parent order has been updated
                Console.WriteLine("Our parent order has been updated: " + e.Message);
            }
            else if (e.OldOrder.SyntheticOrderKey == orderKey)
            {
                // A child order of our parent order has been updated
                Console.WriteLine("A child order of our parent order has been updated: " + e.Message);
            }
        }

        public void ts_OrderDeleted(object sender, OrderDeletedEventArgs e)
        {
            if (e.DeletedUpdate.SiteOrderKey == orderKey)
            {
                // Our parent order has been deleted
                Console.WriteLine("Our parent order has been deleted: " + e.Message);
                Dispose();
            }
            else if (e.DeletedUpdate.SyntheticOrderKey == orderKey)
            {
                // A child order of our parent order has been deleted
                Console.WriteLine("A child order of our parent order has been deleted: " + e.Message);
            }
        }

        public void ts_OrderRejected(object sender, OrderRejectedEventArgs e)
        {
            if (e.Order.SiteOrderKey == orderKey)
            {
                // Our parent order has been rejected
                Console.WriteLine("Our parent order has been rejected: " + e.Message);
                Dispose();
            }
            else if (e.Order.SyntheticOrderKey == orderKey)
            {
                // A child order of our parent order has been rejected
                Console.WriteLine("A child order of our parent order has been rejected: " + e.Message);
            }
        }

        public void ts_OrderFilled(object sender, OrderFilledEventArgs e)
        {
            if (e.Fill.SiteOrderKey == orderKey)
            {
                // Our parent order has been filled
                Console.WriteLine("Our parent order has been " + (e.Fill.FillType == FillType.Full ? "fully" : "partially") + " filled");

                if (e.Fill.FillType == FillType.Full)
                {
                    Dispose();
                }
            }
            else if (e.Fill.ParentKey == orderKey)
            {
                // A child order of our parent order has been filled
                Console.WriteLine("A child order of our parent order has been " + (e.Fill.FillType == FillType.Full ? "fully" : "partially") + " filled");
            }
        }

        public void ts_OrderAdded(object sender, OrderAddedEventArgs e)
        {
            if (e.Order.SiteOrderKey == orderKey)
            {
                // Our parent order has been added
                Console.WriteLine("Our parent order has been added: " + e.Message);
            }
            else if (e.Order.SyntheticOrderKey == orderKey)
            {
                // A child order of our parent order has been added
                Console.WriteLine("A child order of our parent order has been added: " + e.Message);
            }
        }

        public void priceSub_FieldsUpdated(object sender, FieldsUpdatedEventArgs e)
        {
            if (e.Error == null)
            {
                if (orderKey == null)
                {
                    // This is a time-slicer order
                    // In this example, the order is routed to the first order feed in the list of valid order feeds.
                    // You should use the order feed that is appropriate for your purposes.
                    OrderProfile prof = new OrderProfile(e.Fields.Instrument.GetValidOrderFeeds()[0], e.Fields.Instrument);
                    prof.BuySell = BuySell.Buy;
                    prof.AccountType = AccountType.Agent1;
                    prof.AccountName = "123";
                    prof.OrderQuantity = Quantity.FromInt(e.Fields.Instrument, 100);
                    prof.OrderType = OrderType.Limit;
                    prof.LimitPrice = e.Fields.GetBestBidPriceField().Value;

                    prof.SlicerType = SlicerType.TimeSliced;
                    prof.DisclosedQuantity = Quantity.FromInt(e.Fields.Instrument, 10);
                    prof.DisclosedQuantityMode = QuantityMode.Quantity;
                    prof.InterSliceDelay = 10;
                    prof.InterSliceDelayTimeUnits = TimeUnits.Sec;
                    prof.LeftoverAction = LeftoverAction.Leave;
                    prof.LeftoverActionTime = LeftoverActionTime.AtEnd;
                    prof.PriceMode = PriceMode.Absolute;

                    if (!ts.SendOrder(prof))
                    {
                        Console.WriteLine("Send Order failed : " + prof.RoutingStatus.Message);
                        Dispose();
                    }
                    else
                    {
                        orderKey = prof.SiteOrderKey;
                        Console.WriteLine("Order sent with price = " + prof.LimitPrice);
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
