namespace TTAPI_Samples
{
    partial class SpreadDetailsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxSpreadName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxBasedOn = new System.Windows.Forms.ComboBox();
            this.comboBoxSpreadLTP = new System.Windows.Forms.ComboBox();
            this.comboBoxLegColor = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxSlopSettings = new System.Windows.Forms.ComboBox();
            this.dataGridViewSpreadDetails = new System.Windows.Forms.DataGridView();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSpreadDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Spread Name";
            // 
            // textBoxSpreadName
            // 
            this.textBoxSpreadName.Location = new System.Drawing.Point(91, 10);
            this.textBoxSpreadName.Name = "textBoxSpreadName";
            this.textBoxSpreadName.Size = new System.Drawing.Size(375, 20);
            this.textBoxSpreadName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Based On";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Spread LTP";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Leg Color";
            // 
            // comboBoxBasedOn
            // 
            this.comboBoxBasedOn.FormattingEnabled = true;
            this.comboBoxBasedOn.Location = new System.Drawing.Point(91, 36);
            this.comboBoxBasedOn.Name = "comboBoxBasedOn";
            this.comboBoxBasedOn.Size = new System.Drawing.Size(121, 21);
            this.comboBoxBasedOn.TabIndex = 2;
            // 
            // comboBoxSpreadLTP
            // 
            this.comboBoxSpreadLTP.FormattingEnabled = true;
            this.comboBoxSpreadLTP.Location = new System.Drawing.Point(91, 63);
            this.comboBoxSpreadLTP.Name = "comboBoxSpreadLTP";
            this.comboBoxSpreadLTP.Size = new System.Drawing.Size(121, 21);
            this.comboBoxSpreadLTP.TabIndex = 2;
            // 
            // comboBoxLegColor
            // 
            this.comboBoxLegColor.FormattingEnabled = true;
            this.comboBoxLegColor.Location = new System.Drawing.Point(91, 90);
            this.comboBoxLegColor.Name = "comboBoxLegColor";
            this.comboBoxLegColor.Size = new System.Drawing.Size(121, 21);
            this.comboBoxLegColor.TabIndex = 2;
            this.comboBoxLegColor.Click += new System.EventHandler(this.comboBoxLegColor_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(267, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Slop Settings";
            // 
            // comboBoxSlopSettings
            // 
            this.comboBoxSlopSettings.FormattingEnabled = true;
            this.comboBoxSlopSettings.Location = new System.Drawing.Point(345, 36);
            this.comboBoxSlopSettings.Name = "comboBoxSlopSettings";
            this.comboBoxSlopSettings.Size = new System.Drawing.Size(121, 21);
            this.comboBoxSlopSettings.TabIndex = 2;
            // 
            // dataGridViewSpreadDetails
            // 
            this.dataGridViewSpreadDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewSpreadDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSpreadDetails.Location = new System.Drawing.Point(12, 117);
            this.dataGridViewSpreadDetails.Name = "dataGridViewSpreadDetails";
            this.dataGridViewSpreadDetails.Size = new System.Drawing.Size(468, 471);
            this.dataGridViewSpreadDetails.TabIndex = 3;
            this.dataGridViewSpreadDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewSpreadDetails_CellClick);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(324, 594);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 4;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(405, 594);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // SpreadDetailsForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 629);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.dataGridViewSpreadDetails);
            this.Controls.Add(this.comboBoxLegColor);
            this.Controls.Add(this.comboBoxSpreadLTP);
            this.Controls.Add(this.comboBoxSlopSettings);
            this.Controls.Add(this.comboBoxBasedOn);
            this.Controls.Add(this.textBoxSpreadName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "SpreadDetailsForm";
            this.Text = "SpreadDetails";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.SpreadDetailsForm_DragDrop);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.SpreadDetailsForm_DragOver);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSpreadDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxSpreadName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxBasedOn;
        private System.Windows.Forms.ComboBox comboBoxSpreadLTP;
        private System.Windows.Forms.ComboBox comboBoxLegColor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxSlopSettings;
        private System.Windows.Forms.DataGridView dataGridViewSpreadDetails;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
    }
}