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
        //Sanity check. Checks a face included in the rec database against itself
        private void fake_check_Click(object sender, EventArgs e) {
            Console.WriteLine(rec.FakeRec(@"C:\at\s1\1.pgm"));
            rec.SaveRecognizer(@"C:\Users\james\Desktop\out.xml");
        }
    }
}
