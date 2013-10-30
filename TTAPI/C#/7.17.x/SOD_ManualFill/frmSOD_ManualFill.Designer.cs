namespace TTAPI_Samples
{
    partial class frmSOD_ManualFill
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
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.sbaStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnPublish = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDeleteSOD = new System.Windows.Forms.Button();
            this.gridStartOfDay = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtManualFillAudit = new System.Windows.Forms.TextBox();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.mnuAbout = new System.Windows.Forms.MenuItem();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridStartOfDay)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Location = new System.Drawing.Point(6, 19);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.propertyGrid1.Size = new System.Drawing.Size(305, 405);
            this.propertyGrid1.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sbaStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 481);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1192, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // sbaStatus
            // 
            this.sbaStatus.Name = "sbaStatus";
            this.sbaStatus.Size = new System.Drawing.Size(275, 17);
            this.sbaStatus.Text = "X_TRADER must be running to use this application.";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnPublish);
            this.groupBox1.Controls.Add(this.propertyGrid1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(317, 459);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SOD / Manual Fill Profile";
            // 
            // btnPublish
            // 
            this.btnPublish.Location = new System.Drawing.Point(236, 430);
            this.btnPublish.Name = "btnPublish";
            this.btnPublish.Size = new System.Drawing.Size(75, 23);
            this.btnPublish.TabIndex = 1;
            this.btnPublish.Text = "Publish";
            this.btnPublish.UseVisualStyleBackColor = true;
            this.btnPublish.Click += new System.EventHandler(this.btnPublish_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gridStartOfDay);
            this.groupBox2.Controls.Add(this.btnDeleteSOD);
            this.groupBox2.Location = new System.Drawing.Point(335, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(845, 265);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Active SOD Records";
            // 
            // btnDeleteSOD
            // 
            this.btnDeleteSOD.Location = new System.Drawing.Point(764, 236);
            this.btnDeleteSOD.Name = "btnDeleteSOD";
            this.btnDeleteSOD.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteSOD.TabIndex = 2;
            this.btnDeleteSOD.Text = "Delete SOD";
            this.btnDeleteSOD.UseVisualStyleBackColor = true;
            this.btnDeleteSOD.Click += new System.EventHandler(this.btnDeleteSOD_Click);
            // 
            // gridStartOfDay
            // 
            this.gridStartOfDay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridStartOfDay.Location = new System.Drawing.Point(6, 19);
            this.gridStartOfDay.Name = "gridStartOfDay";
            this.gridStartOfDay.Size = new System.Drawing.Size(833, 211);
            this.gridStartOfDay.TabIndex = 3;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtManualFillAudit);
            this.groupBox3.Location = new System.Drawing.Point(335, 283);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(845, 188);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Manual Fill Audit Trail";
            // 
            // txtManualFillAudit
            // 
            this.txtManualFillAudit.Location = new System.Drawing.Point(6, 19);
            this.txtManualFillAudit.Multiline = true;
            this.txtManualFillAudit.Name = "txtManualFillAudit";
            this.txtManualFillAudit.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtManualFillAudit.Size = new System.Drawing.Size(833, 163);
            this.txtManualFillAudit.TabIndex = 0;
            this.txtManualFillAudit.WordWrap = false;
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
            // frmSOD_ManualFill
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 503);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Enabled = false;
            this.Menu = this.mainMenu1;
            this.Name = "frmSOD_ManualFill";
            this.Text = "SOD & ManualFill";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSOD_ManualFill_FormClosing);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.frmSOD_ManualFill_DragDrop);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.frmSOD_ManualFill_DragOver);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridStartOfDay)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel sbaStatus;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnPublish;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnDeleteSOD;
        private System.Windows.Forms.DataGridView gridStartOfDay;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtManualFillAudit;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem mnuAbout;
    }
}

