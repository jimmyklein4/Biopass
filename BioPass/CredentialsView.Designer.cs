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
            this.credentialsList = new System.Windows.Forms.DataGridView();
            this.doneBtn = new System.Windows.Forms.Button();
            this.application = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.application_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.account_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Username = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.password = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.save = new System.Windows.Forms.DataGridViewButtonColumn();
            this.launch = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.credentialsList)).BeginInit();
            this.SuspendLayout();
            // 
            // credentialsList
            // 
            this.credentialsList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.credentialsList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.application,
            this.application_id,
            this.account_id,
            this.Username,
            this.password,
            this.type,
            this.save,
            this.launch});
            this.credentialsList.Location = new System.Drawing.Point(12, 12);
            this.credentialsList.Name = "credentialsList";
            this.credentialsList.Size = new System.Drawing.Size(643, 405);
            this.credentialsList.TabIndex = 0;
            this.credentialsList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.credentialsList_CellContentClick);
            this.credentialsList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.credentialsList_CellFormatting);
            this.credentialsList.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.credentialsList_EditingControlShowing);
            // 
            // doneBtn
            // 
            this.doneBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.doneBtn.Location = new System.Drawing.Point(590, 423);
            this.doneBtn.Name = "doneBtn";
            this.doneBtn.Size = new System.Drawing.Size(65, 20);
            this.doneBtn.TabIndex = 2;
            this.doneBtn.Text = "Done";
            this.doneBtn.UseVisualStyleBackColor = true;
            this.doneBtn.Click += new System.EventHandler(this.doneBtn_Click);
            // 
            // application
            // 
            this.application.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.application.HeaderText = "Application";
            this.application.Name = "application";
            this.application.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.application.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.application.Width = 200;
            // 
            // application_id
            // 
            this.application_id.HeaderText = "application_id";
            this.application_id.Name = "application_id";
            this.application_id.Visible = false;
            // 
            // account_id
            // 
            this.account_id.HeaderText = "account_id";
            this.account_id.Name = "account_id";
            this.account_id.Visible = false;
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
            // type
            // 
            this.type.HeaderText = "type";
            this.type.Name = "type";
            this.type.Visible = false;
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
            this.launch.Text = "Launch";
            this.launch.UseColumnTextForButtonValue = true;
            // 
            // CredentialsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 451);
            this.Controls.Add(this.doneBtn);
            this.Controls.Add(this.credentialsList);
            this.Name = "CredentialsView";
            this.Text = "CredentialsView";
            this.Load += new System.EventHandler(this.CredentialsView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.credentialsList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView credentialsList;
        private System.Windows.Forms.Button doneBtn;
        private System.Windows.Forms.DataGridViewComboBoxColumn application;
        private System.Windows.Forms.DataGridViewTextBoxColumn application_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn account_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Username;
        private System.Windows.Forms.DataGridViewTextBoxColumn password;
        private System.Windows.Forms.DataGridViewTextBoxColumn type;
        private System.Windows.Forms.DataGridViewButtonColumn save;
        private System.Windows.Forms.DataGridViewButtonColumn launch;
    }
}