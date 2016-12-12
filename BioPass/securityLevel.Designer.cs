namespace BioPass {
    partial class securityLevel {
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
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.sliderLabel = new System.Windows.Forms.Label();
            this.doneBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBar
            // 
            this.trackBar.Location = new System.Drawing.Point(12, 12);
            this.trackBar.Maximum = 4;
            this.trackBar.Name = "trackBar";
            this.trackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar.Size = new System.Drawing.Size(45, 238);
            this.trackBar.TabIndex = 0;
            this.trackBar.Scroll += new System.EventHandler(this.trackBar_Scroll);
            // 
            // sliderLabel
            // 
            this.sliderLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sliderLabel.Location = new System.Drawing.Point(63, 12);
            this.sliderLabel.Name = "sliderLabel";
            this.sliderLabel.Size = new System.Drawing.Size(209, 238);
            this.sliderLabel.TabIndex = 1;
            // 
            // doneBtn
            // 
            this.doneBtn.Location = new System.Drawing.Point(197, 254);
            this.doneBtn.Name = "doneBtn";
            this.doneBtn.Size = new System.Drawing.Size(75, 23);
            this.doneBtn.TabIndex = 2;
            this.doneBtn.Text = "Done";
            this.doneBtn.UseVisualStyleBackColor = true;
            this.doneBtn.Click += new System.EventHandler(this.doneBtn_Click);
            // 
            // securityLevel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 289);
            this.Controls.Add(this.doneBtn);
            this.Controls.Add(this.sliderLabel);
            this.Controls.Add(this.trackBar);
            this.Name = "securityLevel";
            this.Text = "securityLevel";
            this.Load += new System.EventHandler(this.securityLevel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBar;
        private System.Windows.Forms.Label sliderLabel;
        private System.Windows.Forms.Button doneBtn;
    }
}