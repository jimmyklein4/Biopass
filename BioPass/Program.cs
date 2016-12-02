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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mainForm = new FaceForm();
            Application.Run(mainForm);
        } 
        public static void recieveCapture(long finger_uid, Bitmap face, String pin) {
            face.Save(tempFacePath);

            //Debug.WriteLine("Identifed from Face " + mainForm.rec.IdentifyUser(face));
            mainForm.blankPin();
            Boolean compareToFP = false;
            if(finger_uid != -1) {
                compareToFP = true;
            }

            int faceIdentity = mainForm.rec.IdentifyUser(face);

            if (!compareToFP && (pin == null || pin.Length != 4)) {
                MessageBox.Show("Bad or No pin provided. Face is not enough to verify user.");
            } else if (!compareToFP && (pin != null && pin.Length == 4)) {
                String match = db.compareIdToPin(faceIdentity, pin);
                if (match != null && match.Length > 0) {
                    MessageBox.Show("(Pin and Face) User identified as: " + match);
                    mainForm.postAuth(faceIdentity);
                }
            } else if (compareToFP && (pin != null && pin.Length == 4)) {
                String match = db.compareIdToPin(faceIdentity, pin);
                if (match != null && match.Length > 0 && finger_uid == faceIdentity) {
                    MessageBox.Show("(Pin, Face, FP) User identified as: " + match);
                    mainForm.postAuth(faceIdentity);
                }
            } else if (compareToFP) {
                Debug.WriteLine(finger_uid);
                Debug.WriteLine(faceIdentity);
                if (faceIdentity == finger_uid) {
                    String match = db.getUserName(finger_uid);
                    MessageBox.Show("(Face, FP) User identified as: " + match);
                    mainForm.postAuth(faceIdentity);
                }
            }
          //finger.Save(tempFingerPath);
        }
        
    }
}
