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
using TradingTechnologies.TTAPI.Tradebook;
using TradingTechnologies.TTAPI.CustomerDefaults;

namespace TTAPI_Samples
{
    /// <summary>
    /// OrderFilter
    /// 
    /// This example demonstrates using the TT API to filter the order updates using
    /// user defined TradeSubscriptionFilter objects. 
    /// </summary>
    public partial class frmOrderFilter : Form
    {
        // Declare the TTAPI objects.
        private XTraderModeTTAPI m_TTAPI = null;
        private CustomerDefaultsSubscription m_customerDefaultsSubscription = null;
        private InstrumentTradeSubscription m_instrumentTradeSubscription = null;
        private bool m_isShutdown = false, m_shutdownInProcess = false;

        public frmOrderFilter()
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

                m_customerDefaultsSubscription = new CustomerDefaultsSubscription(m_TTAPI.Session, Dispatcher.Current);
                m_customerDefaultsSubscription.CustomerDefaultsChanged += new EventHandler(m_CustomerDefaultsSubscription_CustomerDefaultsChanged);
                m_customerDefaultsSubscription.Start();
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
                if (m_customerDefaultsSubscription != null)
                {
                    m_customerDefaultsSubscription.CustomerDefaultsChanged -= m_CustomerDefaultsSubscription_CustomerDefaultsChanged;
                    m_customerDefaultsSubscription.Dispose();
                    m_customerDefaultsSubscription = null;
                }

                if (m_instrumentTradeSubscription != null)
                {
                    m_instrumentTradeSubscription.OrderAdded -= instrumentTradeSubscription_OrderAdded;
                    m_instrumentTradeSubscription.OrderDeleted -= instrumentTradeSubscription_OrderDeleted;
                    m_instrumentTradeSubscription.Dispose();
                    m_instrumentTradeSubscription = null;
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
        /// Finalize the GUI
        /// </summary>
        private void frmOrderFilter_Load(object sender, EventArgs e)
        {
            // column widths are hard-coded for readability
            this.lboAuditLog.Columns.Add("Order Action", 100);
            this.lboAuditLog.Columns.Add("SiteOrderKey", 100);
            this.lboAuditLog.Columns.Add("Price", 60);
            this.lboAuditLog.Columns.Add("Qty", 60);
            this.lboAuditLog.Columns.Add("Buy/Sell", 100);
        }

        /// <summary>
        /// CustomerDefaultsChanged subscription callback.
        /// Update the Customer combo box.
        /// </summary>
        void m_CustomerDefaultsSubscription_CustomerDefaultsChanged(object sender, EventArgs e)
        {
            try
            {
                this.cboCustomer.Items.Clear();
                CustomerDefaultsSubscription cds = sender as CustomerDefaultsSubscription;
                foreach (CustomerDefaultEntry entry in cds.CustomerDefaults)
                    this.cboCustomer.Items.Add(entry);

                if (this.cboCustomer.Items.Count > 0)
                    this.cboCustomer.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception");
            }
        }

        #region Drag and Drop

        /// <summary>
        /// Form drag over event handler.
        /// The form must enable "AllowDrop" for these events to fire.
        /// </summary>
        private void frmOrderFilter_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.HasInstrumentKeys())
                e.Effect = DragDropEffects.Copy;
        }

        /// <summary>
        /// Form drag and drop event handler.
        /// The form must enable "AllowDrop" for these events to fire.
        /// </summary>
        private void frmOrderFilter_DragDrop(object sender, DragEventArgs e)
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

                    // populate the order feed dropdown
                    this.cboOrderFeed.Items.Clear();
                    foreach (OrderFeed orderFeed in e.Instrument.GetValidOrderFeeds())
                        this.cboOrderFeed.Items.Add(orderFeed);

                    this.cboOrderFeed.SelectedIndex = 0;

                    // This sample will monitor new orders and deleting working orders
                    CreateInstrumentTradeSubscription(e.Instrument);
                    m_instrumentTradeSubscription.Start();
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

        private void CreateInstrumentTradeSubscription(Instrument pInstrument)
        {
            if (m_instrumentTradeSubscription != null)
            {
                m_instrumentTradeSubscription.Dispose();
                m_instrumentTradeSubscription = null;
            }

            m_instrumentTradeSubscription = new InstrumentTradeSubscription(m_TTAPI.Session, Dispatcher.Current, pInstrument);
            m_instrumentTradeSubscription.OrderAdded += new EventHandler<OrderAddedEventArgs>(instrumentTradeSubscription_OrderAdded);
            m_instrumentTradeSubscription.OrderDeleted += new EventHandler<OrderDeletedEventArgs>(instrumentTradeSubscription_OrderDeleted);
        }

        #endregion

        #region InstrumentTradeSubscription Updates

        /// <summary>
        /// Triggered when a new Order is received
        /// </summary>
        void instrumentTradeSubscription_OrderAdded(object sender, OrderAddedEventArgs e)
        {
            UpdateAuditLog(e.Order);
        }

        /// <summary>
        /// Triggered when an Order monitored by this subsciption is deleted 
        /// </summary>
        void instrumentTradeSubscription_OrderDeleted(object sender, OrderDeletedEventArgs e)
        {
            UpdateAuditLog(e.DeletedUpdate);
        }

        /// <summary>
        /// Publish the order data to the GUI
        /// </summary>
        /// <param name="eventName">Event which fired the order update</param>
        /// <param name="order">Order which has updated</param>
        private void UpdateAuditLog(Order order)
        {
            ListViewItem item = new ListViewItem();

            item.Text = order.Action.ToString();
            item.SubItems.Add(order.SiteOrderKey);
            item.SubItems.Add(order.LimitPrice.ToString());
            item.SubItems.Add(order.OrderQuantity.ToString());
            item.SubItems.Add(order.BuySell.ToString());

            this.lboAuditLog.Items.Add(item);
        }

        #endregion

        #region Send Order

        /// <summary>
        /// This function is called when the user clicks the buy button.
        /// </summary>
        private void btnBuy_Click(object sender, EventArgs e)
        {
            SendOrder(BuySell.Buy);
        }

        /// <summary>
        /// This function is called when the user clicks the sell button.
        /// </summary>
        private void btnSell_Click(object sender, EventArgs e)
        {
            SendOrder(BuySell.Sell);
        }

        /// <summary>
        /// This function sets up the OrderProfile and submits
        /// the order using the InstrumentTradeSubscription SendOrder method.
        /// </summary>
        /// <param name="buySell">The side of the market to place the order on.</param>
        private void SendOrder(BuySell buySell)
        {
            // make sure the user has dragged a contract before we continue
            if (m_instrumentTradeSubscription == null)
                return;

            try
            {
                OrderFeed orderFeed = this.cboOrderFeed.SelectedItem as OrderFeed;
                CustomerDefaultEntry customer = this.cboCustomer.SelectedItem as CustomerDefaultEntry;

                OrderProfile orderProfile = new OrderProfile(orderFeed, m_instrumentTradeSubscription.Instrument, customer.Customer);

                // Set for Buy or Sell.
                orderProfile.BuySell = buySell;

                // Set the quantity.
                orderProfile.QuantityToWork = Quantity.FromString(m_instrumentTradeSubscription.Instrument, txtQuantity.Text);

                // Set the order type to "Limit" for a limit order.
                orderProfile.OrderType = OrderType.Limit;

                // Set the limit order price.
                orderProfile.LimitPrice = Price.FromString(m_instrumentTradeSubscription.Instrument, txtPrice.Text);

                // Send the order.
                if (m_instrumentTradeSubscription.SendOrder(orderProfile))
                {
                    // log the order send
                    Console.WriteLine("Order Sent");
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        #endregion

        /// <summary>
        /// Apply the filter
        /// </summary>
        private void btnApplyOrderFilter_Click(object sender, EventArgs e)
        {
            // return if object is not instantiated
            if (m_instrumentTradeSubscription == null)
                return;

            CreateInstrumentTradeSubscription(m_instrumentTradeSubscription.Instrument);

            // no filter to set, return
            if (this.cboFilter1.SelectedIndex < 0)
                return;

            // reset the current filters
            m_instrumentTradeSubscription.ClearFilter();

            // create both "and" and "or" filters, setting false so the values are not negated
            TradeSubscriptionAndFilter andFilter = new TradeSubscriptionAndFilter(false);
            TradeSubscriptionOrFilter orFilter = new TradeSubscriptionOrFilter(false);

            // set the first filter
            if (this.rbAnd1.Checked)
                andFilter.AddFilter(FilterFactory.Get(this.cboFilter1.SelectedIndex, this.txtFilter1.Text, m_instrumentTradeSubscription.Instrument));
            else
                orFilter.AddFilter(FilterFactory.Get(this.cboFilter1.SelectedIndex, this.txtFilter1.Text, m_instrumentTradeSubscription.Instrument));

            // append additional filters if applicable
            if (this.cboFilter2.SelectedIndex >= 0)
            {
                if (this.rbAnd2.Checked)
                    andFilter.AddFilter(FilterFactory.Get(this.cboFilter2.SelectedIndex, this.txtFilter2.Text, m_instrumentTradeSubscription.Instrument));
                else
                    orFilter.AddFilter(FilterFactory.Get(this.cboFilter2.SelectedIndex, this.txtFilter2.Text, m_instrumentTradeSubscription.Instrument));
            }

            if (this.cboFilter3.SelectedIndex >= 0)
            {
                if (this.rbAnd3.Checked)
                    andFilter.AddFilter(FilterFactory.Get(this.cboFilter3.SelectedIndex, this.txtFilter3.Text, m_instrumentTradeSubscription.Instrument));
                else
                    orFilter.AddFilter(FilterFactory.Get(this.cboFilter3.SelectedIndex, this.txtFilter3.Text, m_instrumentTradeSubscription.Instrument));
            }

            if (this.cboFilter4.SelectedIndex >= 0)
            {
                if (this.rbAnd4.Checked)
                    andFilter.AddFilter(FilterFactory.Get(this.cboFilter4.SelectedIndex, this.txtFilter4.Text, m_instrumentTradeSubscription.Instrument));
                else
                    orFilter.AddFilter(FilterFactory.Get(this.cboFilter4.SelectedIndex, this.txtFilter4.Text, m_instrumentTradeSubscription.Instrument));
            }

            // add the "and" filter within the "or" filter if entries exist
            if (andFilter.Filters.Count > 0)
                orFilter.AddFilter(andFilter);

            // set the filter and restart the subscription
            m_instrumentTradeSubscription.SetFilter(orFilter);
            m_instrumentTradeSubscription.Start();
        }

        /// <summary>
        /// Enable the next filter option if there is a valid selection
        /// </summary>
        private void cboFilter1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboFilter1.SelectedIndex >= 0)
                this.panelFilter2.Enabled = true;

            // show / hide text box
            if (this.cboFilter1.SelectedIndex < 2)
                this.txtFilter1.Visible = true;
            else
                this.txtFilter1.Visible = false;

            this.txtFilter1.Text = "";
        }

        /// <summary>
        /// Enable the next filter option if there is a valid selection
        /// </summary>
        private void cboFilter2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboFilter2.SelectedIndex >= 0)
                this.panelFilter3.Enabled = true;

            // show / hide text box
            if (this.cboFilter2.SelectedIndex < 2)
                this.txtFilter2.Visible = true;
            else
                this.txtFilter2.Visible = false;

            this.txtFilter2.Text = "";
        }

        /// <summary>
        /// Enable the next filter option if there is a valid selection
        /// </summary>
        private void cboFilter3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboFilter3.SelectedIndex >= 0)
                this.panelFilter4.Enabled = true;

            // show / hide text box
            if (this.cboFilter3.SelectedIndex < 2)
                this.txtFilter3.Visible = true;
            else
                this.txtFilter3.Visible = false;

            this.txtFilter3.Text = "";
        }

        /// <summary>
        /// Enable the next filter option if there is a valid selection
        /// </summary>
        private void cboFilter4_SelectedIndexChanged(object sender, EventArgs e)
        {
            // show / hide text box
            if (this.cboFilter4.SelectedIndex < 2)
                this.txtFilter4.Visible = true;
            else
                this.txtFilter4.Visible = false;

            this.txtFilter4.Text = "";
        }

        /// <summary>
        /// Reset all the filters
        /// </summary>
        private void btnResetOrderFilter_Click(object sender, EventArgs e)
        {
            // reset the current filters
            if (m_instrumentTradeSubscription != null)
                m_instrumentTradeSubscription.ClearFilter();

            this.panelFilter2.Enabled = false;
            this.panelFilter3.Enabled = false;
            this.panelFilter4.Enabled = false;

            // remove any seleted values from the dropdown
            this.cboFilter1.SelectedIndex = -1;
            this.cboFilter2.SelectedIndex = -1;
            this.cboFilter3.SelectedIndex = -1;
            this.cboFilter4.SelectedIndex = -1;

            // disable all the logical operator radio buttons
            this.rbOr1.Checked = true;
            this.rbOr2.Checked = true;
            this.rbOr3.Checked = true;
            this.rbOr4.Checked = true;

            // reset all filter text
            this.txtFilter1.Text = "";
            this.txtFilter2.Text = "";
            this.txtFilter3.Text = "";
            this.txtFilter4.Text = "";

            // hide all filter text boxes
            this.txtFilter1.Visible = false;
            this.txtFilter2.Visible = false;
            this.txtFilter3.Visible = false;
            this.txtFilter4.Visible = false;
        }

        /// <summary>
        /// Factory design pattern used to create filters based on an index (from the dropdown)
        /// </summary>
        static class FilterFactory
        {
            public static TradeSubscriptionFilter Get(int index, string value, Instrument instr)
            {
                switch (index)
                {
                    case 0:
                        return new PriceFilter(instr, value);
                    case 1:
                        return new QuantityFilter(instr, Convert.ToInt32(value));
                    case 2:
                        return new BuySellFilter(BuySell.Buy);
                    case 3:
                        return new BuySellFilter(BuySell.Sell);
                    default:
                        return null;
                }
            }
        }

        /// <summary>
        /// Filter for matching BuySell 
        /// </summary>
        class BuySellFilter : TradeSubscriptionFilter
        {
            private BuySell m_BuySell;

            public BuySellFilter(BuySell buySell)
                : base(false, "BuySell")
            {
                m_BuySell = buySell;
            }

            public override TradeSubscriptionFilter Clone()
            {
                return new BuySellFilter(m_BuySell);
            }

            public override bool IsEqual(TradeSubscriptionFilter filter)
            {
                return filter.Equals(this);
            }

            public override bool IsMatch(Fill fill)
            {
                return m_BuySell.Equals(fill.BuySell);
            }

            public override bool IsMatch(Order order)
            {
                return m_BuySell.Equals(order.BuySell);
            }
        }

        /// <summary>
        /// Filter for matching prices for working orders, and prices that fills were matched at
        /// </summary>
        class PriceFilter : TradeSubscriptionFilter
        {
            private Price m_Price;

            public PriceFilter(Price price)
                : base(false, "Price")
            {
                m_Price = price;
            }

            public PriceFilter(Instrument instr, string price)
                : base(false, "Price")
            {
                m_Price = Price.FromString(instr, price);
            }

            public override TradeSubscriptionFilter Clone()
            {
                return new PriceFilter(m_Price);
            }

            public override bool IsEqual(TradeSubscriptionFilter filter)
            {
                return filter.Equals(this);
            }

            public override bool IsMatch(Fill fill)
            {
                return m_Price.Equals(fill.MatchPrice);
            }

            public override bool IsMatch(Order order)
            {
                return m_Price.Equals(order.LimitPrice);
            }
        }

        /// <summary>
        /// Filter for matching order and fill quantity
        /// </summary>
        class QuantityFilter : TradeSubscriptionFilter
        {
            private Quantity m_Qty;

            public QuantityFilter(Quantity qty)
                : base(false, "Product")
            {
                m_Qty = qty;
            }

            public QuantityFilter(Instrument instr, int qty)
                : base(false, "Price")
            {
                m_Qty = Quantity.FromInt(instr, qty); 
            }

            public override TradeSubscriptionFilter Clone()
            {
                return new QuantityFilter(m_Qty);
            }

            public override bool IsEqual(TradeSubscriptionFilter filter)
            {
                return filter.Equals(this);
            }

            public override bool IsMatch(Fill fill)
            {
                return m_Qty.Equals(fill.Quantity);
            }

            public override bool IsMatch(Order order)
            {
                return m_Qty.Equals(order.OrderQuantity);
            }
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