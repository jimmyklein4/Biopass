using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BioPass
{
    public partial class pathPrompt : Form
    {
        public pathPrompt()
        {
            InitializeComponent();
            openFileDialog1.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            openFileDialog1.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        /*
         * Prompts and sets path in automation script for password protected Excel Document
         */
        private void button1_Click(object sender, EventArgs e)
        {
            desktopAutomater excelPath = new desktopAutomater();

            excelPath.parsePath(label2.Text, @"Login Scripts\Excel13PWProtectedWorkBook.ahk");
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            String directory = System.IO.Path.GetDirectoryName(openFileDialog1.FileName) + "\\" + openFileDialog1.SafeFileName;
            label2.Text = directory;
        }
    }
}
