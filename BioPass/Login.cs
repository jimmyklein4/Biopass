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

namespace BioPass
{
    public partial class Login : Form
    {
        public string application { get; set; }
        public long target;
        public Login(long _target)
        {
            InitializeComponent();
             target = _target;
        }
        private void Login_Load(object sender, EventArgs e)
        {
            DataTable credentials = Program.db.getUserCredentials(target);
             if (credentials != null) {
                foreach (DataRow dr in credentials.Rows) {
                    if (!dr.IsNull("username")) {
                        comboBox1.Items.Add(dr["name"].ToString());
                    }
                }
            }
        }
        private string getAppBoxVal()
        {
            if (comboBox1.SelectedItem != null)
            {
                return (comboBox1.SelectedItem != null ? comboBox1.SelectedItem.ToString() : "");
            }
            else
            {
                return (urlNew.Text);
            }
        }

        private void go_Click(object sender, EventArgs e)
        {
            application = getAppBoxVal();
            this.Close(); 
        }
        private void fpBtn_Click(object sender, EventArgs e) {
            fingerprintView fpView = new BioPass.fingerprintView(target);
            fpView.Show();
        }

        private void credsBtn_Click(object sender, EventArgs e) {
            CredentialsView credsView = new CredentialsView(target);
            credsView.Show();
        }
    }
}
