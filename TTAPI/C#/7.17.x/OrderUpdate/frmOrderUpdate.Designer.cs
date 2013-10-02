namespace TTAPI_Samples
{
    partial class frmOrderUpdate
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
            this.gboAuditTrail = new System.Windows.Forms.GroupBox();
            this.lboAuditLog = new System.Windows.Forms.ListView();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusBar1 = new System.Windows.Forms.StatusStrip();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.mnuAbout = new System.Windows.Forms.MenuItem();
            this.gboLogin = new System.Windows.Forms.GroupBox();
            this.lblNotProduction = new System.Windows.Forms.Label();
            this.lblWarning = new System.Windows.Forms.Label();
            this.gboAuditTrail.SuspendLayout();
            this.statusBar1.SuspendLayout();
            this.gboLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // gboAuditTrail
            // 
            this.gboAuditTrail.Controls.Add(this.lboAuditLog);
            this.gboAuditTrail.Location = new System.Drawing.Point(223, 51);
            this.gboAuditTrail.Name = "gboAuditTrail";
            this.gboAuditTrail.Size = new System.Drawing.Size(668, 186);
            this.gboAuditTrail.TabIndex = 39;
            this.gboAuditTrail.TabStop = false;
            this.gboAuditTrail.Text = "Order Audit Trail";
            // 
            // lboAuditLog
            // 
            this.lboAuditLog.FullRowSelect = true;
            this.lboAuditLog.GridLines = true;
            this.lboAuditLog.Location = new System.Drawing.Point(6, 19);
            this.lboAuditLog.Name = "lboAuditLog";
            this.lboAuditLog.Size = new System.Drawing.Size(656, 161);
            this.lboAuditLog.TabIndex = 0;
            this.lboAuditLog.UseCompatibleStateImageBehavior = false;
            this.lboAuditLog.View = System.Windows.Forms.View.Details;
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(83, 45);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(111, 20);
            this.txtPassword.TabIndex = 33;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 38;
            this.label1.Text = "Password";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(119, 71);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 35;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(364, 17);
            this.toolStripStatusLabel1.Text = "Enter the Universal Login information and click the Connect button.";
            // 
            // statusBar1
            // 
            this.statusBar1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusBar1.Location = new System.Drawing.Point(0, 249);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(903, 22);
            this.statusBar1.TabIndex = 37;
            this.statusBar1.Text = "statusStrip1";
            // 
            // txtUsername
            // 
            this.txtUsername.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.Location = new System.Drawing.Point(83, 19);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(111, 20);
            this.txtUsername.TabIndex = 30;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(11, 22);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(55, 13);
            this.label15.TabIndex = 36;
            this.label15.Text = "Username";
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
            // gboLogin
            // 
            this.gboLogin.Controls.Add(this.txtUsername);
            this.gboLogin.Controls.Add(this.txtPassword);
            this.gboLogin.Controls.Add(this.label15);
            this.gboLogin.Controls.Add(this.label1);
            this.gboLogin.Controls.Add(this.btnConnect);
            this.gboLogin.Location = new System.Drawing.Point(12, 57);
            this.gboLogin.Name = "gboLogin";
            this.gboLogin.Size = new System.Drawing.Size(200, 180);
            this.gboLogin.TabIndex = 40;
            this.gboLogin.TabStop = false;
            this.gboLogin.Text = "Universal Login";
            // 
            // lblNotProduction
            // 
            this.lblNotProduction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotProduction.Location = new System.Drawing.Point(12, 34);
            this.lblNotProduction.Name = "lblNotProduction";
            this.lblNotProduction.Size = new System.Drawing.Size(879, 14);
            this.lblNotProduction.TabIndex = 62;
            this.lblNotProduction.Text = "This sample is NOT to be used in production or during conformance testing.";
            this.lblNotProduction.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblWarning
            // 
            this.lblWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWarning.Location = new System.Drawing.Point(12, 9);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(879, 23);
            this.lblWarning.TabIndex = 61;
            this.lblWarning.Text = "WARNING!";
            this.lblWarning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmMonitorOrders
            // 
            this.AcceptButton = this.btnConnect;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(903, 271);
            this.Controls.Add(this.lblNotProduction);
            this.Controls.Add(this.lblWarning);
            this.Controls.Add(this.gboLogin);
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.gboAuditTrail);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Menu = this.mainMenu1;
            this.Name = "frmMonitorOrders";
            this.Text = "OrderUpdate";
            this.Load += new System.EventHandler(this.frmMonitorOrders_Load);
            this.gboAuditTrail.ResumeLayout(false);
            this.statusBar1.ResumeLayout(false);
            this.statusBar1.PerformLayout();
            this.gboLogin.ResumeLayout(false);
            this.gboLogin.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gboAuditTrail;
        private System.Windows.Forms.ListView lboAuditLog;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.StatusStrip statusBar1;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem mnuAbout;
        private System.Windows.Forms.GroupBox gboLogin;
        private System.Windows.Forms.Label lblNotProduction;
        private System.Windows.Forms.Label lblWarning;
    }
}

