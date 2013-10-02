using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TTAPI_Sample_Console_PriceSubscription
{
    using TradingTechnologies.TTAPI;

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
        private object m_lock = new object();
        private InstrumentLookupSubscription m_req = null;
        private PriceSubscription m_ps = null;
        private string m_username = "";
        private string m_password = "";


        /// <summary>
        /// Private default constructor
        /// </summary>
        private TTAPIFunctions()
        {
        }

        /// <summary>
        /// Primary constructor
        /// </summary>
        public TTAPIFunctions(string u, string p)
        {
            m_username = u;
            m_password = p;
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
                m_apiInstance.Authenticate(m_username, m_password);
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
                    "Jun13");
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
                    Console.WriteLine("Unrecoverable price subscription error: {0}", e.Error.Message);
                    Dispose();
                }
            }
        }

        /// <summary>
        /// Shuts down the TT API
        /// </summary>
        public void Dispose()
        {
            lock (m_lock)
            {
                if (!m_disposed)
                {
                    // Unattached callbacks and dispose of all subscriptions
                    if (m_req != null)
                    {
                        m_req.Update -= m_req_Update;
                        m_req.Dispose();
                        m_req = null;
                    }
                    if (m_ps != null)
                    {
                        m_ps.FieldsUpdated -= m_ps_FieldsUpdated;
                        m_ps.Dispose();
                        m_ps = null;
                    }

                    // Shutdown the TT API
                    if (m_apiInstance != null)
                    {
                        m_apiInstance.Shutdown();
                        m_apiInstance = null;
                    }

                    // Shutdown the Dispatcher
                    if (m_disp != null)
                    {
                        m_disp.BeginInvokeShutdown();
                        m_disp = null;
                    }

                    m_disposed = true;
                }
            }
        }
    }
}
