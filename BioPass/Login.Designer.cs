namespace BioPass
{
    partial class Login
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.go = new System.Windows.Forms.Button();
            this.urlNew = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.credsBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.fpBtn = new System.Windows.Forms.Button();
            this.secLevelBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select application:";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(28, 33);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 3;
            // 
            // go
            // 
            this.go.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.go.Location = new System.Drawing.Point(155, 33);
            this.go.Name = "go";
            this.go.Size = new System.Drawing.Size(45, 23);
            this.go.TabIndex = 4;
            this.go.Text = "Go!";
            this.go.Click += new System.EventHandler(this.go_Click);
            // 
            // urlNew
            // 
            this.urlNew.HideSelection = false;
            this.urlNew.Location = new System.Drawing.Point(28, 84);
            this.urlNew.Margin = new System.Windows.Forms.Padding(2);
            this.urlNew.Name = "urlNew";
            this.urlNew.Size = new System.Drawing.Size(120, 20);
            this.urlNew.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 67);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(174, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Or, enter URL of new website here:";
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(155, 79);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(45, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Go!";
            this.button1.Click += new System.EventHandler(this.go_Click);
            // 
            // credsBtn
            // 
            this.credsBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.credsBtn.Location = new System.Drawing.Point(116, 114);
            this.credsBtn.Name = "credsBtn";
            this.credsBtn.Size = new System.Drawing.Size(75, 23);
            this.credsBtn.TabIndex = 13;
            this.credsBtn.Text = "Credentials";
            this.credsBtn.UseVisualStyleBackColor = true;
            this.credsBtn.Click += new System.EventHandler(this.credsBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(116, 143);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 12;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            // 
            // fpBtn
            // 
            this.fpBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.fpBtn.Location = new System.Drawing.Point(26, 114);
            this.fpBtn.Name = "fpBtn";
            this.fpBtn.Size = new System.Drawing.Size(75, 23);
            this.fpBtn.TabIndex = 11;
            this.fpBtn.Text = "Fingerprints";
            this.fpBtn.UseVisualStyleBackColor = true;
            this.fpBtn.Click += new System.EventHandler(this.fpBtn_Click);
            // 
            // secLevelBtn
            // 
            this.secLevelBtn.Location = new System.Drawing.Point(26, 143);
            this.secLevelBtn.Name = "secLevelBtn";
            this.secLevelBtn.Size = new System.Drawing.Size(75, 23);
            this.secLevelBtn.TabIndex = 15;
            this.secLevelBtn.Text = "Security Level";
            this.secLevelBtn.UseVisualStyleBackColor = true;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(218, 178);
            this.ControlBox = false;
            this.Controls.Add(this.secLevelBtn);
            this.Controls.Add(this.credsBtn);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.fpBtn);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.urlNew);
            this.Controls.Add(this.go);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Login";
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button go;
        private System.Windows.Forms.TextBox urlNew;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button credsBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Button fpBtn;
        private System.Windows.Forms.Button secLevelBtn;
    }
}