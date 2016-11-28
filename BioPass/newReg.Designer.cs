namespace BioPass {
    partial class newReg {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cancelReg = new System.Windows.Forms.Button();
            this.acceptReg = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 36);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(203, 20);
            this.textBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Please enter your full name: ";
            // 
            // cancelReg
            // 
            this.cancelReg.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelReg.Location = new System.Drawing.Point(55, 63);
            this.cancelReg.Name = "cancelReg";
            this.cancelReg.Size = new System.Drawing.Size(75, 23);
            this.cancelReg.TabIndex = 2;
            this.cancelReg.Text = "Nevermind!";
            this.cancelReg.UseVisualStyleBackColor = true;
            // 
            // acceptReg
            // 
            this.acceptReg.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.acceptReg.Location = new System.Drawing.Point(136, 63);
            this.acceptReg.Name = "acceptReg";
            this.acceptReg.Size = new System.Drawing.Size(75, 23);
            this.acceptReg.TabIndex = 3;
            this.acceptReg.Text = "Done!";
            this.acceptReg.UseVisualStyleBackColor = true;
            // 
            // newReg
            // 
            this.AcceptButton = this.acceptReg;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelReg;
            this.ClientSize = new System.Drawing.Size(223, 98);
            this.Controls.Add(this.acceptReg);
            this.Controls.Add(this.cancelReg);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Name = "newReg";
            this.Text = "Registration";
            this.Load += new System.EventHandler(this.firstRegForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cancelReg;
        private System.Windows.Forms.Button acceptReg;
        public System.Windows.Forms.TextBox textBox1;
    }
}