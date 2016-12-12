using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BioPass {
    public partial class CredentialsView : Form {
        private long target = -1;
        public CredentialsView() {
            InitializeComponent();
        }
        public CredentialsView(long _target) {
            InitializeComponent();
            target = _target;
        }
        private void CredentialsView_Load(object sender, EventArgs e) {
            Program.credentialForm = this;
            PopulateDataGridView();

        }
        private void dc_DataError(object sender, DataGridViewDataErrorEventArgs e) {
            // (No need to write anything in here)
        }

        public void PopulateDataGridView() {
            credentialsList.Rows.Clear();
            DataTable allApplications = Program.db.getAllApplications();
            DataTable credentials = Program.db.getUserCredentials(target);
            DataGridViewComboBoxColumn dc = (DataGridViewComboBoxColumn)credentialsList.Columns[0];
            dc.DataSource = allApplications;
            dc.DataPropertyName = "name";
            dc.DisplayMember = "name";
            dc.ValueMember = "application_id";
            dc.ValueType = typeof(String);
            credentialsList.DataError += new DataGridViewDataErrorEventHandler(dc_DataError);

            if (credentials != null) {
                foreach (DataRow dr in credentials.Rows) {
                    if (!dr.IsNull("username")) {
                        credentialsList.Rows.Add(new Object[] {
                            dr["name"].ToString(),
                            dr["application_id"].ToString(),
                            dr["account_id"].ToString(),
                            dr["username"].ToString(),
                            dr["password"].ToString()
                        });
                    }
                }
            }
        }
        private void doneBtn_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void credentialsList_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            if (e.ColumnIndex == credentialsList.Columns["Save"].Index && e.RowIndex >= 0) {
                long fp_id = -1;
                DataGridViewCell account_id = credentialsList[credentialsList.Columns["account_id"].Index, e.RowIndex];
            
                if (account_id != null) {
                    object fpid_val = account_id.Value;
                    if (fpid_val != null) {
                        string fpid = account_id.Value.ToString();
                        fp_id = Int64.Parse(fpid);

                        String username = credentialsList[credentialsList.Columns["username"].Index, e.RowIndex].Value.ToString();
                        String password = credentialsList[credentialsList.Columns["password"].Index, e.RowIndex].Value.ToString();
                        Program.db.updateUserCreds(""+fp_id, username, password);
                    }
                    else {
                        String username = credentialsList[credentialsList.Columns["username"].Index, e.RowIndex].Value.ToString();
                        String password = credentialsList[credentialsList.Columns["password"].Index, e.RowIndex].Value.ToString();


                        String application_id = credentialsList[credentialsList.Columns["application"].Index, e.RowIndex].Value.ToString();
                        Program.db.addAccount(username, password, application_id, ""+target);

                        PopulateDataGridView();
                    }
                }
            } else if (e.ColumnIndex == credentialsList.Columns["Launch"].Index && e.RowIndex >= 0) {
                long fp_id = -1;
                DataGridViewCell fpid_cell = credentialsList[credentialsList.Columns["account_id"].Index, e.RowIndex];
                if (fpid_cell != null) {
                    object fpid_val = fpid_cell.Value;
                    if (fpid_val != null) {
                        string fpid = fpid_cell.Value.ToString();
                        fp_id = Int64.Parse(fpid);

                        String application_id = credentialsList[credentialsList.Columns["account_id"].Index, e.RowIndex].Value.ToString();
                        String website = credentialsList[credentialsList.Columns["application"].Index, e.RowIndex].FormattedValue.ToString();
                        Debug.Write(application_id + " " + website);

                        automateWeb web = new automateWeb(website, application_id);
                    }
                }

            }

        }
        private void credentialsList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e) {
            if (credentialsList.Columns[e.ColumnIndex].Name == "password" && e.Value != null) {
                credentialsList.Rows[e.RowIndex].Tag = e.Value;
                e.Value = new String('*', e.Value.ToString().Length);
            }

            
        }
        private void credentialsList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e) {
            Debug.WriteLine("Current Column: " + credentialsList.CurrentCell.ColumnIndex + "; Password Column:" + credentialsList.Columns["password"].Index);
            if ((credentialsList.CurrentCell.ColumnIndex == credentialsList.Columns["password"].Index)) { 
                TextBox textBox = e.Control as TextBox;
                if (textBox != null) {
                        textBox.UseSystemPasswordChar = true;
                }                
            } else {
                TextBox textBox = e.Control as TextBox;
                if (textBox != null) {
                        textBox.UseSystemPasswordChar = false;
                }  
            }
        }
    }
}
