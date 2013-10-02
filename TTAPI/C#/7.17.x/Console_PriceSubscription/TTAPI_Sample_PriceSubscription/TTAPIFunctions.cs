using System;
using System.Collections.Generic;
using System.Text;

namespace TTAPI_Sample_PriceSubscription
{
    using TradingTechnologies.TTAPI;

    class TTAPIFunctions : IDisposable
    {
        private UniversalLoginTTAPI apiInstance = null;
        private WorkerDispatcher disp = null;
        private InstrumentLookupSubscription req = null;
        private PriceSubscription ps = null;
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
            TTAPI.CreateUniversalLoginTTAPI(Dispatcher.Current, "MATT", "1", h);
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
            }
        }

        void apiInstance_AuthenticationStatusUpdate(object sender, AuthenticationStatusUpdateEventArgs e)
        {
            if (e.Status.IsSuccess)
            {
                req = new InstrumentLookupSubscription(apiInstance.Session, Dispatcher.Current,
                    new ProductKey(MarketKey.Cme, ProductType.Future, "ES"),
                    "Mar13");
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
                // Instrument was found
                Console.WriteLine("Found: " + e.Instrument.Name);

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

        public void priceSub_FieldsUpdated(object sender, FieldsUpdatedEventArgs e)
        {
            if (e.Error == null)
            {
                if (e.UpdateType == UpdateType.Snapshot)
                {
                    // Received a market data snapshot
                    Console.WriteLine("Market Data Snapshot:");

                    foreach (FieldId id in e.Fields.GetFieldIds())
                    {
                        Console.WriteLine("    {0} : {1}", id.ToString(), e.Fields[id].FormattedValue);
                    }
                }
                else
                {
                    // Only some fields have changed
                    Console.WriteLine("Market Data Update:");

                    foreach (FieldId id in e.Fields.GetChangedFieldIds())
                    {
                        Console.WriteLine("    {0} : {1}", id.ToString(), e.Fields[id].FormattedValue);
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
