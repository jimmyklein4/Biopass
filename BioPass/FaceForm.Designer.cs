namespace BioPass
{
    partial class FaceForm
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
            this.pictureBoxDisplay = new System.Windows.Forms.PictureBox();
            this.comboBoxCameras = new System.Windows.Forms.ComboBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnAddAccount = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.detect = new System.Windows.Forms.Button();
            this.create_rec = new System.Windows.Forms.Button();
            this.check = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.applicationsList = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxDisplay
            // 
            this.pictureBoxDisplay.Location = new System.Drawing.Point(10, 10);
            this.pictureBoxDisplay.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBoxDisplay.Name = "pictureBoxDisplay";
            this.pictureBoxDisplay.Size = new System.Drawing.Size(604, 489);
            this.pictureBoxDisplay.TabIndex = 13;
            this.pictureBoxDisplay.TabStop = false;
            // 
            // comboBoxCameras
            // 
            this.comboBoxCameras.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCameras.FormattingEnabled = true;
            this.comboBoxCameras.Location = new System.Drawing.Point(12, 539);
            this.comboBoxCameras.Name = "comboBoxCameras";
            this.comboBoxCameras.Size = new System.Drawing.Size(153, 21);
            this.comboBoxCameras.TabIndex = 12;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(90, 513);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 9;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(12, 513);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 8;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnAddAccount
            // 
            this.btnAddAccount.Location = new System.Drawing.Point(0, 0);
            this.btnAddAccount.Name = "btnAddAccount";
            this.btnAddAccount.Size = new System.Drawing.Size(75, 23);
            this.btnAddAccount.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 0;
            // 
            // detect
            // 
            this.detect.Location = new System.Drawing.Point(172, 513);
            this.detect.Name = "detect";
            this.detect.Size = new System.Drawing.Size(75, 23);
            this.detect.TabIndex = 14;
            this.detect.Text = "Detect Faces";
            this.detect.UseVisualStyleBackColor = true;
            this.detect.Click += new System.EventHandler(this.detect_Click);
            // 
            // create_rec
            // 
            this.create_rec.Location = new System.Drawing.Point(254, 513);
            this.create_rec.Name = "create_rec";
            this.create_rec.Size = new System.Drawing.Size(75, 23);
            this.create_rec.TabIndex = 15;
            this.create_rec.Text = "Create Rec";
            this.create_rec.UseVisualStyleBackColor = true;
            this.create_rec.Click += new System.EventHandler(this.create_rec_Click);
            // 
            // check
            // 
            this.check.Location = new System.Drawing.Point(336, 513);
            this.check.Name = "check";
            this.check.Size = new System.Drawing.Size(75, 23);
            this.check.TabIndex = 16;
            this.check.Text = "Check";
            this.check.UseVisualStyleBackColor = true;
            this.check.Click += new System.EventHandler(this.check_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(539, 513);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 17;
            this.button1.Text = "Authenticate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.checkedListBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checkedListBox1.Font = new System.Drawing.Font("Franklin Gothic Book", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "Finger",
            "Iris",
            "Facial",
            "Pin"});
            this.checkedListBox1.Location = new System.Drawing.Point(45, 347);
            this.checkedListBox1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 4);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(202, 84);
            this.checkedListBox1.TabIndex = 18;
            this.checkedListBox1.Visible = false;
            this.checkedListBox1.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.label1.Font = new System.Drawing.Font("Franklin Gothic Book", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(45, 321);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(202, 26);
            this.label1.TabIndex = 19;
            this.label1.Text = "Select 2 Forms of Authentication";
            this.label1.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Franklin Gothic Book", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(45, 428);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(202, 48);
            this.textBox1.TabIndex = 20;
            this.textBox1.Text = "   Selecting 2 Forms of Authentication                 Will Begin Verification\r\n";
            this.textBox1.Visible = false;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // applicationsList
            // 
            this.applicationsList.Location = new System.Drawing.Point(539, 539);
            this.applicationsList.Name = "applicationsList";
            this.applicationsList.Size = new System.Drawing.Size(75, 23);
            this.applicationsList.TabIndex = 21;
            this.applicationsList.Text = "Show Apps";
            this.applicationsList.UseVisualStyleBackColor = true;
            this.applicationsList.Visible = false;
            this.applicationsList.Click += new System.EventHandler(this.button2_Click);
            // 
            // FaceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 566);
            this.Controls.Add(this.applicationsList);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.check);
            this.Controls.Add(this.create_rec);
            this.Controls.Add(this.detect);
            this.Controls.Add(this.pictureBoxDisplay);
            this.Controls.Add(this.comboBoxCameras);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(638, 515);
            this.Name = "FaceForm";
            this.Text = "FacePass";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FaceForm_FormClosing);
            this.Load += new System.EventHandler(this.FaceForm_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FaceForm_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDisplay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBoxDisplay;
        private System.Windows.Forms.ComboBox comboBoxCameras;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnAddAccount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button detect;
        private System.Windows.Forms.Button create_rec;
        private System.Windows.Forms.Button check;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button applicationsList;
    }
}

