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
    /// TimeAndSales
    /// 
    /// This example demonstrates using the TT API to retrieve time & sales data from a 
    /// single instrument.
    /// </summary>
    public partial class frmTimeAndSales : Form
    {
        // Declare the TTAPI objects.
        private XTraderModeTTAPI m_TTAPI = null;
        private TimeAndSalesSubscription m_timeAndSalesSubscription = null;
        private bool m_isShutdown = false, m_shutdownInProcess = false;

        // Binding list which will be our data store
        private BindingList<TimeAndSalesData> m_TimeAndSalesList = new BindingList<TimeAndSalesData>();

        public frmTimeAndSales()
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
                if (m_timeAndSalesSubscription != null)
                {
                    m_timeAndSalesSubscription.Update -= timeAndSalesSubscription_Update;
                    m_timeAndSalesSubscription.Dispose();
                    m_timeAndSalesSubscription = null;
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
        /// Form load event:  Initialize our DataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            // specifies columns are manually created
            this.dgTimeAndSales.AutoGenerateColumns = false;

            // Create the columns in the DataGrid for the TAS data
            Dictionary<string, string> columnData = new Dictionary<string, string>();
            columnData.Add("TimeStamp", "Time");
            columnData.Add("Direction", "Hit/Take");
            columnData.Add("TradePrice", "LTP");
            columnData.Add("TradeQuantity", "LTQ");

            // Create our Data Grid View mapping, and template for display
            int i = 0;
            foreach (KeyValuePair<string, string> data in columnData)
            {
                // Create the DataGrid column
                DataGridViewColumn column = new DataGridViewColumn();
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                column.DataPropertyName = Convert.ToString(data.Key);
                column.Name = Convert.ToString(data.Key);
                column.HeaderText = Convert.ToString(data.Value);

                // Bind the column to a cell
                DataGridViewCell cell = new DataGridViewTextBoxCell();
                cell.Style.BackColor = Color.FromArgb(234, 234, 234);  // light gray (0xEAEAEA)
                column.CellTemplate = cell;
                this.dgTimeAndSales.Columns.Insert(i++, column);
            }

            // Bind the DataGrid to a member variable list
            this.dgTimeAndSales.DataSource = m_TimeAndSalesList;
        }

        #region Drag and Drop

        /// <summary>
        /// Form drag over event handler.
        /// The form must enable "AllowDrop" for these events to fire.
        /// </summary>
        private void frmTimeAndSales_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.HasInstrumentKeys())
                e.Effect = DragDropEffects.Copy;
        }

        /// <summary>
        /// Form drag and drop event handler.
        /// The form must enable "AllowDrop" for these events to fire.
        /// </summary>
        private void frmTimeAndSales_DragDrop(object sender, DragEventArgs e)
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

                    if (m_timeAndSalesSubscription != null)
                    {
                        m_timeAndSalesSubscription.Dispose();
                        m_timeAndSalesSubscription = null;
                    }

                    // subscribe for time and sales updates
                    m_timeAndSalesSubscription = new TimeAndSalesSubscription(e.Instrument, Dispatcher.Current);
                    m_timeAndSalesSubscription.Update += new EventHandler<TimeAndSalesEventArgs>(timeAndSalesSubscription_Update);
                    m_timeAndSalesSubscription.Start();
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
        /// Event to notify the application there has been a change in the time and sales feed
        /// Here we pull the values and publish them to the GUI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timeAndSalesSubscription_Update(object sender, TimeAndSalesEventArgs e)
        {
            if (e.Error != null)
            {
                Console.WriteLine(String.Format("TT API TimeAndSalesSubscription Error: {0}", e.Error.ToString()));
                return;
            }

            this.dgTimeAndSales.SuspendLayout();

            // TAS data is delivered as a list.  Iterate through and publish to the DataGrid
            // The zero in the first parameter signifies we're inserting at the beginning of the list (new data always on top)
            foreach (TimeAndSalesData tsData in e.Data)
                m_TimeAndSalesList.Insert(0, tsData);

            this.dgTimeAndSales.ResumeLayout();
        }

        /// <summary>
        /// Used to format the cell data depending on if the last trade was a hit or take
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // get the Row (Data Source) 
            TimeAndSalesData tsData = m_TimeAndSalesList[e.RowIndex];

            if (tsData.Direction == TradeDirection.Hit)
                e.CellStyle.ForeColor = Color.IndianRed;
            else if (tsData.Direction == TradeDirection.Take)
                e.CellStyle.ForeColor = Color.SteelBlue;
            else
                e.CellStyle.ForeColor = Color.Gold;
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