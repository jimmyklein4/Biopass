using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Touchless.Vision.Camera;
using System.Drawing.Imaging;
using System.Diagnostics;

namespace BioPass
{
    public partial class FaceForm : Form
    {
        public FaceForm()
        {
            InitializeComponent();
        }

        private void FaceForm_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                // Refresh the list of available cameras
                comboBoxCameras.Items.Clear();
                foreach (Camera cam in CameraService.AvailableCameras)
                    comboBoxCameras.Items.Add(cam);

                if( comboBoxCameras.Items.Count > 0 )
                    comboBoxCameras.SelectedIndex = 0;

                thrashOldCamera();
                startCapturing();
            }

        }

        private void FaceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            thrashOldCamera();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            thrashOldCamera();
        }

        private CameraFrameSource _frameSource;
        private static Bitmap _latestFrame;
        private Boolean detectFaces = false;
        private List<Image> _faces;
        private FacialRecognition rec;
        private List<int> labels;
        private Camera CurrentCamera
        {
           get
           {
              return comboBoxCameras.SelectedItem as Camera;
           }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            // Early return if we've selected the current camera
            if (_frameSource != null && _frameSource.Camera == comboBoxCameras.SelectedItem)
                return;

            thrashOldCamera();
            startCapturing();
        }

        private void startCapturing()
        {
            try
            {
                Camera c = (Camera)comboBoxCameras.SelectedItem;
                setFrameSource(new CameraFrameSource(c));
                _frameSource.Camera.CaptureWidth = 640;
                _frameSource.Camera.CaptureHeight = 480;
                _frameSource.Camera.Fps = 10;
                _frameSource.NewFrame += OnImageCaptured;

                pictureBoxDisplay.Paint += new PaintEventHandler(drawLatestImage);
                _frameSource.StartFrameCapture();
            }
            catch (Exception ex)
            {
                comboBoxCameras.Text = "Select A Camera";
                MessageBox.Show(ex.Message);
            }
        }

        private void drawLatestImage(object sender, PaintEventArgs e)
        {
            if (_latestFrame != null)
            {
                if (detectFaces) {
                    _latestFrame = FacialRecognition.DetectFace(_latestFrame);
                }
                // Draw the latest image from the active camera
                e.Graphics.DrawImage(_latestFrame, 0, 0, _latestFrame.Width, _latestFrame.Height);
            }
        }

        public void OnImageCaptured(Touchless.Vision.Contracts.IFrameSource frameSource, Touchless.Vision.Contracts.Frame frame, double fps)
        {
            _latestFrame = frame.Image;
            pictureBoxDisplay.Invalidate();
        }

        private void setFrameSource(CameraFrameSource cameraFrameSource)
        {
            if (_frameSource == cameraFrameSource)
                return;

            _frameSource = cameraFrameSource;
        }

        //

        private void thrashOldCamera()
        {
            // Trash the old camera
            if (_frameSource != null)
            {
                _frameSource.NewFrame -= OnImageCaptured;
                _frameSource.Camera.Dispose();
                setFrameSource(null);
                pictureBoxDisplay.Paint -= new PaintEventHandler(drawLatestImage);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e) {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            /*     if (Program.auto != null)
                    {
                        Program.auto.service.Dispose();
                    }
                */
        }

        String last4Ints;
        void FaceForm_KeyUp(object sender, KeyEventArgs e) {
            int keyValue = e.KeyValue;
            if ((keyValue >= 0x30 && keyValue <= 0x39) || (keyValue >= 0x41 && keyValue <= 0x5A)) {
               char keyChar = char.ToLower(Convert.ToChar(e.KeyCode));
                if(last4Ints == null) {
                    last4Ints = "";
                }
                if(last4Ints.Length == 4) {
                    last4Ints = last4Ints.Substring(1, last4Ints.Length - 1);
                }
           
                if(char.IsLetterOrDigit(keyChar)) {
                    last4Ints += keyChar;
                    Debug.WriteLine(last4Ints);
                }

            }

            if(e.KeyCode==Keys.Enter) {
                compileUserData();
            }
            e.Handled = true;
        }

        // Get the current webcam image
        private Bitmap getWebcamImage() {
            Bitmap current = (Bitmap)_latestFrame.Clone();
            return current;
        }

        // Get current fingerprint scan
        private Bitmap getFingerprint() {
            return null;
        }
        private String collectPin() {
            return last4Ints;
        }
 
        void compileUserData() {
            Bitmap face = getWebcamImage();
            Bitmap finger = getFingerprint();
            String pin = collectPin();

            Program.recieveCapture(finger,face, pin);
        }
        // Detects face and stores it in a list for later rec
        private void detect_Click(object sender, EventArgs e) {
            if (_faces == null) {
                _faces = new List<Image>();
            }
            _faces.Add(FacialRecognition.DetectFace(_latestFrame));
        }
        // Starts the recognition process
        private void create_rec_Click(object sender, EventArgs e) {
            //rec = new FacialRecognition(@"C:\Users\james\Desktop\out.xml");
            if (_faces.Count >= 10) {
                if (rec == null) {
                    rec = new FacialRecognition();
                }
                rec.CreateInitialRecognizer(_faces.ToArray());
                _faces = null;
            }
        }
        // Checks the face it detects against the recognizer 
        private void check_Click(object sender, EventArgs e) {
            if (rec != null) {
                Console.WriteLine(rec.IdentifyUser(FacialRecognition.DetectFace(_latestFrame)));
                Console.WriteLine(rec.GetDistance(FacialRecognition.DetectFace(_latestFrame)));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (label1.Visible == true)
            {
                label1.Visible = false; textBox1.Visible = false; checkedListBox1.Visible = false;// button3.Visible = false;
            }
            else
            {
                textBox1.Visible = true; checkedListBox1.Visible = true; label1.Visible = true;//button3.Visible = true; 
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            String[] selections = new string[2]; //Stores checklist Selections
            for (int i = 0; i < checkedListBox1.CheckedItems.Count; i++)
            {
                selections[i] = checkedListBox1.CheckedItems[i].ToString();
            }
            if (checkedListBox1.CheckedItems.Count == 2)
            {
                textBox1.Visible = false;
                label1.Visible = false;
                checkedListBox1.Visible = false;
                checkedListBox1.SetItemChecked(0, false); //reset checkbox
                checkedListBox1.SetItemChecked(1, false); //reset checkbox
                checkedListBox1.SetItemChecked(2, false); //reset checkbox
                checkedListBox1.SetItemChecked(3, false); //reset checkbox
                //Launch BioMetrics
                //BioMetrics(selections[0],selections[1]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //db class not on this repo yet
             String user = "";
             int i = 0;
             int k = 0;
             string[] app = new string[] { "tumail", "blackboard.temple.edu", "facebook.com", "en.wikipedia.org" };
             string[] appCata = new string[4];
             string[] appUn = new string[4];
             String cata = "";
             String uncata = "";
             DBhandler db = new DBhandler();
             db.connectToDatabase();*/



             for (int j = 0; j < app.Length; j++)
             {
                //Boolean isCatlgd = false; //remove once db class back in repo
                 Boolean isCatlgd = db.appExistsForUser(app[j], user);
                 if (isCatlgd == true) { appCata[i] = app[j]; i++; }
                 else { appUn[k] = app[j]; k++; }
             }
             //  test filler
             appCata[0] = "test";
             appCata[1] = "test1";
             appCata[2] = " test2";
             //appUn[0] = "test2";
             //appUn[1] = "test3";
             // */
             for (i = 0; i < appCata.Length; i++) { cata = cata + appCata[i] + Environment.NewLine; }
             for (i = 0; i < appUn.Length; i++) { uncata = uncata + appUn[i] + Environment.NewLine; }

             label4.Text = cata;
             label2.Text = uncata;



                 //Will load list of all applications




             Button clickedButton = (Button)sender;

             if (clickedButton.Text == "Show Apps")
             {
                 clickedButton.Text = "Hide Apps"; label4.Visible = true;   //load list
                 label2.Visible = true; label6.Visible = true; label5.Visible = true; appList.Visible = true;
             }
             else
             {
                 clickedButton.Text = "Show Apps";
                 appList.Visible = false; label4.Visible = false; label5.Visible = false; //hide list 
                 label2.Visible = false; label6.Visible = false; 
             }
        }
    }
        }
