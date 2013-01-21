using System;
using System.Collections.Generic;
using System.Text;

namespace TTAPI_Sample_ASEOrderRouting
{
    using TradingTechnologies.TTAPI;
    using TradingTechnologies.TTAPI.Tradebook;
    using TradingTechnologies.TTAPI.Autospreader;

    class TTAPIFunctions : IDisposable
    {
        private UniversalLoginTTAPI apiInstance = null;
        private string orderKey = null;
        private CreateAutospreaderInstrumentRequest casReq = null;
        private InstrumentLookupSubscription req1 = null;
        private InstrumentLookupSubscription req2 = null;
        private ASInstrumentTradeSubscription ts = null;
        private WorkerDispatcher disp = null;
        private bool disposed = false;
        private Dictionary<int, Instrument> spreadLegKeys = new Dictionary<int, Instrument>();

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
                    if (req1 != null)
                    {
                        req1.Dispose();
                        req1 = null;
                    }
                    if (req2 != null)
                    {
                        req2.Dispose();
                        req2 = null;
                    }
                    if (casReq != null)
                    {
                        casReq.Dispose();
                        casReq = null;
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
                StartSpreadDetailsProcess();
            }
            else
            {
                Console.WriteLine("Login Failed: " + e.Status.StatusMessage);
                Dispose();
            }
        }

        private void StartSpreadDetailsProcess()
        {
            // We will create a spread with 2 legs
            Console.WriteLine("Find instruments for the legs...");

            ProductKey prodKeyLeg = new ProductKey(MarketKey.Cbot, ProductType.Future, "ZN");

            // We will use a dictionary to hold all instrument requests and update it when each instrument is found.
            // Once all lookup requests for the legs are complete, we can continue with the creation of the spread.
            // tagValue will be used in the dictionary to identify each lookup request.

            int tagValue = 1000;

            req1 = new InstrumentLookupSubscription(apiInstance.Session, Dispatcher.Current,
                                                prodKeyLeg,
                                                "Sep12");
            req1.Tag = tagValue;
            spreadLegKeys.Add(tagValue, null);
            req1.Update += new EventHandler<InstrumentLookupSubscriptionEventArgs>(legReq_Update);
            req1.Start();

            tagValue++;

            req2 = new InstrumentLookupSubscription(apiInstance.Session, Dispatcher.Current,
                                                prodKeyLeg,
                                                "Dec12");
            req2.Tag = tagValue;
            spreadLegKeys.Add(tagValue, null);
            req2.Update += new EventHandler<InstrumentLookupSubscriptionEventArgs>(legReq_Update);
            req2.Start();
        }

        private bool HaveWeFoundAllLegs()
        {
            foreach (Instrument instrument in spreadLegKeys.Values)
            {
                if (instrument == null)
                {
                    return false;
                }
            }

            return true;
        }

        public void legReq_Update(object sender, InstrumentLookupSubscriptionEventArgs e)
        {
            if (e.Instrument != null && e.Error == null)
            {
                // Update the dictionary to indicate that the instrument was found.
                InstrumentLookupSubscription instrLookupSub = (InstrumentLookupSubscription)sender;

                if (spreadLegKeys.ContainsKey((int)instrLookupSub.Tag))
                {
                    spreadLegKeys[(int)instrLookupSub.Tag] = e.Instrument;
                }
            }
            else if (e.IsFinal)
            {
                // Instrument was not found and TT API has given up looking for it
                Console.WriteLine("Cannot find instrument: " + e.Error.Message);
                Dispose();
            }

            // If we have found all of the leg instruments, proceed with the creation of the spread.
            if (HaveWeFoundAllLegs())
            {
                Console.WriteLine("All leg instruments have been found...");
                CreateSpreadDetails();
            }
        }

        private void CreateSpreadDetails()
        {
            Console.WriteLine("Creating the spread...");

            // SpreadDetails related properties
            SpreadDetails spreadDetails = new SpreadDetails();
            spreadDetails.Name = "My Spread";

            int i = 0;
            // Add the legs to the SpreadDetails
            foreach (Instrument instrument in spreadLegKeys.Values)
            {
                // In this example, the order is routed to the first order feed in the list of valid order feeds.
                // You should use the order feed that is appropriate for your purposes.
                SpreadLegDetails spreadlegDetails = new SpreadLegDetails(instrument.Key, instrument.GetValidOrderFeeds()[0].ConnectionKey);
                spreadlegDetails.SpreadRatio = (i % 2 == 0) ? 1 : -1;
                spreadlegDetails.PriceMultiplier = (i % 2 == 0) ? 1 : -1;
                spreadlegDetails.CustomerName = "<Default>";

                spreadDetails.Legs.Append(spreadlegDetails);
                i++;
            }

            // Create an Instrument corresponding to the synthetic spread
            casReq = new CreateAutospreaderInstrumentRequest(apiInstance.Session, Dispatcher.Current, spreadDetails);
            casReq.Completed += new EventHandler<CreateAutospreaderInstrumentRequestEventArgs>(casReq_Completed);
            casReq.Submit();
        }

        public void casReq_Completed(object sender, CreateAutospreaderInstrumentRequestEventArgs e)
        {
            if (e.Error == null)
            {
                if (e.Instrument != null)
                {
                    StartOrderSubmission((AutospreaderInstrument)e.Instrument);
                }
            }
            else
            {
                // Instrument was not found and TT API has given up looking for it
                Console.WriteLine("Autospreader Instrument creation failed: " + e.Error.Message);
                Dispose();
            }
        }

        private void StartOrderSubmission(AutospreaderInstrument instrument)
        {
            // Submit an Autospreader order to the local Autospreader Engine.  As a result, the
            // Autospreader Instrument need not be launched to it.  If you are going to use any other
            // Autospreader Engine, you will need to launch the Autospreader Instrument to it.
            Console.WriteLine("Submitting the Autospreader order to the local Autospreader Engine...");

            // An ASInstrumentTradeSubscription object is filtered by the given Autospreader instrument
            ts = new ASInstrumentTradeSubscription(apiInstance.Session, Dispatcher.Current, instrument);

            ts.OrderAdded += new EventHandler<OrderAddedEventArgs>(ts_OrderAdded);
            ts.OrderFilled += new EventHandler<OrderFilledEventArgs>(ts_OrderFilled);
            ts.OrderRejected += new EventHandler<OrderRejectedEventArgs>(ts_OrderRejected);
            ts.OrderDeleted += new EventHandler<OrderDeletedEventArgs>(ts_OrderDeleted);
            ts.OrderUpdated += new EventHandler<OrderUpdatedEventArgs>(ts_OrderUpdated);
            ts.Start();

            AutospreaderSyntheticOrderProfile profile = new AutospreaderSyntheticOrderProfile(
                apiInstance.Session.MarketCatalog.LocalAutospreaderEngineOrderFeed, instrument);
            profile.QuantityToWork = Quantity.FromInt(instrument, 1);
            profile.OrderType = OrderType.Limit;
            profile.BuySell = BuySell.Buy;
            profile.LimitPrice = Price.FromInt(instrument, 0);

            if (apiInstance.Session.SendOrder(profile))
            {
                orderKey = profile.SiteOrderKey;
                Console.WriteLine("Order Submitted, key: {0}", profile.SiteOrderKey);
            }
            else
            {
                Console.WriteLine("Send Order failed: {0}", profile.RoutingStatus.Message);
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
    }
}
