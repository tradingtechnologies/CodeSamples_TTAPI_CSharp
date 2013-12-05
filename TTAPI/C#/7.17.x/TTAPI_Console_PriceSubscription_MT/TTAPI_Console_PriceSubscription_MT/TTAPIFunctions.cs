using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TTAPI_Console_PriceSubscription_MT
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
            ApiInitializeHandler h = new ApiInitializeHandler(ttApiInitComplete);
            TTAPI.CreateUniversalLoginTTAPI(Dispatcher.Current, m_username, m_password, h);
        }

        /// <summary>
        /// Event notification for status of TT API initialization
        /// </summary>
        public void ttApiInitComplete(TTAPI api, ApiCreationException ex)
        {
            if (ex == null)
            {
                // Authenticate your credentials
                m_apiInstance = (UniversalLoginTTAPI)api;
                m_apiInstance.AuthenticationStatusUpdate += new EventHandler<AuthenticationStatusUpdateEventArgs>(apiInstance_AuthenticationStatusUpdate);
                m_apiInstance.Start();
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
                // Start Time & Sales subscriptions on a separate thread
                List<ContractDetails> lcd1 = new List<ContractDetails>();
                lcd1.Add(new ContractDetails(MarketKey.Cme, ProductType.Future, "ES", "Dec13"));
                lcd1.Add(new ContractDetails(MarketKey.Cme, ProductType.Future, "NQ", "Dec13"));

                Strategy1 s1 = new Strategy1(m_apiInstance, lcd1);
                Thread workerThread1 = new Thread(s1.Start);
                workerThread1.Name = "Strategy 1 Thread";
                workerThread1.Start();

                // Start more Time & Sales subscriptions on a separate thread
                List<ContractDetails> lcd2 = new List<ContractDetails>();
                lcd2.Add(new ContractDetails(MarketKey.Cbot, ProductType.Future, "ZB", "Dec13"));
                lcd2.Add(new ContractDetails(MarketKey.Cbot, ProductType.Future, "ZN", "Dec13"));

                Strategy2 s2 = new Strategy2(m_apiInstance, lcd2);
                Thread workerThread2 = new Thread(s2.Start);
                workerThread2.Name = "Strategy 2 Thread";
                workerThread2.Start();
            }
            else
            {
                Console.WriteLine("TT Login failed: {0}", e.Status.StatusMessage);
                Dispose();
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
                    // Begin shutdown the TT API
                    TTAPI.ShutdownCompleted += new EventHandler(TTAPI_ShutdownCompleted);
                    TTAPI.Shutdown();

                    m_disposed = true;
                }
            }
        }

        /// <summary>
        /// Event notification for completion of TT API shutdown
        /// </summary>
        public void TTAPI_ShutdownCompleted(object sender, EventArgs e)
        {
            // Shutdown the Dispatcher
            if (m_disp != null)
            {
                m_disp.BeginInvokeShutdown();
                m_disp = null;
            }

            // Dispose of any other objects / resources
        }
    }

    /// <summary>
    /// struct for encapsulating contract details
    /// </summary>
    public struct ContractDetails
    {
        public MarketKey m_marketKey;
        public ProductType m_productType;
        public string m_product;
        public string m_contract;

        public ContractDetails(MarketKey mk, ProductType pt, string prod, string cont)
        {
            m_marketKey = mk;
            m_productType = pt;
            m_product = prod;
            m_contract = cont;
        }
    }
}
