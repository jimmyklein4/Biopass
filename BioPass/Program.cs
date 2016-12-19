using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Collections.Specialized;

namespace BioPass {
    public static class Program
    {
        //appmode = -1 is no mode
        //appmode = 0 is login
        //appmode = 1 is register
        public static int appmode = -1;
        public static String tempFacePath = System.Environment.CurrentDirectory + "/face.jpg";
        public static String tempFingerPath = System.Environment.CurrentDirectory + "/finger.jpg";
        public static String PIN;
        public static DBhandler db = new DBhandler();
        public static long target = -1;
        public static Object lastFingerprint;
        public static FaceForm mainForm;
        public static fingerprintView fingerprintForm;
        public static CredentialsView credentialForm;

        // For Fingerprint enrollment
        public static String targetFingerprintName;
        public static long targetFingerprintID;
        public static long targetFingerUserID;
        public static long targetFingerRow;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
           if (!db.exists()) {
                db.createNewDatabase();
            } else {
                db.connectToDatabase();
            }
            appLoad.loadfromJson();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mainForm = new FaceForm();
            //mainForm.postAuth(0);
            Application.Run(mainForm);

        } 
        public static void recieveCapture(long finger_uid, Bitmap face, String pin) {
            face.Save(tempFacePath);

            //Debug.WriteLine("Identifed from Face " + mainForm.rec.IdentifyUser(face));
            mainForm.blankPin();
            Boolean compareToFP = false;
            long authenticatedAs = -1;
            int fitSecurityLevel = -1;
            if(finger_uid != -1) {
                compareToFP = true;
            }

            int faceIdentity = mainForm.rec.IdentifyUser(face);
            Debug.Write(faceIdentity);
            if (!compareToFP && (pin == null || pin.Length != 4)) {
                MessageBox.Show("Bad or No pin provided. Face is not enough to verify user.");
            } else if(compareToFP && (faceIdentity == -1 || faceIdentity < 41) && (pin != null && pin.Length == 4) ) {
                //LEVEL 0
                String match = db.compareIdToPin(finger_uid, pin);
                if (match != null && match.Length > 0) {
                    MessageBox.Show("(Pin, FP) User identified as: " + match);
                    authenticatedAs = finger_uid;
                    fitSecurityLevel = 0;
                }
            } else if (!compareToFP && (pin != null && pin.Length == 4)) {
                //LEVEL 0
                String match = db.compareIdToPin(faceIdentity, pin);
                if (match != null && match.Length > 0) {
                    MessageBox.Show("(Pin and Face) User identified as: " + match);
                    authenticatedAs = faceIdentity;
                    fitSecurityLevel = 0;
                }
            } else if (compareToFP && (pin != null && pin.Length == 4)) {
                //LEVEL 2
                String match = db.compareIdToPin(faceIdentity, pin);
                if (match != null && match.Length > 0 && finger_uid == faceIdentity) {
                    MessageBox.Show("(Pin, Face, FP) User identified as: " + match);
                    authenticatedAs = faceIdentity;
                    fitSecurityLevel = 2;

                }
            } else if (compareToFP) {
                //LEVEL 1
                if (faceIdentity == finger_uid) {
                    String match = db.getUserName(finger_uid);
                    MessageBox.Show("(Face, FP) User identified as: " + match);
                    authenticatedAs = faceIdentity;
                    fitSecurityLevel = 1;
                }
            } else {
                MessageBox.Show("Cannot identify user.");
            }
            if(authenticatedAs > -1) {
                int userSecurityLevel = db.getUserSecurityLevel(authenticatedAs);
                if(userSecurityLevel <= fitSecurityLevel) { 
                    mainForm.postAuth(authenticatedAs);
                } else {
                    if(fitSecurityLevel > 0 && userSecurityLevel == 2) {
                        String irisMessage = @"This user requires Iris authentication. 
Please bring your eye into the viewport's focus 
and close this box to begin authentication. 
If Iris does not work, please attempt pin authentication.";
                        if(MessageBox.Show(irisMessage, "Iris Required", MessageBoxButtons.OK) == DialogResult.OK) {
                            try {
                                lock (FaceForm._latestFrame) {
                                    Bitmap image = (Bitmap)FaceForm._latestFrame.Clone(
                                        new Rectangle(0, 0, FaceForm._latestFrame.Width, FaceForm._latestFrame.Height),
                                        FaceForm._latestFrame.PixelFormat);
                                    IrisAuth iris = new IrisAuth();
                                    Boolean IrisMatch = iris.VerifyUser(image, (int)authenticatedAs);
                                    if(IrisMatch) {
                                        MessageBox.Show("Iris authenticated.");
                                        mainForm.postAuth(authenticatedAs);
                                    } else {
                                        MessageBox.Show("Iris authenticated failed.");
                                    }
                                }
                            } catch (InvalidOperationException exeception) {
                                Console.Write(exeception.ToString());
                            }
                        }

                    }
                }
            }

        }
        
    }
}
