using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using TradingTechnologies.TTAPI;
using TradingTechnologies.TTAPI.Autospreader;
using TradingTechnologies.TTAPI.WinFormsHelpers;

namespace TTAPI_Samples
{
    /// <summary>
    /// Autospreader
    /// 
    /// This example demonstrates using the TT API to create, edit, and delete 
    /// user defined Autospreader spread contracts.
    /// </summary>
    public partial class AutospreaderManagerForm : Form
    {
        // TT API Session and Dispatcher.
        private Session m_session = null;
        private Dispatcher m_dispatcher = null;
        private XTraderModeTTAPI m_TTAPI = null;

        // MarketCatalog will be used to get the status of any ASE gateways.
        MarketCatalog m_marketCatalog = null;

        // Autospreader specific elements.
        AutospreaderManager m_autospreaderManager = null;
        SpreadDetailSubscription m_spreadDetailSubscription = null;

        // SpreadDetails for each spread.
        BindingList<MutableSpreadDetails> m_spreadDetailsList = null;

        // Dictionary of OrderFeeds.
        Dictionary<string, OrderFeed> m_feedDictionary = null;

        public AutospreaderManagerForm()
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
            m_dispatcher = Dispatcher.Current;
            m_session = apiInstance.Session;

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
                m_spreadDetailsList = new BindingList<MutableSpreadDetails>();
                dataGridViewSpreadView.DataSource = m_spreadDetailsList;
                dataGridViewSpreadView.RowHeadersVisible = false;

                m_feedDictionary = new Dictionary<string, OrderFeed>();

                m_marketCatalog = m_session.MarketCatalog;
                m_marketCatalog.MarketsUpdated += new EventHandler<MarketCatalogUpdatedEventArgs>(m_marketCatalog_MarketsUpdated);

                startSpreadDetailSubscription();
                updateSpreadDetailList();
            }
            else
            {
                MessageBox.Show(String.Format("ConnectionStatusUpdate: {0}", e.Status.StatusMessage));
            }
        }

        /// <summary>
        /// Shutdown TT API on application shutdown.
        /// </summary>
        private void AutospreaderManagerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_TTAPI != null)
            {
                m_TTAPI.Shutdown();
            }
        }

        #region Update ASE gateway combo box. (MarketCatalog)

        /// <summary>
        /// MarketsUpdated event.
        /// Fires when the market catalog is updated.
        /// </summary>
        void m_marketCatalog_MarketsUpdated(object sender, MarketCatalogUpdatedEventArgs e)
        {
            foreach (Market market in m_marketCatalog.Markets.Values)
            {
                if (market.Key == MarketKey.Autospreader)
                {
                    market.FeedStatusChanged += new EventHandler<FeedStatusChangedEventArgs>(market_FeedStatusChanged);
                }
            }
        }

        /// <summary>
        /// Fires when the feed status changes.
        /// Update the GUI comboBoxServerList drop down.
        /// </summary>
        void market_FeedStatusChanged(object sender, FeedStatusChangedEventArgs e)
        {
            OrderFeed feed = e.Feed as OrderFeed;

            // Only add feeds that are Up and Autospreader.
            if (feed != null &&
                feed.Status.Availability == FeedAvailability.Up &&
                feed.Market.Key == MarketKey.Autospreader)
            {
                if (!m_feedDictionary.ContainsKey(feed.Name))
                {
                    m_feedDictionary.Add(feed.Name, feed);
                    comboBoxServerList.Items.Add(feed.Name);

                    // If this is the first entry seed the combo box selection.
                    if (String.IsNullOrEmpty(comboBoxServerList.Text))
                    {
                        comboBoxServerList.Text = feed.Name;
                    }
                }
            }
            else if (feed != null)
            {
                if (m_feedDictionary.ContainsKey(feed.Name))
                {
                    m_feedDictionary.Remove(feed.Name);
                    comboBoxServerList.Items.Remove(feed.Name);

                    // If this is the last entry removed fromt he combo box clear out the selection.
                    if (comboBoxServerList.Items.Count == 0)
                    {
                        comboBoxServerList.Text = "";
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// MutableSpreadDetails is a mutable version of SpreadDetails used for display within the DataGridView.
        /// This class allows for only specific SpreadDetails properties to be displayed.
        /// </summary>
        class MutableSpreadDetails
        {
            internal SpreadDetails SpreadDetails;

            public MutableSpreadDetails(SpreadDetails details)
            {
                SpreadDetails = details;
                Name = SpreadDetails.Name;
            }

            public Color Color
            {
                get { return SpreadDetails.Color; }
            }

            public string Id
            {
                get { return SpreadDetails.InstrumentKey.SeriesKey; }
            }

            public string Name { get; set; }
        }

        #region AutospreaderManager and SpreadDetailSubscription

        /// <summary>
        /// Start the SpreadDetailSubscription.
        /// </summary>
        private void startSpreadDetailSubscription()
        {
            m_autospreaderManager = m_session.TTAPI.AutospreaderManager;
            m_spreadDetailSubscription = new SpreadDetailSubscription(m_session, m_dispatcher);
            m_spreadDetailSubscription.SpreadDetailsAdded += new EventHandler<SpreadDetailsEventArgs>(spreadDetailsChanged);
            m_spreadDetailSubscription.SpreadDetailsUpdated += new EventHandler<SpreadDetailsEventArgs>(spreadDetailsChanged);
            m_spreadDetailSubscription.SpreadDetailsDeleted += new EventHandler<SpreadDetailsEventArgs>(spreadDetailsChanged);
            m_spreadDetailSubscription.Start();
        }

        /// <summary>
        /// Event handler for SpreadDetailsAdded, SpreadDetailsUpdated, and SpreadDetailsDeleted.
        /// </summary>
        private void spreadDetailsChanged(object sender, SpreadDetailsEventArgs e)
        {
            updateSpreadDetailList();
        }

        /// <summary>
        /// Update the spread details list.
        /// </summary>
        private void updateSpreadDetailList()
        {
            m_spreadDetailsList.Clear();
            IList<SpreadDetails> spreadDetails = m_autospreaderManager.GetAllSpreadDetails();

            foreach (SpreadDetails detail in spreadDetails)
            {
                m_spreadDetailsList.Add(new MutableSpreadDetails(detail));
            }
        }

        #endregion

        /// <summary>
        /// Create a new spread.
        /// </summary>
        private void buttonNew_Click(object sender, EventArgs e)
        {
            SpreadDetailsForm spreadDetailsForm = new SpreadDetailsForm(m_session, m_dispatcher);
            spreadDetailsForm.ShowDialog();
        }

        /// <summary>
        /// Edit an existing spread.
        /// </summary>
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedCellCollection selectedCells = dataGridViewSpreadView.SelectedCells;
            if (selectedCells.Count > 1)
            {
                // First get the current spread details.
                SpreadDetails currentSpreadDetails = ((MutableSpreadDetails)selectedCells[0].OwningRow.DataBoundItem).SpreadDetails;
                
                // Make a copy of the spread details so that it can be reverted if necessary.
                SpreadDetails copyOfSpreadDetails = m_autospreaderManager.GetSpreadDetails(currentSpreadDetails.InstrumentKey);

                // Create a new spread details form with the spread details copy.
                SpreadDetailsForm spreadDetailsForm = new SpreadDetailsForm(m_session, m_dispatcher, copyOfSpreadDetails);
                spreadDetailsForm.ShowDialog();
            }
        }

        /// <summary>
        /// Rename an existing spread.
        /// </summary>
        private void buttonRename_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedCellCollection selectedCells = dataGridViewSpreadView.SelectedCells;
            if (selectedCells.Count > 0)
            {
                SpreadDetails currentSpreadDetails = ((MutableSpreadDetails)selectedCells[0].OwningRow.DataBoundItem).SpreadDetails;

                string newName = currentSpreadDetails.Name;
                if (InputDialog.InputDialogBox("Rename", "Please enter a new spread name.", ref newName) == System.Windows.Forms.DialogResult.OK)
                {
                    ASReturnCodes returnCode = m_autospreaderManager.RenameSpreadDetails(currentSpreadDetails, newName);
                    if (returnCode != ASReturnCodes.Success)
                    {
                        MessageBox.Show("Rename spread definition failed: " + returnCode.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// Clone an existing spread.
        /// </summary>
        private void buttonClone_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedCellCollection selectedCells = dataGridViewSpreadView.SelectedCells;
            if (selectedCells.Count > 0)
            {
                SpreadDetails clonedSpreadDetails = null;
                SpreadDetails currentSpreadDetails = ((MutableSpreadDetails)selectedCells[0].OwningRow.DataBoundItem).SpreadDetails;

                ASReturnCodes returnCode = m_autospreaderManager.CloneSpreadDetails(currentSpreadDetails, out clonedSpreadDetails);
                if (returnCode != ASReturnCodes.Success)
                {
                    MessageBox.Show("Clone spread definition failed: " + returnCode.ToString());
                }
            }
        }

        /// <summary>
        /// Delete an existing spread.
        /// </summary>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedCellCollection selectedCells = dataGridViewSpreadView.SelectedCells;
            if (selectedCells.Count > 0)
            {
                SpreadDetails currentSpreadDetails = ((MutableSpreadDetails)selectedCells[0].OwningRow.DataBoundItem).SpreadDetails;

                ASReturnCodes returnCode = m_autospreaderManager.DeleteSpreadDetails(currentSpreadDetails);
                if (returnCode != ASReturnCodes.Success)
                {
                    MessageBox.Show("Delete spread definition failed: " + returnCode.ToString());
                }
            }
        }

        /// <summary>
        /// Launch an autospreader instrument to a selected order server.
        /// First find the instrument then launch it to the selected order feed.
        /// </summary>
        private void buttonLaunch_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedCellCollection selectedCells = dataGridViewSpreadView.SelectedCells;
            if (selectedCells.Count > 0)
            {
                SpreadDetails currentSpreadDetails = ((MutableSpreadDetails)selectedCells[0].OwningRow.DataBoundItem).SpreadDetails;

                InstrumentLookupSubscription instrumentLookupSubscription = new InstrumentLookupSubscription(m_session, m_dispatcher, currentSpreadDetails.InstrumentKey);
                instrumentLookupSubscription.Update += new EventHandler<InstrumentLookupSubscriptionEventArgs>(instrumentLookupSubscription_Update);
                instrumentLookupSubscription.Start();
            }
        }

        /// <summary>
        /// Once the instrument is found launch it to the selected order feed.
        /// </summary>
        void instrumentLookupSubscription_Update(object sender, InstrumentLookupSubscriptionEventArgs e)
        {
            if (e.IsFinal == true && e.Instrument != null)
            {
                if (e.Instrument is AutospreaderInstrument)
                {
                    string messageString = String.Empty;
                    LaunchReturnCode result = (e.Instrument as AutospreaderInstrument).LaunchToOrderFeed(m_feedDictionary[(string)comboBoxServerList.SelectedItem]);

                    if (result == LaunchReturnCode.Success)
                    {
                        messageString = String.Format("Instrument '{0}' Successfully launched to server.", e.Instrument.Name);
                    }
                    else
                    {
                        messageString = String.Format("Instrument '{0}' could not be launched to server because {1}.", e.Instrument.Name, result.ToString());
                    }

                    MessageBox.Show(messageString);
                }
            }
            else if (e.IsFinal)
            {
                MessageBox.Show(String.Format("Instrument '{0}' could not be found.", e.Instrument.Name));
            }
        }
    }
}