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
            this.fpBtn = new System.Windows.Forms.Button();
            this.faceBtn = new System.Windows.Forms.Button();
            this.pinBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.createBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(28, 25);
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
            this.cancelReg.Location = new System.Drawing.Point(92, 181);
            this.cancelReg.Name = "cancelReg";
            this.cancelReg.Size = new System.Drawing.Size(75, 23);
            this.cancelReg.TabIndex = 2;
            this.cancelReg.Text = "Nevermind!";
            this.cancelReg.UseVisualStyleBackColor = true;
            // 
            // acceptReg
            // 
            this.acceptReg.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.acceptReg.Location = new System.Drawing.Point(173, 181);
            this.acceptReg.Name = "acceptReg";
            this.acceptReg.Size = new System.Drawing.Size(75, 23);
            this.acceptReg.TabIndex = 3;
            this.acceptReg.Text = "Done!";
            this.acceptReg.UseVisualStyleBackColor = true;
            // 
            // fpBtn
            // 
            this.fpBtn.Location = new System.Drawing.Point(13, 95);
            this.fpBtn.Name = "fpBtn";
            this.fpBtn.Size = new System.Drawing.Size(91, 67);
            this.fpBtn.TabIndex = 4;
            this.fpBtn.Text = "Fingerprints";
            this.fpBtn.UseVisualStyleBackColor = true;
            // 
            // faceBtn
            // 
            this.faceBtn.Location = new System.Drawing.Point(125, 95);
            this.faceBtn.Name = "faceBtn";
            this.faceBtn.Size = new System.Drawing.Size(91, 67);
            this.faceBtn.TabIndex = 5;
            this.faceBtn.Text = "Facial model";
            this.faceBtn.UseVisualStyleBackColor = true;
            // 
            // pinBtn
            // 
            this.pinBtn.Location = new System.Drawing.Point(237, 95);
            this.pinBtn.Name = "pinBtn";
            this.pinBtn.Size = new System.Drawing.Size(91, 67);
            this.pinBtn.TabIndex = 6;
            this.pinBtn.Text = "Pin";
            this.pinBtn.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(275, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Select the following options to build your biometric model:";
            // 
            // createBtn
            // 
            this.createBtn.Location = new System.Drawing.Point(237, 22);
            this.createBtn.Name = "createBtn";
            this.createBtn.Size = new System.Drawing.Size(75, 23);
            this.createBtn.TabIndex = 8;
            this.createBtn.Text = "Create";
            this.createBtn.UseVisualStyleBackColor = true;
            // 
            // newReg
            // 
            this.AcceptButton = this.acceptReg;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelReg;
            this.ClientSize = new System.Drawing.Size(340, 224);
            this.Controls.Add(this.createBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pinBtn);
            this.Controls.Add(this.faceBtn);
            this.Controls.Add(this.fpBtn);
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
        private System.Windows.Forms.Button fpBtn;
        private System.Windows.Forms.Button faceBtn;
        private System.Windows.Forms.Button pinBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button createBtn;
    }
}