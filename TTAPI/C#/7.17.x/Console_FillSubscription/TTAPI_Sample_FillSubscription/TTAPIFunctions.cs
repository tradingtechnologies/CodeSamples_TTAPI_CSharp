using System;
using System.Collections.Generic;
using System.Text;

namespace TTAPI_Sample_FillSubscription
{
    using TradingTechnologies.TTAPI;

    class TTAPIFunctions : IDisposable
    {
        private UniversalLoginTTAPI apiInstance = null;
        private WorkerDispatcher disp = null;
        private FillsSubscription fs = null;
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
                    if (fs != null)
                    {
                        fs.Dispose();
                        fs = null;
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

        public void apiInstance_AuthenticationStatusUpdate(object sender, AuthenticationStatusUpdateEventArgs e)
        {
            if (e.Status.IsSuccess)
            {
                fs = new FillsSubscription(apiInstance.Session, Dispatcher.Current);
                fs.FillListStart += new EventHandler<FillListEventArgs>(fs_FillListStart);
                fs.FillBookDownload += new EventHandler<FillBookDownloadEventArgs>(fs_FillBookDownload);
                fs.FillListEnd += new EventHandler<FillListEventArgs>(fs_FillListEnd);
                fs.FillAdded += new EventHandler<FillAddedEventArgs>(fs_FillAdded);
                fs.FillDeleted += new EventHandler<FillDeletedEventArgs>(fs_FillDeleted);
                fs.FillAmended += new EventHandler<FillAmendedEventArgs>(fs_FillAmended);
                fs.Start();
            }
            else
            {
                Console.WriteLine("Login Failed: " + e.Status.StatusMessage);
                Dispose();
            }
        }

        public void fs_FillAmended(object sender, FillAmendedEventArgs e)
        {
            Console.WriteLine("Fill Amended:");
            Console.WriteLine("    Old Fill: FillKey={0}, InstrKey={1}, Qty={2}, MatchPrice={3}", e.OldFill.FillKey, e.OldFill.InstrumentKey, e.OldFill.Quantity, e.OldFill.MatchPrice);
            Console.WriteLine("    New Fill: FillKey={0}, InstrKey={1}, Qty={2}, MatchPrice={3}", e.NewFill.FillKey, e.NewFill.InstrumentKey, e.NewFill.Quantity, e.NewFill.MatchPrice);
        }

        public void fs_FillDeleted(object sender, FillDeletedEventArgs e)
        {
            Console.WriteLine("Fill Deleted:");
            Console.WriteLine("    Fill: FillKey={0}, InstrKey={1}, Qty={2}, MatchPrice={3}", e.Fill.FillKey, e.Fill.InstrumentKey, e.Fill.Quantity, e.Fill.MatchPrice);
        }

        public void fs_FillAdded(object sender, FillAddedEventArgs e)
        {
            Console.WriteLine("Fill Added:");
            Console.WriteLine("    Fill: FillKey={0}, InstrKey={1}, Qty={2}, MatchPrice={3}", e.Fill.FillKey, e.Fill.InstrumentKey, e.Fill.Quantity, e.Fill.MatchPrice);
        }

        public void fs_FillListEnd(object sender, FillListEventArgs e)
        {
            Console.WriteLine("Finished adding fills from {0}", e.FeedConnectionKey.ToString());
        }

        public void fs_FillBookDownload(object sender, FillBookDownloadEventArgs e)
        {
            foreach (Fill f in e.Fills)
            {
                Console.WriteLine("Fill from download:");
                Console.WriteLine("    Fill: FillKey={0}, InstrKey={1}, Qty={2}, MatchPrice={3}", f.FillKey, f.InstrumentKey, f.Quantity, f.MatchPrice);
            }
        }

        public void fs_FillListStart(object sender, FillListEventArgs e)
        {
            Console.WriteLine("Begin adding fills from {0}", e.FeedConnectionKey.ToString());
        }
    }
}
