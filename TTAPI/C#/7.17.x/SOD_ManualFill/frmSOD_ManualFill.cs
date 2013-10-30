using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using TradingTechnologies.TTAPI;
using TradingTechnologies.TTAPI.Tradebook;
using TradingTechnologies.TTAPI.WinFormsHelpers;
using TradingTechnologies.TTAPI.Risk;

namespace TTAPI_Samples
{
    public partial class frmSOD_ManualFill : Form
    {
        // Declare private TTAPI member variables.
        private XTraderModeTTAPI m_TTAPI = null;
        private TradeSubscription m_tradeSubscription = null;
        private bool m_isShutdown = false, m_shutdownInProcess = false;

        private SOD_ManualFill_Settings m_settings = null;
        private GridBoundComponent<Fill> m_SODGridBoundComponent = null;

        public frmSOD_ManualFill()
        {
            InitializeComponent();

            m_settings = new SOD_ManualFill_Settings();
            this.propertyGrid1.SelectedObject = m_settings;

            m_SODGridBoundComponent = new GridBoundComponent<Fill>(gridStartOfDay, GetFillKey);
            List<string> sodVisibleProperties = new List<string>();
            sodVisibleProperties.Add("FeedConnectionKey");
            sodVisibleProperties.Add("InstrumentKey");
            sodVisibleProperties.Add("BuySell");
            sodVisibleProperties.Add("FillKey");
            sodVisibleProperties.Add("FillSource");
            sodVisibleProperties.Add("MatchPrice");
            sodVisibleProperties.Add("OpenClose");
            sodVisibleProperties.Add("Quantity");
            sodVisibleProperties.Add("IsStartOfDay");
            sodVisibleProperties.Add("IsConfirmed");
            sodVisibleProperties.Add("BuySell");
            sodVisibleProperties.Add("AccountName");
            sodVisibleProperties.Add("UserName");
            sodVisibleProperties.Add("MemberId");
            sodVisibleProperties.Add("GroupId");
            sodVisibleProperties.Add("TraderId");
            sodVisibleProperties.Add("RiskAccount");
            m_SODGridBoundComponent.SetVisibleProperties(sodVisibleProperties);
        }

        /// <summary>
        /// Dispose of all the TT API objects and shutdown the TT API 
        /// </summary>
        public void shutdownTTAPI()
        {
            if (!m_shutdownInProcess)
            {
                // Dispose of all request objects
                if (m_tradeSubscription != null)
                {
                    m_tradeSubscription.FillRecordAdded -= m_tradeSubscription_FillRecordAdded;
                    m_tradeSubscription.AdminFillAdded -= m_tradeSubscription_AdminFillAdded;
                    m_tradeSubscription.AdminFillDeleted -= m_tradeSubscription_AdminFillDeleted;
                    m_tradeSubscription.FillBookDownload -= m_tradeSubscription_FillBookDownload;
                    m_tradeSubscription.FillAmended -= m_tradeSubscription_FillAmended;
                    m_tradeSubscription.Dispose();
                    m_tradeSubscription = null;
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
        private void frmSOD_ManualFill_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!m_isShutdown)
            {
                e.Cancel = true;
                shutdownTTAPI();
            }
        }

        public static string GetFillKey(Object fill)
        {
            return ((Fill)fill).FillKey;
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
                m_TTAPI.ConnectionStatusUpdate += new EventHandler<ConnectionStatusUpdateEventArgs>(m_TTAPI_ConnectionStatusUpdate);
                m_TTAPI.Start();
            }
            else if (!ex.IsRecoverable)
            {
                MessageBox.Show("API Initialization Failed: " + ex.Message);
            }
        }

        void m_TTAPI_ConnectionStatusUpdate(object sender, ConnectionStatusUpdateEventArgs e)
        {
            if (e.Status.IsSuccess)
            {
                this.Enabled = true;

                // Update the Status Bar text.
                UpdateStatusBar("Drag a contract from X_TRADER to begin");

                m_tradeSubscription = new TradeSubscription(m_TTAPI.Session, Dispatcher.Current);
                m_tradeSubscription.FillRecordAdded += new EventHandler<FillAddedEventArgs>(m_tradeSubscription_FillRecordAdded);
                m_tradeSubscription.AdminFillAdded += new EventHandler<FillAddedEventArgs>(m_tradeSubscription_AdminFillAdded);
                m_tradeSubscription.AdminFillDeleted += new EventHandler<FillDeletedEventArgs>(m_tradeSubscription_AdminFillDeleted);
                m_tradeSubscription.FillBookDownload += new EventHandler<FillBookDownloadEventArgs>(m_tradeSubscription_FillBookDownload);
                m_tradeSubscription.FillAmended += new EventHandler<FillAmendedEventArgs>(m_tradeSubscription_FillAmended);

                m_tradeSubscription.Start();
            }
            else
            {
                MessageBox.Show(String.Format("ConnectionStatusUpdate: {0}", e.Status.StatusMessage));
            }
        }

        private void frmSOD_ManualFill_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.HasInstrumentKeys())
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void frmSOD_ManualFill_DragDrop(object sender, DragEventArgs e)
        {
            foreach (InstrumentKey key in e.Data.GetInstrumentKeys())
            {
                // Update the Status Bar text.
                UpdateStatusBar(String.Format("TT API FindInstrument {0}", key.ToString()));

                InstrumentLookupSubscription instrRequest = new InstrumentLookupSubscription(m_TTAPI.Session, Dispatcher.Current, key);
                instrRequest.Update += new EventHandler<InstrumentLookupSubscriptionEventArgs>(instrRequest_Update);
                instrRequest.Start();

                // Only allow the first instrument.
                break; 
            }
        }

        /// <summary>
        /// Fired when the TT API has information about the InstrumentLookupSubscription
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void instrRequest_Update(object sender, InstrumentLookupSubscriptionEventArgs e)
        {
            if (e.IsFinal && e.Instrument != null)
            {
                try
                {
                    UpdateStatusBar(String.Format("TT API FindInstrument {0}", e.Instrument.Name));

                    // Instrument is found, send to our PropertyGrid
                    m_settings.Instrument = new SOD_ManualFill_Settings.TTInstrument(e.Instrument);
                    this.propertyGrid1.Refresh();
                }
                catch (Exception err)
                {
                    UpdateStatusBar(String.Format("TT API FindInstrument Exception: {0}", err.Message));
                }
            }
            else if (e.IsFinal)
            {
                UpdateStatusBar(String.Format("TT API FindInstrument Instrument Not Found: {0}", e.Error));
            }
            else
            {
                UpdateStatusBar(String.Format("TT API FindInstrument Instrument Not Found: (Still Searching) {0}", e.Error));
            }
        }

        void m_tradeSubscription_FillRecordAdded(object sender, FillAddedEventArgs e)
        {
            if (e.Fill.OpenClose == OpenClose.XRiskAdmin)
            {
                Console.WriteLine("FillRecordAdded (XRiskAdmin): {0}", e.Fill.FillKey);

                UpdateManualFillAuditLog(e.Fill);
            }
        }

        void m_tradeSubscription_AdminFillAdded(object sender, FillAddedEventArgs e)
        {
            if (e.Fill.OpenClose == OpenClose.StartOfDay)
            {
                Console.WriteLine("AdminFillAdded (StartOfDay): {0}", e.Fill.FillKey);

                m_SODGridBoundComponent.UpdateOrAdd(e.Fill);
            }
            else if (e.Fill.OpenClose == OpenClose.XRiskAdmin)
            {
                Console.WriteLine("AdminFillAdded (XRiskAdmin): {0}", e.Fill.FillKey);

                UpdateManualFillAuditLog(e.Fill);
            }
        }

        void m_tradeSubscription_AdminFillDeleted(object sender, FillDeletedEventArgs e)
        {
            if (e.Fill.OpenClose == OpenClose.StartOfDay)
            {
                Console.WriteLine("AdminFillDeleted (StartOfDay): {0}", e.Fill.FillKey);

                m_SODGridBoundComponent.Remove(e.Fill);
            }
        }

        void m_tradeSubscription_FillBookDownload(object sender, FillBookDownloadEventArgs e)
        {
            foreach (Fill fill in e.Fills)
            {
                if (fill.OpenClose == OpenClose.XRiskAdmin)
                {
                    Console.WriteLine("FillBookDownload (XRiskAdmin): {0}", fill.FillKey);

                    UpdateManualFillAuditLog(fill);
                }
            }
        }

        void m_tradeSubscription_FillAmended(object sender, FillAmendedEventArgs e)
        {
            if (e.NewFill.OpenClose == OpenClose.StartOfDay)
            {
                Console.WriteLine("FillAmended (StartOfDay): {0}", e.NewFill.FillKey);

                m_SODGridBoundComponent.UpdateOrAdd(e.NewFill);
            }
            else if (e.NewFill.OpenClose == OpenClose.XRiskAdmin)
            {
                Console.WriteLine("FillAmended (XRiskAdmin): {0}", e.NewFill.FillKey);

                UpdateManualFillAuditLog(e.NewFill);
            }
        }

        private void UpdateManualFillAuditLog(Fill fill)
        {
            string record = String.Format("InstrumentKey={0}, BuySell={1}, FillKey={2}, Quantity={3}, Price={4}, Member={5}, Group={6}, Trader={7}", 
                                          fill.InstrumentKey,
                                          fill.BuySell,
                                          fill.FillKey,
                                          fill.Quantity.ToString(),
                                          fill.MatchPrice.ToString(),
                                          fill.MemberId, 
                                          fill.GroupId, 
                                          fill.TraderId);

            this.txtManualFillAudit.Text += record + Environment.NewLine;
        }

        private void btnPublish_Click(object sender, EventArgs e)
        {
            if (m_settings.PositionType == PositionType.AdminFill)
            {
                PublishManualFillRecord();
            }
            else
            {
                PublishStartOfDayRecord();
            }
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
                sbaStatus.Text = message;

                // Also write this message to the console.
                Console.WriteLine(message);
            }
        }

        #endregion

        /// <summary>
        /// Publishes a Manual Fill
        /// </summary>
        private void PublishManualFillRecord()
        {
            AdminFillRequiredFields requiredFields = new AdminFillRequiredFields();
            requiredFields.InstrumentDetails = m_settings.Instrument.NativeInstrument.InstrumentDetails;
            requiredFields.OrderFeed = m_settings.OrderFeed;
            
            requiredFields.MemberId = m_settings.Member;
            requiredFields.GroupId = m_settings.Group;
            requiredFields.TraderId = m_settings.Trader;
            requiredFields.Username = m_settings.Username;

            requiredFields.BuySell = m_settings.BuySell;
            requiredFields.Price = Price.FromString(m_settings.Instrument.NativeInstrument, m_settings.Price);
            requiredFields.Quantity = Quantity.FromInt(m_settings.Instrument.NativeInstrument, m_settings.Quantity);

            AdminFillRecord manualFillRecord = new AdminFillRecord(requiredFields);

            if (m_settings.AccountType != AccountType.None)
            {
                manualFillRecord.AccountType = m_settings.AccountType;
            }

            if (m_settings.Account != null && m_settings.Account.Trim() != String.Empty)
            {
                manualFillRecord.AccountName = m_settings.Account;
            }

            if (m_settings.RiskAccount != null && m_settings.RiskAccount.Trim() != String.Empty)
            {
                manualFillRecord.RiskAccount = m_settings.RiskAccount;
            }

            if (m_settings.FFT2 != null && m_settings.FFT2.Trim() != String.Empty)
            {
                manualFillRecord.FFT2 = m_settings.FFT2;
            }

            if (m_settings.FFT3 != null && m_settings.FFT3.Trim() != String.Empty)
            {
                manualFillRecord.FFT3 = m_settings.FFT3;
            }

            if (m_settings.UserTag != null && m_settings.UserTag.Trim() != String.Empty)
            {
                manualFillRecord.UserTag = m_settings.UserTag;
            }

            if (m_settings.OrderTag != null && m_settings.OrderTag.Trim() != String.Empty)
            {
                manualFillRecord.OrderTag = m_settings.OrderTag;
            }

            if (m_settings.CounterPartyMemberID != null && m_settings.CounterPartyMemberID.Trim() != String.Empty)
            {
                manualFillRecord.CounterpartyMemberId = m_settings.CounterPartyMemberID;
            }

            if (m_settings.CounterPartyGiveUpID != null && m_settings.CounterPartyGiveUpID.Trim() != String.Empty)
            {
                manualFillRecord.CounterPartyGiveUpId = m_settings.CounterPartyGiveUpID;
            }

            if (m_settings.ExchangeOrderID != null && m_settings.ExchangeOrderID.Trim() != String.Empty)
            {
                manualFillRecord.ExchangeOrderId = m_settings.ExchangeOrderID;
            }

            if (m_settings.GiveUpID != null && m_settings.GiveUpID.Trim() != String.Empty)
            {
                manualFillRecord.GiveUpId = m_settings.GiveUpID;
            }

            if (m_settings.GiveUpMemberID != null && m_settings.GiveUpMemberID.Trim() != String.Empty)
            {
                manualFillRecord.GiveupMemberId = m_settings.GiveUpMemberID;
            }

            if (m_settings.OrderNumber != null && m_settings.OrderNumber.Trim() != String.Empty)
            {
                manualFillRecord.OrderNumber = Convert.ToUInt64(m_settings.OrderNumber);
            }

            string errorMessage = "";
            if (!m_TTAPI.Session.RiskManager.Publish(manualFillRecord, out errorMessage))
            {
                MessageBox.Show(String.Format("Publish failed with: {0}", errorMessage));
            }
        }

        /// <summary>
        /// Publishes a SOD record
        /// </summary>
        private void PublishStartOfDayRecord()
        {
            StartOfDayRequiredFields requiredFields = new StartOfDayRequiredFields();
            requiredFields.InstrumentDetails = m_settings.Instrument.NativeInstrument.InstrumentDetails;
            requiredFields.OrderFeed = m_settings.OrderFeed;

            requiredFields.MemberId = m_settings.Member;
            requiredFields.GroupId = m_settings.Group;
            requiredFields.TraderId = m_settings.Trader;
            requiredFields.Username = m_settings.Username;

            requiredFields.BuySell = m_settings.BuySell;
            requiredFields.Quantity = Quantity.FromInt(m_settings.Instrument.NativeInstrument, m_settings.Quantity);

            StartOfDayRecord sodRecord = new StartOfDayRecord(requiredFields);

            if (m_settings.RiskAccount != null && m_settings.RiskAccount.Trim() != String.Empty)
            {
                sodRecord.RiskAccount = m_settings.RiskAccount;
            }

            double price;
            bool isDouble = double.TryParse(m_settings.Price, out price);
            if (isDouble)
            {
                sodRecord.Price = Price.FromString(m_settings.Instrument.NativeInstrument, m_settings.Price);
            }

            string errorMessage = "";
            if (!m_TTAPI.Session.RiskManager.Publish(sodRecord, out errorMessage))
            {
                MessageBox.Show(String.Format("Publish failed with: {0}", errorMessage));
            }
        }

        /// <summary>
        /// Deletes the selected SOD value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteSOD_Click(object sender, EventArgs e)
        {
            if (gridStartOfDay.SelectedRows.Count == 1)
            {
                Fill fill = (Fill)m_SODGridBoundComponent.GetItem(gridStartOfDay.SelectedRows[0].Index);

                if (fill != null)
                {
                    string errorMessage = "";
                    if (m_TTAPI.Session.RiskManager.Delete(fill, out errorMessage))
                    {
                        Console.WriteLine("SOD deleted successfully");
                    }
                    else
                    {
                        MessageBox.Show(String.Format("Delete failed with: {0}", errorMessage));
                    }
                }
                else
                {
                    MessageBox.Show("Invalid fill object");
                }
            }
            else
            {
                MessageBox.Show("Invalid row(s) selected");
            }
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            AboutDTS aboutForm = new AboutDTS();
            aboutForm.ShowDialog(this);	
        }
    }

    /// <summary>
    /// The list of all properties available when publishing Manual Fill / SOD records.
    /// </summary>
    public class SOD_ManualFill_Settings
    {
        // properties only available to manual fill updates
        string[] manualFillOnly = { "AccountType", 
                                    "Account", 
                                    "FFT2", 
                                    "FFT3", 
                                    "UserTag", 
                                    "OrderTag", 
                                    "CounterPartyMemberID", 
                                    "CounterPartyGiveUpID", 
                                    "ExchangeOrderID",
                                    "GiveUpID", 
                                    "GiveUpMemberID",
                                    "OrderNumber" };

        private TTInstrument m_instr = null;
        private PositionType m_positionType = PositionType.AdminFill;
        private OrderFeed m_orderFeed = null;
        private BuySell m_buySell = BuySell.Buy;
        private AccountType m_acctType = AccountType.None;

        [CategoryAttribute("1) Read Only"),
        ReadOnlyAttribute(false),
        Browsable(true),
        DescriptionAttribute("Instrument from Drag && Drop event")]
        public TTInstrument Instrument
        {
            get { return m_instr; }
            set 
            { 
                m_instr = value;

                OrderFeeds = m_instr.NativeInstrument.GetValidOrderFeeds();
            }
        }

        [CategoryAttribute("2) Required"),
        ReadOnlyAttribute(false),
        Browsable(true),
        DefaultValueAttribute(PositionType.AdminFill),
        DescriptionAttribute("Supported Position type adjustments")]
        public PositionType PositionType
        {
            get { return m_positionType; }
            set 
            {
                m_positionType = value;

                // enable/disable properties based on if manual fill or SOD is selected
                PropertyDescriptor descriptor;
                ReadOnlyAttribute attrib;
                FieldInfo isReadOnly;
                foreach (string property in manualFillOnly)
                {
                    descriptor = TypeDescriptor.GetProperties(this.GetType())[property];
                    attrib = (ReadOnlyAttribute)descriptor.Attributes[typeof(ReadOnlyAttribute)];
                    isReadOnly = attrib.GetType().GetField("isReadOnly", BindingFlags.NonPublic | BindingFlags.Instance);
                    isReadOnly.SetValue(attrib, m_positionType != PositionType.AdminFill);
                } 
            }
        }

        [BrowsableAttribute(false)]
        public System.Collections.ObjectModel.ReadOnlyCollection<OrderFeed> OrderFeeds { get; private set; }

        [CategoryAttribute("2) Required"),
        ReadOnlyAttribute(false),
        Browsable(true),
        TypeConverter(typeof(OrderFeedConverter)),
        DefaultValueAttribute(null),
        DescriptionAttribute("Gets the server connection")]
        public OrderFeed OrderFeed
        {
            get { return m_orderFeed; }
            set { m_orderFeed = value; }
        }

        [CategoryAttribute("2) Required"),
        ReadOnlyAttribute(false),
        Browsable(true),
        DefaultValueAttribute(BuySell.Buy),
        DescriptionAttribute("Indicates the side of the market for this fill")]
        public BuySell BuySell
        {
            get { return m_buySell; }
            set { m_buySell = value; }
        }

        [CategoryAttribute("2) Required"),
        ReadOnlyAttribute(false),
        Browsable(true),
        DescriptionAttribute("Indicates the trader login associated with this fill")]
        public string Username { get; set; }

        [CategoryAttribute("2) Required"),
        ReadOnlyAttribute(false),
        Browsable(true),
        DescriptionAttribute("Specifies the Member ID portion of the TT MGT associated with this fill")]
        public string Member { get; set; }

        [CategoryAttribute("2) Required"),
        ReadOnlyAttribute(false),
        Browsable(true),
        DescriptionAttribute("Specifies the Group ID portion of the TT MGT associated with this fill")]
        public string Group { get; set; }

        [CategoryAttribute("2) Required"),
        ReadOnlyAttribute(false),
        Browsable(true),
        DescriptionAttribute("Specifies the Group ID portion of the TT MGT associated with this fill")]
        public string Trader { get; set; }

        [CategoryAttribute("2) Required"),
        ReadOnlyAttribute(false),
        Browsable(true),
        DescriptionAttribute("Gets the price for the fill\r\n(NOTE: Optional field for SODs)")]
        public string Price { get; set; }

        [CategoryAttribute("2) Required"),
        ReadOnlyAttribute(false),
        Browsable(true),
        DefaultValueAttribute(0),
        DescriptionAttribute("Gets the fill quantity")]
        public int Quantity { get; set; }

        [CategoryAttribute("3) Optional"),
        ReadOnlyAttribute(false),
        Browsable(true),
        DefaultValueAttribute(AccountType.None),
        DescriptionAttribute("The account type associated with the fill")]
        public AccountType AccountType
        {
            get { return m_acctType; }
            set { m_acctType = value; }
        }

        [CategoryAttribute("3) Optional"),
        ReadOnlyAttribute(false),
        Browsable(true),
        DefaultValueAttribute(null),
        DescriptionAttribute("The customer or trader account number")]
        public string Account { get; set; }

        [CategoryAttribute("3) Optional"),
        ReadOnlyAttribute(false),
        Browsable(true),
        DefaultValueAttribute(null),
        DescriptionAttribute("The risk account")]
        public string RiskAccount { get; set; }

        [CategoryAttribute("3) Optional"),
        ReadOnlyAttribute(false),
        Browsable(true),
        DefaultValueAttribute(null),
        DescriptionAttribute("The FFT2 free-form text field")]
        public string FFT2 { get; set; }

        [CategoryAttribute("3) Optional"),
        ReadOnlyAttribute(false),
        Browsable(true),
        DefaultValueAttribute(null),
        DescriptionAttribute("The FFT3 free-form text field")]
        public string FFT3 { get; set; }

        [CategoryAttribute("3) Optional"),
        ReadOnlyAttribute(false),
        Browsable(true),
        DefaultValueAttribute(null),
        DescriptionAttribute("User-defined text included in the fill")]
        public string UserTag { get; set; }

        [CategoryAttribute("3) Optional"),
        ReadOnlyAttribute(false),
        Browsable(true),
        DefaultValueAttribute(null),
        DescriptionAttribute("User-defined text included in the fill")]
        public string OrderTag { get; set; }

        [CategoryAttribute("3) Optional"),
        ReadOnlyAttribute(false),
        Browsable(true),
        DefaultValueAttribute(null),
        DescriptionAttribute("The member ID of the counter party for the fill")]
        public string CounterPartyMemberID { get; set; }

        [CategoryAttribute("3) Optional"),
        ReadOnlyAttribute(false),
        Browsable(true),
        DefaultValueAttribute(null),
        DescriptionAttribute("The give-up member's clearing ID when the fill is assigned to another member")]
        public string CounterPartyGiveUpID { get; set; }

        [CategoryAttribute("3) Optional"),
        ReadOnlyAttribute(false),
        Browsable(true),
        DefaultValueAttribute(null),
        DescriptionAttribute("The ID the Exchange assigned of the order associated with this fill")]
        public string ExchangeOrderID { get; set; }

        [CategoryAttribute("3) Optional"),
        ReadOnlyAttribute(false),
        Browsable(true),
        DefaultValueAttribute(null),
        DescriptionAttribute("The ID of the give-up member")]
        public string GiveUpID { get; set; }

        [CategoryAttribute("3) Optional"),
        ReadOnlyAttribute(false),
        Browsable(true),
        DefaultValueAttribute(null),
        DescriptionAttribute("The Give-Up member's clearing ID for the position")]
        public string GiveUpMemberID { get; set; }

        [CategoryAttribute("3) Optional"),
        ReadOnlyAttribute(false),
        Browsable(true),
        DefaultValueAttribute(null),
        DescriptionAttribute("The order number assigned by the Exchange to associate with this fill")]
        public string OrderNumber { get; set; }

        /// <summary>
        /// Expandable object - Shows the isntrument details when expanded
        /// </summary>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class TTInstrument
        {
            private readonly Instrument m_instr = null;

            public TTInstrument(Instrument instr)
            {
                m_instr = instr;
            }

            [Browsable(false)]
            public Instrument NativeInstrument
            {
                get { return m_instr; }
            }

            [ReadOnlyAttribute(true),
            Browsable(true),
            DescriptionAttribute("The name of the exchange the instrument is traded on")]
            public string Exchange
            {
                get { return m_instr.Key.MarketKey.Name; }
            }

            [ReadOnlyAttribute(true),
            Browsable(true),
            DescriptionAttribute("The product the instrument belongs to")]
            public string Product
            {
                get { return m_instr.Key.ProductKey.Name; }
            }

            [ReadOnlyAttribute(true),
            Browsable(true),
            DescriptionAttribute("The instrument's product type")]
            public string ProductType
            {
                get { return m_instr.Key.ProductKey.Type.Name; }
            }

            [ReadOnlyAttribute(true),
            Browsable(true),
            DescriptionAttribute("The name of the instrument")]
            public string Contract
            {
                get { return m_instr.Name; }
            }

            public override string ToString()
            {
                return m_instr.Key.ToString();
            }
        }
    }

    /// <summary>
    /// Used to convert the OrderFeed list returned from the instrument into dropdown values
    /// available to the PropertyGrid
    /// </summary>
    public class OrderFeedConverter : TypeConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection((context.Instance as SOD_ManualFill_Settings).OrderFeeds); 
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return typeof(OrderFeed) == destinationType;        
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type destinationType)
        {
            return typeof(string) == destinationType;
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (typeof(string) == destinationType)
            {
                OrderFeed orderFeed = value as OrderFeed;
                if (orderFeed != null)
                    return orderFeed.Name;
            }

            return "(none)";
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            foreach (OrderFeed orderFeed in (context.Instance as SOD_ManualFill_Settings).OrderFeeds)
            {
                if ((value as string) == orderFeed.Name)
                {
                    return orderFeed;
                }
            }

            return null;
        }
    }
}