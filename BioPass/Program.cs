using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;


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
            Application.Run(new FaceForm());
        }

        public static void recieveCapture(Bitmap finger, Bitmap face, String pin) {
            face.Save(tempFacePath);
          //finger.Save(tempFingerPath);
        }
    }
}
