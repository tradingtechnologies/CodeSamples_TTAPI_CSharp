/***************************************************************************
 *    
 *      Copyright (c) 2012 Trading Technologies International, Inc.
 *                     All Rights Reserved Worldwide
 *
 *        * * *   S T R I C T L Y   P R O P R I E T A R Y   * * *
 *
 * WARNING:  This file is the confidential property of Trading Technologies
 * International, Inc. and is to be maintained in strict confidence.  For
 * use only by those with the express written permission and license from
 * Trading Technologies International, Inc.  Unauthorized reproduction,
 * distribution, use or disclosure of this file or any program (or document)
 * derived from it is prohibited by State and Federal law, and by local law
 * outside of the U.S. 
 *
 ***************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using TradingTechnologies.TTAPI;
using TradingTechnologies.TTAPI.Tradebook;
using TradingTechnologies.TTAPI.WinFormsHelpers;

namespace TTAPI_Samples
{
    /// <summary>
    /// ProfitLoss
    /// 
    /// This example demonstrates using the TT API to retrieve the P&L (profit & loss) 
    /// for a single instrument.  
    /// </summary>
    public partial class frmProfitLoss : Form
    {
        /// <summary>
        /// P&L Display Types
        /// </summary>
        private enum PLDisplay
        {
            NativeCurrency,
            PrimaryCurrency,
            QuantityXPrice,
            Ticks,
        }

        // Declare the TTAPI objects.
        private XTraderModeTTAPI m_TTAPI = null;
        private InstrumentTradeSubscription m_InstrumentTradeSubscription = null;

        // Private member variables
        private PLDisplay m_DisplayType = PLDisplay.NativeCurrency;

        public frmProfitLoss()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Init and start TT API.
        /// </summary>
        /// <param name="instance">XTraderModeTTAPI instance</param>
        /// <param name="ex">Any exception generated from the XTraderModeDelegate</param>
        public void initTTAPI(XTraderModeTTAPI apiInstance, Exception ex)
        {
            m_TTAPI = apiInstance;

            m_TTAPI.ConnectionStatusUpdate += ttapiInstance_ConnectionStatusUpdate;
            m_TTAPI.ConnectToXTrader();
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
        /// Event fired when the form loads.  Populate the combo boxes
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            // Populate the display type
            this.cboPLDisplay.Items.AddRange(System.Enum.GetNames(typeof(PLDisplay)));
            this.cboPLDisplay.SelectedIndex = 0;

            // Populate the calculation types
            this.cboPLCalculation.Items.AddRange(System.Enum.GetNames(typeof(ProfitLossCalculationType)));
            this.cboPLCalculation.SelectedIndex = 0;
        }

        #region Drag and Drop

        /// <summary>
        /// Form drag over event handler.
        /// The form must enable "AllowDrop" for these events to fire.
        /// </summary>
        private void Form1_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.HasInstrumentKeys())
                e.Effect = DragDropEffects.Copy;
        }

        /// <summary>
        /// Form drag and drop event handler.
        /// The form must enable "AllowDrop" for these events to fire.
        /// </summary>
        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.HasInstrumentKeys())
                FindInstrument(e.Data.GetInstrumentKeys());
        }

        #endregion

        #region FindInstrument

        /// <summary>
        /// Function to find a list of InstrumentKeys.
        /// </summary>
        /// <param name="keys">List of InstrumentKeys.</param>
        public void FindInstrument(IList<InstrumentKey> keys)
        {
            if (keys.Count == 1)
            {
                UpdateStatusBar("Drag & Drop detected.  Initializing instrument...");
                Console.WriteLine(String.Format("TT API FindInstrument {0}", keys[0].ToString()));

                InstrumentLookupSubscription instrRequest = new InstrumentLookupSubscription(m_TTAPI.Session, Dispatcher.Current, keys[0]);
                instrRequest.Update += instrRequest_Completed;
                instrRequest.Start();
            }
            else
            {
                MessageBox.Show("This application only accepts a single Contract");
            }
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

                    // Grab the contract specifications and output to the user
                    this.txtExchange.Text = e.Instrument.Product.Market.Name;
                    this.txtProduct.Text = e.Instrument.Product.Name;
                    this.txtProductType.Text = e.Instrument.Product.Type.ToString();
                    this.txtContract.Text = e.Instrument.GetFormattedName(InstrumentNameFormat.User);

                    if (m_InstrumentTradeSubscription != null)
                    {
                        m_InstrumentTradeSubscription.Dispose();
                        m_InstrumentTradeSubscription = null;
                    }

                    // The use of the InstrumentTradeSubscription will filter for a specific instrument
                    m_InstrumentTradeSubscription = new InstrumentTradeSubscription(m_TTAPI.Session, Dispatcher.Current, e.Instrument);
                    m_InstrumentTradeSubscription.EnablePNL = true;
                    m_InstrumentTradeSubscription.ProfitLossChanged += new EventHandler<ProfitLossChangedEventArgs>(instrumentTradeSubscription_ProfitLossChanged);
                    m_InstrumentTradeSubscription.Start();
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
        /// Fired when there is a change in the P&L
        /// </summary>
        void instrumentTradeSubscription_ProfitLossChanged(object sender, ProfitLossChangedEventArgs e)
        {
            PublishProfitLossStatistics();
        }  

        /// <summary>
        /// Publish the P&L Statistics to the GUI
        /// </summary>
        private void PublishProfitLossStatistics()
        {
            if (m_InstrumentTradeSubscription != null)
            {
                this.txtNetPosition.Text = m_InstrumentTradeSubscription.ProfitLossStatistics.NetPosition.ToString();
                this.txtBuyPosition.Text = m_InstrumentTradeSubscription.ProfitLossStatistics.BuyPosition.ToString();
                this.txtSellPosition.Text = m_InstrumentTradeSubscription.ProfitLossStatistics.SellPosition.ToString();

                switch (m_DisplayType)
                {
                    case PLDisplay.NativeCurrency:
                        this.txtTotalPL.Text = m_InstrumentTradeSubscription.ProfitLoss.AsNativeCurrency.ToString();
                        this.txtOpenPL.Text = m_InstrumentTradeSubscription.OpenProfitLoss.AsNativeCurrency.ToString();
                        this.txtRealizedPL.Text = m_InstrumentTradeSubscription.RealizedProfitLoss.AsNativeCurrency.ToString();
                        break;
                    case PLDisplay.PrimaryCurrency:
                        this.txtTotalPL.Text = m_InstrumentTradeSubscription.ProfitLoss.AsPrimaryCurrency.ToString();
                        this.txtOpenPL.Text = m_InstrumentTradeSubscription.OpenProfitLoss.AsPrimaryCurrency.ToString();
                        this.txtRealizedPL.Text = m_InstrumentTradeSubscription.RealizedProfitLoss.AsPrimaryCurrency.ToString();
                        break;
                    case PLDisplay.QuantityXPrice:
                        this.txtTotalPL.Text = m_InstrumentTradeSubscription.ProfitLoss.AsQuantityTimesPrice.ToString();
                        this.txtOpenPL.Text = m_InstrumentTradeSubscription.OpenProfitLoss.AsQuantityTimesPrice.ToString();
                        this.txtRealizedPL.Text = m_InstrumentTradeSubscription.RealizedProfitLoss.AsQuantityTimesPrice.ToString();
                        break;
                    case PLDisplay.Ticks:
                        this.txtTotalPL.Text = m_InstrumentTradeSubscription.ProfitLoss.AsTicks.ToString();
                        this.txtOpenPL.Text = m_InstrumentTradeSubscription.OpenProfitLoss.AsTicks.ToString();
                        this.txtRealizedPL.Text = m_InstrumentTradeSubscription.RealizedProfitLoss.AsTicks.ToString();
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Set how the P&L is calculated
        /// </summary>
        private void plCalcCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_InstrumentTradeSubscription == null)
                return;

            // reset the calculation type
            m_InstrumentTradeSubscription.CalculationType = (ProfitLossCalculationType)System.Enum.Parse(typeof(ProfitLossCalculationType), (string)cboPLCalculation.SelectedItem);

            // re-publish the P&L statistics
            PublishProfitLossStatistics();
        }

        /// <summary>
        /// Set in what format the P&L is displayed
        /// </summary>
        private void plDisplayCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // reset the calculation type
            m_DisplayType = (PLDisplay)System.Enum.Parse(typeof(PLDisplay), (string)cboPLDisplay.SelectedItem);

            // re-publish the P&L statistics
            PublishProfitLossStatistics();
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
