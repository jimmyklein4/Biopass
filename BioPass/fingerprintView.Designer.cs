namespace BioPass {
    partial class fingerprintView {
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
            this.fingerprintList = new System.Windows.Forms.DataGridView();
            this.doneBtn = new System.Windows.Forms.Button();
            this.Finger = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fp_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fingerprint = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Delete = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.fingerprintList)).BeginInit();
            this.SuspendLayout();
            // 
            // fingerprintList
            // 
            this.fingerprintList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fingerprintList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.fingerprintList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Finger,
            this.fp_id,
            this.Fingerprint,
            this.Delete});
            this.fingerprintList.Location = new System.Drawing.Point(12, 12);
            this.fingerprintList.Name = "fingerprintList";
            this.fingerprintList.Size = new System.Drawing.Size(349, 277);
            this.fingerprintList.TabIndex = 0;
            this.fingerprintList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.fingerprintList_CellClick);
            // 
            // doneBtn
            // 
            this.doneBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.doneBtn.Location = new System.Drawing.Point(296, 295);
            this.doneBtn.Name = "doneBtn";
            this.doneBtn.Size = new System.Drawing.Size(65, 20);
            this.doneBtn.TabIndex = 1;
            this.doneBtn.Text = "Done";
            this.doneBtn.UseVisualStyleBackColor = true;
            this.doneBtn.Click += new System.EventHandler(this.doneBtn_Click);
            // 
            // Finger
            // 
            this.Finger.HeaderText = "Finger";
            this.Finger.Name = "Finger";
            // 
            // fp_id
            // 
            this.fp_id.HeaderText = "fp_id";
            this.fp_id.Name = "fp_id";
            this.fp_id.ReadOnly = true;
            this.fp_id.Visible = false;
            // 
            // Fingerprint
            // 
            this.Fingerprint.HeaderText = "Fingerprint";
            this.Fingerprint.Name = "Fingerprint";
            this.Fingerprint.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Fingerprint.Text = "Set Fingerprint";
            this.Fingerprint.UseColumnTextForButtonValue = true;
            // 
            // Delete
            // 
            this.Delete.HeaderText = "Delete";
            this.Delete.Name = "Delete";
            this.Delete.Text = "Delete";
            this.Delete.UseColumnTextForButtonValue = true;
            // 
            // fingerprintView
            // 
            this.AcceptButton = this.doneBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.doneBtn;
            this.ClientSize = new System.Drawing.Size(373, 323);
            this.ControlBox = false;
            this.Controls.Add(this.doneBtn);
            this.Controls.Add(this.fingerprintList);
            this.Name = "fingerprintView";
            this.Text = "Fingerprints";
            this.Load += new System.EventHandler(this.fingerprintView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fingerprintList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView fingerprintList;
        private System.Windows.Forms.Button doneBtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Finger;
        private System.Windows.Forms.DataGridViewTextBoxColumn fp_id;
        private System.Windows.Forms.DataGridViewButtonColumn Fingerprint;
        private System.Windows.Forms.DataGridViewButtonColumn Delete;
    }
}