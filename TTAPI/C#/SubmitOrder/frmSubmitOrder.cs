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
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;

using TradingTechnologies.TTAPI;
using TradingTechnologies.TTAPI.Tradebook;
using TradingTechnologies.TTAPI.WinFormsHelpers;
using TradingTechnologies.TTAPI.CustomerDefaults;

namespace TTAPI_Samples
{
    /// <summary>
    /// SubmitOrder
    /// 
    /// This example demonstrates using the TT API to submit an order.  The order types
    /// available in the application are market, limit, stop market and stop limit.  
    /// </summary>
    public class frmSubmitOrder : Form
    {
        // Declare private TTAPI member variables.
        private XTraderModeTTAPI m_TTAPI = null;
        private Dispatcher m_dispatcher = null;
        private CustomerDefaultsSubscription m_customerDefaultsSubscription = null;
        private InstrumentTradeSubscription m_instrumentTradeSubscription = null;
        private PriceSubscription m_priceSubscription = null;

        public frmSubmitOrder()
        {
            InitializeComponent();

            // Start TT API.
            startTTAPI();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null) 
                {
                    components.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private IContainer components;
        private Label label1;
        private ComboBox comboBoxOrderFeed;
        private System.Windows.Forms.StatusBar sbaStatus;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem mnuAbout;
        private System.Windows.Forms.GroupBox gboInstrumentInfo;
        private System.Windows.Forms.Label lblProductType;
        private System.Windows.Forms.TextBox txtProduct;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.Label lblExchange;
        private System.Windows.Forms.TextBox txtContract;
        private System.Windows.Forms.Label lblContract;
        private System.Windows.Forms.TextBox txtExchange;
        private System.Windows.Forms.TextBox txtProductType;
        private System.Windows.Forms.GroupBox gboInstrumentMarketData;
        private System.Windows.Forms.Label lblAskPrice;
        private System.Windows.Forms.TextBox txtAskPrice;
        private System.Windows.Forms.TextBox txtBidPrice;
        private System.Windows.Forms.Label lblChange;
        private System.Windows.Forms.Label lblBidPrice;
        private System.Windows.Forms.Label lblLastPrice;
        private System.Windows.Forms.TextBox txtLastPrice;
        private System.Windows.Forms.GroupBox gboOrderEntry;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.Label lblStopPrice;
        private System.Windows.Forms.Label lblOrderType;
        private System.Windows.Forms.Label lblNotProduction;
        private System.Windows.Forms.Label lblWarning;
        private System.Windows.Forms.TextBox txtChange;
        private System.Windows.Forms.Button btnBuy;
        private System.Windows.Forms.Button btnSell;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.ComboBox cboOrderType;
        private System.Windows.Forms.TextBox txtStopPrice;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.ComboBox cboCustomer;
        private System.Windows.Forms.TextBox txtOrderBook;

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.sbaStatus = new System.Windows.Forms.StatusBar();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.mnuAbout = new System.Windows.Forms.MenuItem();
            this.gboInstrumentInfo = new System.Windows.Forms.GroupBox();
            this.lblProductType = new System.Windows.Forms.Label();
            this.txtProduct = new System.Windows.Forms.TextBox();
            this.lblProduct = new System.Windows.Forms.Label();
            this.lblExchange = new System.Windows.Forms.Label();
            this.txtContract = new System.Windows.Forms.TextBox();
            this.lblContract = new System.Windows.Forms.Label();
            this.txtExchange = new System.Windows.Forms.TextBox();
            this.txtProductType = new System.Windows.Forms.TextBox();
            this.gboInstrumentMarketData = new System.Windows.Forms.GroupBox();
            this.lblAskPrice = new System.Windows.Forms.Label();
            this.txtAskPrice = new System.Windows.Forms.TextBox();
            this.txtBidPrice = new System.Windows.Forms.TextBox();
            this.lblChange = new System.Windows.Forms.Label();
            this.txtChange = new System.Windows.Forms.TextBox();
            this.lblBidPrice = new System.Windows.Forms.Label();
            this.lblLastPrice = new System.Windows.Forms.Label();
            this.txtLastPrice = new System.Windows.Forms.TextBox();
            this.gboOrderEntry = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxOrderFeed = new System.Windows.Forms.ComboBox();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.cboCustomer = new System.Windows.Forms.ComboBox();
            this.txtOrderBook = new System.Windows.Forms.TextBox();
            this.lblOrderType = new System.Windows.Forms.Label();
            this.btnSell = new System.Windows.Forms.Button();
            this.btnBuy = new System.Windows.Forms.Button();
            this.lblStopPrice = new System.Windows.Forms.Label();
            this.txtStopPrice = new System.Windows.Forms.TextBox();
            this.cboOrderType = new System.Windows.Forms.ComboBox();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.lblPrice = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.lblNotProduction = new System.Windows.Forms.Label();
            this.lblWarning = new System.Windows.Forms.Label();
            this.gboInstrumentInfo.SuspendLayout();
            this.gboInstrumentMarketData.SuspendLayout();
            this.gboOrderEntry.SuspendLayout();
            this.SuspendLayout();
            // 
            // sbaStatus
            // 
            this.sbaStatus.Location = new System.Drawing.Point(0, 420);
            this.sbaStatus.Name = "sbaStatus";
            this.sbaStatus.Size = new System.Drawing.Size(408, 22);
            this.sbaStatus.SizingGrip = false;
            this.sbaStatus.TabIndex = 62;
            this.sbaStatus.Text = "X_TRADER must be running to use this application.";
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuAbout});
            // 
            // mnuAbout
            // 
            this.mnuAbout.Index = 0;
            this.mnuAbout.Text = "About...";
            this.mnuAbout.Click += new System.EventHandler(this.AboutMenuItem_Click);
            // 
            // gboInstrumentInfo
            // 
            this.gboInstrumentInfo.Controls.Add(this.lblProductType);
            this.gboInstrumentInfo.Controls.Add(this.txtProduct);
            this.gboInstrumentInfo.Controls.Add(this.lblProduct);
            this.gboInstrumentInfo.Controls.Add(this.lblExchange);
            this.gboInstrumentInfo.Controls.Add(this.txtContract);
            this.gboInstrumentInfo.Controls.Add(this.lblContract);
            this.gboInstrumentInfo.Controls.Add(this.txtExchange);
            this.gboInstrumentInfo.Controls.Add(this.txtProductType);
            this.gboInstrumentInfo.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.gboInstrumentInfo.Location = new System.Drawing.Point(8, 56);
            this.gboInstrumentInfo.Name = "gboInstrumentInfo";
            this.gboInstrumentInfo.Size = new System.Drawing.Size(216, 136);
            this.gboInstrumentInfo.TabIndex = 63;
            this.gboInstrumentInfo.TabStop = false;
            this.gboInstrumentInfo.Text = "Instrument Information";
            // 
            // lblProductType
            // 
            this.lblProductType.Location = new System.Drawing.Point(8, 72);
            this.lblProductType.Name = "lblProductType";
            this.lblProductType.Size = new System.Drawing.Size(80, 16);
            this.lblProductType.TabIndex = 38;
            this.lblProductType.Text = "Product Type:";
            this.lblProductType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtProduct
            // 
            this.txtProduct.Location = new System.Drawing.Point(96, 48);
            this.txtProduct.Name = "txtProduct";
            this.txtProduct.Size = new System.Drawing.Size(100, 20);
            this.txtProduct.TabIndex = 35;
            // 
            // lblProduct
            // 
            this.lblProduct.Location = new System.Drawing.Point(40, 48);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(48, 16);
            this.lblProduct.TabIndex = 36;
            this.lblProduct.Text = "Product:";
            this.lblProduct.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblExchange
            // 
            this.lblExchange.Location = new System.Drawing.Point(24, 24);
            this.lblExchange.Name = "lblExchange";
            this.lblExchange.Size = new System.Drawing.Size(64, 16);
            this.lblExchange.TabIndex = 34;
            this.lblExchange.Text = "Market:";
            this.lblExchange.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtContract
            // 
            this.txtContract.Location = new System.Drawing.Point(96, 96);
            this.txtContract.Name = "txtContract";
            this.txtContract.Size = new System.Drawing.Size(100, 20);
            this.txtContract.TabIndex = 39;
            // 
            // lblContract
            // 
            this.lblContract.Location = new System.Drawing.Point(32, 96);
            this.lblContract.Name = "lblContract";
            this.lblContract.Size = new System.Drawing.Size(56, 16);
            this.lblContract.TabIndex = 40;
            this.lblContract.Text = "Contract:";
            this.lblContract.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtExchange
            // 
            this.txtExchange.Location = new System.Drawing.Point(96, 24);
            this.txtExchange.Name = "txtExchange";
            this.txtExchange.Size = new System.Drawing.Size(100, 20);
            this.txtExchange.TabIndex = 33;
            // 
            // txtProductType
            // 
            this.txtProductType.Location = new System.Drawing.Point(96, 72);
            this.txtProductType.Name = "txtProductType";
            this.txtProductType.Size = new System.Drawing.Size(100, 20);
            this.txtProductType.TabIndex = 37;
            // 
            // gboInstrumentMarketData
            // 
            this.gboInstrumentMarketData.Controls.Add(this.lblAskPrice);
            this.gboInstrumentMarketData.Controls.Add(this.txtAskPrice);
            this.gboInstrumentMarketData.Controls.Add(this.txtBidPrice);
            this.gboInstrumentMarketData.Controls.Add(this.lblChange);
            this.gboInstrumentMarketData.Controls.Add(this.txtChange);
            this.gboInstrumentMarketData.Controls.Add(this.lblBidPrice);
            this.gboInstrumentMarketData.Controls.Add(this.lblLastPrice);
            this.gboInstrumentMarketData.Controls.Add(this.txtLastPrice);
            this.gboInstrumentMarketData.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.gboInstrumentMarketData.Location = new System.Drawing.Point(232, 56);
            this.gboInstrumentMarketData.Name = "gboInstrumentMarketData";
            this.gboInstrumentMarketData.Size = new System.Drawing.Size(168, 136);
            this.gboInstrumentMarketData.TabIndex = 64;
            this.gboInstrumentMarketData.TabStop = false;
            this.gboInstrumentMarketData.Text = "Instrument Market Data";
            // 
            // lblAskPrice
            // 
            this.lblAskPrice.Location = new System.Drawing.Point(8, 48);
            this.lblAskPrice.Name = "lblAskPrice";
            this.lblAskPrice.Size = new System.Drawing.Size(64, 16);
            this.lblAskPrice.TabIndex = 46;
            this.lblAskPrice.Text = "Ask Price:";
            this.lblAskPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAskPrice
            // 
            this.txtAskPrice.Location = new System.Drawing.Point(80, 48);
            this.txtAskPrice.Name = "txtAskPrice";
            this.txtAskPrice.Size = new System.Drawing.Size(72, 20);
            this.txtAskPrice.TabIndex = 45;
            // 
            // txtBidPrice
            // 
            this.txtBidPrice.Location = new System.Drawing.Point(80, 24);
            this.txtBidPrice.Name = "txtBidPrice";
            this.txtBidPrice.Size = new System.Drawing.Size(72, 20);
            this.txtBidPrice.TabIndex = 41;
            // 
            // lblChange
            // 
            this.lblChange.Location = new System.Drawing.Point(8, 96);
            this.lblChange.Name = "lblChange";
            this.lblChange.Size = new System.Drawing.Size(64, 16);
            this.lblChange.TabIndex = 52;
            this.lblChange.Text = "Change:";
            this.lblChange.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtChange
            // 
            this.txtChange.Location = new System.Drawing.Point(80, 96);
            this.txtChange.Name = "txtChange";
            this.txtChange.Size = new System.Drawing.Size(72, 20);
            this.txtChange.TabIndex = 51;
            // 
            // lblBidPrice
            // 
            this.lblBidPrice.Location = new System.Drawing.Point(8, 24);
            this.lblBidPrice.Name = "lblBidPrice";
            this.lblBidPrice.Size = new System.Drawing.Size(64, 16);
            this.lblBidPrice.TabIndex = 42;
            this.lblBidPrice.Text = "Bid Price:";
            this.lblBidPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLastPrice
            // 
            this.lblLastPrice.Location = new System.Drawing.Point(8, 72);
            this.lblLastPrice.Name = "lblLastPrice";
            this.lblLastPrice.Size = new System.Drawing.Size(64, 16);
            this.lblLastPrice.TabIndex = 50;
            this.lblLastPrice.Text = "Last Price:";
            this.lblLastPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLastPrice
            // 
            this.txtLastPrice.Location = new System.Drawing.Point(80, 72);
            this.txtLastPrice.Name = "txtLastPrice";
            this.txtLastPrice.Size = new System.Drawing.Size(72, 20);
            this.txtLastPrice.TabIndex = 49;
            // 
            // gboOrderEntry
            // 
            this.gboOrderEntry.Controls.Add(this.label1);
            this.gboOrderEntry.Controls.Add(this.comboBoxOrderFeed);
            this.gboOrderEntry.Controls.Add(this.lblCustomer);
            this.gboOrderEntry.Controls.Add(this.cboCustomer);
            this.gboOrderEntry.Controls.Add(this.txtOrderBook);
            this.gboOrderEntry.Controls.Add(this.lblOrderType);
            this.gboOrderEntry.Controls.Add(this.btnSell);
            this.gboOrderEntry.Controls.Add(this.btnBuy);
            this.gboOrderEntry.Controls.Add(this.lblStopPrice);
            this.gboOrderEntry.Controls.Add(this.txtStopPrice);
            this.gboOrderEntry.Controls.Add(this.cboOrderType);
            this.gboOrderEntry.Controls.Add(this.lblQuantity);
            this.gboOrderEntry.Controls.Add(this.txtQuantity);
            this.gboOrderEntry.Controls.Add(this.lblPrice);
            this.gboOrderEntry.Controls.Add(this.txtPrice);
            this.gboOrderEntry.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.gboOrderEntry.Location = new System.Drawing.Point(8, 200);
            this.gboOrderEntry.Name = "gboOrderEntry";
            this.gboOrderEntry.Size = new System.Drawing.Size(392, 215);
            this.gboOrderEntry.TabIndex = 65;
            this.gboOrderEntry.TabStop = false;
            this.gboOrderEntry.Text = "Order Entry";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 21);
            this.label1.TabIndex = 49;
            this.label1.Text = "Order Feed:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxOrderFeed
            // 
            this.comboBoxOrderFeed.DisplayMember = "Name";
            this.comboBoxOrderFeed.Enabled = false;
            this.comboBoxOrderFeed.Items.AddRange(new object[] {
            "Market",
            "Limit",
            "Stop Market",
            "Stop Limit"});
            this.comboBoxOrderFeed.Location = new System.Drawing.Point(80, 51);
            this.comboBoxOrderFeed.Name = "comboBoxOrderFeed";
            this.comboBoxOrderFeed.Size = new System.Drawing.Size(88, 21);
            this.comboBoxOrderFeed.TabIndex = 48;
            // 
            // lblCustomer
            // 
            this.lblCustomer.Location = new System.Drawing.Point(8, 24);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(64, 16);
            this.lblCustomer.TabIndex = 47;
            this.lblCustomer.Text = "Customer:";
            this.lblCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboCustomer
            // 
            this.cboCustomer.DisplayMember = "Customer";
            this.cboCustomer.Enabled = false;
            this.cboCustomer.Location = new System.Drawing.Point(80, 24);
            this.cboCustomer.Name = "cboCustomer";
            this.cboCustomer.Size = new System.Drawing.Size(88, 21);
            this.cboCustomer.TabIndex = 46;
            // 
            // txtOrderBook
            // 
            this.txtOrderBook.Location = new System.Drawing.Point(184, 24);
            this.txtOrderBook.Multiline = true;
            this.txtOrderBook.Name = "txtOrderBook";
            this.txtOrderBook.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOrderBook.Size = new System.Drawing.Size(192, 181);
            this.txtOrderBook.TabIndex = 45;
            // 
            // lblOrderType
            // 
            this.lblOrderType.Location = new System.Drawing.Point(8, 78);
            this.lblOrderType.Name = "lblOrderType";
            this.lblOrderType.Size = new System.Drawing.Size(64, 16);
            this.lblOrderType.TabIndex = 44;
            this.lblOrderType.Text = "Order Type:";
            this.lblOrderType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSell
            // 
            this.btnSell.Enabled = false;
            this.btnSell.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSell.Location = new System.Drawing.Point(112, 182);
            this.btnSell.Name = "btnSell";
            this.btnSell.Size = new System.Drawing.Size(56, 23);
            this.btnSell.TabIndex = 43;
            this.btnSell.Text = "Sell";
            this.btnSell.Click += new System.EventHandler(this.SellButton_Click);
            // 
            // btnBuy
            // 
            this.btnBuy.Enabled = false;
            this.btnBuy.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnBuy.Location = new System.Drawing.Point(56, 182);
            this.btnBuy.Name = "btnBuy";
            this.btnBuy.Size = new System.Drawing.Size(56, 23);
            this.btnBuy.TabIndex = 42;
            this.btnBuy.Text = "Buy";
            this.btnBuy.Click += new System.EventHandler(this.BuyButton_Click);
            // 
            // lblStopPrice
            // 
            this.lblStopPrice.Location = new System.Drawing.Point(8, 150);
            this.lblStopPrice.Name = "lblStopPrice";
            this.lblStopPrice.Size = new System.Drawing.Size(64, 16);
            this.lblStopPrice.TabIndex = 41;
            this.lblStopPrice.Text = "Stop Price:";
            this.lblStopPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtStopPrice
            // 
            this.txtStopPrice.Enabled = false;
            this.txtStopPrice.Location = new System.Drawing.Point(80, 150);
            this.txtStopPrice.Name = "txtStopPrice";
            this.txtStopPrice.Size = new System.Drawing.Size(88, 20);
            this.txtStopPrice.TabIndex = 40;
            // 
            // cboOrderType
            // 
            this.cboOrderType.Enabled = false;
            this.cboOrderType.Items.AddRange(new object[] {
            "Market",
            "Limit",
            "Stop Market",
            "Stop Limit"});
            this.cboOrderType.Location = new System.Drawing.Point(80, 78);
            this.cboOrderType.Name = "cboOrderType";
            this.cboOrderType.Size = new System.Drawing.Size(88, 21);
            this.cboOrderType.TabIndex = 39;
            this.cboOrderType.SelectedIndexChanged += new System.EventHandler(this.orderTypeComboBox_SelectedIndexChanged);
            // 
            // lblQuantity
            // 
            this.lblQuantity.Location = new System.Drawing.Point(8, 126);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(64, 16);
            this.lblQuantity.TabIndex = 38;
            this.lblQuantity.Text = "Quantity:";
            this.lblQuantity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtQuantity
            // 
            this.txtQuantity.Enabled = false;
            this.txtQuantity.Location = new System.Drawing.Point(80, 126);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(88, 20);
            this.txtQuantity.TabIndex = 37;
            // 
            // lblPrice
            // 
            this.lblPrice.Location = new System.Drawing.Point(8, 102);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(64, 16);
            this.lblPrice.TabIndex = 36;
            this.lblPrice.Text = "Price:";
            this.lblPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPrice
            // 
            this.txtPrice.Enabled = false;
            this.txtPrice.Location = new System.Drawing.Point(80, 102);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(88, 20);
            this.txtPrice.TabIndex = 35;
            // 
            // lblNotProduction
            // 
            this.lblNotProduction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotProduction.Location = new System.Drawing.Point(8, 34);
            this.lblNotProduction.Name = "lblNotProduction";
            this.lblNotProduction.Size = new System.Drawing.Size(392, 14);
            this.lblNotProduction.TabIndex = 67;
            this.lblNotProduction.Text = "This sample is NOT to be used in production or during conformance testing.";
            this.lblNotProduction.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblWarning
            // 
            this.lblWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWarning.Location = new System.Drawing.Point(8, 9);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(392, 23);
            this.lblWarning.TabIndex = 66;
            this.lblWarning.Text = "WARNING!";
            this.lblWarning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmSubmitOrder
            // 
            this.AllowDrop = true;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(408, 442);
            this.Controls.Add(this.lblNotProduction);
            this.Controls.Add(this.lblWarning);
            this.Controls.Add(this.gboOrderEntry);
            this.Controls.Add(this.gboInstrumentMarketData);
            this.Controls.Add(this.gboInstrumentInfo);
            this.Controls.Add(this.sbaStatus);
            this.Enabled = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Menu = this.mainMenu1;
            this.Name = "frmSubmitOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SubmitOrder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSubmitOrder_FormClosing);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.frmSubmitOrder_DragDrop);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.frmSubmitOrder_DragOver);
            this.gboInstrumentInfo.ResumeLayout(false);
            this.gboInstrumentInfo.PerformLayout();
            this.gboInstrumentMarketData.ResumeLayout(false);
            this.gboInstrumentMarketData.PerformLayout();
            this.gboOrderEntry.ResumeLayout(false);
            this.gboOrderEntry.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() 
        {
            // Confirm TTAPI installation archetecture.
            AboutDTS.TTAPIArchitectureCheck();

            // Enable Visual Styles for XP Look and Feel.
            Application.EnableVisualStyles();
            Application.Run(new frmSubmitOrder());
        }

        #region Startup and Shutdown

        /// <summary>
        /// Start TT API and attach the dispatcher to this thread.
        /// </summary>
        private void startTTAPI()
        {
            m_dispatcher = Dispatcher.AttachUIDispatcher();

            // Create an instance of TTAPI.
            TTAPI.XTraderModeDelegate xtDelegate = new TTAPI.XTraderModeDelegate(initTTAPI);
            TTAPI.CreateXTraderModeTTAPI(m_dispatcher, xtDelegate);
        }

        /// <summary>
        /// Init and start TT API.
        /// Create a customer defaults scbscription.
        /// </summary>
        public void initTTAPI(XTraderModeTTAPI apiInstance, Exception ex)
        {
            m_TTAPI = apiInstance;
            m_TTAPI.ConnectionStatusUpdate += new EventHandler<ConnectionStatusUpdateEventArgs>(m_TTAPI_ConnectionStatusUpdate);
            m_TTAPI.ConnectToXTrader();

            m_customerDefaultsSubscription = new CustomerDefaultsSubscription(m_TTAPI.Session, m_dispatcher);
            m_customerDefaultsSubscription.CustomerDefaultsChanged += new EventHandler(m_customerDefaultsSubscription_CustomerDefaultsChanged);
            m_customerDefaultsSubscription.Start();
        }

        /// <summary>
        /// ConnectionStatusUpdate
        /// Enable the form when successfully connected to X_Trader.
        /// If an error occurs display it in the status bar.
        /// </summary>
        void m_TTAPI_ConnectionStatusUpdate(object sender, ConnectionStatusUpdateEventArgs e)
        {
            if (e.Status.IsSuccess)
            {
                Enabled = true;
                UpdateStatusBar("Drag and Drop an instrument from the Market Grid in X_TRADER to this window.");
            }
            else
            {
                UpdateStatusBar(String.Format("ConnectionStatusUpdate: {0}", e.Status.StatusMessage));
            }
        }

        /// <summary>
        /// Window form closing event.
        /// Shutdown TT API and the dispatcher before exiting the application.
        /// </summary>
        private void frmSubmitOrder_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_TTAPI != null)
            {
                m_TTAPI.Shutdown();
            }
            m_TTAPI = null;
            m_dispatcher.Dispose();
            m_dispatcher = null;
        }
        #endregion

        #region Misc 

        /// <summary>
        /// populate the OrderFeed drop down menu.
        /// </summary>
        /// <remarks>
        /// comboBoxOrderFeed DisplayMember is set to Name to display the OrderFeed's Name property.
        /// </remarks>
        /// <param name="instrument">Instrument to find valid order feeds.</param>
        private void populateOrderFeedDropDown(Instrument instrument)
        {
            comboBoxOrderFeed.Items.Clear();
            foreach (OrderFeed orderFeed in instrument.GetValidOrderFeeds())
            {
                comboBoxOrderFeed.Items.Add(orderFeed);
            }
        }

        /// <summary>
        /// CustomerDefaultsChanged subscription callback.
        /// Update the Customer combo box.
        /// </summary>
        void m_customerDefaultsSubscription_CustomerDefaultsChanged(object sender, EventArgs e)
        {
            cboCustomer.Items.Clear();

            CustomerDefaultsSubscription cds = sender as CustomerDefaultsSubscription;
            foreach (CustomerDefaultEntry entry in cds.CustomerDefaults)
            {
                cboCustomer.Items.Add(entry);
            }
        }

        /// <summary>
        /// This function enables and disables the appropriate
        /// text boxes on the user interface.
        /// </summary>
        /// <param name="sender">Object which fires the method</param>
        /// <param name="e">Event arguments of the callback</param>
        private void orderTypeComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cboOrderType.SelectedIndex == 2 || cboOrderType.SelectedIndex == 3)
            {
                // Enable the stop price text box if the selected order type is stop limit or stop market.
                txtStopPrice.Enabled = true;
            }
            else
            {
                // Clear and disable the stop price text box if not stop limit or stop market.
                txtStopPrice.Clear();
                txtStopPrice.Enabled = false;
            }

            if (cboOrderType.SelectedIndex == 0 || cboOrderType.SelectedIndex == 2)
            {
                // Clear and disable the price text box if the selected order type is market or stop market.
                txtPrice.Clear();
                txtPrice.Enabled = false;
            }
            else
            {
                // Enable the price text box if the if the selected order type is limit or stop limit.
                txtPrice.Enabled = true;
            }
        }

        #endregion

        #region SendOrder

        /// <summary>
        /// This function is called when the user clicks the buy button.
        /// </summary>
        /// <param name="sender">Object which fires the method</param>
        /// <param name="e">Event arguments of the callback</param>
        private void BuyButton_Click(object sender, System.EventArgs e)
        {
            // Call the SendOrder function with a Buy request.
            SendOrder(BuySell.Buy);
        }

        /// <summary>
        /// This function is called when the user clicks the sell button.
        /// </summary>
        /// <param name="sender">Object which fires the method</param>
        /// <param name="e">Event arguments of the callback</param>
        private void SellButton_Click(object sender, System.EventArgs e)
        {
            // Call the SendOrder function with a Sell request.
            SendOrder(BuySell.Sell);
        }

        /// <summary>
        /// This function sets up the OrderProfile and submits
        /// the order using the InstrumentTradeSubscription SendOrder method.
        /// </summary>
        /// <param name="buySell">The side of the market to place the order on.</param>
        private void SendOrder(BuySell buySell)
        {
            try
            {
                OrderFeed orderFeed = comboBoxOrderFeed.SelectedItem as OrderFeed;
                CustomerDefaultEntry customer = cboCustomer.SelectedItem as CustomerDefaultEntry;

                OrderProfile orderProfile = new OrderProfile(orderFeed, m_instrumentTradeSubscription.Instrument, customer.Customer);

                // Set for Buy or Sell.
                orderProfile.BuySell = buySell;

                // Set the quantity.
                orderProfile.QuantityToWork = Quantity.FromString(m_instrumentTradeSubscription.Instrument, txtQuantity.Text);

                // Determine which Order Type is selected.
                if (cboOrderType.SelectedIndex == 0)  // Market Order
                {
                    // Set the order type to "Market" for a market order.
                    orderProfile.OrderType = OrderType.Market;
                }
                else if (cboOrderType.SelectedIndex == 1)  // Limit Order
                {
                    // Set the order type to "Limit" for a limit order.
                    orderProfile.OrderType = OrderType.Limit;
                    // Set the limit order price.
                    orderProfile.LimitPrice = Price.FromString(m_instrumentTradeSubscription.Instrument, txtPrice.Text);
                }
                else if (cboOrderType.SelectedIndex == 2)  // Stop Market Order
                {
                    // Set the order type to "Market" for a market order.
                    orderProfile.OrderType = OrderType.Market;
                    // Set the order modifiers to "Stop" for a stop order.
                    orderProfile.Modifiers = OrderModifiers.Stop;
                    // Set the stop price.
                    orderProfile.StopPrice = Price.FromString(m_instrumentTradeSubscription.Instrument, txtStopPrice.Text);
                }
                else if (cboOrderType.SelectedIndex == 3)  // Stop Limit Order
                {
                    // Set the order type to "Limit" for a limit order.
                    orderProfile.OrderType = OrderType.Limit;
                    // Set the order modifiers to "Stop" for a stop order.
                    orderProfile.Modifiers = OrderModifiers.Stop;
                    // Set the limit order price.
                    orderProfile.LimitPrice = Price.FromString(m_instrumentTradeSubscription.Instrument, txtPrice.Text);
                    // Set the stop price.
                    orderProfile.StopPrice = Price.FromString(m_instrumentTradeSubscription.Instrument, txtStopPrice.Text);
                }

                // Send the order.
                m_instrumentTradeSubscription.SendOrder(orderProfile);

                // Update the GUI.
                txtOrderBook.Text += String.Format("Send {0} {1}|{2}@{3}{4}",
                    orderProfile.SiteOrderKey,
                    orderProfile.BuySell.ToString(),
                    orderProfile.QuantityToWork.ToString(),
                    orderProfile.OrderType == OrderType.Limit ? orderProfile.LimitPrice.ToString() : "Market Price",
                    System.Environment.NewLine);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        #endregion

        #region Update Prices

        /// <summary>
        /// PriceSubscription FieldsUpdated event.
        /// </summary>
        void m_priceSubscription_FieldsUpdated(object sender, FieldsUpdatedEventArgs e)
        {
            if (e.Error == null)
            {
                if (e.UpdateType == UpdateType.Snapshot)
                {
                    updatePrices(e.Fields);
                }
                else if (e.UpdateType == UpdateType.Update)
                {
                    updatePrices(e.Fields);
                }
            }
            else
            {
                Console.WriteLine(String.Format("PriceSubscription FieldsUpdated Error: {0}", e.Error.Message));
            }
        }

        /// <summary>
        /// Update the price information.
        /// </summary>
        /// <param name="fields">PriceSubscriptionFields</param>
        private void updatePrices(PriceSubscriptionFields fields)
        {
            txtBidPrice.Text = fields.GetBestBidPriceField().Value.ToString();
            txtAskPrice.Text = fields.GetBestAskPriceField().Value.ToString();
            txtLastPrice.Text = fields.GetLastTradedPriceField().Value.ToString();
            txtChange.Text = fields.GetNetChangeField().Value.ToString();
        }

        #endregion

        #region Update GUI for TradeSubscription events.

        /// <summary>
        /// OrderRejected InstrumentTradeSubscription callback.
        /// </summary>
        /// <param name="sender">Sender (InstrumentTradeSubscription)</param>
        /// <param name="e">OrderRejectedEventArgs</param>
        void m_instrumentTradeSubscription_OrderRejected(object sender, OrderRejectedEventArgs e)
        {
            txtOrderBook.Text += String.Format("Rejected {0} {1}{2}",
                e.Order.SiteOrderKey,
                e.Message,
                System.Environment.NewLine);
        }

        /// <summary>
        /// OrderAdded InstrumentTradeSubscription callback.
        /// </summary>
        /// <param name="sender">Sender (InstrumentTradeSubscription)</param>
        /// <param name="e">OrderAddedEventArgs</param>
        void m_instrumentTradeSubscription_OrderAdded(object sender, OrderAddedEventArgs e)
        {
            txtOrderBook.Text += String.Format("Added {0} {1}|{2}@{3}{4}",
                e.Order.SiteOrderKey,
                e.Order.BuySell.ToString(),
                e.Order.OrderQuantity.ToString(),
                e.Order.OrderType == OrderType.Limit ? e.Order.LimitPrice.ToString() : "Market Price",
                System.Environment.NewLine);
        }
        #endregion

        #region FindInstrument
        /// <summary>
        /// Function to find a list of InstrumentKeys.
        /// </summary>
        /// <param name="keys">List of InstrumentKeys.</param>
        public void FindInstrument(IList<InstrumentKey> keys)
        {
            foreach (InstrumentKey key in keys)
            {
                // Update the Status Bar text.
                UpdateStatusBar(String.Format("TT API FindInstrument {0}", key.ToString()));
                
                InstrumentLookupSubscription instrRequest = new InstrumentLookupSubscription(m_TTAPI.Session, m_dispatcher, key);
                instrRequest.Update += instrRequest_Completed;
                instrRequest.Tag = key.ToString();
                instrRequest.Start();
                
                // Only allow the first instrument.
                break;
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
                    UpdateStatusBar(String.Format("TT API FindInstrument {0}", e.Instrument.Name));
                    instrumentFound(e.Instrument);
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

        /// <summary>
        /// Create subscriptions and update the GUI.
        /// </summary>
        /// <param name="instrument">Instrument to create subscriptions with.</param>
        private void instrumentFound(Instrument instrument)
        {
            txtExchange.Text = instrument.Key.MarketKey.Name;
            txtProduct.Text = instrument.Key.ProductKey.Name;
            txtProductType.Text = instrument.Key.ProductKey.Type.Name;
            txtContract.Text = instrument.Name;

            m_priceSubscription = new PriceSubscription(instrument, m_dispatcher);
            m_priceSubscription.FieldsUpdated += new FieldsUpdatedEventHandler(m_priceSubscription_FieldsUpdated);
            m_priceSubscription.Start();

            m_instrumentTradeSubscription = new InstrumentTradeSubscription(m_TTAPI.Session, m_dispatcher, instrument);
            m_instrumentTradeSubscription.OrderAdded += new EventHandler<OrderAddedEventArgs>(m_instrumentTradeSubscription_OrderAdded);
            m_instrumentTradeSubscription.OrderRejected += new EventHandler<OrderRejectedEventArgs>(m_instrumentTradeSubscription_OrderRejected);
            m_instrumentTradeSubscription.Start();

            populateOrderFeedDropDown(instrument);

            // Enable the user interface items.
            txtQuantity.Enabled = true;
            cboOrderType.Enabled = true;
            comboBoxOrderFeed.Enabled = true;
            cboCustomer.Enabled = true;
            btnBuy.Enabled = true;
            btnSell.Enabled = true;
        }
        #endregion

        #region Drag and Drop
        /// <summary>
        /// Form drag and drop event handler.
        /// The form must enable "AllowDrop" for these events to fire.
        /// </summary>
        private void frmSubmitOrder_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.HasInstrumentKeys())
            {
                FindInstrument(e.Data.GetInstrumentKeys());
            }
        }

        /// <summary>
        /// Form drag over event handler.
        /// The form must enable "AllowDrop" for these events to fire.
        /// </summary>
        private void frmSubmitOrder_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.HasInstrumentKeys())
            {
                e.Effect = DragDropEffects.Copy;
            }
        }
        #endregion

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
        /// Display the About dialog box.
        /// </summary>
        /// <param name="sender">Object which fires the method</param>
        /// <param name="e">Event arguments of the callback</param>
        private void AboutMenuItem_Click(object sender, System.EventArgs e)
        {
            AboutDTS aboutForm = new AboutDTS();
            aboutForm.ShowDialog(this);
        }
    }
}