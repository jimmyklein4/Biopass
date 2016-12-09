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
            this.fpBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.urlNew = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select application:";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(37, 41);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(160, 24);
            this.comboBox1.TabIndex = 3;
            // 
            // go
            // 
            this.go.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.go.Location = new System.Drawing.Point(207, 41);
            this.go.Margin = new System.Windows.Forms.Padding(4);
            this.go.Name = "go";
            this.go.Size = new System.Drawing.Size(60, 28);
            this.go.TabIndex = 4;
            this.go.Text = "Go!";
            this.go.Click += new System.EventHandler(this.go_Click);
            // 
            // fpBtn
            // 
            this.fpBtn.Location = new System.Drawing.Point(167, 149);
            this.fpBtn.Margin = new System.Windows.Forms.Padding(4);
            this.fpBtn.Name = "fpBtn";
            this.fpBtn.Size = new System.Drawing.Size(100, 28);
            this.fpBtn.TabIndex = 5;
            this.fpBtn.Text = "Fingerprints";
            this.fpBtn.UseVisualStyleBackColor = true;
            this.fpBtn.Click += new System.EventHandler(this.fpBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(25, 149);
            this.cancelBtn.Margin = new System.Windows.Forms.Padding(4);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(100, 28);
            this.cancelBtn.TabIndex = 6;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            // 
            // urlNew
            // 
            this.urlNew.HideSelection = false;
            this.urlNew.Location = new System.Drawing.Point(37, 103);
            this.urlNew.Name = "urlNew";
            this.urlNew.Size = new System.Drawing.Size(159, 22);
            this.urlNew.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(230, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Or, enter URL of new website here:";
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(207, 97);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(60, 28);
            this.button1.TabIndex = 9;
            this.button1.Text = "Go!";
            this.button1.Click += new System.EventHandler(this.go_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(283, 192);
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.urlNew);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.fpBtn);
            this.Controls.Add(this.go);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
        private System.Windows.Forms.Button fpBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.TextBox urlNew;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
    }
}