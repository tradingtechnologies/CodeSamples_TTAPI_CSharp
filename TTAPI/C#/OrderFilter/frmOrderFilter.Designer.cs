namespace TTAPI_Samples
{
    partial class frmOrderFilter
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
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.mnuAbout = new System.Windows.Forms.MenuItem();
            this.lblNotProduction = new System.Windows.Forms.Label();
            this.lblWarning = new System.Windows.Forms.Label();
            this.gboLimitOrderEntry = new System.Windows.Forms.GroupBox();
            this.lblOrderFeed = new System.Windows.Forms.Label();
            this.cboOrderFeed = new System.Windows.Forms.ComboBox();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.cboCustomer = new System.Windows.Forms.ComboBox();
            this.btnSell = new System.Windows.Forms.Button();
            this.btnBuy = new System.Windows.Forms.Button();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.lblPrice = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.gboInstrumentInfo = new System.Windows.Forms.GroupBox();
            this.lblProductType = new System.Windows.Forms.Label();
            this.txtProduct = new System.Windows.Forms.TextBox();
            this.txtContract = new System.Windows.Forms.TextBox();
            this.txtExchange = new System.Windows.Forms.TextBox();
            this.lblProduct = new System.Windows.Forms.Label();
            this.txtProductType = new System.Windows.Forms.TextBox();
            this.lblExchange = new System.Windows.Forms.Label();
            this.lblContract = new System.Windows.Forms.Label();
            this.gboOrderFilters = new System.Windows.Forms.GroupBox();
            this.btnResetOrderFilter = new System.Windows.Forms.Button();
            this.btnApplyOrderFilter = new System.Windows.Forms.Button();
            this.panelFilter4 = new System.Windows.Forms.Panel();
            this.txtFilter4 = new System.Windows.Forms.TextBox();
            this.lblFilter4 = new System.Windows.Forms.Label();
            this.cboFilter4 = new System.Windows.Forms.ComboBox();
            this.rbAnd4 = new System.Windows.Forms.RadioButton();
            this.rbOr4 = new System.Windows.Forms.RadioButton();
            this.panelFilter3 = new System.Windows.Forms.Panel();
            this.txtFilter3 = new System.Windows.Forms.TextBox();
            this.lblFilter3 = new System.Windows.Forms.Label();
            this.cboFilter3 = new System.Windows.Forms.ComboBox();
            this.rbAnd3 = new System.Windows.Forms.RadioButton();
            this.rbOr3 = new System.Windows.Forms.RadioButton();
            this.panelFilter2 = new System.Windows.Forms.Panel();
            this.txtFilter2 = new System.Windows.Forms.TextBox();
            this.lblFilter2 = new System.Windows.Forms.Label();
            this.cboFilter2 = new System.Windows.Forms.ComboBox();
            this.rbAnd2 = new System.Windows.Forms.RadioButton();
            this.rbOr2 = new System.Windows.Forms.RadioButton();
            this.panelFilter1 = new System.Windows.Forms.Panel();
            this.rbAnd1 = new System.Windows.Forms.RadioButton();
            this.rbOr1 = new System.Windows.Forms.RadioButton();
            this.txtFilter1 = new System.Windows.Forms.TextBox();
            this.lblFilter1 = new System.Windows.Forms.Label();
            this.cboFilter1 = new System.Windows.Forms.ComboBox();
            this.gboOrderAuditTrail = new System.Windows.Forms.GroupBox();
            this.lboAuditLog = new System.Windows.Forms.ListView();
            this.statusBar1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.gboLimitOrderEntry.SuspendLayout();
            this.gboInstrumentInfo.SuspendLayout();
            this.gboOrderFilters.SuspendLayout();
            this.panelFilter4.SuspendLayout();
            this.panelFilter3.SuspendLayout();
            this.panelFilter2.SuspendLayout();
            this.panelFilter1.SuspendLayout();
            this.gboOrderAuditTrail.SuspendLayout();
            this.statusBar1.SuspendLayout();
            this.SuspendLayout();
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
            this.lblNotProduction.Location = new System.Drawing.Point(12, 30);
            this.lblNotProduction.Name = "lblNotProduction";
            this.lblNotProduction.Size = new System.Drawing.Size(412, 14);
            this.lblNotProduction.TabIndex = 73;
            this.lblNotProduction.Text = "This sample is NOT to be used in production or during conformance testing.";
            this.lblNotProduction.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblWarning
            // 
            this.lblWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWarning.Location = new System.Drawing.Point(12, 5);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(412, 23);
            this.lblWarning.TabIndex = 72;
            this.lblWarning.Text = "WARNING!";
            this.lblWarning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gboLimitOrderEntry
            // 
            this.gboLimitOrderEntry.Controls.Add(this.lblOrderFeed);
            this.gboLimitOrderEntry.Controls.Add(this.cboOrderFeed);
            this.gboLimitOrderEntry.Controls.Add(this.lblCustomer);
            this.gboLimitOrderEntry.Controls.Add(this.cboCustomer);
            this.gboLimitOrderEntry.Controls.Add(this.btnSell);
            this.gboLimitOrderEntry.Controls.Add(this.btnBuy);
            this.gboLimitOrderEntry.Controls.Add(this.lblQuantity);
            this.gboLimitOrderEntry.Controls.Add(this.txtQuantity);
            this.gboLimitOrderEntry.Controls.Add(this.lblPrice);
            this.gboLimitOrderEntry.Controls.Add(this.txtPrice);
            this.gboLimitOrderEntry.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.gboLimitOrderEntry.Location = new System.Drawing.Point(250, 49);
            this.gboLimitOrderEntry.Name = "gboLimitOrderEntry";
            this.gboLimitOrderEntry.Size = new System.Drawing.Size(188, 149);
            this.gboLimitOrderEntry.TabIndex = 71;
            this.gboLimitOrderEntry.TabStop = false;
            this.gboLimitOrderEntry.Text = "Limit Order Entry";
            // 
            // lblOrderFeed
            // 
            this.lblOrderFeed.Location = new System.Drawing.Point(9, 44);
            this.lblOrderFeed.Name = "lblOrderFeed";
            this.lblOrderFeed.Size = new System.Drawing.Size(53, 21);
            this.lblOrderFeed.TabIndex = 51;
            this.lblOrderFeed.Text = "Feed:";
            this.lblOrderFeed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboOrderFeed
            // 
            this.cboOrderFeed.DisplayMember = "Name";
            this.cboOrderFeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOrderFeed.Location = new System.Drawing.Point(68, 45);
            this.cboOrderFeed.Name = "cboOrderFeed";
            this.cboOrderFeed.Size = new System.Drawing.Size(113, 21);
            this.cboOrderFeed.TabIndex = 50;
            // 
            // lblCustomer
            // 
            this.lblCustomer.Location = new System.Drawing.Point(7, 23);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(56, 16);
            this.lblCustomer.TabIndex = 47;
            this.lblCustomer.Text = "Customer:";
            this.lblCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboCustomer
            // 
            this.cboCustomer.DisplayMember = "Customer";
            this.cboCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCustomer.Location = new System.Drawing.Point(68, 21);
            this.cboCustomer.Name = "cboCustomer";
            this.cboCustomer.Size = new System.Drawing.Size(113, 21);
            this.cboCustomer.TabIndex = 46;
            // 
            // btnSell
            // 
            this.btnSell.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSell.Location = new System.Drawing.Point(126, 120);
            this.btnSell.Name = "btnSell";
            this.btnSell.Size = new System.Drawing.Size(56, 23);
            this.btnSell.TabIndex = 43;
            this.btnSell.Text = "Sell";
            this.btnSell.Click += new System.EventHandler(this.btnSell_Click);
            // 
            // btnBuy
            // 
            this.btnBuy.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnBuy.Location = new System.Drawing.Point(69, 120);
            this.btnBuy.Name = "btnBuy";
            this.btnBuy.Size = new System.Drawing.Size(56, 23);
            this.btnBuy.TabIndex = 42;
            this.btnBuy.Text = "Buy";
            this.btnBuy.Click += new System.EventHandler(this.btnBuy_Click);
            // 
            // lblQuantity
            // 
            this.lblQuantity.Location = new System.Drawing.Point(6, 94);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(56, 16);
            this.lblQuantity.TabIndex = 38;
            this.lblQuantity.Text = "Quantity:";
            this.lblQuantity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(68, 93);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(113, 20);
            this.txtQuantity.TabIndex = 37;
            // 
            // lblPrice
            // 
            this.lblPrice.Location = new System.Drawing.Point(7, 71);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(56, 16);
            this.lblPrice.TabIndex = 36;
            this.lblPrice.Text = "Price:";
            this.lblPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(68, 69);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(113, 20);
            this.txtPrice.TabIndex = 35;
            // 
            // gboInstrumentInfo
            // 
            this.gboInstrumentInfo.Controls.Add(this.lblProductType);
            this.gboInstrumentInfo.Controls.Add(this.txtProduct);
            this.gboInstrumentInfo.Controls.Add(this.txtContract);
            this.gboInstrumentInfo.Controls.Add(this.txtExchange);
            this.gboInstrumentInfo.Controls.Add(this.lblProduct);
            this.gboInstrumentInfo.Controls.Add(this.txtProductType);
            this.gboInstrumentInfo.Controls.Add(this.lblExchange);
            this.gboInstrumentInfo.Controls.Add(this.lblContract);
            this.gboInstrumentInfo.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.gboInstrumentInfo.Location = new System.Drawing.Point(12, 49);
            this.gboInstrumentInfo.Name = "gboInstrumentInfo";
            this.gboInstrumentInfo.Size = new System.Drawing.Size(232, 149);
            this.gboInstrumentInfo.TabIndex = 70;
            this.gboInstrumentInfo.TabStop = false;
            this.gboInstrumentInfo.Text = "Instrument Information";
            // 
            // lblProductType
            // 
            this.lblProductType.Location = new System.Drawing.Point(6, 71);
            this.lblProductType.Name = "lblProductType";
            this.lblProductType.Size = new System.Drawing.Size(75, 16);
            this.lblProductType.TabIndex = 38;
            this.lblProductType.Text = "Product Type:";
            this.lblProductType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtProduct
            // 
            this.txtProduct.Location = new System.Drawing.Point(87, 45);
            this.txtProduct.Name = "txtProduct";
            this.txtProduct.Size = new System.Drawing.Size(139, 20);
            this.txtProduct.TabIndex = 35;
            // 
            // txtContract
            // 
            this.txtContract.Location = new System.Drawing.Point(87, 93);
            this.txtContract.Name = "txtContract";
            this.txtContract.Size = new System.Drawing.Size(139, 20);
            this.txtContract.TabIndex = 39;
            // 
            // txtExchange
            // 
            this.txtExchange.Location = new System.Drawing.Point(87, 22);
            this.txtExchange.Name = "txtExchange";
            this.txtExchange.Size = new System.Drawing.Size(139, 20);
            this.txtExchange.TabIndex = 33;
            // 
            // lblProduct
            // 
            this.lblProduct.Location = new System.Drawing.Point(6, 47);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(75, 16);
            this.lblProduct.TabIndex = 36;
            this.lblProduct.Text = "Product:";
            this.lblProduct.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtProductType
            // 
            this.txtProductType.Location = new System.Drawing.Point(87, 69);
            this.txtProductType.Name = "txtProductType";
            this.txtProductType.Size = new System.Drawing.Size(139, 20);
            this.txtProductType.TabIndex = 37;
            // 
            // lblExchange
            // 
            this.lblExchange.Location = new System.Drawing.Point(6, 23);
            this.lblExchange.Name = "lblExchange";
            this.lblExchange.Size = new System.Drawing.Size(75, 16);
            this.lblExchange.TabIndex = 34;
            this.lblExchange.Text = "Exchange:";
            this.lblExchange.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblContract
            // 
            this.lblContract.Location = new System.Drawing.Point(6, 95);
            this.lblContract.Name = "lblContract";
            this.lblContract.Size = new System.Drawing.Size(75, 16);
            this.lblContract.TabIndex = 40;
            this.lblContract.Text = "Contract:";
            this.lblContract.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // gboOrderFilters
            // 
            this.gboOrderFilters.Controls.Add(this.btnResetOrderFilter);
            this.gboOrderFilters.Controls.Add(this.btnApplyOrderFilter);
            this.gboOrderFilters.Controls.Add(this.panelFilter4);
            this.gboOrderFilters.Controls.Add(this.panelFilter3);
            this.gboOrderFilters.Controls.Add(this.panelFilter2);
            this.gboOrderFilters.Controls.Add(this.panelFilter1);
            this.gboOrderFilters.Location = new System.Drawing.Point(12, 204);
            this.gboOrderFilters.Name = "gboOrderFilters";
            this.gboOrderFilters.Size = new System.Drawing.Size(426, 174);
            this.gboOrderFilters.TabIndex = 74;
            this.gboOrderFilters.TabStop = false;
            this.gboOrderFilters.Text = "Order Filters";
            // 
            // btnResetOrderFilter
            // 
            this.btnResetOrderFilter.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnResetOrderFilter.Location = new System.Drawing.Point(307, 143);
            this.btnResetOrderFilter.Name = "btnResetOrderFilter";
            this.btnResetOrderFilter.Size = new System.Drawing.Size(56, 23);
            this.btnResetOrderFilter.TabIndex = 77;
            this.btnResetOrderFilter.Text = "Reset";
            this.btnResetOrderFilter.Click += new System.EventHandler(this.btnResetOrderFilter_Click);
            // 
            // btnApplyOrderFilter
            // 
            this.btnApplyOrderFilter.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnApplyOrderFilter.Location = new System.Drawing.Point(364, 143);
            this.btnApplyOrderFilter.Name = "btnApplyOrderFilter";
            this.btnApplyOrderFilter.Size = new System.Drawing.Size(56, 23);
            this.btnApplyOrderFilter.TabIndex = 75;
            this.btnApplyOrderFilter.Text = "Apply";
            this.btnApplyOrderFilter.Click += new System.EventHandler(this.btnApplyOrderFilter_Click);
            // 
            // panelFilter4
            // 
            this.panelFilter4.BackColor = System.Drawing.Color.Gainsboro;
            this.panelFilter4.Controls.Add(this.txtFilter4);
            this.panelFilter4.Controls.Add(this.lblFilter4);
            this.panelFilter4.Controls.Add(this.cboFilter4);
            this.panelFilter4.Controls.Add(this.rbAnd4);
            this.panelFilter4.Controls.Add(this.rbOr4);
            this.panelFilter4.Enabled = false;
            this.panelFilter4.Location = new System.Drawing.Point(6, 112);
            this.panelFilter4.Name = "panelFilter4";
            this.panelFilter4.Size = new System.Drawing.Size(414, 25);
            this.panelFilter4.TabIndex = 9;
            // 
            // txtFilter4
            // 
            this.txtFilter4.Location = new System.Drawing.Point(172, 2);
            this.txtFilter4.Name = "txtFilter4";
            this.txtFilter4.Size = new System.Drawing.Size(122, 20);
            this.txtFilter4.TabIndex = 81;
            this.txtFilter4.Visible = false;
            // 
            // lblFilter4
            // 
            this.lblFilter4.AutoSize = true;
            this.lblFilter4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilter4.Location = new System.Drawing.Point(3, 6);
            this.lblFilter4.Name = "lblFilter4";
            this.lblFilter4.Size = new System.Drawing.Size(18, 13);
            this.lblFilter4.TabIndex = 79;
            this.lblFilter4.Text = "4)";
            // 
            // cboFilter4
            // 
            this.cboFilter4.DisplayMember = "Name";
            this.cboFilter4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilter4.Items.AddRange(new object[] {
            "Price",
            "Quantity",
            "Buy Orders",
            "Sell Orders"});
            this.cboFilter4.Location = new System.Drawing.Point(27, 2);
            this.cboFilter4.Name = "cboFilter4";
            this.cboFilter4.Size = new System.Drawing.Size(139, 21);
            this.cboFilter4.TabIndex = 78;
            this.cboFilter4.SelectedIndexChanged += new System.EventHandler(this.cboFilter4_SelectedIndexChanged);
            // 
            // rbAnd4
            // 
            this.rbAnd4.AutoSize = true;
            this.rbAnd4.Location = new System.Drawing.Point(315, 4);
            this.rbAnd4.Name = "rbAnd4";
            this.rbAnd4.Size = new System.Drawing.Size(44, 17);
            this.rbAnd4.TabIndex = 2;
            this.rbAnd4.Text = "And";
            this.rbAnd4.UseVisualStyleBackColor = true;
            // 
            // rbOr4
            // 
            this.rbOr4.AutoSize = true;
            this.rbOr4.Checked = true;
            this.rbOr4.Location = new System.Drawing.Point(367, 4);
            this.rbOr4.Name = "rbOr4";
            this.rbOr4.Size = new System.Drawing.Size(36, 17);
            this.rbOr4.TabIndex = 1;
            this.rbOr4.TabStop = true;
            this.rbOr4.Text = "Or";
            this.rbOr4.UseVisualStyleBackColor = true;
            // 
            // panelFilter3
            // 
            this.panelFilter3.BackColor = System.Drawing.Color.Gainsboro;
            this.panelFilter3.Controls.Add(this.txtFilter3);
            this.panelFilter3.Controls.Add(this.lblFilter3);
            this.panelFilter3.Controls.Add(this.cboFilter3);
            this.panelFilter3.Controls.Add(this.rbAnd3);
            this.panelFilter3.Controls.Add(this.rbOr3);
            this.panelFilter3.Enabled = false;
            this.panelFilter3.Location = new System.Drawing.Point(6, 81);
            this.panelFilter3.Name = "panelFilter3";
            this.panelFilter3.Size = new System.Drawing.Size(414, 25);
            this.panelFilter3.TabIndex = 8;
            // 
            // txtFilter3
            // 
            this.txtFilter3.Location = new System.Drawing.Point(172, 2);
            this.txtFilter3.Name = "txtFilter3";
            this.txtFilter3.Size = new System.Drawing.Size(122, 20);
            this.txtFilter3.TabIndex = 80;
            this.txtFilter3.Visible = false;
            // 
            // lblFilter3
            // 
            this.lblFilter3.AutoSize = true;
            this.lblFilter3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilter3.Location = new System.Drawing.Point(3, 6);
            this.lblFilter3.Name = "lblFilter3";
            this.lblFilter3.Size = new System.Drawing.Size(18, 13);
            this.lblFilter3.TabIndex = 79;
            this.lblFilter3.Text = "3)";
            // 
            // cboFilter3
            // 
            this.cboFilter3.DisplayMember = "Name";
            this.cboFilter3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilter3.Items.AddRange(new object[] {
            "Price",
            "Quantity",
            "Buy Orders",
            "Sell Orders"});
            this.cboFilter3.Location = new System.Drawing.Point(27, 2);
            this.cboFilter3.Name = "cboFilter3";
            this.cboFilter3.Size = new System.Drawing.Size(139, 21);
            this.cboFilter3.TabIndex = 78;
            this.cboFilter3.SelectedIndexChanged += new System.EventHandler(this.cboFilter3_SelectedIndexChanged);
            // 
            // rbAnd3
            // 
            this.rbAnd3.AutoSize = true;
            this.rbAnd3.Location = new System.Drawing.Point(315, 4);
            this.rbAnd3.Name = "rbAnd3";
            this.rbAnd3.Size = new System.Drawing.Size(44, 17);
            this.rbAnd3.TabIndex = 2;
            this.rbAnd3.Text = "And";
            this.rbAnd3.UseVisualStyleBackColor = true;
            // 
            // rbOr3
            // 
            this.rbOr3.AutoSize = true;
            this.rbOr3.Checked = true;
            this.rbOr3.Location = new System.Drawing.Point(367, 4);
            this.rbOr3.Name = "rbOr3";
            this.rbOr3.Size = new System.Drawing.Size(36, 17);
            this.rbOr3.TabIndex = 1;
            this.rbOr3.TabStop = true;
            this.rbOr3.Text = "Or";
            this.rbOr3.UseVisualStyleBackColor = true;
            // 
            // panelFilter2
            // 
            this.panelFilter2.BackColor = System.Drawing.Color.Gainsboro;
            this.panelFilter2.Controls.Add(this.txtFilter2);
            this.panelFilter2.Controls.Add(this.lblFilter2);
            this.panelFilter2.Controls.Add(this.cboFilter2);
            this.panelFilter2.Controls.Add(this.rbAnd2);
            this.panelFilter2.Controls.Add(this.rbOr2);
            this.panelFilter2.Enabled = false;
            this.panelFilter2.Location = new System.Drawing.Point(6, 50);
            this.panelFilter2.Name = "panelFilter2";
            this.panelFilter2.Size = new System.Drawing.Size(414, 25);
            this.panelFilter2.TabIndex = 7;
            // 
            // txtFilter2
            // 
            this.txtFilter2.Location = new System.Drawing.Point(172, 2);
            this.txtFilter2.Name = "txtFilter2";
            this.txtFilter2.Size = new System.Drawing.Size(122, 20);
            this.txtFilter2.TabIndex = 79;
            this.txtFilter2.Visible = false;
            // 
            // lblFilter2
            // 
            this.lblFilter2.AutoSize = true;
            this.lblFilter2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilter2.Location = new System.Drawing.Point(3, 6);
            this.lblFilter2.Name = "lblFilter2";
            this.lblFilter2.Size = new System.Drawing.Size(18, 13);
            this.lblFilter2.TabIndex = 79;
            this.lblFilter2.Text = "2)";
            // 
            // cboFilter2
            // 
            this.cboFilter2.DisplayMember = "Name";
            this.cboFilter2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilter2.Items.AddRange(new object[] {
            "Price",
            "Quantity",
            "Buy Orders",
            "Sell Orders"});
            this.cboFilter2.Location = new System.Drawing.Point(27, 2);
            this.cboFilter2.Name = "cboFilter2";
            this.cboFilter2.Size = new System.Drawing.Size(139, 21);
            this.cboFilter2.TabIndex = 78;
            this.cboFilter2.SelectedIndexChanged += new System.EventHandler(this.cboFilter2_SelectedIndexChanged);
            // 
            // rbAnd2
            // 
            this.rbAnd2.AutoSize = true;
            this.rbAnd2.Location = new System.Drawing.Point(315, 4);
            this.rbAnd2.Name = "rbAnd2";
            this.rbAnd2.Size = new System.Drawing.Size(44, 17);
            this.rbAnd2.TabIndex = 2;
            this.rbAnd2.Text = "And";
            this.rbAnd2.UseVisualStyleBackColor = true;
            // 
            // rbOr2
            // 
            this.rbOr2.AutoSize = true;
            this.rbOr2.Checked = true;
            this.rbOr2.Location = new System.Drawing.Point(367, 4);
            this.rbOr2.Name = "rbOr2";
            this.rbOr2.Size = new System.Drawing.Size(36, 17);
            this.rbOr2.TabIndex = 1;
            this.rbOr2.TabStop = true;
            this.rbOr2.Text = "Or";
            this.rbOr2.UseVisualStyleBackColor = true;
            // 
            // panelFilter1
            // 
            this.panelFilter1.BackColor = System.Drawing.Color.Gainsboro;
            this.panelFilter1.Controls.Add(this.rbAnd1);
            this.panelFilter1.Controls.Add(this.rbOr1);
            this.panelFilter1.Controls.Add(this.txtFilter1);
            this.panelFilter1.Controls.Add(this.lblFilter1);
            this.panelFilter1.Controls.Add(this.cboFilter1);
            this.panelFilter1.Location = new System.Drawing.Point(6, 19);
            this.panelFilter1.Name = "panelFilter1";
            this.panelFilter1.Size = new System.Drawing.Size(414, 25);
            this.panelFilter1.TabIndex = 6;
            // 
            // rbAnd1
            // 
            this.rbAnd1.AutoSize = true;
            this.rbAnd1.Location = new System.Drawing.Point(315, 4);
            this.rbAnd1.Name = "rbAnd1";
            this.rbAnd1.Size = new System.Drawing.Size(44, 17);
            this.rbAnd1.TabIndex = 80;
            this.rbAnd1.Text = "And";
            this.rbAnd1.UseVisualStyleBackColor = true;
            // 
            // rbOr1
            // 
            this.rbOr1.AutoSize = true;
            this.rbOr1.Checked = true;
            this.rbOr1.Location = new System.Drawing.Point(367, 4);
            this.rbOr1.Name = "rbOr1";
            this.rbOr1.Size = new System.Drawing.Size(36, 17);
            this.rbOr1.TabIndex = 79;
            this.rbOr1.TabStop = true;
            this.rbOr1.Text = "Or";
            this.rbOr1.UseVisualStyleBackColor = true;
            // 
            // txtFilter1
            // 
            this.txtFilter1.Location = new System.Drawing.Point(172, 2);
            this.txtFilter1.Name = "txtFilter1";
            this.txtFilter1.Size = new System.Drawing.Size(122, 20);
            this.txtFilter1.TabIndex = 78;
            this.txtFilter1.Visible = false;
            // 
            // lblFilter1
            // 
            this.lblFilter1.AutoSize = true;
            this.lblFilter1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilter1.Location = new System.Drawing.Point(3, 6);
            this.lblFilter1.Name = "lblFilter1";
            this.lblFilter1.Size = new System.Drawing.Size(18, 13);
            this.lblFilter1.TabIndex = 77;
            this.lblFilter1.Text = "1)";
            // 
            // cboFilter1
            // 
            this.cboFilter1.DisplayMember = "Name";
            this.cboFilter1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilter1.Items.AddRange(new object[] {
            "Price",
            "Quantity",
            "Buy Orders",
            "Sell Orders"});
            this.cboFilter1.Location = new System.Drawing.Point(27, 2);
            this.cboFilter1.Name = "cboFilter1";
            this.cboFilter1.Size = new System.Drawing.Size(139, 21);
            this.cboFilter1.TabIndex = 51;
            this.cboFilter1.SelectedIndexChanged += new System.EventHandler(this.cboFilter1_SelectedIndexChanged);
            // 
            // gboOrderAuditTrail
            // 
            this.gboOrderAuditTrail.Controls.Add(this.lboAuditLog);
            this.gboOrderAuditTrail.Location = new System.Drawing.Point(12, 384);
            this.gboOrderAuditTrail.Name = "gboOrderAuditTrail";
            this.gboOrderAuditTrail.Size = new System.Drawing.Size(426, 174);
            this.gboOrderAuditTrail.TabIndex = 75;
            this.gboOrderAuditTrail.TabStop = false;
            this.gboOrderAuditTrail.Text = " Order Add/Delete Audit Trail";
            // 
            // lboAuditLog
            // 
            this.lboAuditLog.FullRowSelect = true;
            this.lboAuditLog.GridLines = true;
            this.lboAuditLog.Location = new System.Drawing.Point(6, 19);
            this.lboAuditLog.Name = "lboAuditLog";
            this.lboAuditLog.Size = new System.Drawing.Size(414, 149);
            this.lboAuditLog.TabIndex = 1;
            this.lboAuditLog.UseCompatibleStateImageBehavior = false;
            this.lboAuditLog.View = System.Windows.Forms.View.Details;
            // 
            // statusBar1
            // 
            this.statusBar1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusBar1.Location = new System.Drawing.Point(0, 569);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(452, 22);
            this.statusBar1.TabIndex = 76;
            this.statusBar1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(430, 17);
            this.toolStripStatusLabel1.Text = "Drag and Drop an instrument from the Market Grid in X_TRADER to this window.";
            // 
            // frmOrderFilter
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 591);
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.gboOrderAuditTrail);
            this.Controls.Add(this.gboOrderFilters);
            this.Controls.Add(this.lblNotProduction);
            this.Controls.Add(this.lblWarning);
            this.Controls.Add(this.gboLimitOrderEntry);
            this.Controls.Add(this.gboInstrumentInfo);
            this.Enabled = false;
            this.Menu = this.mainMenu1;
            this.Name = "frmOrderFilter";
            this.Text = "OrderFilter";
            this.Load += new System.EventHandler(this.frmOrderFilter_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.frmOrderFilter_DragDrop);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.frmOrderFilter_DragOver);
            this.gboLimitOrderEntry.ResumeLayout(false);
            this.gboLimitOrderEntry.PerformLayout();
            this.gboInstrumentInfo.ResumeLayout(false);
            this.gboInstrumentInfo.PerformLayout();
            this.gboOrderFilters.ResumeLayout(false);
            this.panelFilter4.ResumeLayout(false);
            this.panelFilter4.PerformLayout();
            this.panelFilter3.ResumeLayout(false);
            this.panelFilter3.PerformLayout();
            this.panelFilter2.ResumeLayout(false);
            this.panelFilter2.PerformLayout();
            this.panelFilter1.ResumeLayout(false);
            this.panelFilter1.PerformLayout();
            this.gboOrderAuditTrail.ResumeLayout(false);
            this.statusBar1.ResumeLayout(false);
            this.statusBar1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem mnuAbout;
        private System.Windows.Forms.Label lblNotProduction;
        private System.Windows.Forms.Label lblWarning;
        private System.Windows.Forms.GroupBox gboLimitOrderEntry;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.ComboBox cboCustomer;
        private System.Windows.Forms.Button btnSell;
        private System.Windows.Forms.Button btnBuy;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.GroupBox gboInstrumentInfo;
        private System.Windows.Forms.Label lblProductType;
        private System.Windows.Forms.TextBox txtProduct;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.Label lblExchange;
        private System.Windows.Forms.TextBox txtContract;
        private System.Windows.Forms.Label lblContract;
        private System.Windows.Forms.TextBox txtExchange;
        private System.Windows.Forms.TextBox txtProductType;
        private System.Windows.Forms.GroupBox gboOrderFilters;
        private System.Windows.Forms.Panel panelFilter4;
        private System.Windows.Forms.RadioButton rbAnd4;
        private System.Windows.Forms.RadioButton rbOr4;
        private System.Windows.Forms.Panel panelFilter3;
        private System.Windows.Forms.RadioButton rbAnd3;
        private System.Windows.Forms.RadioButton rbOr3;
        private System.Windows.Forms.Panel panelFilter2;
        private System.Windows.Forms.RadioButton rbAnd2;
        private System.Windows.Forms.RadioButton rbOr2;
        private System.Windows.Forms.Panel panelFilter1;
        private System.Windows.Forms.Button btnApplyOrderFilter;
        private System.Windows.Forms.GroupBox gboOrderAuditTrail;
        private System.Windows.Forms.ListView lboAuditLog;
        private System.Windows.Forms.StatusStrip statusBar1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Label lblOrderFeed;
        private System.Windows.Forms.ComboBox cboOrderFeed;
        private System.Windows.Forms.ComboBox cboFilter1;
        private System.Windows.Forms.Button btnResetOrderFilter;
        private System.Windows.Forms.Label lblFilter4;
        private System.Windows.Forms.ComboBox cboFilter4;
        private System.Windows.Forms.Label lblFilter3;
        private System.Windows.Forms.ComboBox cboFilter3;
        private System.Windows.Forms.Label lblFilter2;
        private System.Windows.Forms.ComboBox cboFilter2;
        private System.Windows.Forms.Label lblFilter1;
        private System.Windows.Forms.TextBox txtFilter4;
        private System.Windows.Forms.TextBox txtFilter3;
        private System.Windows.Forms.TextBox txtFilter2;
        private System.Windows.Forms.TextBox txtFilter1;
        private System.Windows.Forms.RadioButton rbAnd1;
        private System.Windows.Forms.RadioButton rbOr1;
    }
}

