using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BioPass {
    public partial class newReg : Form {
        public long target;

        public newReg() {
            InitializeComponent();
        }

        private void firstRegForm_Load(object sender, EventArgs e) {
            toggleBioPanel(false);
            target = -1;
        }

        private void toggleBioPanel(Boolean toggle) {
            foreach (Control item in bioPanel.Controls)
            {
                 item.Enabled = toggle;
            }
            faceModBtn.Enabled = false;
        }
        private void toggleNamePanel(Boolean toggle) {
            foreach (Control item in namePanel.Controls)
            {
                 item.Enabled = toggle;
            }
        }
        private void createBtn_Click(object sender, EventArgs e) {
            String name = nameBox.Text;
            String pin = pinBox.Text;
            long user_id = Program.db.addUser(name, pin);
            target = user_id;
            toggleBioPanel(true);
            toggleNamePanel(false);
        }

        private void fpBtn_Click(object sender, EventArgs e) {
            fingerprintView fpView = new BioPass.fingerprintView(target);
            fpView.Show();
        }

        private void faceBtn_Click(object sender, EventArgs e) {

            // prep faces array
            if(Program.mainForm._faces == null) { Program.mainForm._faces = new List<Image>(); }
            try {
                lock (FaceForm._latestFrame) {
                    Program.mainForm._faces.Add((Image) FaceForm._latestFrame.Clone());
                }
            } catch (InvalidOperationException exeception) {
                Console.Write(exeception.ToString());
            }
            faceModBtn.Text = ""+Program.mainForm._faces.Count + "/10";
            if(Program.mainForm._faces.Count >= 10) {
                faceModBtn.Text = ""+Program.mainForm._faces.Count + "/10; Build model";
                faceModBtn.Enabled = true;
            }
        }

        private void faceModBtn_Click(object sender, EventArgs e) {
            //actually add the user now
            if (Program.mainForm.rec == null) {
                Program.mainForm.rec = new FacialRecognition("facereq.xml");
            }
            Program.mainForm.rec.AddNewUser(Program.mainForm._faces.ToArray(), (int) target);
            Program.mainForm._faces = null;
        }

        private void IrisBtn_Click(object sender, EventArgs e) {
            try {
                lock (FaceForm._latestFrame) {
                    Bitmap image = (Bitmap)FaceForm._latestFrame.Clone();
                    IrisAuth iris = new IrisAuth();
                    iris.AddUser(image, target);
                }
            } catch (InvalidOperationException exeception) {
                Console.Write(exeception.ToString());
            }
        }
    }
}
