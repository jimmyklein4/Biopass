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
    public partial class fingerprintView : Form {
        private long target = -1;
        public fingerprintView() {
            InitializeComponent();
        }
        public fingerprintView(long _target) {
            InitializeComponent();
            target = _target;
        }
        public void PopulateDataGridView() {
            fingerprintList.Rows.Clear();
            DataTable fingerprints = Program.db.getUserFPs(target);
            if (fingerprints != null) { 
                foreach (DataRow dr in fingerprints.Rows) {
                    if (!dr.IsNull("fingerprint")) {
                        String[] row = {dr["finger"].ToString(), dr["fp_id"].ToString()};
                        if(fingerprintList != null) fingerprintList.Rows.Add(row);
                    }
                }
            }
            Debug.WriteLine("Fingerprints populated");
        }

        private void fingerprintView_Load(object sender, EventArgs e) {
            Program.fingerprintForm = this;
            PopulateDataGridView();
        }
        private void fingerprintList_CellClick(object sender, DataGridViewCellEventArgs e) {
            //Debug.WriteLine("Fingerprint stuff " + fingerprintList[fingerprintList.Columns["Finger"].Index, e.RowIndex].Value);
            if (fingerprintList[fingerprintList.Columns["Finger"].Index, e.RowIndex].Value == null) return;

            if (e.ColumnIndex == fingerprintList.Columns["Fingerprint"].Index && e.RowIndex >= 0) {
                Program.targetFingerprintName = fingerprintList[fingerprintList.Columns["Finger"].Index, e.RowIndex].Value.ToString();
                Program.targetFingerUserID = target;
                Program.targetFingerRow = e.RowIndex;

                DataGridViewCell fpid_cell = fingerprintList[fingerprintList.Columns["fp_id"].Index, e.RowIndex];
                if (fpid_cell != null) {
                    object fpid_val = fpid_cell.Value;
                    if (fpid_val != null) {
                        string fpid = fpid_cell.Value.ToString();
                        Program.targetFingerprintID = Int64.Parse(fpid);
                    }
                }

                Program.mainForm.beginFPRegistration();
            } else if (e.ColumnIndex == fingerprintList.Columns["Delete"].Index && e.RowIndex >= 0) {
                long fp_id = -1;
                DataGridViewCell fpid_cell = fingerprintList[fingerprintList.Columns["fp_id"].Index, e.RowIndex];
                if (fpid_cell != null) {
                    object fpid_val = fpid_cell.Value;
                    if (fpid_val != null) {
                        string fpid = fpid_cell.Value.ToString();
                        fp_id = Int64.Parse(fpid);
                        Program.db.deleteFP(fp_id);
                        PopulateDataGridView();
                    }
                }

            }
        }

        private void doneBtn_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
