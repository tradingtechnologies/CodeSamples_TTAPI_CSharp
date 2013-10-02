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
	/// ModifyOrder
    /// 
    /// This example demonstrates using the TT API to modify an order.  Modifications 
    /// include change, cancel/replace, delete last order, delete all orders and delete
    /// a specified range of orders.
    /// 
    /// Note:	Deleting all orders can include orders placed outside of the TT API 
    /// 		application. 
	/// </summary>
	public class frmModifyOrder : Form
    {
        // Declare private TTAPI member variables.
        private XTraderModeTTAPI m_TTAPI = null;
        private CustomerDefaultsSubscription m_customerDefaultsSubscription = null;
        private InstrumentTradeSubscription m_instrumentTradeSubscription = null;
        private bool m_isShutdown = false, m_shutdownInProcess = false;
        
        // Class member variables
		private string m_LastOrderSiteOrderKey = String.Empty;
        private Order m_lastOrder = null;

		public frmModifyOrder()
		{
			// Required for Windows Form Designer support
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

                m_customerDefaultsSubscription = new CustomerDefaultsSubscription(m_TTAPI.Session, Dispatcher.Current);
                m_customerDefaultsSubscription.CustomerDefaultsChanged += new EventHandler(m_customerDefaultsSubscription_CustomerDefaultsChanged);
                m_customerDefaultsSubscription.Start();
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
                if (m_customerDefaultsSubscription != null)
                {
                    m_customerDefaultsSubscription.CustomerDefaultsChanged -= m_customerDefaultsSubscription_CustomerDefaultsChanged;
                    m_customerDefaultsSubscription.Dispose();
                    m_customerDefaultsSubscription = null;
                }

                if (m_instrumentTradeSubscription != null)
                {
                    m_instrumentTradeSubscription.OrderAdded -= m_instrumentTradeSubscription_OrderAdded;
                    m_instrumentTradeSubscription.OrderRejected -= m_instrumentTradeSubscription_OrderRejected;
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

		#region Windows Form Designer generated code

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

        private IContainer components;
        private Button buttonDeleteAll;
        private Button buttonDeleteLast;
        private Button buttonDeleteBuy;
        private Button buttonDeleteSell;
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
        private System.Windows.Forms.GroupBox gboLimtOrderEntry;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.ComboBox cboCustomer;
        private System.Windows.Forms.Button btnSell;
        private System.Windows.Forms.Button btnBuy;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label lblSiteOrderKey;
        private System.Windows.Forms.TextBox txtSiteOrderKey;
        private System.Windows.Forms.GroupBox gboLastOrder;
        private System.Windows.Forms.Label lblNotProduction;
        private System.Windows.Forms.Label lblWarning;
        private System.Windows.Forms.Label lblNewQuantity;
        private System.Windows.Forms.Label lblNewPrice;
        private System.Windows.Forms.GroupBox gboDeleteOrder;
        private System.Windows.Forms.Button btnInvokeModify;
        private System.Windows.Forms.ComboBox cboModifyType;
        private System.Windows.Forms.TextBox txtNewQuantity;
        private System.Windows.Forms.TextBox txtNewPrice;
        private System.Windows.Forms.GroupBox gboModifyOrder;

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
            this.gboLimtOrderEntry = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxOrderFeed = new System.Windows.Forms.ComboBox();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.cboCustomer = new System.Windows.Forms.ComboBox();
            this.btnSell = new System.Windows.Forms.Button();
            this.btnBuy = new System.Windows.Forms.Button();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.lblPrice = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.gboDeleteOrder = new System.Windows.Forms.GroupBox();
            this.buttonDeleteAll = new System.Windows.Forms.Button();
            this.buttonDeleteLast = new System.Windows.Forms.Button();
            this.buttonDeleteBuy = new System.Windows.Forms.Button();
            this.buttonDeleteSell = new System.Windows.Forms.Button();
            this.lblSiteOrderKey = new System.Windows.Forms.Label();
            this.txtSiteOrderKey = new System.Windows.Forms.TextBox();
            this.gboModifyOrder = new System.Windows.Forms.GroupBox();
            this.txtNewQuantity = new System.Windows.Forms.TextBox();
            this.lblNewQuantity = new System.Windows.Forms.Label();
            this.txtNewPrice = new System.Windows.Forms.TextBox();
            this.lblNewPrice = new System.Windows.Forms.Label();
            this.cboModifyType = new System.Windows.Forms.ComboBox();
            this.btnInvokeModify = new System.Windows.Forms.Button();
            this.gboLastOrder = new System.Windows.Forms.GroupBox();
            this.lblNotProduction = new System.Windows.Forms.Label();
            this.lblWarning = new System.Windows.Forms.Label();
            this.gboInstrumentInfo.SuspendLayout();
            this.gboLimtOrderEntry.SuspendLayout();
            this.gboDeleteOrder.SuspendLayout();
            this.gboModifyOrder.SuspendLayout();
            this.gboLastOrder.SuspendLayout();
            this.SuspendLayout();
            // 
            // sbaStatus
            // 
            this.sbaStatus.Location = new System.Drawing.Point(0, 449);
            this.sbaStatus.Name = "sbaStatus";
            this.sbaStatus.Size = new System.Drawing.Size(426, 22);
            this.sbaStatus.SizingGrip = false;
            this.sbaStatus.TabIndex = 63;
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
            this.gboInstrumentInfo.Location = new System.Drawing.Point(8, 57);
            this.gboInstrumentInfo.Name = "gboInstrumentInfo";
            this.gboInstrumentInfo.Size = new System.Drawing.Size(216, 167);
            this.gboInstrumentInfo.TabIndex = 64;
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
            // gboLimtOrderEntry
            // 
            this.gboLimtOrderEntry.Controls.Add(this.label1);
            this.gboLimtOrderEntry.Controls.Add(this.comboBoxOrderFeed);
            this.gboLimtOrderEntry.Controls.Add(this.lblCustomer);
            this.gboLimtOrderEntry.Controls.Add(this.cboCustomer);
            this.gboLimtOrderEntry.Controls.Add(this.btnSell);
            this.gboLimtOrderEntry.Controls.Add(this.btnBuy);
            this.gboLimtOrderEntry.Controls.Add(this.lblQuantity);
            this.gboLimtOrderEntry.Controls.Add(this.txtQuantity);
            this.gboLimtOrderEntry.Controls.Add(this.lblPrice);
            this.gboLimtOrderEntry.Controls.Add(this.txtPrice);
            this.gboLimtOrderEntry.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.gboLimtOrderEntry.Location = new System.Drawing.Point(232, 57);
            this.gboLimtOrderEntry.Name = "gboLimtOrderEntry";
            this.gboLimtOrderEntry.Size = new System.Drawing.Size(184, 167);
            this.gboLimtOrderEntry.TabIndex = 66;
            this.gboLimtOrderEntry.TabStop = false;
            this.gboLimtOrderEntry.Text = "Limit Order Entry";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 21);
            this.label1.TabIndex = 51;
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
            this.comboBoxOrderFeed.TabIndex = 50;
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
            // btnSell
            // 
            this.btnSell.Enabled = false;
            this.btnSell.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSell.Location = new System.Drawing.Point(112, 134);
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
            this.btnBuy.Location = new System.Drawing.Point(56, 134);
            this.btnBuy.Name = "btnBuy";
            this.btnBuy.Size = new System.Drawing.Size(56, 23);
            this.btnBuy.TabIndex = 42;
            this.btnBuy.Text = "Buy";
            this.btnBuy.Click += new System.EventHandler(this.BuyButton_Click);
            // 
            // lblQuantity
            // 
            this.lblQuantity.Location = new System.Drawing.Point(8, 102);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(64, 16);
            this.lblQuantity.TabIndex = 38;
            this.lblQuantity.Text = "Quantity:";
            this.lblQuantity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(80, 102);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(88, 20);
            this.txtQuantity.TabIndex = 37;
            // 
            // lblPrice
            // 
            this.lblPrice.Location = new System.Drawing.Point(8, 78);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(64, 16);
            this.lblPrice.TabIndex = 36;
            this.lblPrice.Text = "Price:";
            this.lblPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(80, 78);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(88, 20);
            this.txtPrice.TabIndex = 35;
            // 
            // gboDeleteOrder
            // 
            this.gboDeleteOrder.Controls.Add(this.buttonDeleteAll);
            this.gboDeleteOrder.Controls.Add(this.buttonDeleteLast);
            this.gboDeleteOrder.Controls.Add(this.buttonDeleteBuy);
            this.gboDeleteOrder.Controls.Add(this.buttonDeleteSell);
            this.gboDeleteOrder.Enabled = false;
            this.gboDeleteOrder.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.gboDeleteOrder.Location = new System.Drawing.Point(232, 294);
            this.gboDeleteOrder.Name = "gboDeleteOrder";
            this.gboDeleteOrder.Size = new System.Drawing.Size(184, 146);
            this.gboDeleteOrder.TabIndex = 67;
            this.gboDeleteOrder.TabStop = false;
            this.gboDeleteOrder.Text = "Delete Orders";
            // 
            // buttonDeleteAll
            // 
            this.buttonDeleteAll.Location = new System.Drawing.Point(10, 80);
            this.buttonDeleteAll.Name = "buttonDeleteAll";
            this.buttonDeleteAll.Size = new System.Drawing.Size(166, 23);
            this.buttonDeleteAll.TabIndex = 0;
            this.buttonDeleteAll.Text = "Delete All";
            this.buttonDeleteAll.UseVisualStyleBackColor = true;
            this.buttonDeleteAll.Click += new System.EventHandler(this.buttonDeleteAll_Click);
            // 
            // buttonDeleteLast
            // 
            this.buttonDeleteLast.Enabled = false;
            this.buttonDeleteLast.Location = new System.Drawing.Point(11, 22);
            this.buttonDeleteLast.Name = "buttonDeleteLast";
            this.buttonDeleteLast.Size = new System.Drawing.Size(165, 23);
            this.buttonDeleteLast.TabIndex = 0;
            this.buttonDeleteLast.Text = "Delete Last Order";
            this.buttonDeleteLast.UseVisualStyleBackColor = true;
            this.buttonDeleteLast.Click += new System.EventHandler(this.buttonDeleteLast_Click);
            // 
            // buttonDeleteBuy
            // 
            this.buttonDeleteBuy.Location = new System.Drawing.Point(10, 51);
            this.buttonDeleteBuy.Name = "buttonDeleteBuy";
            this.buttonDeleteBuy.Size = new System.Drawing.Size(80, 23);
            this.buttonDeleteBuy.TabIndex = 0;
            this.buttonDeleteBuy.Text = "Delete Buy";
            this.buttonDeleteBuy.UseVisualStyleBackColor = true;
            this.buttonDeleteBuy.Click += new System.EventHandler(this.buttonDeleteBuy_Click);
            // 
            // buttonDeleteSell
            // 
            this.buttonDeleteSell.Location = new System.Drawing.Point(96, 51);
            this.buttonDeleteSell.Name = "buttonDeleteSell";
            this.buttonDeleteSell.Size = new System.Drawing.Size(80, 23);
            this.buttonDeleteSell.TabIndex = 0;
            this.buttonDeleteSell.Text = "Delete Sell";
            this.buttonDeleteSell.UseVisualStyleBackColor = true;
            this.buttonDeleteSell.Click += new System.EventHandler(this.buttonDeleteSell_Click);
            // 
            // lblSiteOrderKey
            // 
            this.lblSiteOrderKey.Location = new System.Drawing.Point(8, 27);
            this.lblSiteOrderKey.Name = "lblSiteOrderKey";
            this.lblSiteOrderKey.Size = new System.Drawing.Size(80, 14);
            this.lblSiteOrderKey.TabIndex = 36;
            this.lblSiteOrderKey.Text = "SiteOrderKey:";
            this.lblSiteOrderKey.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSiteOrderKey
            // 
            this.txtSiteOrderKey.Location = new System.Drawing.Point(96, 24);
            this.txtSiteOrderKey.Name = "txtSiteOrderKey";
            this.txtSiteOrderKey.Size = new System.Drawing.Size(104, 20);
            this.txtSiteOrderKey.TabIndex = 35;
            // 
            // gboModifyOrder
            // 
            this.gboModifyOrder.Controls.Add(this.txtNewQuantity);
            this.gboModifyOrder.Controls.Add(this.lblNewQuantity);
            this.gboModifyOrder.Controls.Add(this.txtNewPrice);
            this.gboModifyOrder.Controls.Add(this.lblNewPrice);
            this.gboModifyOrder.Controls.Add(this.cboModifyType);
            this.gboModifyOrder.Controls.Add(this.btnInvokeModify);
            this.gboModifyOrder.Enabled = false;
            this.gboModifyOrder.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.gboModifyOrder.Location = new System.Drawing.Point(8, 294);
            this.gboModifyOrder.Name = "gboModifyOrder";
            this.gboModifyOrder.Size = new System.Drawing.Size(216, 146);
            this.gboModifyOrder.TabIndex = 68;
            this.gboModifyOrder.TabStop = false;
            this.gboModifyOrder.Text = "Modify Order";
            // 
            // txtNewQuantity
            // 
            this.txtNewQuantity.Location = new System.Drawing.Point(120, 80);
            this.txtNewQuantity.Name = "txtNewQuantity";
            this.txtNewQuantity.Size = new System.Drawing.Size(72, 20);
            this.txtNewQuantity.TabIndex = 52;
            // 
            // lblNewQuantity
            // 
            this.lblNewQuantity.Location = new System.Drawing.Point(24, 80);
            this.lblNewQuantity.Name = "lblNewQuantity";
            this.lblNewQuantity.Size = new System.Drawing.Size(88, 16);
            this.lblNewQuantity.TabIndex = 53;
            this.lblNewQuantity.Text = "New Quantity:";
            this.lblNewQuantity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNewPrice
            // 
            this.txtNewPrice.Location = new System.Drawing.Point(120, 56);
            this.txtNewPrice.Name = "txtNewPrice";
            this.txtNewPrice.Size = new System.Drawing.Size(72, 20);
            this.txtNewPrice.TabIndex = 50;
            // 
            // lblNewPrice
            // 
            this.lblNewPrice.Location = new System.Drawing.Point(48, 56);
            this.lblNewPrice.Name = "lblNewPrice";
            this.lblNewPrice.Size = new System.Drawing.Size(64, 16);
            this.lblNewPrice.TabIndex = 51;
            this.lblNewPrice.Text = "New Price:";
            this.lblNewPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboModifyType
            // 
            this.cboModifyType.Items.AddRange(new object[] {
            "Change Last Order",
            "Cancel/Replace Last Order"});
            this.cboModifyType.Location = new System.Drawing.Point(24, 24);
            this.cboModifyType.Name = "cboModifyType";
            this.cboModifyType.Size = new System.Drawing.Size(168, 21);
            this.cboModifyType.TabIndex = 47;
            this.cboModifyType.Text = "Select Modify Type";
            // 
            // btnInvokeModify
            // 
            this.btnInvokeModify.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnInvokeModify.Location = new System.Drawing.Point(120, 112);
            this.btnInvokeModify.Name = "btnInvokeModify";
            this.btnInvokeModify.Size = new System.Drawing.Size(72, 23);
            this.btnInvokeModify.TabIndex = 37;
            this.btnInvokeModify.Text = "Invoke";
            this.btnInvokeModify.Click += new System.EventHandler(this.btnInvokeModify_Click);
            // 
            // gboLastOrder
            // 
            this.gboLastOrder.Controls.Add(this.lblSiteOrderKey);
            this.gboLastOrder.Controls.Add(this.txtSiteOrderKey);
            this.gboLastOrder.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.gboLastOrder.Location = new System.Drawing.Point(8, 230);
            this.gboLastOrder.Name = "gboLastOrder";
            this.gboLastOrder.Size = new System.Drawing.Size(216, 56);
            this.gboLastOrder.TabIndex = 69;
            this.gboLastOrder.TabStop = false;
            this.gboLastOrder.Text = "Last Order";
            // 
            // lblNotProduction
            // 
            this.lblNotProduction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotProduction.Location = new System.Drawing.Point(8, 34);
            this.lblNotProduction.Name = "lblNotProduction";
            this.lblNotProduction.Size = new System.Drawing.Size(408, 14);
            this.lblNotProduction.TabIndex = 71;
            this.lblNotProduction.Text = "This sample is NOT to be used in production or during conformance testing.";
            this.lblNotProduction.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblWarning
            // 
            this.lblWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWarning.Location = new System.Drawing.Point(8, 9);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(408, 23);
            this.lblWarning.TabIndex = 70;
            this.lblWarning.Text = "WARNING!";
            this.lblWarning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmModifyOrder
            // 
            this.AllowDrop = true;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(426, 471);
            this.Controls.Add(this.lblNotProduction);
            this.Controls.Add(this.lblWarning);
            this.Controls.Add(this.gboLastOrder);
            this.Controls.Add(this.gboDeleteOrder);
            this.Controls.Add(this.gboLimtOrderEntry);
            this.Controls.Add(this.gboInstrumentInfo);
            this.Controls.Add(this.sbaStatus);
            this.Controls.Add(this.gboModifyOrder);
            this.Enabled = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Menu = this.mainMenu1;
            this.Name = "frmModifyOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ModifyOrder";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.frmModifyOrder_DragDrop);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.frmModifyOrder_DragOver);
            this.gboInstrumentInfo.ResumeLayout(false);
            this.gboInstrumentInfo.PerformLayout();
            this.gboLimtOrderEntry.ResumeLayout(false);
            this.gboLimtOrderEntry.PerformLayout();
            this.gboDeleteOrder.ResumeLayout(false);
            this.gboModifyOrder.ResumeLayout(false);
            this.gboModifyOrder.PerformLayout();
            this.gboLastOrder.ResumeLayout(false);
            this.gboLastOrder.PerformLayout();
            this.ResumeLayout(false);

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

                // Set the order type to "Limit" for a limit order.
                orderProfile.OrderType = OrderType.Limit;
                // Set the limit order price.
                orderProfile.LimitPrice = Price.FromString(m_instrumentTradeSubscription.Instrument, txtPrice.Text);

                // Send the order.
                m_instrumentTradeSubscription.SendOrder(orderProfile);

                m_LastOrderSiteOrderKey = orderProfile.SiteOrderKey;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
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
            if (e.Order.SiteOrderKey == m_LastOrderSiteOrderKey)
            {
                MessageBox.Show(String.Format("Rejected {0} {1}",
                    e.Order.SiteOrderKey,
                    e.Message));
                gboModifyOrder.Enabled = false;
                buttonDeleteLast.Enabled = false;
            }
        }

        /// <summary>
        /// OrderAdded InstrumentTradeSubscription callback.
        /// </summary>
        /// <param name="sender">Sender (InstrumentTradeSubscription)</param>
        /// <param name="e">OrderAddedEventArgs</param>
        void m_instrumentTradeSubscription_OrderAdded(object sender, OrderAddedEventArgs e)
        {
            if (e.Order.SiteOrderKey == m_LastOrderSiteOrderKey)
            {
                txtSiteOrderKey.Text = e.Order.SiteOrderKey;
                m_lastOrder = e.Order;
                gboModifyOrder.Enabled = true;
                buttonDeleteLast.Enabled = true;
            }
        }

        /// <summary>
        /// OrderFilledEventArgs InstrumentTradeSubscription callback.
        /// </summary>
        /// <param name="sender">Sender (InstrumentTradeSubscription)</param>
        /// <param name="e">OrderFilledEventArgs</param>
        void m_instrumentTradeSubscription_OrderFilled(object sender, OrderFilledEventArgs e)
        {
            if (e.NewOrder.SiteOrderKey == m_LastOrderSiteOrderKey)
            {
                if (e.FillType == FillType.Full)
                {
                    txtSiteOrderKey.Text = String.Empty;
                    m_lastOrder = null;
                    gboModifyOrder.Enabled = false;
                    buttonDeleteLast.Enabled = false;
                }
                else
                {
                    m_lastOrder = e.NewOrder;
                }
            }
        }

        /// <summary>
        /// OrderDeleted InstrumentTradeSubscription callback.
        /// </summary>
        /// <param name="sender">Sender (InstrumentTradeSubscription)</param>
        /// <param name="e">OrderDeletedEventArgs</param>
        void m_instrumentTradeSubscription_OrderDeleted(object sender, OrderDeletedEventArgs e)
        {
            if (e.DeletedUpdate.SiteOrderKey == m_LastOrderSiteOrderKey)
            {
                txtSiteOrderKey.Text = String.Empty;
                m_lastOrder = null;
                gboModifyOrder.Enabled = false;
                buttonDeleteLast.Enabled = false;
            }
        }

        /// <summary>
        /// OrderUpdated InstrumentTradeSubscription callback.
        /// </summary>
        /// <param name="sender">Sender (InstrumentTradeSubscription)</param>
        /// <param name="e">OrderUpdatedEventArgs</param>
        void m_instrumentTradeSubscription_OrderUpdated(object sender, OrderUpdatedEventArgs e)
        {
            if (e.NewOrder.SiteOrderKey == m_LastOrderSiteOrderKey)
            {
                m_lastOrder = e.NewOrder;
            }
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

                InstrumentLookupSubscription instrRequest = new InstrumentLookupSubscription(m_TTAPI.Session, Dispatcher.Current, key);
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

            m_instrumentTradeSubscription = new InstrumentTradeSubscription(m_TTAPI.Session, Dispatcher.Current, instrument);
            m_instrumentTradeSubscription.OrderAdded += new EventHandler<OrderAddedEventArgs>(m_instrumentTradeSubscription_OrderAdded);
            m_instrumentTradeSubscription.OrderRejected += new EventHandler<OrderRejectedEventArgs>(m_instrumentTradeSubscription_OrderRejected);
            m_instrumentTradeSubscription.OrderUpdated += new EventHandler<OrderUpdatedEventArgs>(m_instrumentTradeSubscription_OrderUpdated);
            m_instrumentTradeSubscription.OrderDeleted += new EventHandler<OrderDeletedEventArgs>(m_instrumentTradeSubscription_OrderDeleted);
            m_instrumentTradeSubscription.OrderFilled += new EventHandler<OrderFilledEventArgs>(m_instrumentTradeSubscription_OrderFilled);
            m_instrumentTradeSubscription.Start();

            populateOrderFeedDropDown(instrument);

            // Enable the user interface items.
            txtQuantity.Enabled = true;
            comboBoxOrderFeed.Enabled = true;
            cboCustomer.Enabled = true;
            btnBuy.Enabled = true;
            btnSell.Enabled = true;
            gboDeleteOrder.Enabled = true;
        }

        #endregion

        #region Drag and Drop
        /// <summary>
        /// Form drag and drop event handler.
        /// The form must enable "AllowDrop" for these events to fire.
        /// </summary>
        private void frmModifyOrder_DragDrop(object sender, DragEventArgs e)
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
        private void frmModifyOrder_DragOver(object sender, DragEventArgs e)
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
        /// This function is called when the Invoke button in the
        /// Delete Order group is clicked.  Three ways of deleting
        /// orders are demonstrated.
        /// </summary>
        /// <param name="sender">Object which fires the method</param>
        /// <param name="e">Event arguments of the callback</param>
        private void btnInvokeModify_Click(object sender, EventArgs e)
        {
            bool didChange = false;

            // Do we have an order to modify?
            if (m_lastOrder == null)
            {
                MessageBox.Show("No order was found to modify!");
                return;
            }

            // Get the order profile from the previous order.
            OrderProfileBase orderProfile = m_lastOrder.GetOrderProfile();

            // Update Order as change or cancel/replace.
            orderProfile.Action = cboModifyType.SelectedIndex <= 0 ? OrderAction.Change : OrderAction.Replace;
            
            // Set the new price.
            if (!String.IsNullOrEmpty(txtNewPrice.Text))
            {
                orderProfile.LimitPrice = Price.FromString(m_lastOrder, txtNewPrice.Text);
                didChange = true;
            }

            // Set the new quantity.
            if (!String.IsNullOrEmpty(txtNewQuantity.Text))
            {
                orderProfile.QuantityToWork = Quantity.FromString(m_lastOrder, txtNewQuantity.Text);
                didChange = true;
            }

            // If there was a change then send the order.
            if (didChange)
            {
                m_instrumentTradeSubscription.SendOrder(orderProfile);
            }
        }

        /// <summary>
        /// Delete the last order placed by this application.
        /// </summary>
        private void buttonDeleteLast_Click(object sender, EventArgs e)
        {
            // Do we have an order to modify?
            if (m_lastOrder == null)
            {
                MessageBox.Show("No order was found to modify!");
                return;
            }

            // Get the order profile from the previous order.
            OrderProfileBase orderProfile = m_lastOrder.GetOrderProfile();

            // Set the order action to delete.
            orderProfile.Action = OrderAction.Delete;

            // Send the delete.
            m_instrumentTradeSubscription.SendOrder(orderProfile);
        }

        /// <summary>
        /// Delete all buy orders for this instrument subscription.
        /// </summary>
        private void buttonDeleteBuy_Click(object sender, EventArgs e)
        {
            // Delete buy side for this instrument.
            m_instrumentTradeSubscription.DeleteBySide(BuySell.Buy);
        }

        /// <summary>
        /// Delete all sell orders for this instrument subscription.
        /// </summary>
        private void buttonDeleteSell_Click(object sender, EventArgs e)
        {
            // Delete sell side for this instrument.
            m_instrumentTradeSubscription.DeleteBySide(BuySell.Sell);
        }

        /// <summary>
        /// Delete all orders for this instrument subscription.
        /// </summary>
        private void buttonDeleteAll_Click(object sender, EventArgs e)
        {
            // Delete all orders on this instrument.
            m_instrumentTradeSubscription.DeleteAll();
        }

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