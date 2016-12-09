using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//TODO: Auto-take photos

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
            fingerprintView fpView = new fingerprintView(target);
            fpView.Show();
        }

        private void faceBtn_Click(object sender, EventArgs e) {

            // prep faces array
            if(Program.mainForm._faces == null) { Program.mainForm._faces = new List<Image>(); }
            try {
                while (Program.mainForm._faces.Count < 10) {
                    lock (FaceForm._latestFrame) {
                        Program.mainForm._faces.Add((Image)FaceForm._latestFrame.Clone(
                            new Rectangle(0, 0, FaceForm._latestFrame.Width, FaceForm._latestFrame.Height),
                            FaceForm._latestFrame.PixelFormat));
                    }
                }
            } catch (InvalidOperationException exeception) {
                Console.Write(exeception.ToString());
            }
            /*
            if(Program.mainForm._faces.Count >= 10) {
                faceBtn.Text = ""+Program.mainForm._faces.Count + "";
                faceBtn.Enabled = true;
            }*/

            //actually add the user now
            if (Program.mainForm.rec == null) {
                Program.mainForm.rec = new FacialRecognition("facereq.xml");
            }
            Program.mainForm.rec.AddNewUser(Program.mainForm._faces.ToArray(), (int)target);
            Program.mainForm._faces = null;
            faceBtn.Text = "Facial model built";
        }
/*
        private void faceModBtn_Click(object sender, EventArgs e) {
            //actually add the user now
            if (Program.mainForm.rec == null) {
                Program.mainForm.rec = new FacialRecognition("facereq.xml");
            }
            Program.mainForm.rec.AddNewUser(Program.mainForm._faces.ToArray(), (int) target);
            Program.mainForm._faces = null;
        }*/

        private void IrisBtn_Click(object sender, EventArgs e) {
            try {
                lock (FaceForm._latestFrame) {
                    Bitmap image = (Bitmap)FaceForm._latestFrame.Clone(
                        new Rectangle(0, 0, FaceForm._latestFrame.Width, FaceForm._latestFrame.Height),
                        FaceForm._latestFrame.PixelFormat);
                    IrisAuth iris = new IrisAuth();
                    iris.AddUser(image, target);
                }
            } catch (InvalidOperationException exeception) {
                Console.Write(exeception.ToString());
            }
        }

        private void credsBtn_Click(object sender, EventArgs e) {
            CredentialsView credsView = new CredentialsView(target);
            credsView.Show();
        }
    }
}
