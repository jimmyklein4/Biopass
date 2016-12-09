namespace BioPass {
    partial class CredentialsView {
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
            this.credentialsGridView = new System.Windows.Forms.DataGridView();
            this.application = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Username = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.password = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.save = new System.Windows.Forms.DataGridViewButtonColumn();
            this.launch = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.credentialsGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // credentialsGridView
            // 
            this.credentialsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.credentialsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.application,
            this.Username,
            this.password,
            this.save,
            this.launch});
            this.credentialsGridView.Location = new System.Drawing.Point(12, 12);
            this.credentialsGridView.Name = "credentialsGridView";
            this.credentialsGridView.Size = new System.Drawing.Size(643, 405);
            this.credentialsGridView.TabIndex = 0;
            this.credentialsGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.credentialsGridView_CellContentClick);
            // 
            // application
            // 
            this.application.HeaderText = "Application";
            this.application.Name = "application";
            this.application.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.application.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.application.Width = 200;
            // 
            // Username
            // 
            this.Username.HeaderText = "Username";
            this.Username.Name = "Username";
            // 
            // password
            // 
            this.password.HeaderText = "Password";
            this.password.Name = "password";
            // 
            // save
            // 
            this.save.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.save.HeaderText = "Save";
            this.save.Name = "save";
            this.save.Text = "Save";
            this.save.UseColumnTextForButtonValue = true;
            // 
            // launch
            // 
            this.launch.HeaderText = "Launch";
            this.launch.Name = "launch";
            this.launch.UseColumnTextForButtonValue = true;
            // 
            // CredentialsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 429);
            this.Controls.Add(this.credentialsGridView);
            this.Name = "CredentialsView";
            this.Text = "CredentialsView";
            ((System.ComponentModel.ISupportInitialize)(this.credentialsGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView credentialsGridView;
        private System.Windows.Forms.DataGridViewComboBoxColumn application;
        private System.Windows.Forms.DataGridViewTextBoxColumn Username;
        private System.Windows.Forms.DataGridViewTextBoxColumn password;
        private System.Windows.Forms.DataGridViewButtonColumn save;
        private System.Windows.Forms.DataGridViewButtonColumn launch;
    }
}