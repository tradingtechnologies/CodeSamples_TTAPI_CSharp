namespace TTAPI_Samples
{
    partial class frmPriceUpdateDepth
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
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.mnuAbout = new System.Windows.Forms.MenuItem();
            this.lblNotProduction = new System.Windows.Forms.Label();
            this.lblWarning = new System.Windows.Forms.Label();
            this.gboInstrumentDepthData = new System.Windows.Forms.GroupBox();
            this.lboBidDepth = new System.Windows.Forms.ListBox();
            this.lboAskDepth = new System.Windows.Forms.ListBox();
            this.lblAskDepth = new System.Windows.Forms.Label();
            this.lblBidDepth = new System.Windows.Forms.Label();
            this.gboInstrumentInfo = new System.Windows.Forms.GroupBox();
            this.txtExchange = new System.Windows.Forms.TextBox();
            this.txtContract = new System.Windows.Forms.TextBox();
            this.lblProductType = new System.Windows.Forms.Label();
            this.lblExchange = new System.Windows.Forms.Label();
            this.lblContract = new System.Windows.Forms.Label();
            this.lblProduct = new System.Windows.Forms.Label();
            this.txtProduct = new System.Windows.Forms.TextBox();
            this.txtProductType = new System.Windows.Forms.TextBox();
            this.statusBar1.SuspendLayout();
            this.gboInstrumentDepthData.SuspendLayout();
            this.gboInstrumentInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusBar1
            // 
            this.statusBar1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusBar1.Location = new System.Drawing.Point(0, 325);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(650, 22);
            this.statusBar1.TabIndex = 34;
            this.statusBar1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(430, 17);
            this.toolStripStatusLabel1.Text = "Drag and Drop an instrument from the Market Grid in X_TRADER to this window.";
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
            // lblNotProduction
            // 
            this.lblNotProduction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotProduction.Location = new System.Drawing.Point(12, 34);
            this.lblNotProduction.Name = "lblNotProduction";
            this.lblNotProduction.Size = new System.Drawing.Size(629, 14);
            this.lblNotProduction.TabIndex = 116;
            this.lblNotProduction.Text = "This sample is NOT to be used in production or during conformance testing.";
            this.lblNotProduction.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblWarning
            // 
            this.lblWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWarning.Location = new System.Drawing.Point(12, 9);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(629, 23);
            this.lblWarning.TabIndex = 115;
            this.lblWarning.Text = "WARNING!";
            this.lblWarning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gboInstrumentDepthData
            // 
            this.gboInstrumentDepthData.Controls.Add(this.lboBidDepth);
            this.gboInstrumentDepthData.Controls.Add(this.lboAskDepth);
            this.gboInstrumentDepthData.Controls.Add(this.lblAskDepth);
            this.gboInstrumentDepthData.Controls.Add(this.lblBidDepth);
            this.gboInstrumentDepthData.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.gboInstrumentDepthData.Location = new System.Drawing.Point(233, 53);
            this.gboInstrumentDepthData.Name = "gboInstrumentDepthData";
            this.gboInstrumentDepthData.Size = new System.Drawing.Size(408, 264);
            this.gboInstrumentDepthData.TabIndex = 113;
            this.gboInstrumentDepthData.TabStop = false;
            this.gboInstrumentDepthData.Text = "Instrument Depth Data";
            // 
            // lboBidDepth
            // 
            this.lboBidDepth.Location = new System.Drawing.Point(16, 40);
            this.lboBidDepth.Name = "lboBidDepth";
            this.lboBidDepth.Size = new System.Drawing.Size(184, 212);
            this.lboBidDepth.TabIndex = 95;
            // 
            // lboAskDepth
            // 
            this.lboAskDepth.Location = new System.Drawing.Point(208, 40);
            this.lboAskDepth.Name = "lboAskDepth";
            this.lboAskDepth.Size = new System.Drawing.Size(184, 212);
            this.lboAskDepth.TabIndex = 96;
            // 
            // lblAskDepth
            // 
            this.lblAskDepth.Location = new System.Drawing.Point(208, 24);
            this.lblAskDepth.Name = "lblAskDepth";
            this.lblAskDepth.Size = new System.Drawing.Size(96, 16);
            this.lblAskDepth.TabIndex = 97;
            this.lblAskDepth.Text = "AskDepth:";
            // 
            // lblBidDepth
            // 
            this.lblBidDepth.Location = new System.Drawing.Point(16, 24);
            this.lblBidDepth.Name = "lblBidDepth";
            this.lblBidDepth.Size = new System.Drawing.Size(96, 16);
            this.lblBidDepth.TabIndex = 98;
            this.lblBidDepth.Text = "BidDepth:";
            // 
            // gboInstrumentInfo
            // 
            this.gboInstrumentInfo.Controls.Add(this.txtExchange);
            this.gboInstrumentInfo.Controls.Add(this.txtContract);
            this.gboInstrumentInfo.Controls.Add(this.lblProductType);
            this.gboInstrumentInfo.Controls.Add(this.lblExchange);
            this.gboInstrumentInfo.Controls.Add(this.lblContract);
            this.gboInstrumentInfo.Controls.Add(this.lblProduct);
            this.gboInstrumentInfo.Controls.Add(this.txtProduct);
            this.gboInstrumentInfo.Controls.Add(this.txtProductType);
            this.gboInstrumentInfo.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.gboInstrumentInfo.Location = new System.Drawing.Point(9, 53);
            this.gboInstrumentInfo.Name = "gboInstrumentInfo";
            this.gboInstrumentInfo.Size = new System.Drawing.Size(216, 136);
            this.gboInstrumentInfo.TabIndex = 112;
            this.gboInstrumentInfo.TabStop = false;
            this.gboInstrumentInfo.Text = "Instrument Information";
            // 
            // txtExchange
            // 
            this.txtExchange.Location = new System.Drawing.Point(96, 24);
            this.txtExchange.Name = "txtExchange";
            this.txtExchange.Size = new System.Drawing.Size(104, 20);
            this.txtExchange.TabIndex = 100;
            // 
            // txtContract
            // 
            this.txtContract.Location = new System.Drawing.Point(96, 96);
            this.txtContract.Name = "txtContract";
            this.txtContract.Size = new System.Drawing.Size(104, 20);
            this.txtContract.TabIndex = 103;
            // 
            // lblProductType
            // 
            this.lblProductType.Location = new System.Drawing.Point(14, 72);
            this.lblProductType.Name = "lblProductType";
            this.lblProductType.Size = new System.Drawing.Size(74, 16);
            this.lblProductType.TabIndex = 105;
            this.lblProductType.Text = "Product Type:";
            this.lblProductType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblExchange
            // 
            this.lblExchange.Location = new System.Drawing.Point(15, 24);
            this.lblExchange.Name = "lblExchange";
            this.lblExchange.Size = new System.Drawing.Size(73, 16);
            this.lblExchange.TabIndex = 14;
            this.lblExchange.Text = "Exchange:";
            this.lblExchange.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblContract
            // 
            this.lblContract.Location = new System.Drawing.Point(15, 96);
            this.lblContract.Name = "lblContract";
            this.lblContract.Size = new System.Drawing.Size(73, 16);
            this.lblContract.TabIndex = 106;
            this.lblContract.Text = "Contract:";
            this.lblContract.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblProduct
            // 
            this.lblProduct.Location = new System.Drawing.Point(15, 48);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(73, 16);
            this.lblProduct.TabIndex = 104;
            this.lblProduct.Text = "Product:";
            this.lblProduct.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtProduct
            // 
            this.txtProduct.Location = new System.Drawing.Point(96, 48);
            this.txtProduct.Name = "txtProduct";
            this.txtProduct.Size = new System.Drawing.Size(104, 20);
            this.txtProduct.TabIndex = 101;
            // 
            // txtProductType
            // 
            this.txtProductType.Location = new System.Drawing.Point(96, 72);
            this.txtProductType.Name = "txtProductType";
            this.txtProductType.Size = new System.Drawing.Size(104, 20);
            this.txtProductType.TabIndex = 102;
            // 
            // frmPriceUpdateDepth
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 347);
            this.Controls.Add(this.lblNotProduction);
            this.Controls.Add(this.lblWarning);
            this.Controls.Add(this.gboInstrumentDepthData);
            this.Controls.Add(this.gboInstrumentInfo);
            this.Controls.Add(this.statusBar1);
            this.Enabled = false;
            this.Menu = this.mainMenu1;
            this.Name = "frmPriceUpdateDepth";
            this.Text = "PriceUpdateDepth";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.frmPriceUpdateDepth_DragDrop);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.frmPriceUpdateDepth_DragOver);
            this.statusBar1.ResumeLayout(false);
            this.statusBar1.PerformLayout();
            this.gboInstrumentDepthData.ResumeLayout(false);
            this.gboInstrumentInfo.ResumeLayout(false);
            this.gboInstrumentInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusBar1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem mnuAbout;
        private System.Windows.Forms.Label lblNotProduction;
        private System.Windows.Forms.Label lblWarning;
        private System.Windows.Forms.GroupBox gboInstrumentDepthData;
        private System.Windows.Forms.ListBox lboBidDepth;
        private System.Windows.Forms.ListBox lboAskDepth;
        private System.Windows.Forms.Label lblAskDepth;
        private System.Windows.Forms.Label lblBidDepth;
        private System.Windows.Forms.GroupBox gboInstrumentInfo;
        private System.Windows.Forms.TextBox txtExchange;
        private System.Windows.Forms.TextBox txtContract;
        private System.Windows.Forms.Label lblProductType;
        private System.Windows.Forms.Label lblExchange;
        private System.Windows.Forms.Label lblContract;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.TextBox txtProduct;
        private System.Windows.Forms.TextBox txtProductType;
    }
}

