using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TTAPI_Sample_DragAndDrop
{
    using TradingTechnologies.TTAPI;
    using TradingTechnologies.TTAPI.WinFormsHelpers;

    public partial class Form1 : Form
    {
        private XTraderModeTTAPI apiInstance = null;
        private InstrumentLookupSubscription req = null;
        private bool shutdownRequested = false;
        private bool shutdownCompleted = false;

        public Form1()
        {
            InitializeComponent();
        }

        public void ttApiInitHandler(TTAPI api, ApiCreationException ex)
        {
            if (ex == null)
            {
                apiInstance = (XTraderModeTTAPI)api;
                apiInstance.ConnectionStatusUpdate += new EventHandler<ConnectionStatusUpdateEventArgs>(apiInstance_ConnectionStatusUpdate);
                apiInstance.Start();
            }
            else if (!ex.IsRecoverable)
            {
                MessageBox.Show("API Initialization Failed: " + ex.Message);
            }
        }

        public void apiInstance_ConnectionStatusUpdate(object sender, ConnectionStatusUpdateEventArgs e)
        {
            if (e.Status.IsSuccess)
            {
                // add other code here to begin working with TT API
            }
            else
            {
                MessageBox.Show("Connection to X_TRADER failed: " + e.Status.StatusMessage);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!shutdownRequested)
            {
                e.Cancel = true;
                this.shutdownTTAPI();
            }
            else if (shutdownCompleted)
            {
                base.OnFormClosing(e);
            }
        }

        public void shutdownTTAPI()
        {
            // Shutdown the API
            if (!shutdownRequested)
            {
                // Dispose of all request objects
                if (req != null)
                {
                    req.Dispose();
                    req = null;
                }

                TTAPI.ShutdownCompleted += new EventHandler(TTAPI_ShutdownCompleted);
                TTAPI.Shutdown();
                shutdownRequested = true;
            }
        }

        public void TTAPI_ShutdownCompleted(object sender, EventArgs e)
        {
            shutdownCompleted = true;
            Close();
            System.Environment.Exit(0);
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            // If the Drop-data contains at least one contract, ...
            if (e.Data.HasInstrumentKeys())
            {
                label2.Text = "";
                foreach (InstrumentKey ik in e.Data.GetInstrumentKeys())
                {
                    // Begin an instrument subscription
                    req = new InstrumentLookupSubscription(apiInstance.Session, Dispatcher.Current, ik);
                    req.Update += new EventHandler<InstrumentLookupSubscriptionEventArgs>(req_Update);
                    req.Start();
                }
            }
        }

        private void Form1_DragOver(object sender, DragEventArgs e)
        {
            // Only display the "Copy" cursor if the user drags contracts
            if (e.Data.HasInstrumentKeys())
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        public void req_Update(object sender, InstrumentLookupSubscriptionEventArgs e)
        {
            if (e.Instrument != null && e.Error == null)
            {
                // Instrument was found
                label2.Text += "Found: " + e.Instrument.Name + ", ";
            }
            else if (e.IsFinal)
            {
                // Instrument was not found and TT API has given up looking for it
                label2.Text += "Cannot find instrument: " + e.Error.Message + ", ";
            }
        }
    }
}
