namespace TTAPI_Samples
{
    using TradingTechnologies.TTAPI;

    partial class frmFillUpdate
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

            // Shut down the API
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
            this.gbFillTree = new System.Windows.Forms.GroupBox();
            this.cboAutoScrollTree = new System.Windows.Forms.CheckBox();
            this.treeFill = new System.Windows.Forms.TreeView();
            this.gbNotificationList = new System.Windows.Forms.GroupBox();
            this.cboAutoScrollList = new System.Windows.Forms.CheckBox();
            this.lbNotificationList = new System.Windows.Forms.ListBox();
            this.statusBar1.SuspendLayout();
            this.gbFillTree.SuspendLayout();
            this.gbNotificationList.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusBar1
            // 
            this.statusBar1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusBar1.Location = new System.Drawing.Point(0, 602);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(694, 22);
            this.statusBar1.TabIndex = 31;
            this.statusBar1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
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
            this.lblNotProduction.Size = new System.Drawing.Size(670, 14);
            this.lblNotProduction.TabIndex = 115;
            this.lblNotProduction.Text = "This sample is NOT to be used in production or during conformance testing.";
            this.lblNotProduction.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblWarning
            // 
            this.lblWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWarning.Location = new System.Drawing.Point(12, 9);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(670, 23);
            this.lblWarning.TabIndex = 114;
            this.lblWarning.Text = "WARNING!";
            this.lblWarning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbFillTree
            // 
            this.gbFillTree.Controls.Add(this.cboAutoScrollTree);
            this.gbFillTree.Controls.Add(this.treeFill);
            this.gbFillTree.Location = new System.Drawing.Point(12, 62);
            this.gbFillTree.Name = "gbFillTree";
            this.gbFillTree.Size = new System.Drawing.Size(670, 262);
            this.gbFillTree.TabIndex = 117;
            this.gbFillTree.TabStop = false;
            this.gbFillTree.Text = "Fill Tree";
            // 
            // cboAutoScrollTree
            // 
            this.cboAutoScrollTree.AutoSize = true;
            this.cboAutoScrollTree.Checked = true;
            this.cboAutoScrollTree.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cboAutoScrollTree.Location = new System.Drawing.Point(7, 238);
            this.cboAutoScrollTree.Name = "cboAutoScrollTree";
            this.cboAutoScrollTree.Size = new System.Drawing.Size(151, 17);
            this.cboAutoScrollTree.TabIndex = 1;
            this.cboAutoScrollTree.Text = "AutoScroll To End Of Tree";
            this.cboAutoScrollTree.UseVisualStyleBackColor = true;
            // 
            // treeFill
            // 
            this.treeFill.Location = new System.Drawing.Point(6, 19);
            this.treeFill.Name = "treeFill";
            this.treeFill.Size = new System.Drawing.Size(658, 212);
            this.treeFill.TabIndex = 0;
            // 
            // gbNotificationList
            // 
            this.gbNotificationList.Controls.Add(this.cboAutoScrollList);
            this.gbNotificationList.Controls.Add(this.lbNotificationList);
            this.gbNotificationList.Location = new System.Drawing.Point(12, 330);
            this.gbNotificationList.Name = "gbNotificationList";
            this.gbNotificationList.Size = new System.Drawing.Size(670, 262);
            this.gbNotificationList.TabIndex = 118;
            this.gbNotificationList.TabStop = false;
            this.gbNotificationList.Text = "Fill Notification List";
            // 
            // cboAutoScrollList
            // 
            this.cboAutoScrollList.AutoSize = true;
            this.cboAutoScrollList.Checked = true;
            this.cboAutoScrollList.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cboAutoScrollList.Location = new System.Drawing.Point(7, 237);
            this.cboAutoScrollList.Name = "cboAutoScrollList";
            this.cboAutoScrollList.Size = new System.Drawing.Size(145, 17);
            this.cboAutoScrollList.TabIndex = 2;
            this.cboAutoScrollList.Text = "AutoScroll To End Of List";
            this.cboAutoScrollList.UseVisualStyleBackColor = true;
            // 
            // lbNotificationList
            // 
            this.lbNotificationList.FormattingEnabled = true;
            this.lbNotificationList.Location = new System.Drawing.Point(6, 19);
            this.lbNotificationList.Name = "lbNotificationList";
            this.lbNotificationList.Size = new System.Drawing.Size(658, 212);
            this.lbNotificationList.TabIndex = 0;
            // 
            // frmFillUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 624);
            this.Controls.Add(this.gbNotificationList);
            this.Controls.Add(this.gbFillTree);
            this.Controls.Add(this.lblNotProduction);
            this.Controls.Add(this.lblWarning);
            this.Controls.Add(this.statusBar1);
            this.Enabled = false;
            this.Menu = this.mainMenu1;
            this.Name = "frmFillUpdate";
            this.Text = "FillUpdate";
            this.statusBar1.ResumeLayout(false);
            this.statusBar1.PerformLayout();
            this.gbFillTree.ResumeLayout(false);
            this.gbFillTree.PerformLayout();
            this.gbNotificationList.ResumeLayout(false);
            this.gbNotificationList.PerformLayout();
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
        private System.Windows.Forms.GroupBox gbFillTree;
        private System.Windows.Forms.TreeView treeFill;
        private System.Windows.Forms.GroupBox gbNotificationList;
        private System.Windows.Forms.ListBox lbNotificationList;
        private System.Windows.Forms.CheckBox cboAutoScrollTree;
        private System.Windows.Forms.CheckBox cboAutoScrollList;
    }
}

