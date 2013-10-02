// **********************************************************************************************************************
//
//	Copyright © 2005-2013 Trading Technologies International, Inc.
//	All Rights Reserved Worldwide
//
// 	* * * S T R I C T L Y   P R O P R I E T A R Y * * *
//
//  WARNING: This file and all related programs (including any computer programs, example programs, and all source code) 
//  are the exclusive property of Trading Technologies International, Inc. (“TT”), are protected by copyright law and 
//  international treaties, and are for use only by those with the express written permission from TT.  Unauthorized 
//  possession, reproduction, distribution, use or disclosure of this file and any related program (or document) derived 
//  from it is prohibited by State and Federal law, and by local law outside of the U.S. and may result in severe civil 
//  and criminal penalties.
//
// ************************************************************************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using TradingTechnologies.TTAPI;
using TradingTechnologies.TTAPI.WinFormsHelpers;

namespace TTAPI_Samples
{
    /// <summary>
    /// PriceUpdateManual
    /// 
    /// This example demonstrates using the TT API to retrieve market data from a 
    /// single instrument by manually specifying the contract parameters.
    /// </summary>
    public partial class frmPriceUpdateManual : Form
    {
        // Declare the TTAPI objects.
        private XTraderModeTTAPI m_TTAPI = null;
        private PriceSubscription m_priceSubscription = null;
        private bool m_isShutdown = false, m_shutdownInProcess = false;

        public frmPriceUpdateManual()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Init and start TT API.
        /// </summary>
        /// <param name="instance">XTraderModeTTAPI instance</param>
        /// <param name="ex">Any exception generated from the ApiCreationException</param>
        public void ttApiInitHandler(TTAPI api, ApiCreationException ex)
        {
            if (ex == null)
            {
                m_TTAPI = (XTraderModeTTAPI)api;
                m_TTAPI.ConnectionStatusUpdate += new EventHandler<ConnectionStatusUpdateEventArgs>(ttapiInstance_ConnectionStatusUpdate);
                m_TTAPI.Start();
            }
            else if (!ex.IsRecoverable)
            {
                MessageBox.Show("API Initialization Failed: " + ex.Message);
            }
        }

        /// <summary>
        /// ConnectionStatusUpdate callback.
        /// Give feedback to the user that there was an issue starting up and connecting to XT.
        /// </summary>
        void ttapiInstance_ConnectionStatusUpdate(object sender, ConnectionStatusUpdateEventArgs e)
        {
            if (e.Status.IsSuccess)
            {
                this.Enabled = true;
            }
            else
            {
                MessageBox.Show(String.Format("ConnectionStatusUpdate: {0}", e.Status.StatusMessage));
            }
        }

        /// <summary>
        /// Dispose of all the TT API objects and shutdown the TT API 
        /// </summary>
        public void shutdownTTAPI()
        {
            if (!m_shutdownInProcess)
            {
                // Dispose of all request objects
                if (m_priceSubscription != null)
                {
                    m_priceSubscription.FieldsUpdated -= priceSubscription_FieldsUpdated;
                    m_priceSubscription.Dispose();
                    m_priceSubscription = null;
                }

                TTAPI.ShutdownCompleted += new EventHandler(TTAPI_ShutdownCompleted);
                TTAPI.Shutdown();
            }

            // only run shutdown once
            m_shutdownInProcess = true;
        }

        /// <summary>
        /// Event fired when the TT API has been successfully shutdown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TTAPI_ShutdownCompleted(object sender, EventArgs e)
        {
            m_isShutdown = true;
            Close();
        }

        /// <summary>
        /// Suspends the FormClosing event until the TT API has been shutdown
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!m_isShutdown)
            {
                e.Cancel = true;
                shutdownTTAPI();
            }
            else
            {
                base.OnFormClosing(e);
            }
        }

        /// <summary>
        /// When the form is loaded populate the product type dropdown menu with all available values.
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                this.cboProductType.DataSource = ProductType.AllAvailableValues.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region FindInstrument

        /// <summary>
        /// Click event which will initiate the price subscription
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConnect_Click(object sender, EventArgs e)
        {
            UpdateStatusBar("Connecting to Instrument...");

            // Create a product key from the given values
            ProductKey key = new ProductKey(this.txtExchange.Text,
                                            (ProductType)this.cboProductType.SelectedItem,
                                            this.txtProduct.Text);

            // Find out instrument based on the previously created key and the contract name
            InstrumentLookupSubscription instrRequest = new InstrumentLookupSubscription(this.m_TTAPI.Session, Dispatcher.Current, key, this.txtContract.Text);
            instrRequest.Update += instrRequest_Completed;
            instrRequest.Start();
        }

        /// <summary>
        /// Instrument request completed event.
        /// </summary>
        void instrRequest_Completed(object sender, InstrumentLookupSubscriptionEventArgs e)
        {
            if (e.IsFinal && e.Instrument != null)
            {
                try
                {
                    UpdateStatusBar("Instrument Found.");
                    Console.WriteLine(String.Format("TT API FindInstrument {0}", e.Instrument.Name));

                    if (m_priceSubscription != null)
                    {
                        m_priceSubscription.Dispose();
                        m_priceSubscription = null;
                    }

                    // subscribe for price updates
                    m_priceSubscription = new PriceSubscription(e.Instrument, Dispatcher.Current);
                    m_priceSubscription.Settings = new PriceSubscriptionSettings(PriceSubscriptionType.InsideMarket);
                    m_priceSubscription.FieldsUpdated += new FieldsUpdatedEventHandler(priceSubscription_FieldsUpdated);
                    m_priceSubscription.Start();
                }
                catch (Exception err)
                {
                    Console.WriteLine(String.Format("TT API FindInstrument Exception: {0}", err.Message));
                }
            }
            else if (e.IsFinal)
            {
                Console.WriteLine(String.Format("TT API FindInstrument Instrument Not Found: {0}", e.Error));
            }
            else
            {
                Console.WriteLine(String.Format("TT API FindInstrument Instrument Not Found: (Still Searching) {0}", e.Error));
            }
        }

        #endregion

        /// <summary>
        /// Event to notify the application there has been a change in the price feed
        /// Here we pull the values and publish them to the GUI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void priceSubscription_FieldsUpdated(object sender, FieldsUpdatedEventArgs e)
        {
            if (e.Error != null)
            {
                Console.WriteLine(String.Format("TT API FieldsUpdated Error: {0}", e.Error));
                return;
            }

            // Extract the values we want as Typed fields
            this.txtBidPrice.Text = e.Fields.GetDirectBidPriceField().FormattedValue;
            this.txtBidQty.Text = e.Fields.GetDirectBidQuantityField().FormattedValue;
            this.txtAskPrice.Text = e.Fields.GetDirectAskPriceField().FormattedValue;
            this.txtAskQty.Text = e.Fields.GetDirectAskQuantityField().FormattedValue;
            this.txtLastPrice.Text = e.Fields.GetLastTradedPriceField().FormattedValue;
            this.txtLastQty.Text = e.Fields.GetLastTradedQuantityField().FormattedValue;  
        }

        /// <summary>
        /// Event which opens the About window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuAbout_Click(object sender, EventArgs e)
        {
            AboutDTS aboutForm = new AboutDTS();
            aboutForm.ShowDialog(this);
        }

        #region UpdateStatusBar
        /// <summary>
        /// Update the status bar and write the message to the console in a thread safe way.
        /// </summary>
        /// <param name="message">Message to update the status bar with.</param>
        delegate void UpdateStatusBarCallback(string message);
        public void UpdateStatusBar(string message)
        {
            if (this.InvokeRequired)
            {
                UpdateStatusBarCallback statCB = new UpdateStatusBarCallback(UpdateStatusBar);
                this.Invoke(statCB, new object[] { message });
            }
            else
            {
                // Update the status bar.
                toolStripStatusLabel1.Text = message;

                // Also write this message to the console.
                Console.WriteLine(message);
            }
        }
        #endregion
    }
}