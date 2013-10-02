namespace TTAPI_Samples
{
    partial class frmProfitLoss
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
            this.label11 = new System.Windows.Forms.Label();
            this.cboPLCalculation = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cboPLDisplay = new System.Windows.Forms.ComboBox();
            this.statusBar1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblNotProduction = new System.Windows.Forms.Label();
            this.lblWarning = new System.Windows.Forms.Label();
            this.gboContract1 = new System.Windows.Forms.GroupBox();
            this.txtSellPosition = new System.Windows.Forms.TextBox();
            this.lblSellPosition = new System.Windows.Forms.Label();
            this.txtBuyPosition = new System.Windows.Forms.TextBox();
            this.lblBuyPosition = new System.Windows.Forms.Label();
            this.txtNetPosition = new System.Windows.Forms.TextBox();
            this.lblNetPosition = new System.Windows.Forms.Label();
            this.txtRealizedPL = new System.Windows.Forms.TextBox();
            this.lblRealizedPL = new System.Windows.Forms.Label();
            this.lblExchange = new System.Windows.Forms.Label();
            this.txtProduct = new System.Windows.Forms.TextBox();
            this.lblContract = new System.Windows.Forms.Label();
            this.txtProductType = new System.Windows.Forms.TextBox();
            this.txtOpenPL = new System.Windows.Forms.TextBox();
            this.lblOpenPL = new System.Windows.Forms.Label();
            this.txtExchange = new System.Windows.Forms.TextBox();
            this.lblProduct = new System.Windows.Forms.Label();
            this.lblProductType = new System.Windows.Forms.Label();
            this.txtContract = new System.Windows.Forms.TextBox();
            this.txtTotalPL = new System.Windows.Forms.TextBox();
            this.lblTotalPL = new System.Windows.Forms.Label();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.mnuAbout = new System.Windows.Forms.MenuItem();
            this.statusBar1.SuspendLayout();
            this.gboContract1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(396, 113);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(86, 13);
            this.label11.TabIndex = 51;
            this.label11.Text = "Calculation Type";
            // 
            // cboPLCalculation
            // 
            this.cboPLCalculation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPLCalculation.FormattingEnabled = true;
            this.cboPLCalculation.Location = new System.Drawing.Point(488, 110);
            this.cboPLCalculation.Name = "cboPLCalculation";
            this.cboPLCalculation.Size = new System.Drawing.Size(142, 21);
            this.cboPLCalculation.TabIndex = 50;
            this.cboPLCalculation.SelectedIndexChanged += new System.EventHandler(this.plCalcCombo_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(429, 140);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 13);
            this.label10.TabIndex = 49;
            this.label10.Text = "Display In";
            // 
            // cboPLDisplay
            // 
            this.cboPLDisplay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPLDisplay.FormattingEnabled = true;
            this.cboPLDisplay.Location = new System.Drawing.Point(488, 137);
            this.cboPLDisplay.Name = "cboPLDisplay";
            this.cboPLDisplay.Size = new System.Drawing.Size(142, 21);
            this.cboPLDisplay.TabIndex = 48;
            this.cboPLDisplay.SelectedIndexChanged += new System.EventHandler(this.plDisplayCombo_SelectedIndexChanged);
            // 
            // statusBar1
            // 
            this.statusBar1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusBar1.Location = new System.Drawing.Point(0, 232);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(669, 22);
            this.statusBar1.TabIndex = 46;
            this.statusBar1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(430, 17);
            this.toolStripStatusLabel1.Text = "Drag and Drop an instrument from the Market Grid in X_TRADER to this window.";
            // 
            // lblNotProduction
            // 
            this.lblNotProduction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotProduction.Location = new System.Drawing.Point(12, 32);
            this.lblNotProduction.Name = "lblNotProduction";
            this.lblNotProduction.Size = new System.Drawing.Size(645, 14);
            this.lblNotProduction.TabIndex = 78;
            this.lblNotProduction.Text = "This sample is NOT to be used in production or during conformance testing.";
            this.lblNotProduction.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblWarning
            // 
            this.lblWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWarning.Location = new System.Drawing.Point(8, 9);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(645, 23);
            this.lblWarning.TabIndex = 77;
            this.lblWarning.Text = "WARNING!";
            this.lblWarning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gboContract1
            // 
            this.gboContract1.Controls.Add(this.txtSellPosition);
            this.gboContract1.Controls.Add(this.lblSellPosition);
            this.gboContract1.Controls.Add(this.txtBuyPosition);
            this.gboContract1.Controls.Add(this.label11);
            this.gboContract1.Controls.Add(this.label10);
            this.gboContract1.Controls.Add(this.lblBuyPosition);
            this.gboContract1.Controls.Add(this.cboPLDisplay);
            this.gboContract1.Controls.Add(this.cboPLCalculation);
            this.gboContract1.Controls.Add(this.txtNetPosition);
            this.gboContract1.Controls.Add(this.lblNetPosition);
            this.gboContract1.Controls.Add(this.txtRealizedPL);
            this.gboContract1.Controls.Add(this.lblRealizedPL);
            this.gboContract1.Controls.Add(this.lblExchange);
            this.gboContract1.Controls.Add(this.txtProduct);
            this.gboContract1.Controls.Add(this.lblContract);
            this.gboContract1.Controls.Add(this.txtProductType);
            this.gboContract1.Controls.Add(this.txtOpenPL);
            this.gboContract1.Controls.Add(this.lblOpenPL);
            this.gboContract1.Controls.Add(this.txtExchange);
            this.gboContract1.Controls.Add(this.lblProduct);
            this.gboContract1.Controls.Add(this.lblProductType);
            this.gboContract1.Controls.Add(this.txtContract);
            this.gboContract1.Controls.Add(this.txtTotalPL);
            this.gboContract1.Controls.Add(this.lblTotalPL);
            this.gboContract1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.gboContract1.Location = new System.Drawing.Point(12, 49);
            this.gboContract1.Name = "gboContract1";
            this.gboContract1.Size = new System.Drawing.Size(645, 172);
            this.gboContract1.TabIndex = 79;
            this.gboContract1.TabStop = false;
            this.gboContract1.Text = "Contract Specific P&&L";
            // 
            // txtSellPosition
            // 
            this.txtSellPosition.Location = new System.Drawing.Point(528, 71);
            this.txtSellPosition.Name = "txtSellPosition";
            this.txtSellPosition.Size = new System.Drawing.Size(102, 20);
            this.txtSellPosition.TabIndex = 81;
            // 
            // lblSellPosition
            // 
            this.lblSellPosition.Location = new System.Drawing.Point(428, 72);
            this.lblSellPosition.Name = "lblSellPosition";
            this.lblSellPosition.Size = new System.Drawing.Size(94, 16);
            this.lblSellPosition.TabIndex = 80;
            this.lblSellPosition.Text = "Sell Position:";
            this.lblSellPosition.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBuyPosition
            // 
            this.txtBuyPosition.Location = new System.Drawing.Point(528, 45);
            this.txtBuyPosition.Name = "txtBuyPosition";
            this.txtBuyPosition.Size = new System.Drawing.Size(102, 20);
            this.txtBuyPosition.TabIndex = 79;
            // 
            // lblBuyPosition
            // 
            this.lblBuyPosition.Location = new System.Drawing.Point(428, 46);
            this.lblBuyPosition.Name = "lblBuyPosition";
            this.lblBuyPosition.Size = new System.Drawing.Size(94, 16);
            this.lblBuyPosition.TabIndex = 78;
            this.lblBuyPosition.Text = "Buy Position:";
            this.lblBuyPosition.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNetPosition
            // 
            this.txtNetPosition.Location = new System.Drawing.Point(528, 19);
            this.txtNetPosition.Name = "txtNetPosition";
            this.txtNetPosition.Size = new System.Drawing.Size(102, 20);
            this.txtNetPosition.TabIndex = 77;
            // 
            // lblNetPosition
            // 
            this.lblNetPosition.Location = new System.Drawing.Point(428, 20);
            this.lblNetPosition.Name = "lblNetPosition";
            this.lblNetPosition.Size = new System.Drawing.Size(94, 16);
            this.lblNetPosition.TabIndex = 76;
            this.lblNetPosition.Text = "Net Position:";
            this.lblNetPosition.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRealizedPL
            // 
            this.txtRealizedPL.Location = new System.Drawing.Point(320, 71);
            this.txtRealizedPL.Name = "txtRealizedPL";
            this.txtRealizedPL.Size = new System.Drawing.Size(102, 20);
            this.txtRealizedPL.TabIndex = 73;
            // 
            // lblRealizedPL
            // 
            this.lblRealizedPL.Location = new System.Drawing.Point(234, 72);
            this.lblRealizedPL.Name = "lblRealizedPL";
            this.lblRealizedPL.Size = new System.Drawing.Size(80, 16);
            this.lblRealizedPL.TabIndex = 72;
            this.lblRealizedPL.Text = "Realized P&&L:";
            this.lblRealizedPL.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblExchange
            // 
            this.lblExchange.Location = new System.Drawing.Point(13, 20);
            this.lblExchange.Name = "lblExchange";
            this.lblExchange.Size = new System.Drawing.Size(80, 16);
            this.lblExchange.TabIndex = 57;
            this.lblExchange.Text = "Exchange:";
            this.lblExchange.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtProduct
            // 
            this.txtProduct.Location = new System.Drawing.Point(97, 45);
            this.txtProduct.Name = "txtProduct";
            this.txtProduct.Size = new System.Drawing.Size(121, 20);
            this.txtProduct.TabIndex = 65;
            // 
            // lblContract
            // 
            this.lblContract.Location = new System.Drawing.Point(13, 98);
            this.lblContract.Name = "lblContract";
            this.lblContract.Size = new System.Drawing.Size(80, 16);
            this.lblContract.TabIndex = 60;
            this.lblContract.Text = "Contract:";
            this.lblContract.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtProductType
            // 
            this.txtProductType.Location = new System.Drawing.Point(97, 71);
            this.txtProductType.Name = "txtProductType";
            this.txtProductType.Size = new System.Drawing.Size(121, 20);
            this.txtProductType.TabIndex = 66;
            // 
            // txtOpenPL
            // 
            this.txtOpenPL.Location = new System.Drawing.Point(320, 45);
            this.txtOpenPL.Name = "txtOpenPL";
            this.txtOpenPL.Size = new System.Drawing.Size(102, 20);
            this.txtOpenPL.TabIndex = 71;
            // 
            // lblOpenPL
            // 
            this.lblOpenPL.Location = new System.Drawing.Point(234, 46);
            this.lblOpenPL.Name = "lblOpenPL";
            this.lblOpenPL.Size = new System.Drawing.Size(80, 16);
            this.lblOpenPL.TabIndex = 70;
            this.lblOpenPL.Text = "Open P&&L:";
            this.lblOpenPL.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtExchange
            // 
            this.txtExchange.Location = new System.Drawing.Point(97, 19);
            this.txtExchange.Name = "txtExchange";
            this.txtExchange.Size = new System.Drawing.Size(121, 20);
            this.txtExchange.TabIndex = 64;
            // 
            // lblProduct
            // 
            this.lblProduct.Location = new System.Drawing.Point(13, 46);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(80, 16);
            this.lblProduct.TabIndex = 58;
            this.lblProduct.Text = "Product:";
            this.lblProduct.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblProductType
            // 
            this.lblProductType.Location = new System.Drawing.Point(11, 72);
            this.lblProductType.Name = "lblProductType";
            this.lblProductType.Size = new System.Drawing.Size(80, 16);
            this.lblProductType.TabIndex = 59;
            this.lblProductType.Text = "Product Type:";
            this.lblProductType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtContract
            // 
            this.txtContract.Location = new System.Drawing.Point(97, 97);
            this.txtContract.Name = "txtContract";
            this.txtContract.Size = new System.Drawing.Size(121, 20);
            this.txtContract.TabIndex = 67;
            // 
            // txtTotalPL
            // 
            this.txtTotalPL.Location = new System.Drawing.Point(320, 19);
            this.txtTotalPL.Name = "txtTotalPL";
            this.txtTotalPL.Size = new System.Drawing.Size(102, 20);
            this.txtTotalPL.TabIndex = 68;
            // 
            // lblTotalPL
            // 
            this.lblTotalPL.Location = new System.Drawing.Point(234, 20);
            this.lblTotalPL.Name = "lblTotalPL";
            this.lblTotalPL.Size = new System.Drawing.Size(80, 16);
            this.lblTotalPL.TabIndex = 61;
            this.lblTotalPL.Text = "Total P&&L:";
            this.lblTotalPL.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            // frmProfitLoss
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 254);
            this.Controls.Add(this.gboContract1);
            this.Controls.Add(this.lblNotProduction);
            this.Controls.Add(this.lblWarning);
            this.Controls.Add(this.statusBar1);
            this.Enabled = false;
            this.Menu = this.mainMenu1;
            this.Name = "frmProfitLoss";
            this.Text = "ProfitLoss";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.Form1_DragOver);
            this.statusBar1.ResumeLayout(false);
            this.statusBar1.PerformLayout();
            this.gboContract1.ResumeLayout(false);
            this.gboContract1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cboPLCalculation;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cboPLDisplay;
        private System.Windows.Forms.StatusStrip statusBar1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Label lblNotProduction;
        private System.Windows.Forms.Label lblWarning;
        private System.Windows.Forms.GroupBox gboContract1;
        private System.Windows.Forms.TextBox txtSellPosition;
        private System.Windows.Forms.Label lblSellPosition;
        private System.Windows.Forms.TextBox txtBuyPosition;
        private System.Windows.Forms.Label lblBuyPosition;
        private System.Windows.Forms.TextBox txtNetPosition;
        private System.Windows.Forms.Label lblNetPosition;
        private System.Windows.Forms.TextBox txtRealizedPL;
        private System.Windows.Forms.Label lblRealizedPL;
        private System.Windows.Forms.Label lblExchange;
        private System.Windows.Forms.TextBox txtProduct;
        private System.Windows.Forms.Label lblContract;
        private System.Windows.Forms.TextBox txtProductType;
        private System.Windows.Forms.TextBox txtOpenPL;
        private System.Windows.Forms.Label lblOpenPL;
        private System.Windows.Forms.TextBox txtExchange;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.Label lblProductType;
        private System.Windows.Forms.TextBox txtContract;
        private System.Windows.Forms.TextBox txtTotalPL;
        private System.Windows.Forms.Label lblTotalPL;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem mnuAbout;
    }
}

