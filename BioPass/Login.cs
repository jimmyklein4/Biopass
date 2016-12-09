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
            apps[x] = "Steam"; x++;
            apps[x] = "Battle.Net"; x++;
            apps[x] = "Outlook"; x++;
            apps[x] = "Discord"; x++;
            apps[x] = "MSWord"; x++;
            apps[x] = "Excel"; x++;
            apps[x] = "Access"; x++;
            apps[x] = "Pw Protected Excel File"; x++;

            i = 0;
            while (apps[i] != null)
            {
                comboBox1.Items.Add(apps[i]);
                i++;
            }
        }
        private string getAppBoxVal()
        {
            return (comboBox1.SelectedItem!=null?comboBox1.SelectedItem.ToString() : "");
        }

        private void go_Click(object sender, EventArgs e)
        {
            application = getAppBoxVal();
            Debug.Write(application);
            Close(); 
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
