﻿namespace BioPass {
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
            this.secLevelBtn = new System.Windows.Forms.Button();
            this.credsBtn = new System.Windows.Forms.Button();
            this.IrisBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.faceBtn = new System.Windows.Forms.Button();
            this.fpBtn = new System.Windows.Forms.Button();
            this.namePanel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.pinBox = new System.Windows.Forms.TextBox();
            this.createBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.nameBox = new System.Windows.Forms.TextBox();
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
            this.bioPanel.Controls.Add(this.secLevelBtn);
            this.bioPanel.Controls.Add(this.credsBtn);
            this.bioPanel.Controls.Add(this.IrisBtn);
            this.bioPanel.Controls.Add(this.label2);
            this.bioPanel.Controls.Add(this.faceBtn);
            this.bioPanel.Controls.Add(this.fpBtn);
            this.bioPanel.Location = new System.Drawing.Point(3, 64);
            this.bioPanel.Name = "bioPanel";
            this.bioPanel.Size = new System.Drawing.Size(331, 108);
            this.bioPanel.TabIndex = 9;
            // 
            // secLevelBtn
            // 
            this.secLevelBtn.Location = new System.Drawing.Point(54, 68);
            this.secLevelBtn.Name = "secLevelBtn";
            this.secLevelBtn.Size = new System.Drawing.Size(104, 28);
            this.secLevelBtn.TabIndex = 14;
            this.secLevelBtn.Text = "Security Level";
            this.secLevelBtn.UseVisualStyleBackColor = true;
            this.secLevelBtn.Click += new System.EventHandler(this.secLevelBtn_Click);
            // 
            // credsBtn
            // 
            this.credsBtn.Location = new System.Drawing.Point(178, 66);
            this.credsBtn.Name = "credsBtn";
            this.credsBtn.Size = new System.Drawing.Size(104, 28);
            this.credsBtn.TabIndex = 11;
            this.credsBtn.Text = "Credentials";
            this.credsBtn.UseVisualStyleBackColor = true;
            this.credsBtn.Click += new System.EventHandler(this.credsBtn_Click);
            // 
            // IrisBtn
            // 
            this.IrisBtn.Location = new System.Drawing.Point(232, 29);
            this.IrisBtn.Name = "IrisBtn";
            this.IrisBtn.Size = new System.Drawing.Size(91, 33);
            this.IrisBtn.TabIndex = 13;
            this.IrisBtn.Text = "Iris";
            this.IrisBtn.UseVisualStyleBackColor = true;
            this.IrisBtn.Click += new System.EventHandler(this.IrisBtn_Click);
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
            // faceBtn
            // 
            this.faceBtn.Location = new System.Drawing.Point(105, 29);
            this.faceBtn.Name = "faceBtn";
            this.faceBtn.Size = new System.Drawing.Size(121, 33);
            this.faceBtn.TabIndex = 9;
            this.faceBtn.Text = "Take Photo";
            this.faceBtn.UseVisualStyleBackColor = true;
            this.faceBtn.Click += new System.EventHandler(this.faceBtn_Click);
            // 
            // fpBtn
            // 
            this.fpBtn.Location = new System.Drawing.Point(8, 29);
            this.fpBtn.Name = "fpBtn";
            this.fpBtn.Size = new System.Drawing.Size(91, 33);
            this.fpBtn.TabIndex = 8;
            this.fpBtn.Text = "Fingerprints";
            this.fpBtn.UseVisualStyleBackColor = true;
            this.fpBtn.Click += new System.EventHandler(this.fpBtn_Click);
            // 
            // namePanel
            // 
            this.namePanel.Controls.Add(this.label3);
            this.namePanel.Controls.Add(this.pinBox);
            this.namePanel.Controls.Add(this.createBtn);
            this.namePanel.Controls.Add(this.label1);
            this.namePanel.Controls.Add(this.nameBox);
            this.namePanel.Location = new System.Drawing.Point(3, 4);
            this.namePanel.Name = "namePanel";
            this.namePanel.Size = new System.Drawing.Size(331, 50);
            this.namePanel.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(157, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Please enter a pin: ";
            // 
            // pinBox
            // 
            this.pinBox.Location = new System.Drawing.Point(160, 23);
            this.pinBox.MaxLength = 4;
            this.pinBox.Name = "pinBox";
            this.pinBox.Size = new System.Drawing.Size(76, 20);
            this.pinBox.TabIndex = 2;
            // 
            // createBtn
            // 
            this.createBtn.Location = new System.Drawing.Point(242, 20);
            this.createBtn.Name = "createBtn";
            this.createBtn.Size = new System.Drawing.Size(75, 23);
            this.createBtn.TabIndex = 3;
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
            this.nameBox.Size = new System.Drawing.Size(109, 20);
            this.nameBox.TabIndex = 1;
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
        private System.Windows.Forms.Button faceBtn;
        private System.Windows.Forms.Button fpBtn;
        private System.Windows.Forms.Panel namePanel;
        private System.Windows.Forms.Button createBtn;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.Button IrisBtn;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox pinBox;
        private System.Windows.Forms.Button credsBtn;
        private System.Windows.Forms.Button secLevelBtn;
    }
}