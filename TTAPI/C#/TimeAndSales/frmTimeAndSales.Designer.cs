namespace TTAPI_Samples
{
    partial class frmTimeAndSales
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
            this.dgTimeAndSales = new System.Windows.Forms.DataGridView();
            this.statusBar1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.gboInstrumentInfo = new System.Windows.Forms.GroupBox();
            this.txtExchange = new System.Windows.Forms.TextBox();
            this.txtContract = new System.Windows.Forms.TextBox();
            this.lblProductType = new System.Windows.Forms.Label();
            this.lblExchange = new System.Windows.Forms.Label();
            this.lblContract = new System.Windows.Forms.Label();
            this.lblProduct = new System.Windows.Forms.Label();
            this.txtProduct = new System.Windows.Forms.TextBox();
            this.txtProductType = new System.Windows.Forms.TextBox();
            this.gboTimeAndSales = new System.Windows.Forms.GroupBox();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.mnuAbout = new System.Windows.Forms.MenuItem();
            this.lblNotProduction = new System.Windows.Forms.Label();
            this.lblWarning = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgTimeAndSales)).BeginInit();
            this.statusBar1.SuspendLayout();
            this.gboInstrumentInfo.SuspendLayout();
            this.gboTimeAndSales.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgTimeAndSales
            // 
            this.dgTimeAndSales.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dgTimeAndSales.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgTimeAndSales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTimeAndSales.Location = new System.Drawing.Point(6, 15);
            this.dgTimeAndSales.Name = "dgTimeAndSales";
            this.dgTimeAndSales.RowHeadersVisible = false;
            this.dgTimeAndSales.Size = new System.Drawing.Size(388, 187);
            this.dgTimeAndSales.TabIndex = 32;
            this.dgTimeAndSales.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            // 
            // statusBar1
            // 
            this.statusBar1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusBar1.Location = new System.Drawing.Point(0, 272);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(643, 22);
            this.statusBar1.TabIndex = 31;
            this.statusBar1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(430, 17);
            this.toolStripStatusLabel1.Text = "Drag and Drop an instrument from the Market Grid in X_TRADER to this window.";
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
            this.gboInstrumentInfo.Location = new System.Drawing.Point(12, 55);
            this.gboInstrumentInfo.Name = "gboInstrumentInfo";
            this.gboInstrumentInfo.Size = new System.Drawing.Size(216, 136);
            this.gboInstrumentInfo.TabIndex = 113;
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
            // gboTimeAndSales
            // 
            this.gboTimeAndSales.Controls.Add(this.dgTimeAndSales);
            this.gboTimeAndSales.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.gboTimeAndSales.Location = new System.Drawing.Point(234, 55);
            this.gboTimeAndSales.Name = "gboTimeAndSales";
            this.gboTimeAndSales.Size = new System.Drawing.Size(400, 208);
            this.gboTimeAndSales.TabIndex = 114;
            this.gboTimeAndSales.TabStop = false;
            this.gboTimeAndSales.Text = "Time && Sales Data";
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
            this.lblNotProduction.Location = new System.Drawing.Point(13, 38);
            this.lblNotProduction.Name = "lblNotProduction";
            this.lblNotProduction.Size = new System.Drawing.Size(621, 14);
            this.lblNotProduction.TabIndex = 116;
            this.lblNotProduction.Text = "This sample is NOT to be used in production or during conformance testing.";
            this.lblNotProduction.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblWarning
            // 
            this.lblWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWarning.Location = new System.Drawing.Point(12, 9);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(621, 23);
            this.lblWarning.TabIndex = 115;
            this.lblWarning.Text = "WARNING!";
            this.lblWarning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmTimeAndSales
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 294);
            this.Controls.Add(this.lblNotProduction);
            this.Controls.Add(this.lblWarning);
            this.Controls.Add(this.gboTimeAndSales);
            this.Controls.Add(this.gboInstrumentInfo);
            this.Controls.Add(this.statusBar1);
            this.Enabled = false;
            this.Menu = this.mainMenu1;
            this.Name = "frmTimeAndSales";
            this.Text = "TimeAndSales";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.frmTimeAndSales_DragDrop);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.frmTimeAndSales_DragOver);
            ((System.ComponentModel.ISupportInitialize)(this.dgTimeAndSales)).EndInit();
            this.statusBar1.ResumeLayout(false);
            this.statusBar1.PerformLayout();
            this.gboInstrumentInfo.ResumeLayout(false);
            this.gboInstrumentInfo.PerformLayout();
            this.gboTimeAndSales.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgTimeAndSales;
        private System.Windows.Forms.StatusStrip statusBar1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.GroupBox gboInstrumentInfo;
        private System.Windows.Forms.TextBox txtExchange;
        private System.Windows.Forms.TextBox txtContract;
        private System.Windows.Forms.Label lblProductType;
        private System.Windows.Forms.Label lblExchange;
        private System.Windows.Forms.Label lblContract;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.TextBox txtProduct;
        private System.Windows.Forms.TextBox txtProductType;
        private System.Windows.Forms.GroupBox gboTimeAndSales;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem mnuAbout;
        private System.Windows.Forms.Label lblNotProduction;
        private System.Windows.Forms.Label lblWarning;
    }
}

