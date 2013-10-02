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
using TradingTechnologies.TTAPI.CustomerDefaults;
using TradingTechnologies.TTAPI.WinFormsHelpers;

namespace TTAPI_Samples
{
    public partial class SpreadDetailsForm : Form
    {
        private Session m_session = null;
        private Dispatcher m_dispatcher = null;
        private SpreadDetails m_spreadDetails = null;

        private List<ColumnInfo> m_rows = null;
        private Dictionary<InstrumentKey, Instrument> m_spreadLegs = null;

        private bool m_isNewSpread;

        /// <summary>
        /// Constructor for a new SpreadDetailsForm.
        /// </summary>
        /// <param name="session">Session</param>
        /// <param name="dispatcher">Dispatcher</param>
        public SpreadDetailsForm(Session session, Dispatcher dispatcher)
        {
            InitializeComponent();

            m_isNewSpread = true;
            m_session = session;
            m_dispatcher = dispatcher;
            m_spreadDetails = new SpreadDetails();
            initFields();
        }

        /// <summary>
        /// Constructor for a SpreadDetailsForm from an existing SpreadDetails definition.
        /// </summary>
        /// <param name="session">Session</param>
        /// <param name="dispatcher">Dispatcher</param>
        /// <param name="spreadDetails">SpreadDetails</param>
        public SpreadDetailsForm(Session session, Dispatcher dispatcher, SpreadDetails spreadDetails)
        {
            InitializeComponent();

            m_isNewSpread = false;
            m_session = session;
            m_dispatcher = dispatcher;
            m_spreadDetails = spreadDetails;
            initFields();
        }

        /// <summary>
        /// Small class to make defining a column for the GUI simpler.
        /// </summary>
        public class ColumnInfo
        {
            public enum ColumnInfoType
            {
                Header = 0,
                TextBox,
                CheckBox,
                ComboBox
            }

            public string Name { get; set; }
            public ColumnInfoType Type { get; set; }

            public ColumnInfo(string name, ColumnInfoType type)
            {
                Name = name;
                Type = type;
            }
        } 
        
        /// <summary>
        /// Enum to define each field in the GUI.
        /// </summary>
        private enum FieldType
        {
            DeleteButton = 0,      
            BasicPropertis,
            Contract,
            OrderFeedCombo,
            CustomerAccount,
            ActiveQuoting,
            ConsiderOwnOrders,
            OffsetHedge,
            PayupTicks,
            SpreadRatio,
            SpreadMultiplier,
            QuantityMultiplier,
            BasicVolumeLean,
            UserCancelReplace,
            QueueHolderOrders,
            InsideSmartQuote,
            SmartQuoteLimit,
            AdvancedProperties,
            ConsiderImplied,
            HedgeRound,
            MaxPriceMove,
            MaxOrderMove,
            CancelReplaceForQuantity,
            OneOverPrice,
            HiddenObject
        }

        /// <summary>
        /// Set up the table.
        /// </summary>
        private void initFields()
        {
            m_rows = new List<ColumnInfo>();
            m_spreadLegs = new Dictionary<InstrumentKey, Instrument>();

            comboBoxBasedOn.Items.Add("Implied");
            comboBoxBasedOn.Items.Add("ImpliedFraction");
            comboBoxBasedOn.Items.Add("NetChange");
            comboBoxBasedOn.Items.Add("Ratio");
            comboBoxBasedOn.Items.Add("Yield");
            comboBoxBasedOn.SelectedIndex = 0;

            comboBoxSlopSettings.Items.Add("Basic");
            comboBoxSlopSettings.Items.Add("Advanced");
            comboBoxSlopSettings.Items.Add("Smart");
            comboBoxSlopSettings.SelectedIndex = 2;

            comboBoxSpreadLTP.Items.Add("LastToLast");
            comboBoxSpreadLTP.Items.Add("BidToBidAskToAsk");
            comboBoxSpreadLTP.SelectedIndex = 1;

            // Do not create dataGridVeiw columns automatically
            dataGridViewSpreadDetails.AutoGenerateColumns = false;
            dataGridViewSpreadDetails.RowHeadersVisible = false;

            // Set up all the column rows titles.
            m_rows.Add(new ColumnInfo("", ColumnInfo.ColumnInfoType.TextBox));
            m_rows.Add(new ColumnInfo("Basic Properties", ColumnInfo.ColumnInfoType.Header));
            m_rows.Add(new ColumnInfo("Contract", ColumnInfo.ColumnInfoType.ComboBox));
            m_rows.Add(new ColumnInfo("Order Feed", ColumnInfo.ColumnInfoType.ComboBox));
            m_rows.Add(new ColumnInfo("Customer Account", ColumnInfo.ColumnInfoType.ComboBox));
            m_rows.Add(new ColumnInfo("Active Quoting", ColumnInfo.ColumnInfoType.CheckBox));
            m_rows.Add(new ColumnInfo("Consider Own Orders", ColumnInfo.ColumnInfoType.CheckBox));
            m_rows.Add(new ColumnInfo("Offset Hedge", ColumnInfo.ColumnInfoType.ComboBox));
            m_rows.Add(new ColumnInfo("Payup Ticks", ColumnInfo.ColumnInfoType.TextBox));
            m_rows.Add(new ColumnInfo("Spread Ratio", ColumnInfo.ColumnInfoType.TextBox));
            m_rows.Add(new ColumnInfo("Spread Multiplier", ColumnInfo.ColumnInfoType.TextBox));
            m_rows.Add(new ColumnInfo("Offset Volume Multiplier", ColumnInfo.ColumnInfoType.TextBox));
            m_rows.Add(new ColumnInfo("Basic Volume Lean", ColumnInfo.ColumnInfoType.TextBox));
            m_rows.Add(new ColumnInfo("User Cancel Replace", ColumnInfo.ColumnInfoType.CheckBox));
            m_rows.Add(new ColumnInfo("Queue Holder Orders", ColumnInfo.ColumnInfoType.TextBox));
            m_rows.Add(new ColumnInfo("Inside Smart Quote", ColumnInfo.ColumnInfoType.TextBox));
            m_rows.Add(new ColumnInfo("Smart Quote Limit", ColumnInfo.ColumnInfoType.TextBox));
            m_rows.Add(new ColumnInfo("Advanced Properties", ColumnInfo.ColumnInfoType.Header));
            m_rows.Add(new ColumnInfo("Consider Implied", ColumnInfo.ColumnInfoType.CheckBox));
            m_rows.Add(new ColumnInfo("Hedge Round", ColumnInfo.ColumnInfoType.CheckBox));
            m_rows.Add(new ColumnInfo("Max Price Move", ColumnInfo.ColumnInfoType.TextBox));
            m_rows.Add(new ColumnInfo("Max Order Move", ColumnInfo.ColumnInfoType.TextBox));
            m_rows.Add(new ColumnInfo("Cancel/Replace for Quantity", ColumnInfo.ColumnInfoType.CheckBox));
            m_rows.Add(new ColumnInfo("1/Price", ColumnInfo.ColumnInfoType.CheckBox));
            m_rows.Add(new ColumnInfo("Instrument", ColumnInfo.ColumnInfoType.TextBox));

            DataGridViewColumn col = new DataGridViewColumn();
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            col.Width = 200;
            col.HeaderText = "";
            col.Name = "colHeader";
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            DataGridViewCell cell = new DataGridViewTextBoxCell();
            cell.Style.BackColor = Color.White;
            col.CellTemplate = cell;
            dataGridViewSpreadDetails.Columns.Add(col);
            comboBoxLegColor.BackColor = Color.White;

            // Populate the rows.
            foreach (ColumnInfo colInfo in m_rows)
            {
                DataGridViewRow row = new DataGridViewRow();

                cell = new DataGridViewTextBoxCell();

                if (colInfo.Type == ColumnInfo.ColumnInfoType.Header)
                {
                    DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
                    cell.Style.Font = new Font("Tahoma", 10, FontStyle.Bold);
                    cell.Value = colInfo.Name;
                }
                else
                {
                    cell.Value = "     " + colInfo.Name;
                }

                row.Cells.Add(cell);

                dataGridViewSpreadDetails.Rows.Add(row);
            }

            // Add a hidden row to keep track of the instrument.
            DataGridViewRow rowHidden = new DataGridViewRow();
            rowHidden.Visible = false;
            dataGridViewSpreadDetails.Rows.Add(rowHidden);

            if (!m_isNewSpread)
            {
                moveSpreadDetailsToColumn();
            }
        }

        /// <summary>
        /// Add a leg to the spread given a SpreadLegDetails object.
        /// </summary>
        /// <param name="spreadLegParams">SpreadLegDetails</param>
        private void addLeg(SpreadLegDetails spreadLegParams)
        {
            DataGridViewColumn col = new DataGridViewColumn();
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            col.Width = 150;

            col.HeaderText = "Leg " + Convert.ToString(dataGridViewSpreadDetails.Columns.Count);
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewTextBoxCell textCell = new DataGridViewTextBoxCell();
            textCell.Style.BackColor = Color.White;
            col.CellTemplate = textCell;
            int retunValue = dataGridViewSpreadDetails.Columns.Add(col);
            int columnNum = dataGridViewSpreadDetails.Columns.Count;

            DataGridViewButtonCell cell = new DataGridViewButtonCell();
            cell.ToolTipText = "Delete this leg";
            cell.Value = "Delete this leg";
            cell.Style.BackColor = Color.Red;
            dataGridViewSpreadDetails.Rows[(int)FieldType.DeleteButton].Cells[columnNum - 1] = cell;

            textCell = new DataGridViewTextBoxCell();
            textCell.Value = spreadLegParams.Name;
            dataGridViewSpreadDetails.Rows[(int)FieldType.Contract].Cells[columnNum - 1] = textCell;

            DataGridViewComboBoxCell comboCell = new DataGridViewComboBoxCell();
            comboCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
            comboCell.Items.Add(spreadLegParams.InstrumentKey.MarketKey.Name);
            dataGridViewSpreadDetails.Rows[(int)FieldType.OrderFeedCombo].Cells[columnNum - 1] = comboCell;
            comboCell.Value = spreadLegParams.InstrumentKey.MarketKey.Name;

            comboCell = new DataGridViewComboBoxCell();
            comboCell.Items.Add(spreadLegParams.CustomerName);
            comboCell.Value = comboCell.Items[0];
            dataGridViewSpreadDetails.Rows[(int)FieldType.CustomerAccount].Cells[columnNum - 1] = comboCell;

            DataGridViewCheckBoxCell checkCell = new DataGridViewCheckBoxCell();
            checkCell.Value = spreadLegParams.ActiveQuoting;
            dataGridViewSpreadDetails.Rows[(int)FieldType.ActiveQuoting].Cells[columnNum - 1] = checkCell;

            checkCell = new DataGridViewCheckBoxCell();
            checkCell.Value = spreadLegParams.ConsiderOwnOrders;
            dataGridViewSpreadDetails.Rows[(int)FieldType.ConsiderOwnOrders].Cells[columnNum - 1] = checkCell;

            comboCell = new DataGridViewComboBoxCell();
            comboCell.Items.Add("LimitOrder");
            comboCell.Items.Add("MarketOrder");
            comboCell.Items.Add("MLMOrder");
            comboCell.Value = spreadLegParams.OffsetHedgeType.ToString();
            dataGridViewSpreadDetails.Rows[(int)FieldType.OffsetHedge].Cells[columnNum - 1] = comboCell;

            //Payup ticks
            textCell = new DataGridViewTextBoxCell();
            textCell.Value = spreadLegParams.PayupTicks;
            dataGridViewSpreadDetails.Rows[(int)FieldType.PayupTicks].Cells[columnNum - 1] = textCell;

            //spread ratio
            textCell = new DataGridViewTextBoxCell();
            textCell.Value = spreadLegParams.SpreadRatio;
            dataGridViewSpreadDetails.Rows[(int)FieldType.SpreadRatio].Cells[columnNum - 1] = textCell;

            //spread Multiplier
            textCell = new DataGridViewTextBoxCell();
            textCell.Value = spreadLegParams.PriceMultiplier;
            dataGridViewSpreadDetails.Rows[(int)FieldType.SpreadMultiplier].Cells[columnNum - 1] = textCell;

            //offset volume multiplier
            textCell = new DataGridViewTextBoxCell();
            textCell.Value = spreadLegParams.QuantityMultiplier;
            dataGridViewSpreadDetails.Rows[(int)FieldType.QuantityMultiplier].Cells[columnNum - 1] = textCell;

            //base volume mulitplier
            textCell = new DataGridViewTextBoxCell();
            textCell.Value = spreadLegParams.BaseVolumeLean;
            dataGridViewSpreadDetails.Rows[(int)FieldType.BasicVolumeLean].Cells[columnNum - 1] = textCell;

            //use cancel replace
            checkCell = new DataGridViewCheckBoxCell();
            checkCell.Value = spreadLegParams.UseCancelReplace; 
            dataGridViewSpreadDetails.Rows[(int)FieldType.UserCancelReplace].Cells[columnNum - 1] = checkCell;

            //Queue Holder orders
            textCell = new DataGridViewTextBoxCell();
            textCell.Value = spreadLegParams.QueueHolderOrders;
            dataGridViewSpreadDetails.Rows[(int)FieldType.QueueHolderOrders].Cells[columnNum - 1] = textCell;

            //inside smart quote
            textCell = new DataGridViewTextBoxCell();
            textCell.Value = spreadLegParams.InsideSmartQuote;
            dataGridViewSpreadDetails.Rows[(int)FieldType.InsideSmartQuote].Cells[columnNum - 1] = textCell;

            //Smart Quote Limit
            textCell = new DataGridViewTextBoxCell();
            textCell.Value = spreadLegParams.SmartQuoteLimit;
            dataGridViewSpreadDetails.Rows[(int)FieldType.SmartQuoteLimit].Cells[columnNum - 1] = textCell;

            //Consider implied
            checkCell = new DataGridViewCheckBoxCell();
            checkCell.Value = spreadLegParams.ConsiderImplied;
            dataGridViewSpreadDetails.Rows[(int)FieldType.ConsiderImplied].Cells[columnNum - 1] = checkCell;

            //Hedge round
            checkCell = new DataGridViewCheckBoxCell();
            checkCell.Value = spreadLegParams.HedgeRound;
            dataGridViewSpreadDetails.Rows[(int)FieldType.HedgeRound].Cells[columnNum - 1] = checkCell;

            //Max Price Move
            textCell = new DataGridViewTextBoxCell();
            textCell.Value = spreadLegParams.MaxPriceMove;
            dataGridViewSpreadDetails.Rows[(int)FieldType.MaxPriceMove].Cells[columnNum - 1] = textCell;

            //Max Order Move
            textCell = new DataGridViewTextBoxCell();
            textCell.Value = spreadLegParams.MaxOrderMove;
            dataGridViewSpreadDetails.Rows[(int)FieldType.MaxOrderMove].Cells[columnNum - 1] = textCell;

            //Cancel replace for Quantity
            checkCell = new DataGridViewCheckBoxCell();
            checkCell.Value = spreadLegParams.UseCancelReplaceForQtyReduction;
            dataGridViewSpreadDetails.Rows[(int)FieldType.CancelReplaceForQuantity].Cells[columnNum - 1] = checkCell;

            //1/Price
            checkCell = new DataGridViewCheckBoxCell();
            checkCell.Value = spreadLegParams.OneOverPrice;
            dataGridViewSpreadDetails.Rows[(int)FieldType.OneOverPrice].Cells[columnNum - 1] = checkCell;

            //Hidden row has the actual object ot instrument
            textCell = new DataGridViewTextBoxCell();
            textCell.Value = spreadLegParams.InstrumentKey;
            dataGridViewSpreadDetails.Rows[(int)FieldType.HiddenObject].Visible = false;
            dataGridViewSpreadDetails.Rows[(int)FieldType.HiddenObject].Cells[columnNum - 1] = textCell;

            if (m_spreadLegs.ContainsKey(spreadLegParams.InstrumentKey))
            {
                Instrument instrument = (Instrument)m_spreadLegs[spreadLegParams.InstrumentKey];
                updateInstrument(instrument);

                // Select the right order feed
                DataGridViewComboBoxCell combo = (DataGridViewComboBoxCell)dataGridViewSpreadDetails.Rows[(int)FieldType.OrderFeedCombo].Cells[columnNum - 1];
                foreach (string orderFeedName in combo.Items)
                {
                    if (orderFeedName.Equals(spreadLegParams.InstrumentKey.MarketKey.Name))
                    {
                        combo.Value = orderFeedName;
                        break;
                    }
                }
            }
            else
            {
                // Pass the Key to our subscription method
                findInstrument(spreadLegParams.InstrumentKey);
            }
        }

        /// <summary>
        /// Add a new leg to the spread given a InstrumentKey.
        /// </summary>
        /// <param name="instrKey">InstrumentKey</param>
        private void addDefaultLeg(InstrumentKey instrKey)
        {
            DataGridViewColumn col = new DataGridViewColumn();
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            col.Width = 150;

            col.HeaderText = "Leg " + Convert.ToString(dataGridViewSpreadDetails.Columns.Count);

            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewTextBoxCell textCell = new DataGridViewTextBoxCell();
            textCell.Style.BackColor = Color.White;
            col.CellTemplate = textCell;
            int retunValue = dataGridViewSpreadDetails.Columns.Add(col);
            int columnNum = dataGridViewSpreadDetails.Columns.Count;

            DataGridViewButtonCell cell = new DataGridViewButtonCell();
            cell.ToolTipText = "Delete this leg";
            cell.Style.BackColor = Color.Red;
            cell.Value = "Delete this leg";

            dataGridViewSpreadDetails.Rows[(int)FieldType.DeleteButton].Cells[columnNum - 1] = cell;

            textCell = new DataGridViewTextBoxCell();
            textCell.Value = "Waiting for subscription";
            dataGridViewSpreadDetails.Rows[(int)FieldType.Contract].Cells[columnNum - 1] = textCell;

            DataGridViewComboBoxCell comboCell = new DataGridViewComboBoxCell();
            comboCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
            comboCell.Items.Clear();
            dataGridViewSpreadDetails.Rows[(int)FieldType.OrderFeedCombo].Cells[columnNum - 1] = comboCell;

            comboCell = new DataGridViewComboBoxCell();
            comboCell.Items.Add("<Default>");
            comboCell.Value = comboCell.Items[0];
            dataGridViewSpreadDetails.Rows[(int)FieldType.CustomerAccount].Cells[columnNum - 1] = comboCell;

            DataGridViewCheckBoxCell checkCell = new DataGridViewCheckBoxCell();
            checkCell.Value = true;
            dataGridViewSpreadDetails.Rows[(int)FieldType.ActiveQuoting].Cells[columnNum - 1] = checkCell;

            checkCell = new DataGridViewCheckBoxCell();
            checkCell.Value = false;
            dataGridViewSpreadDetails.Rows[(int)FieldType.ConsiderOwnOrders].Cells[columnNum - 1] = checkCell;

            comboCell = new DataGridViewComboBoxCell();
            comboCell.Items.Add("LimitOrder");
            comboCell.Items.Add("MarketOrder");
            comboCell.Items.Add("MLMOrder");
            comboCell.Value = comboCell.Items[0];
            dataGridViewSpreadDetails.Rows[(int)FieldType.OffsetHedge].Cells[columnNum - 1] = comboCell;

            //Payup ticks
            textCell = new DataGridViewTextBoxCell();
            textCell.Value = "0";
            dataGridViewSpreadDetails.Rows[(int)FieldType.PayupTicks].Cells[columnNum - 1] = textCell;

            //spread ratio
            textCell = new DataGridViewTextBoxCell();
            textCell.Value = (columnNum % 2) == 0 ? "1" : "-1";
            dataGridViewSpreadDetails.Rows[(int)FieldType.SpreadRatio].Cells[columnNum - 1] = textCell;

            //spread Multiplier
            textCell = new DataGridViewTextBoxCell();
            textCell.Value = (columnNum % 2) == 0 ? "1" : "-1";
            dataGridViewSpreadDetails.Rows[(int)FieldType.SpreadMultiplier].Cells[columnNum - 1] = textCell;

            //offset volume multiplier
            textCell = new DataGridViewTextBoxCell();
            textCell.Value = "1";
            dataGridViewSpreadDetails.Rows[(int)FieldType.QuantityMultiplier].Cells[columnNum - 1] = textCell;

            //base volume mulitplier
            textCell = new DataGridViewTextBoxCell();
            textCell.Value = "0";
            dataGridViewSpreadDetails.Rows[(int)FieldType.BasicVolumeLean].Cells[columnNum - 1] = textCell;

            //use cancel replace
            checkCell = new DataGridViewCheckBoxCell();
            checkCell.Value = false;
            dataGridViewSpreadDetails.Rows[(int)FieldType.UserCancelReplace].Cells[columnNum - 1] = checkCell;

            //Queue Holder orders
            textCell = new DataGridViewTextBoxCell();
            textCell.Value = "0";
            dataGridViewSpreadDetails.Rows[(int)FieldType.QueueHolderOrders].Cells[columnNum - 1] = textCell;

            //inside smart quote
            textCell = new DataGridViewTextBoxCell();
            textCell.Value = "99";
            dataGridViewSpreadDetails.Rows[(int)FieldType.InsideSmartQuote].Cells[columnNum - 1] = textCell;

            //Smart Quote Limit
            textCell = new DataGridViewTextBoxCell();
            textCell.Value = "99";
            dataGridViewSpreadDetails.Rows[(int)FieldType.SmartQuoteLimit].Cells[columnNum - 1] = textCell;

            //Consider implied
            checkCell = new DataGridViewCheckBoxCell();
            checkCell.Value = true;
            dataGridViewSpreadDetails.Rows[(int)FieldType.ConsiderImplied].Cells[columnNum - 1] = checkCell;

            //Hedge round
            checkCell = new DataGridViewCheckBoxCell();
            checkCell.Value = false;
            dataGridViewSpreadDetails.Rows[(int)FieldType.HedgeRound].Cells[columnNum - 1] = checkCell;

            //Max Price Move
            textCell = new DataGridViewTextBoxCell();
            textCell.Value = "9999";
            dataGridViewSpreadDetails.Rows[(int)FieldType.MaxPriceMove].Cells[columnNum - 1] = textCell;

            //Max Order Move
            textCell = new DataGridViewTextBoxCell();
            textCell.Value = "9999";
            dataGridViewSpreadDetails.Rows[(int)FieldType.MaxOrderMove].Cells[columnNum - 1] = textCell;

            //Cancel replace for Quantity
            checkCell = new DataGridViewCheckBoxCell();
            checkCell.Value = false;
            dataGridViewSpreadDetails.Rows[(int)FieldType.CancelReplaceForQuantity].Cells[columnNum - 1] = checkCell;

            //1/Price
            checkCell = new DataGridViewCheckBoxCell();
            checkCell.Value = false;
            dataGridViewSpreadDetails.Rows[(int)FieldType.OneOverPrice].Cells[columnNum - 1] = checkCell;

            //Hidden row has the actual object ot instrument
            textCell = new DataGridViewTextBoxCell();
            textCell.Value = instrKey;
            dataGridViewSpreadDetails.Rows[(int)FieldType.HiddenObject].Visible = false;
            dataGridViewSpreadDetails.Rows[(int)FieldType.HiddenObject].Cells[columnNum - 1] = textCell;
        }

        /// <summary>
        /// Find a given instrument based on the InstrumentKey.
        /// </summary>
        /// <param name="instrumentKey">InstrumentKey</param>
        private void findInstrument(InstrumentKey instrumentKey)
        {
            InstrumentLookupSubscription sub = new InstrumentLookupSubscription(m_session, m_dispatcher, instrumentKey);
            sub.Update += new EventHandler<InstrumentLookupSubscriptionEventArgs>(instrumentLookupSubscription_Update);
            sub.Start();
        }

        /// <summary>
        /// InstrumentLookupSubscription Update.
        /// </summary>
        void instrumentLookupSubscription_Update(object sender, InstrumentLookupSubscriptionEventArgs e)
        {
            if (e.Instrument != null && e.IsFinal)
            {
                if (!m_spreadLegs.ContainsKey(e.Instrument.Key))
                {
                    m_spreadLegs.Add(e.Instrument.Key, e.Instrument);
                }

                updateInstrument(e.Instrument);
            }
        }

        /// <summary>
        /// Update the instrument information in the grid.
        /// </summary>
        /// <param name="instrument">Instrument</param>
        private void updateInstrument(Instrument instrument)
        {
            for (int i = 1; i < dataGridViewSpreadDetails.Columns.Count; i++)
            {
                //get the instrument which is a hidden row
                InstrumentKey instrKey = (InstrumentKey)dataGridViewSpreadDetails.Rows[(int)FieldType.HiddenObject].Cells[i].Value;
                if (instrument.Key == instrKey)
                {
                    string instrName = instrument.GetFormattedName(InstrumentNameFormat.Full);
                    dataGridViewSpreadDetails.Rows[(int)FieldType.Contract].Cells[i].Value = instrName;

                    DataGridViewComboBoxCell combo = (DataGridViewComboBoxCell)dataGridViewSpreadDetails.Rows[(int)FieldType.OrderFeedCombo].Cells[i];
                    combo.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;

                    string selectedOrderFeed = (string)combo.Value;
                    combo.Items.Clear();
                    combo.Value = "";
                    foreach (var feed in instrument.GetValidOrderFeeds())
                    {
                        combo.Items.Add(feed.ConnectionKey.GatewayKey.Name);
                    }

                    if (!String.IsNullOrEmpty(selectedOrderFeed) && combo.Items.Contains(selectedOrderFeed))
                    {
                        combo.Value = selectedOrderFeed;
                    }
                }
            }
        }

        /// <summary>
        /// Move the SpreadDetails object into the column legs.
        /// </summary>
        private void moveSpreadDetailsToColumn()
        {
            //delete all legs except for column 0 which is the header properties
            while (dataGridViewSpreadDetails.Columns.Count > 1)
            {
                dataGridViewSpreadDetails.Columns.RemoveAt(dataGridViewSpreadDetails.Columns.Count - 1);
            }

            textBoxSpreadName.Text = m_spreadDetails.Name;
            comboBoxLegColor.BackColor = m_spreadDetails.Color;

            int index = comboBoxBasedOn.FindStringExact(m_spreadDetails.PricingModel.ToString());
            comboBoxBasedOn.SelectedIndex = index;

            index = comboBoxSlopSettings.FindStringExact(m_spreadDetails.SlopType.ToString());
            comboBoxSlopSettings.SelectedIndex = index;

            index = comboBoxSpreadLTP.FindStringExact(m_spreadDetails.LTPModel.ToString());
            comboBoxSpreadLTP.SelectedIndex = index;

            foreach (SpreadLegDetails spreadLegDetail in m_spreadDetails.Legs)
            {
                addLeg(spreadLegDetail);
            }
        }

        /// <summary>
        /// Move the column information about the legs into the SpreadDetails object.
        /// </summary>
        /// <returns></returns>
        private bool moveColumnToSpreadDetails()
        {
            m_spreadDetails.Legs.Clear();
            m_spreadDetails.Name = textBoxSpreadName.Text.ToString();
            m_spreadDetails.PricingModel = (PricingModel)System.Enum.Parse(typeof(PricingModel), comboBoxBasedOn.Text, true);
            m_spreadDetails.SlopType = (SlopType)System.Enum.Parse(typeof(SlopType), comboBoxSlopSettings.Text, true); 
            m_spreadDetails.LTPModel = (LTPModel)System.Enum.Parse(typeof(LTPModel), comboBoxSpreadLTP.Text, true);
            m_spreadDetails.Color = comboBoxLegColor.BackColor;

            for (int i = 1; i < dataGridViewSpreadDetails.Columns.Count; i++)
            {
                //get the instrument which is a hidden row
                InstrumentKey instrKey = (InstrumentKey)dataGridViewSpreadDetails.Rows[(int)FieldType.HiddenObject].Cells[i].Value;
                if (m_spreadLegs.ContainsKey(instrKey))
                {
                    Instrument instrument = (Instrument)m_spreadLegs[instrKey];

                    string orderFeedName = dataGridViewSpreadDetails.Rows[(int)FieldType.OrderFeedCombo].Cells[i].Value.ToString();
                    OrderFeed of = instrument.GetValidOrderFeeds().FirstOrDefault(f => f.ConnectionKey.GatewayKey.Name == orderFeedName);

                    if (of == null)
                    {
                        MessageBox.Show("OrderFeed must be selected!");
                        return false;
                    }

                    if (m_isNewSpread)
                    {
                        SpreadLegDetails spreadLeg = new SpreadLegDetails(instrKey, of.ConnectionKey);
                        m_spreadDetails.Legs.Append(spreadLeg);
                    }

                    m_spreadDetails.Legs[i - 1].CustomerName = dataGridViewSpreadDetails.Rows[(int)FieldType.CustomerAccount].Cells[i].Value.ToString();
                    m_spreadDetails.Legs[i - 1].ActiveQuoting = System.Convert.ToBoolean(dataGridViewSpreadDetails.Rows[(int)FieldType.ActiveQuoting].Cells[i].Value);
                    m_spreadDetails.Legs[i - 1].ConsiderOwnOrders = System.Convert.ToBoolean(dataGridViewSpreadDetails.Rows[(int)FieldType.ConsiderOwnOrders].Cells[i].Value);
                    m_spreadDetails.Legs[i - 1].OffsetHedgeType = (HedgeType)System.Enum.Parse(typeof(HedgeType), dataGridViewSpreadDetails.Rows[(int)FieldType.OffsetHedge].Cells[i].Value.ToString(), true);
                    m_spreadDetails.Legs[i - 1].PayupTicks = System.Convert.ToInt32(dataGridViewSpreadDetails.Rows[(int)FieldType.PayupTicks].Cells[i].Value);
                    m_spreadDetails.Legs[i - 1].SpreadRatio = System.Convert.ToInt32(dataGridViewSpreadDetails.Rows[(int)FieldType.SpreadRatio].Cells[i].Value);
                    m_spreadDetails.Legs[i - 1].PriceMultiplier = System.Convert.ToInt32(dataGridViewSpreadDetails.Rows[(int)FieldType.SpreadMultiplier].Cells[i].Value);
                    m_spreadDetails.Legs[i - 1].QuantityMultiplier = System.Convert.ToInt32(dataGridViewSpreadDetails.Rows[(int)FieldType.QuantityMultiplier].Cells[i].Value);
                    m_spreadDetails.Legs[i - 1].BaseVolumeLean = System.Convert.ToInt32(dataGridViewSpreadDetails.Rows[(int)FieldType.BasicVolumeLean].Cells[i].Value);
                    m_spreadDetails.Legs[i - 1].UseCancelReplace = System.Convert.ToBoolean(dataGridViewSpreadDetails.Rows[(int)FieldType.UserCancelReplace].Cells[i].Value);
                    m_spreadDetails.Legs[i - 1].QueueHolderOrders = System.Convert.ToInt32(dataGridViewSpreadDetails.Rows[(int)FieldType.QueueHolderOrders].Cells[i].Value);
                    m_spreadDetails.Legs[i - 1].InsideSmartQuote = System.Convert.ToInt32(dataGridViewSpreadDetails.Rows[(int)FieldType.InsideSmartQuote].Cells[i].Value);
                    m_spreadDetails.Legs[i - 1].SmartQuoteLimit = System.Convert.ToInt32(dataGridViewSpreadDetails.Rows[(int)FieldType.SmartQuoteLimit].Cells[i].Value);
                    m_spreadDetails.Legs[i - 1].ConsiderImplied = System.Convert.ToBoolean(dataGridViewSpreadDetails.Rows[(int)FieldType.ConsiderImplied].Cells[i].Value);
                    m_spreadDetails.Legs[i - 1].HedgeRound = System.Convert.ToBoolean(dataGridViewSpreadDetails.Rows[(int)FieldType.HedgeRound].Cells[i].Value);
                    m_spreadDetails.Legs[i - 1].MaxPriceMove = System.Convert.ToInt32(dataGridViewSpreadDetails.Rows[(int)FieldType.MaxPriceMove].Cells[i].Value);
                    m_spreadDetails.Legs[i - 1].MaxOrderMove = System.Convert.ToInt32(dataGridViewSpreadDetails.Rows[(int)FieldType.MaxOrderMove].Cells[i].Value);
                    m_spreadDetails.Legs[i - 1].UseCancelReplaceForQtyReduction = System.Convert.ToBoolean(dataGridViewSpreadDetails.Rows[(int)FieldType.CancelReplaceForQuantity].Cells[i].Value);
                    m_spreadDetails.Legs[i - 1].OneOverPrice = System.Convert.ToBoolean(dataGridViewSpreadDetails.Rows[(int)FieldType.OneOverPrice].Cells[i].Value);
                }
                else
                {
                    MessageBox.Show("could not find insturment in the map for instrument key" + instrKey.SeriesKey);
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Save or update the spread.
        /// </summary>
        private void buttonSave_Click(object sender, EventArgs e)
        {
            ASReturnCodes retCode = ASReturnCodes.Success;

            if (moveColumnToSpreadDetails())
            {
                if (m_isNewSpread)
                {
                    retCode = m_session.TTAPI.AutospreaderManager.AddSpreadDetails(m_spreadDetails);
                }
                else
                {
                    retCode = m_session.TTAPI.AutospreaderManager.UpdateSpreadDetails(m_spreadDetails);
                }

                if (retCode == ASReturnCodes.Success)
                {
                    Close();
                }
                else
                {
                    MessageBox.Show("spread definition save failed " + System.Enum.GetName(typeof(ASReturnCodes), retCode));
                }
            }
        }

        /// <summary>
        /// Cancel the update.
        /// </summary>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Update the spread leg color.
        /// </summary>
        private void comboBoxLegColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.AllowFullOpen = false;
            colorDialog.ShowHelp = true;
            colorDialog.Color = comboBoxLegColor.BackColor;

            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                comboBoxLegColor.BackColor = colorDialog.Color;
            }
        }

        /// <summary>
        /// Delete a given leg when the "Delete" leg button is pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewSpreadDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == 0 && e.ColumnIndex > 0)
            {
                dataGridViewSpreadDetails.Columns.RemoveAt(e.ColumnIndex);

                for (int i = 1; i < dataGridViewSpreadDetails.Columns.Count; i++)
                {
                    dataGridViewSpreadDetails.Columns[i].HeaderText = "Leg " + i.ToString();
                }            
            }
        }

        #region DragDrop
        private void SpreadDetailsForm_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.HasInstrumentKeys())
            {
                foreach (InstrumentKey instrumentKey in e.Data.GetInstrumentKeys())
                {
                    addDefaultLeg(instrumentKey);
                    findInstrument(instrumentKey);
                }
            }
        }

        private void SpreadDetailsForm_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.HasInstrumentKeys())
            {
                e.Effect = DragDropEffects.Copy;
            }
        }
        #endregion

    }
}
