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

        public Form1()
        {
            InitializeComponent();
        }

        public void ttApiInitComplete(XTraderModeTTAPI api, Exception ex)
        {
            if (ex == null)
            {
                // Connect to X_TRADER
                apiInstance = api;
                apiInstance.ConnectionStatusUpdate += new EventHandler<ConnectionStatusUpdateEventArgs>(apiInstance_ConnectionStatusUpdate);
                apiInstance.ConnectToXTrader();
            }
            else
            {
                label1.Text = "API Initialization Failed: " + ex.Message;
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
                label1.Text = "Connection to X_TRADER failed: " + e.Status.StatusMessage;
            }
        }

        public void shutdownTTAPI()
        {
            // Dispose of all request objects
            if (req != null)
            {
                req.Dispose();
                req = null;
            }

            // Shutdown the API
            if (apiInstance != null)
            {
                apiInstance.Shutdown();
                apiInstance = null;
            }
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
