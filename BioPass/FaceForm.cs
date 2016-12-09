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
using System.IO;

namespace BioPass
{
    public partial class FaceForm : Form
    {
        public FaceForm()
        {
            InitializeComponent();
            if (File.Exists(Directory.GetCurrentDirectory() + "/facereq.xml")) {
                rec = new FacialRecognition("facereq.xml");
            } else {
                rec = new FacialRecognition();
            }
        }

        private void FaceForm_Load(object sender, EventArgs e)
        {
            if (!DesignMode) {
                // Refresh the list of available cameras
                comboBoxCameras.Items.Clear();
                foreach (Camera cam in CameraService.AvailableCameras) { 
                    comboBoxCameras.Items.Add(cam);
                }
                //Debug.WriteLine(comboBoxCameras.Items.Count);
                if (comboBoxCameras.Items.Count > 0) {
                    comboBoxCameras.SelectedIndex = 0;
                    thrashOldCamera();
                    startCapturing();
                }

                foreach(Control item in this.Controls) {
                    item.TabStop = false;
                }

                init_fingerprint();
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
        public static Bitmap _latestFrame;
        public List<Image> _faces;
        public FacialRecognition rec;
        private Camera CurrentCamera {
           get
           {
              return comboBoxCameras.SelectedItem as Camera;
           }
        }

        private void comboBoxCameras_DropDownClose(object sender, EventArgs e) {
            if (_frameSource != null && _frameSource.Camera == comboBoxCameras.SelectedItem)
                return;
            thrashOldCamera();
            startCapturing();
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
                Debug.WriteLine("Changing camera to: " + c);
                setFrameSource(new CameraFrameSource(c));
                _frameSource.Camera.CaptureWidth = 640;
                _frameSource.Camera.CaptureHeight = 480;
                _frameSource.Camera.Fps = 20;
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

        private void drawLatestImage(object sender, PaintEventArgs e) {
            try { 
                if (_latestFrame != null) {
                    // Draw the latest image from the active camera
                        int Width = _latestFrame.Width;
                        int Height = _latestFrame.Height;
                        e.Graphics.DrawImage(_latestFrame, 0, 0, Width, Height);
                }
            } catch (Exception ex) { 
                thrashOldCamera();
                startCapturing();
                Debug.WriteLine(ex);
            }
        }

        public void OnImageCaptured(Touchless.Vision.Contracts.IFrameSource frameSource, Touchless.Vision.Contracts.Frame frame, double fps) {
            _latestFrame = frame.Image;
            _latestFrame.RotateFlip(RotateFlipType.RotateNoneFlipX);
            pictureBoxDisplay.Invalidate();
        }

        private void setFrameSource(CameraFrameSource cameraFrameSource)
        {
            if (_frameSource == cameraFrameSource)
                return;

            _frameSource = cameraFrameSource;
        }

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
        public void blankPin() {
            last4Ints = pinLabel.Text = "";
        }
        void FaceForm_KeyUp(object sender, KeyEventArgs e) {
            this.Focus();
            int keyValue = e.KeyValue;
            if ((keyValue >= 0x30 && keyValue <= 0x39) || (keyValue >= 0x41 && keyValue <= 0x5A)) {
               char keyChar = char.ToLower(Convert.ToChar(e.KeyCode));
                if(last4Ints == null) {
                    last4Ints = "";
                }
                if(last4Ints.Length == 4) {
                    last4Ints = "";
                }
           
                if(char.IsLetterOrDigit(keyChar)) {
                    last4Ints += keyChar;
                    pinLabel.Text = last4Ints;
                    //Debug.WriteLine(last4Ints);
                }
            }

            if(e.KeyCode==Keys.Enter) {
                compileUserData();
            }
            e.Handled = true;
        }

        // Get the current webcam image
        private Bitmap getWebcamImage() {
            Bitmap current = (Bitmap)_latestFrame.Clone(
                new Rectangle(0, 0, _latestFrame.Width, 
                _latestFrame.Height), _latestFrame.PixelFormat);
            return current;
        }

        // Get current fingerprint scan
        private Bitmap getFingerprint() {
            return null;
        }
         
        private String collectPin() {
            return last4Ints;
        }
 
        public void compileUserData(long fpid = -1) {
            Bitmap face = getWebcamImage();
            String pin = collectPin();

            Program.recieveCapture(fpid, face, pin);
        }

        // Detects face and stores it in a list for later re
        
        private void registerBtn_Click(object sender, EventArgs e) {
            Program.appmode = 1;
            appmodeLabel.Visible = true;
            appmodeLabel.Text = "Registering";

            newReg nameDialog = new newReg(); 
            if(nameDialog.ShowDialog() == DialogResult.OK) {
                Reset();
            } else {
                Reset();
            }
        }
        private void Reset() {
            pictureBoxDisplay.Focus();
            Program.appmode = 0;
            appmodeLabel.Visible = false;
            Program.target = 0;
        }
        public void postAuth(long _target)
        {
            Login LoginWin = new Login(_target);
            if (LoginWin.ShowDialog() == DialogResult.OK) {
                automateWeb web = new automateWeb(LoginWin.application, ""+_target, true);
            } else {
                automateWeb web = new automateWeb(LoginWin.application, ""+_target, true);
            }
            //Debug.Write(LoginWin.application);
        }
    }
}
