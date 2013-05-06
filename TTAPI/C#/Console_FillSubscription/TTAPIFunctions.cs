using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TTAPI_Sample_Console_FillSubscription
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
        private FillsSubscription m_fs = null;
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
                // Start a fill subscription
                m_fs = new FillsSubscription(m_apiInstance.Session, Dispatcher.Current);
                m_fs.FillAdded += new EventHandler<FillAddedEventArgs>(m_fs_FillAdded);
                m_fs.FillAmended += new EventHandler<FillAmendedEventArgs>(m_fs_FillAmended);
                m_fs.FillBookDownload += new EventHandler<FillBookDownloadEventArgs>(m_fs_FillBookDownload);
                m_fs.FillDeleted += new EventHandler<FillDeletedEventArgs>(m_fs_FillDeleted);
                m_fs.FillListEnd += new EventHandler<FillListEventArgs>(m_fs_FillListEnd);
                m_fs.FillListStart += new EventHandler<FillListEventArgs>(m_fs_FillListStart);
                m_fs.Rollover += new EventHandler<RolloverEventArgs>(m_fs_Rollover);
                m_fs.Start();
            }
            else
            {
                Console.WriteLine("TT Login failed: {0}", e.Status.StatusMessage);
                Dispose();
            }
        }

        /// <summary>
        /// Event notification for fill server rollover
        /// </summary>
        void m_fs_Rollover(object sender, RolloverEventArgs e)
        {
            Console.WriteLine("{0} rolled", e.FeedConnectionKey.ToString());
        }

        /// <summary>
        /// Event notification for fill download beginning for a given gateway
        /// </summary>
        void m_fs_FillListStart(object sender, FillListEventArgs e)
        {
            Console.WriteLine("Begin adding fills from {0}", e.FeedConnectionKey.ToString());
        }

        /// <summary>
        /// Event notification for fill download completion for a given gateway
        /// </summary>
        void m_fs_FillListEnd(object sender, FillListEventArgs e)
        {
            Console.WriteLine("Finished adding fills from {0}", e.FeedConnectionKey.ToString());
        }

        /// <summary>
        /// Event notification for fill deletion
        /// </summary>
        void m_fs_FillDeleted(object sender, FillDeletedEventArgs e)
        {
            Console.WriteLine("Fill Deleted:");
            Console.WriteLine("    Fill: FillKey={0}, InstrKey={1}, Qty={2}, MatchPrice={3}", e.Fill.FillKey, e.Fill.InstrumentKey, e.Fill.Quantity, e.Fill.MatchPrice);
        }

        /// <summary>
        /// Event notification for fills being downloaded
        /// </summary>
        void m_fs_FillBookDownload(object sender, FillBookDownloadEventArgs e)
        {
            foreach (Fill f in e.Fills)
            {
                Console.WriteLine("Fill from download:");
                Console.WriteLine("    Fill: FillKey={0}, InstrKey={1}, Qty={2}, MatchPrice={3}", f.FillKey, f.InstrumentKey, f.Quantity, f.MatchPrice);
            }
        }

        /// <summary>
        /// Event notification for fill amendments
        /// </summary>
        void m_fs_FillAmended(object sender, FillAmendedEventArgs e)
        {
            Console.WriteLine("Fill Amended:");
            Console.WriteLine("    Old Fill: FillKey={0}, InstrKey={1}, Qty={2}, MatchPrice={3}", e.OldFill.FillKey, e.OldFill.InstrumentKey, e.OldFill.Quantity, e.OldFill.MatchPrice);
            Console.WriteLine("    New Fill: FillKey={0}, InstrKey={1}, Qty={2}, MatchPrice={3}", e.NewFill.FillKey, e.NewFill.InstrumentKey, e.NewFill.Quantity, e.NewFill.MatchPrice);
        }

        /// <summary>
        /// Event notification for a new fill
        /// </summary>
        void m_fs_FillAdded(object sender, FillAddedEventArgs e)
        {
            Console.WriteLine("Fill Added:");
            Console.WriteLine("    Fill: FillKey={0}, InstrKey={1}, Qty={2}, MatchPrice={3}", e.Fill.FillKey, e.Fill.InstrumentKey, e.Fill.Quantity, e.Fill.MatchPrice);
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
                    if (m_fs != null)
                    {
                        m_fs.FillAdded -= m_fs_FillAdded;
                        m_fs.FillAmended -= m_fs_FillAmended;
                        m_fs.FillBookDownload -= m_fs_FillBookDownload;
                        m_fs.FillDeleted -= m_fs_FillDeleted;
                        m_fs.FillListEnd -= m_fs_FillListEnd;
                        m_fs.FillListStart -= m_fs_FillListStart;
                        m_fs.Rollover -= m_fs_Rollover;
                        m_fs.Dispose();
                        m_fs = null;
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
