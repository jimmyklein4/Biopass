namespace BioPass {
    partial class testControl {
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.createBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.pinBtn = new System.Windows.Forms.Button();
            this.faceBtn = new System.Windows.Forms.Button();
            this.fpBtn = new System.Windows.Forms.Button();
            this.acceptReg = new System.Windows.Forms.Button();
            this.cancelReg = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // createBtn
            // 
            this.createBtn.Location = new System.Drawing.Point(242, 29);
            this.createBtn.Name = "createBtn";
            this.createBtn.Size = new System.Drawing.Size(75, 23);
            this.createBtn.TabIndex = 26;
            this.createBtn.Text = "Create";
            this.createBtn.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(275, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Select the following options to build your biometric model:";
            // 
            // pinBtn
            // 
            this.pinBtn.Location = new System.Drawing.Point(242, 102);
            this.pinBtn.Name = "pinBtn";
            this.pinBtn.Size = new System.Drawing.Size(91, 67);
            this.pinBtn.TabIndex = 24;
            this.pinBtn.Text = "Pin";
            this.pinBtn.UseVisualStyleBackColor = true;
            // 
            // faceBtn
            // 
            this.faceBtn.Location = new System.Drawing.Point(130, 102);
            this.faceBtn.Name = "faceBtn";
            this.faceBtn.Size = new System.Drawing.Size(91, 67);
            this.faceBtn.TabIndex = 23;
            this.faceBtn.Text = "Facial model";
            this.faceBtn.UseVisualStyleBackColor = true;
            // 
            // fpBtn
            // 
            this.fpBtn.Location = new System.Drawing.Point(18, 102);
            this.fpBtn.Name = "fpBtn";
            this.fpBtn.Size = new System.Drawing.Size(91, 67);
            this.fpBtn.TabIndex = 22;
            this.fpBtn.Text = "Fingerprints";
            this.fpBtn.UseVisualStyleBackColor = true;
            // 
            // acceptReg
            // 
            this.acceptReg.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.acceptReg.Location = new System.Drawing.Point(178, 188);
            this.acceptReg.Name = "acceptReg";
            this.acceptReg.Size = new System.Drawing.Size(75, 23);
            this.acceptReg.TabIndex = 21;
            this.acceptReg.Text = "Done!";
            this.acceptReg.UseVisualStyleBackColor = true;
            // 
            // cancelReg
            // 
            this.cancelReg.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelReg.Location = new System.Drawing.Point(97, 188);
            this.cancelReg.Name = "cancelReg";
            this.cancelReg.Size = new System.Drawing.Size(75, 23);
            this.cancelReg.TabIndex = 20;
            this.cancelReg.Text = "Nevermind!";
            this.cancelReg.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Please enter your full name: ";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(33, 32);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(203, 20);
            this.textBox1.TabIndex = 18;
            // 
            // testControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.createBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pinBtn);
            this.Controls.Add(this.faceBtn);
            this.Controls.Add(this.fpBtn);
            this.Controls.Add(this.acceptReg);
            this.Controls.Add(this.cancelReg);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Name = "testControl";
            this.Size = new System.Drawing.Size(346, 226);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button createBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button pinBtn;
        private System.Windows.Forms.Button faceBtn;
        private System.Windows.Forms.Button fpBtn;
        private System.Windows.Forms.Button acceptReg;
        private System.Windows.Forms.Button cancelReg;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox textBox1;
    }
}
