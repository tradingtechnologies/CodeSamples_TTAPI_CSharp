using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace TTAPI_Samples
{
	/// <summary>
	/// Summary description for AboutDTS.
	/// </summary>
	public class AboutDTS : System.Windows.Forms.Form
	{

		public AboutDTS()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.textVersion.Text = Convert.ToString(System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);
			this.textVersionDate.Text = "January 18, 2013";
			this.textAuthors.Text = "Brian Fortman, Josh Bramlett";
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label textVersion;
        private System.Windows.Forms.Label textVersionDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label textAuthors;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel linkLabelDevnet;
        private System.Windows.Forms.LinkLabel linkLabelWWW;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutDTS));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textVersion = new System.Windows.Forms.Label();
            this.textVersionDate = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textAuthors = new System.Windows.Forms.Label();
            this.linkLabelDevnet = new System.Windows.Forms.LinkLabel();
            this.linkLabelWWW = new System.Windows.Forms.LinkLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.textVersion);
            this.panel1.Controls.Add(this.textVersionDate);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.textAuthors);
            this.panel1.Controls.Add(this.linkLabelDevnet);
            this.panel1.Controls.Add(this.linkLabelWWW);
            this.panel1.Location = new System.Drawing.Point(8, 104);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(368, 160);
            this.panel1.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(0, 136);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(96, 16);
            this.label9.TabIndex = 15;
            this.label9.Text = "Main Website:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(0, 120);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 16);
            this.label7.TabIndex = 14;
            this.label7.Text = "Support Website:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textVersion
            // 
            this.textVersion.Location = new System.Drawing.Point(112, 8);
            this.textVersion.Name = "textVersion";
            this.textVersion.Size = new System.Drawing.Size(224, 16);
            this.textVersion.TabIndex = 13;
            this.textVersion.Text = "a.b.c";
            this.textVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textVersionDate
            // 
            this.textVersionDate.Location = new System.Drawing.Point(112, 24);
            this.textVersionDate.Name = "textVersionDate";
            this.textVersionDate.Size = new System.Drawing.Size(224, 16);
            this.textVersionDate.TabIndex = 12;
            this.textVersionDate.Text = "Xyy 00, 0000";
            this.textVersionDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(51, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "Date:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(48, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Version:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(48, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 16);
            this.label5.TabIndex = 11;
            this.label5.Text = "Authors:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textAuthors
            // 
            this.textAuthors.Location = new System.Drawing.Point(112, 48);
            this.textAuthors.Name = "textAuthors";
            this.textAuthors.Size = new System.Drawing.Size(224, 64);
            this.textAuthors.TabIndex = 9;
            this.textAuthors.Text = "Name Name, Name Name";
            // 
            // linkLabelDevnet
            // 
            this.linkLabelDevnet.Location = new System.Drawing.Point(112, 120);
            this.linkLabelDevnet.Name = "linkLabelDevnet";
            this.linkLabelDevnet.Size = new System.Drawing.Size(200, 16);
            this.linkLabelDevnet.TabIndex = 4;
            this.linkLabelDevnet.TabStop = true;
            this.linkLabelDevnet.Text = "http://devnet.tradingtechnologies.com";
            this.linkLabelDevnet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabelDevnet.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelDevnet_LinkClicked);
            // 
            // linkLabelWWW
            // 
            this.linkLabelWWW.Location = new System.Drawing.Point(112, 136);
            this.linkLabelWWW.Name = "linkLabelWWW";
            this.linkLabelWWW.Size = new System.Drawing.Size(192, 16);
            this.linkLabelWWW.TabIndex = 2;
            this.linkLabelWWW.TabStop = true;
            this.linkLabelWWW.Text = "http://www.tradingtechnologies.com";
            this.linkLabelWWW.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabelWWW.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelWWW_LinkClicked);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Location = new System.Drawing.Point(8, 96);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(368, 1);
            this.panel2.TabIndex = 18;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(400, 4);
            this.panel4.TabIndex = 15;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel5.Location = new System.Drawing.Point(-2, -2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(400, 4);
            this.panel5.TabIndex = 14;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Location = new System.Drawing.Point(-2, -2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(400, 4);
            this.panel3.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label8.Location = new System.Drawing.Point(192, 4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(188, 16);
            this.label8.TabIndex = 20;
            this.label8.Text = "Development Technical Support";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.panel7);
            this.panel6.Controls.Add(this.panel9);
            this.panel6.Location = new System.Drawing.Point(8, 272);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(368, 1);
            this.panel6.TabIndex = 19;
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel7.Controls.Add(this.panel8);
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(400, 4);
            this.panel7.TabIndex = 15;
            // 
            // panel8
            // 
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel8.Location = new System.Drawing.Point(-2, -2);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(400, 4);
            this.panel8.TabIndex = 14;
            // 
            // panel9
            // 
            this.panel9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel9.Location = new System.Drawing.Point(-2, -2);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(400, 4);
            this.panel9.TabIndex = 14;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(8, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(224, 72);
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // AboutDTS
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(384, 280);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AboutDTS";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About PriceUpdateManual";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		private void linkLabelDevnet_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("http://devnet.tradingtechnologies.com");
		}

		private void linkLabelWWW_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.tradingtechnologies.com");		
		}

        /// <summary>
        /// Verify the application build settings match the architecture of the TT API installed.
        /// </summary>
        public static void TTAPIArchitectureCheck()
        {
            try
            {
                System.Diagnostics.FileVersionInfo fileVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo((new System.IO.FileInfo("TradingTechnologies.TTAPI.dll")).FullName);

                if (fileVersionInfo != null && fileVersionInfo.FileName != String.Empty)
                {
                    System.Reflection.Assembly appAssembly = System.Reflection.Assembly.GetExecutingAssembly();
                    System.Reflection.Assembly apiAssembly = System.Reflection.Assembly.ReflectionOnlyLoadFrom(fileVersionInfo.FileName);

                    if (appAssembly != null && apiAssembly != null)
                    {
                        System.Reflection.PortableExecutableKinds appKinds, apiKinds;
                        System.Reflection.ImageFileMachine appImgFileMachine, apiImgFileMachine;

                        appAssembly.ManifestModule.GetPEKind(out appKinds, out appImgFileMachine);
                        apiAssembly.ManifestModule.GetPEKind(out apiKinds, out apiImgFileMachine);

                        if (!appKinds.HasFlag(apiKinds))
                        {
                            MessageBox.Show(String.Format("WARNING: This application must be compiled as a {0} application to run with a {0} version of TT API.",
                                (apiKinds.HasFlag(System.Reflection.PortableExecutableKinds.Required32Bit) ? "32Bit" : "64bit")));
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(String.Format("ERROR: An error occured while attempting to verify the application build settings match the architecture of the TT API installed. {0}", err.Message));
            }
        }
	}
}
