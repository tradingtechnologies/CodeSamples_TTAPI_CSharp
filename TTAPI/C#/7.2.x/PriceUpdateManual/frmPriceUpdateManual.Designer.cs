namespace TTAPI_Samples
{
    partial class frmPriceUpdateManual
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            // shut down the API
            m_TTAPI.Shutdown();

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.statusBar1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblNotProduction = new System.Windows.Forms.Label();
            this.lblAbout = new System.Windows.Forms.Label();
            this.gboInstrumentMarketData = new System.Windows.Forms.GroupBox();
            this.lblAskPrice = new System.Windows.Forms.Label();
            this.txtAskPrice = new System.Windows.Forms.TextBox();
            this.txtBidPrice = new System.Windows.Forms.TextBox();
            this.lblLastQty = new System.Windows.Forms.Label();
            this.txtLastQty = new System.Windows.Forms.TextBox();
            this.lblBidPrice = new System.Windows.Forms.Label();
            this.lblAskQty = new System.Windows.Forms.Label();
            this.txtAskQty = new System.Windows.Forms.TextBox();
            this.lblLastPrice = new System.Windows.Forms.Label();
            this.lblBidQty = new System.Windows.Forms.Label();
            this.txtBidQty = new System.Windows.Forms.TextBox();
            this.txtLastPrice = new System.Windows.Forms.TextBox();
            this.gboInstrumentInfo = new System.Windows.Forms.GroupBox();
            this.cboProductType = new System.Windows.Forms.ComboBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.lblProductType = new System.Windows.Forms.Label();
            this.txtProduct = new System.Windows.Forms.TextBox();
            this.lblProduct = new System.Windows.Forms.Label();
            this.lblExchange = new System.Windows.Forms.Label();
            this.txtContract = new System.Windows.Forms.TextBox();
            this.lblContract = new System.Windows.Forms.Label();
            this.txtExchange = new System.Windows.Forms.TextBox();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.mnuAbout = new System.Windows.Forms.MenuItem();
            this.statusBar1.SuspendLayout();
            this.gboInstrumentMarketData.SuspendLayout();
            this.gboInstrumentInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusBar1
            // 
            this.statusBar1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusBar1.Location = new System.Drawing.Point(0, 240);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(448, 22);
            this.statusBar1.TabIndex = 43;
            this.statusBar1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(366, 17);
            this.toolStripStatusLabel1.Text = "Complete the Instrument Information and click the Connect button.";
            // 
            // lblNotProduction
            // 
            this.lblNotProduction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotProduction.Location = new System.Drawing.Point(12, 34);
            this.lblNotProduction.Name = "lblNotProduction";
            this.lblNotProduction.Size = new System.Drawing.Size(424, 14);
            this.lblNotProduction.TabIndex = 82;
            this.lblNotProduction.Text = "This sample is NOT to be used in production or during conformance testing.";
            this.lblNotProduction.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAbout
            // 
            this.lblAbout.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAbout.Location = new System.Drawing.Point(12, 9);
            this.lblAbout.Name = "lblAbout";
            this.lblAbout.Size = new System.Drawing.Size(424, 23);
            this.lblAbout.TabIndex = 81;
            this.lblAbout.Text = "WARNING!";
            this.lblAbout.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gboInstrumentMarketData
            // 
            this.gboInstrumentMarketData.Controls.Add(this.lblAskPrice);
            this.gboInstrumentMarketData.Controls.Add(this.txtAskPrice);
            this.gboInstrumentMarketData.Controls.Add(this.txtBidPrice);
            this.gboInstrumentMarketData.Controls.Add(this.lblLastQty);
            this.gboInstrumentMarketData.Controls.Add(this.txtLastQty);
            this.gboInstrumentMarketData.Controls.Add(this.lblBidPrice);
            this.gboInstrumentMarketData.Controls.Add(this.lblAskQty);
            this.gboInstrumentMarketData.Controls.Add(this.txtAskQty);
            this.gboInstrumentMarketData.Controls.Add(this.lblLastPrice);
            this.gboInstrumentMarketData.Controls.Add(this.lblBidQty);
            this.gboInstrumentMarketData.Controls.Add(this.txtBidQty);
            this.gboInstrumentMarketData.Controls.Add(this.txtLastPrice);
            this.gboInstrumentMarketData.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.gboInstrumentMarketData.Location = new System.Drawing.Point(232, 55);
            this.gboInstrumentMarketData.Name = "gboInstrumentMarketData";
            this.gboInstrumentMarketData.Size = new System.Drawing.Size(208, 179);
            this.gboInstrumentMarketData.TabIndex = 80;
            this.gboInstrumentMarketData.TabStop = false;
            this.gboInstrumentMarketData.Text = "Instrument Market Data";
            // 
            // lblAskPrice
            // 
            this.lblAskPrice.Location = new System.Drawing.Point(16, 72);
            this.lblAskPrice.Name = "lblAskPrice";
            this.lblAskPrice.Size = new System.Drawing.Size(81, 16);
            this.lblAskPrice.TabIndex = 46;
            this.lblAskPrice.Text = "Ask Price:";
            this.lblAskPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAskPrice
            // 
            this.txtAskPrice.Location = new System.Drawing.Point(103, 72);
            this.txtAskPrice.Name = "txtAskPrice";
            this.txtAskPrice.Size = new System.Drawing.Size(89, 20);
            this.txtAskPrice.TabIndex = 45;
            // 
            // txtBidPrice
            // 
            this.txtBidPrice.Location = new System.Drawing.Point(103, 24);
            this.txtBidPrice.Name = "txtBidPrice";
            this.txtBidPrice.Size = new System.Drawing.Size(89, 20);
            this.txtBidPrice.TabIndex = 41;
            // 
            // lblLastQty
            // 
            this.lblLastQty.Location = new System.Drawing.Point(16, 144);
            this.lblLastQty.Name = "lblLastQty";
            this.lblLastQty.Size = new System.Drawing.Size(81, 16);
            this.lblLastQty.TabIndex = 52;
            this.lblLastQty.Text = "Last Qty:";
            this.lblLastQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLastQty
            // 
            this.txtLastQty.Location = new System.Drawing.Point(103, 144);
            this.txtLastQty.Name = "txtLastQty";
            this.txtLastQty.Size = new System.Drawing.Size(89, 20);
            this.txtLastQty.TabIndex = 51;
            // 
            // lblBidPrice
            // 
            this.lblBidPrice.Location = new System.Drawing.Point(16, 24);
            this.lblBidPrice.Name = "lblBidPrice";
            this.lblBidPrice.Size = new System.Drawing.Size(81, 16);
            this.lblBidPrice.TabIndex = 42;
            this.lblBidPrice.Text = "Bid Price:";
            this.lblBidPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAskQty
            // 
            this.lblAskQty.Location = new System.Drawing.Point(16, 96);
            this.lblAskQty.Name = "lblAskQty";
            this.lblAskQty.Size = new System.Drawing.Size(81, 16);
            this.lblAskQty.TabIndex = 48;
            this.lblAskQty.Text = "Ask Qty:";
            this.lblAskQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAskQty
            // 
            this.txtAskQty.Location = new System.Drawing.Point(103, 96);
            this.txtAskQty.Name = "txtAskQty";
            this.txtAskQty.Size = new System.Drawing.Size(89, 20);
            this.txtAskQty.TabIndex = 47;
            // 
            // lblLastPrice
            // 
            this.lblLastPrice.Location = new System.Drawing.Point(16, 120);
            this.lblLastPrice.Name = "lblLastPrice";
            this.lblLastPrice.Size = new System.Drawing.Size(81, 16);
            this.lblLastPrice.TabIndex = 50;
            this.lblLastPrice.Text = "Last Price:";
            this.lblLastPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblBidQty
            // 
            this.lblBidQty.Location = new System.Drawing.Point(16, 48);
            this.lblBidQty.Name = "lblBidQty";
            this.lblBidQty.Size = new System.Drawing.Size(81, 16);
            this.lblBidQty.TabIndex = 44;
            this.lblBidQty.Text = "Bid Qty:";
            this.lblBidQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBidQty
            // 
            this.txtBidQty.Location = new System.Drawing.Point(103, 48);
            this.txtBidQty.Name = "txtBidQty";
            this.txtBidQty.Size = new System.Drawing.Size(89, 20);
            this.txtBidQty.TabIndex = 43;
            // 
            // txtLastPrice
            // 
            this.txtLastPrice.Location = new System.Drawing.Point(103, 120);
            this.txtLastPrice.Name = "txtLastPrice";
            this.txtLastPrice.Size = new System.Drawing.Size(89, 20);
            this.txtLastPrice.TabIndex = 49;
            // 
            // gboInstrumentInfo
            // 
            this.gboInstrumentInfo.Controls.Add(this.cboProductType);
            this.gboInstrumentInfo.Controls.Add(this.btnConnect);
            this.gboInstrumentInfo.Controls.Add(this.lblProductType);
            this.gboInstrumentInfo.Controls.Add(this.txtProduct);
            this.gboInstrumentInfo.Controls.Add(this.lblProduct);
            this.gboInstrumentInfo.Controls.Add(this.lblExchange);
            this.gboInstrumentInfo.Controls.Add(this.txtContract);
            this.gboInstrumentInfo.Controls.Add(this.lblContract);
            this.gboInstrumentInfo.Controls.Add(this.txtExchange);
            this.gboInstrumentInfo.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.gboInstrumentInfo.Location = new System.Drawing.Point(8, 55);
            this.gboInstrumentInfo.Name = "gboInstrumentInfo";
            this.gboInstrumentInfo.Size = new System.Drawing.Size(216, 179);
            this.gboInstrumentInfo.TabIndex = 79;
            this.gboInstrumentInfo.TabStop = false;
            this.gboInstrumentInfo.Text = "Instrument Information";
            // 
            // cboProductType
            // 
            this.cboProductType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProductType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboProductType.FormattingEnabled = true;
            this.cboProductType.Location = new System.Drawing.Point(96, 71);
            this.cboProductType.Name = "cboProductType";
            this.cboProductType.Size = new System.Drawing.Size(114, 21);
            this.cboProductType.TabIndex = 42;
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.SystemColors.Control;
            this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnConnect.Location = new System.Drawing.Point(135, 137);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 41;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
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
            this.txtProduct.Size = new System.Drawing.Size(114, 20);
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
            this.lblExchange.Text = "Exchange:";
            this.lblExchange.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtContract
            // 
            this.txtContract.Location = new System.Drawing.Point(96, 96);
            this.txtContract.Name = "txtContract";
            this.txtContract.Size = new System.Drawing.Size(114, 20);
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
            this.txtExchange.Size = new System.Drawing.Size(114, 20);
            this.txtExchange.TabIndex = 33;
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
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // frmPriceUpdateManual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 262);
            this.Controls.Add(this.lblNotProduction);
            this.Controls.Add(this.lblAbout);
            this.Controls.Add(this.gboInstrumentMarketData);
            this.Controls.Add(this.gboInstrumentInfo);
            this.Controls.Add(this.statusBar1);
            this.Enabled = false;
            this.Menu = this.mainMenu1;
            this.Name = "frmPriceUpdateManual";
            this.Text = "PriceUpdateManual";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusBar1.ResumeLayout(false);
            this.statusBar1.PerformLayout();
            this.gboInstrumentMarketData.ResumeLayout(false);
            this.gboInstrumentMarketData.PerformLayout();
            this.gboInstrumentInfo.ResumeLayout(false);
            this.gboInstrumentInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusBar1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Label lblNotProduction;
        private System.Windows.Forms.Label lblAbout;
        private System.Windows.Forms.GroupBox gboInstrumentMarketData;
        private System.Windows.Forms.Label lblAskPrice;
        private System.Windows.Forms.TextBox txtAskPrice;
        private System.Windows.Forms.TextBox txtBidPrice;
        private System.Windows.Forms.Label lblLastQty;
        private System.Windows.Forms.TextBox txtLastQty;
        private System.Windows.Forms.Label lblBidPrice;
        private System.Windows.Forms.Label lblAskQty;
        private System.Windows.Forms.TextBox txtAskQty;
        private System.Windows.Forms.Label lblLastPrice;
        private System.Windows.Forms.Label lblBidQty;
        private System.Windows.Forms.TextBox txtBidQty;
        private System.Windows.Forms.TextBox txtLastPrice;
        private System.Windows.Forms.GroupBox gboInstrumentInfo;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label lblProductType;
        private System.Windows.Forms.TextBox txtProduct;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.Label lblExchange;
        private System.Windows.Forms.TextBox txtContract;
        private System.Windows.Forms.Label lblContract;
        private System.Windows.Forms.TextBox txtExchange;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem mnuAbout;
        private System.Windows.Forms.ComboBox cboProductType;
    }
}

