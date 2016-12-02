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
            this.cancelReg = new System.Windows.Forms.Button();
            this.acceptReg = new System.Windows.Forms.Button();
            this.bioPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.pinBtn = new System.Windows.Forms.Button();
            this.faceBtn = new System.Windows.Forms.Button();
            this.fpBtn = new System.Windows.Forms.Button();
            this.namePanel = new System.Windows.Forms.Panel();
            this.createBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.faceModBtn = new System.Windows.Forms.Button();
            this.bioPanel.SuspendLayout();
            this.namePanel.SuspendLayout();
            this.SuspendLayout();
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
            // bioPanel
            // 
            this.bioPanel.Controls.Add(this.faceModBtn);
            this.bioPanel.Controls.Add(this.label2);
            this.bioPanel.Controls.Add(this.pinBtn);
            this.bioPanel.Controls.Add(this.faceBtn);
            this.bioPanel.Controls.Add(this.fpBtn);
            this.bioPanel.Location = new System.Drawing.Point(3, 64);
            this.bioPanel.Name = "bioPanel";
            this.bioPanel.Size = new System.Drawing.Size(331, 108);
            this.bioPanel.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(275, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Select the following options to build your biometric model:";
            // 
            // pinBtn
            // 
            this.pinBtn.Location = new System.Drawing.Point(232, 29);
            this.pinBtn.Name = "pinBtn";
            this.pinBtn.Size = new System.Drawing.Size(91, 67);
            this.pinBtn.TabIndex = 10;
            this.pinBtn.Text = "Pin";
            this.pinBtn.UseVisualStyleBackColor = true;
            this.pinBtn.Click += new System.EventHandler(this.pinBtn_Click);
            // 
            // faceBtn
            // 
            this.faceBtn.Location = new System.Drawing.Point(120, 29);
            this.faceBtn.Name = "faceBtn";
            this.faceBtn.Size = new System.Drawing.Size(91, 33);
            this.faceBtn.TabIndex = 9;
            this.faceBtn.Text = "Take Photo";
            this.faceBtn.UseVisualStyleBackColor = true;
            this.faceBtn.Click += new System.EventHandler(this.faceBtn_Click);
            // 
            // fpBtn
            // 
            this.fpBtn.Location = new System.Drawing.Point(8, 29);
            this.fpBtn.Name = "fpBtn";
            this.fpBtn.Size = new System.Drawing.Size(91, 67);
            this.fpBtn.TabIndex = 8;
            this.fpBtn.Text = "Fingerprints";
            this.fpBtn.UseVisualStyleBackColor = true;
            this.fpBtn.Click += new System.EventHandler(this.fpBtn_Click);
            // 
            // namePanel
            // 
            this.namePanel.Controls.Add(this.createBtn);
            this.namePanel.Controls.Add(this.label1);
            this.namePanel.Controls.Add(this.nameBox);
            this.namePanel.Location = new System.Drawing.Point(3, 4);
            this.namePanel.Name = "namePanel";
            this.namePanel.Size = new System.Drawing.Size(331, 50);
            this.namePanel.TabIndex = 10;
            // 
            // createBtn
            // 
            this.createBtn.Location = new System.Drawing.Point(242, 20);
            this.createBtn.Name = "createBtn";
            this.createBtn.Size = new System.Drawing.Size(75, 23);
            this.createBtn.TabIndex = 11;
            this.createBtn.Text = "Create";
            this.createBtn.UseVisualStyleBackColor = true;
            this.createBtn.Click += new System.EventHandler(this.createBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Please enter your full name: ";
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(33, 23);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(203, 20);
            this.nameBox.TabIndex = 9;
            // 
            // faceModBtn
            // 
            this.faceModBtn.Enabled = false;
            this.faceModBtn.Location = new System.Drawing.Point(120, 63);
            this.faceModBtn.Name = "faceModBtn";
            this.faceModBtn.Size = new System.Drawing.Size(91, 33);
            this.faceModBtn.TabIndex = 12;
            this.faceModBtn.Text = "Facial Model";
            this.faceModBtn.UseVisualStyleBackColor = true;
            this.faceModBtn.Click += new System.EventHandler(this.faceModBtn_Click);
            // 
            // newReg
            // 
            this.AcceptButton = this.acceptReg;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelReg;
            this.ClientSize = new System.Drawing.Size(340, 212);
            this.ControlBox = false;
            this.Controls.Add(this.namePanel);
            this.Controls.Add(this.bioPanel);
            this.Controls.Add(this.acceptReg);
            this.Controls.Add(this.cancelReg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "newReg";
            this.Text = "Registration";
            this.Load += new System.EventHandler(this.firstRegForm_Load);
            this.bioPanel.ResumeLayout(false);
            this.bioPanel.PerformLayout();
            this.namePanel.ResumeLayout(false);
            this.namePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button cancelReg;
        private System.Windows.Forms.Button acceptReg;
        private System.Windows.Forms.Panel bioPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button pinBtn;
        private System.Windows.Forms.Button faceBtn;
        private System.Windows.Forms.Button fpBtn;
        private System.Windows.Forms.Panel namePanel;
        private System.Windows.Forms.Button createBtn;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.Button faceModBtn;
    }
}