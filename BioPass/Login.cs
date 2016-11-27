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
        public Login()
        {
            InitializeComponent();
        }
        private void Login_Load(object sender, EventArgs e)
        {
            int x = 0; int i = 0;
            string[] apps = new string[100];
            apps[x] = "tumail"; x++;
            apps[x] = "blackboard.temple.edu"; x++;
            apps[x] = "facebook.com"; x++;
            apps[x] = "en.wikipedia.org"; x++;

            i = 0;
            while (apps[i] != null)
            {
                comboBox1.Items.Add(apps[i]);
                i++;
            }
        }
        private string getAppBoxVal()
        {
            return comboBox1.SelectedItem.ToString();
        }

        private void go_Click(object sender, EventArgs e)
        {
            application = getAppBoxVal();
            Debug.Write(application);
            Close(); 
        }
    }
}
