namespace TTAPI_Samples
{
    partial class frmMarketExplorer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMarketExplorer));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxSearchText = new System.Windows.Forms.TextBox();
            this.listViewMarketList = new System.Windows.Forms.ListView();
            this.imageListGatewayIcons = new System.Windows.Forms.ImageList(this.components);
            this.listViewProductTypeList = new System.Windows.Forms.ListView();
            this.imageListProductTypes = new System.Windows.Forms.ImageList(this.components);
            this.treeViewProductList = new System.Windows.Forms.TreeView();
            this.listViewMarketFeeds = new System.Windows.Forms.ListView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 487);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(395, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.textBoxSearchText);
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(369, 42);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Contains:";
            // 
            // textBoxSearchText
            // 
            this.textBoxSearchText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSearchText.Location = new System.Drawing.Point(63, 13);
            this.textBoxSearchText.Name = "textBoxSearchText";
            this.textBoxSearchText.Size = new System.Drawing.Size(300, 20);
            this.textBoxSearchText.TabIndex = 2;
            this.textBoxSearchText.TextChanged += new System.EventHandler(this.textBoxSearchText_TextChanged);
            // 
            // listViewExchangeList
            // 
            this.listViewMarketList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewMarketList.HideSelection = false;
            this.listViewMarketList.Location = new System.Drawing.Point(12, 75);
            this.listViewMarketList.Name = "listViewExchangeList";
            this.listViewMarketList.Size = new System.Drawing.Size(109, 210);
            this.listViewMarketList.SmallImageList = this.imageListGatewayIcons;
            this.listViewMarketList.TabIndex = 3;
            this.listViewMarketList.UseCompatibleStateImageBehavior = false;
            this.listViewMarketList.View = System.Windows.Forms.View.Details;
            this.listViewMarketList.SelectedIndexChanged += new System.EventHandler(this.listViewMarketList_SelectedIndexChanged);
            // 
            // imageListGatewayIcons
            // 
            this.imageListGatewayIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListGatewayIcons.ImageStream")));
            this.imageListGatewayIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListGatewayIcons.Images.SetKeyName(0, "server-down.ico");
            this.imageListGatewayIcons.Images.SetKeyName(1, "server-partial.ico");
            this.imageListGatewayIcons.Images.SetKeyName(2, "server-up.ico");
            // 
            // listViewProductTypeList
            // 
            this.listViewProductTypeList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewProductTypeList.HideSelection = false;
            this.listViewProductTypeList.Location = new System.Drawing.Point(12, 291);
            this.listViewProductTypeList.Name = "listViewProductTypeList";
            this.listViewProductTypeList.Size = new System.Drawing.Size(109, 183);
            this.listViewProductTypeList.SmallImageList = this.imageListProductTypes;
            this.listViewProductTypeList.TabIndex = 4;
            this.listViewProductTypeList.UseCompatibleStateImageBehavior = false;
            this.listViewProductTypeList.View = System.Windows.Forms.View.Details;
            this.listViewProductTypeList.SelectedIndexChanged += new System.EventHandler(this.listViewProductTypeList_SelectedIndexChanged);
            // 
            // imageListProductTypes
            // 
            this.imageListProductTypes.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListProductTypes.ImageStream")));
            this.imageListProductTypes.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListProductTypes.Images.SetKeyName(0, "futures.ico");
            this.imageListProductTypes.Images.SetKeyName(1, "fspread.ico");
            this.imageListProductTypes.Images.SetKeyName(2, "ostrategies.ico");
            this.imageListProductTypes.Images.SetKeyName(3, "bonds.ico");
            this.imageListProductTypes.Images.SetKeyName(4, "energy.ico");
            this.imageListProductTypes.Images.SetKeyName(5, "forex.ico");
            this.imageListProductTypes.Images.SetKeyName(6, "stocks.ico");
            this.imageListProductTypes.Images.SetKeyName(7, "options.ico");
            this.imageListProductTypes.Images.SetKeyName(8, "warrant.ico");
            this.imageListProductTypes.Images.SetKeyName(9, "ndf.ico");
            this.imageListProductTypes.Images.SetKeyName(10, "swap.ico");
            this.imageListProductTypes.Images.SetKeyName(11, "fspread.ico");
            // 
            // treeViewProductList
            // 
            this.treeViewProductList.FullRowSelect = true;
            this.treeViewProductList.HideSelection = false;
            this.treeViewProductList.ImageIndex = 0;
            this.treeViewProductList.ImageList = this.imageListProductTypes;
            this.treeViewProductList.Location = new System.Drawing.Point(127, 75);
            this.treeViewProductList.Name = "treeViewProductList";
            this.treeViewProductList.SelectedImageIndex = 0;
            this.treeViewProductList.ShowNodeToolTips = true;
            this.treeViewProductList.Size = new System.Drawing.Size(254, 269);
            this.treeViewProductList.TabIndex = 5;
            this.treeViewProductList.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewProductList_BeforeExpand);
            this.treeViewProductList.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeViewProductList_ItemDrag);
            // 
            // listViewMarketFeeds
            // 
            this.listViewMarketFeeds.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewMarketFeeds.HideSelection = false;
            this.listViewMarketFeeds.Location = new System.Drawing.Point(127, 350);
            this.listViewMarketFeeds.Name = "listViewMarketFeeds";
            this.listViewMarketFeeds.Size = new System.Drawing.Size(254, 124);
            this.listViewMarketFeeds.SmallImageList = this.imageListGatewayIcons;
            this.listViewMarketFeeds.TabIndex = 3;
            this.listViewMarketFeeds.UseCompatibleStateImageBehavior = false;
            this.listViewMarketFeeds.View = System.Windows.Forms.View.Details;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(395, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.aboutToolStripMenuItem.Text = "About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // MarketExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 509);
            this.Controls.Add(this.treeViewProductList);
            this.Controls.Add(this.listViewProductTypeList);
            this.Controls.Add(this.listViewMarketFeeds);
            this.Controls.Add(this.listViewMarketList);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(411, 547);
            this.MinimumSize = new System.Drawing.Size(411, 547);
            this.Name = "MarketExplorer";
            this.Text = "Market Explorer";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxSearchText;
        private System.Windows.Forms.ListView listViewMarketList;
        private System.Windows.Forms.ListView listViewProductTypeList;
        private System.Windows.Forms.TreeView treeViewProductList;
        private System.Windows.Forms.ImageList imageListGatewayIcons;
        private System.Windows.Forms.ImageList imageListProductTypes;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ListView listViewMarketFeeds;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    }
}
