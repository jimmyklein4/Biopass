using System;
using System.Collections.Generic;
using System.IO;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.Face;
using System.Drawing;

namespace BioPass {
    class FacialRecognition : authMethod {
        private EigenFaceRecognizer rec;
        private Capture cap;

        /**
         * Constructor if you already have a training set that you want to load in
         */
        public FacialRecognition(String filename) {
            rec = new EigenFaceRecognizer();
            rec.Load(filename);
        }
        /**
         * Default constructor
         */
        public FacialRecognition() { }

        /**
         * Private method used to take the photos 
         */
        private Image<Gray, Byte> takePhotos() {
            if (cap == null) {
                cap = new Capture();
            }
            cap.Start();
            return cap.QueryFrame().ToImage<Gray, Byte>();
        }
        /**
         * Reads images and labels in from a CSV file 
         */
        private static void readCSV(String filename, ref List<Image<Gray, Byte>> images, ref List<int> labels) {
            char separator = ';';
            Mat pic;
            String[] allPhotos = File.ReadAllLines(filename);
            for (int i = 0; i < allPhotos.Length; i++) {
                String[] splitValues = allPhotos[i].Split(separator);
                if (splitValues.Length == 2) {
                    pic = CvInvoke.Imread(splitValues[0], Emgu.CV.CvEnum.LoadImageType.AnyColor);
                    images.Add(pic.ToImage<Gray, Byte>());
                    labels.Add(Int32.Parse(splitValues[1]));
                }
            }
        }
        /**
         * Facial detection method. Calling this will take a photo of the person, detect their face,
         * cut out and return only the facial detection area. 
         * Note: Because the EigenFaceRecognizer requires you to have all the images the same size, 300x300 is hard coded
         */
        public Image<Gray, Byte> DetectFace() {
            CascadeClassifier cascadeClassifier = new CascadeClassifier(@"C:\Emgu\emgucv-windesktop 3.1.0.2282\opencv\data\haarcascades\haarcascade_frontalface_default.xml");
            Image<Gray, Byte> face = this.takePhotos();
            if (face != null) {
                Rectangle[] detected = cascadeClassifier.DetectMultiScale(face, 1.1, 10, Size.Empty);
                while (detected.Length == 0) {
                    face = this.takePhotos();
                    detected = cascadeClassifier.DetectMultiScale(face, 1.1, 10, Size.Empty);
                }
                Rectangle fixedFace = new Rectangle(detected[0].X, detected[0].Y, 300, 300);
                Image<Gray, Byte> cutFace = face.Copy(fixedFace);
                return cutFace;
            }
            return null;
        }
        /**
        * Facial detection method. Calling this will take the photo given to it, detect a face,
        * cut out and return only the facial detection area. 
        * Note: Because the EigenFaceRecognizer requires you to have all the images the same size, 300x300 is hard coded
        */
        public static Image<Gray, Byte> DetectFace(Image<Gray, Byte> face) {
            CascadeClassifier cascadeClassifier = new CascadeClassifier(@"C:\Emgu\emgucv-windesktop 3.1.0.2282\opencv\data\haarcascades\haarcascade_frontalface_default.xml");
            if (face != null) {
                Rectangle[] detected = cascadeClassifier.DetectMultiScale(face, 1.1, 10, Size.Empty);
                if (detected.Length == 0) {
                    return null;
                }
                Rectangle fixedFace = new Rectangle(detected[0].X, detected[0].Y, 300, 300);
                Image<Gray, Byte> cutFace = face.Copy(fixedFace);
                return cutFace;
            }
            return null;
        }

        public static Bitmap DetectFace(Image image) {
            CascadeClassifier cascadeClassifier = new CascadeClassifier(@"C:\Emgu\emgucv-windesktop 3.1.0.2282\opencv\data\haarcascades\haarcascade_frontalface_default.xml");
            Image<Bgr, Byte> faces = new Image<Bgr, byte>((Bitmap)image);
            if (faces != null) {
                var detected = cascadeClassifier.DetectMultiScale(faces, 1.1, 10, Size.Empty);
                foreach (var face in detected) {
                    faces.Draw(face, new Bgr(0, double.MaxValue, 0), 3);
                }
                return faces.ToBitmap();
            }
            return null;
        }
        /**
         * Method to create initial Eigenface recoginizer. Default uses 10 values
         */
        public void CreateInitialRecognizer() {
            Image<Gray, Byte>[] faces = new Image<Gray, byte>[10];
            int[] labels = new int[10];
            for (int i = 0; i < 10; i++) {
                faces[i] = this.DetectFace();
                labels[i] = 0;
            }
            rec = new EigenFaceRecognizer();
            rec.Train<Gray, Byte>(faces, labels);
        }
        /**
         * Method to create initial Eigenface recoginizer. Takes trainingSamles amount of samles
         */
        public void CreateInitialRecognizer(int trainingSamles) {
            Image<Gray, Byte>[] faces = new Image<Gray, byte>[10];
            int[] labels = new int[trainingSamles];
            for (int i = 0; i < trainingSamles; i++) {
                faces[i] = this.DetectFace();
                labels[i] = 0;
            }
            rec = new EigenFaceRecognizer();
            rec.Train<Gray, Byte>(faces, labels);
        }
        /**
         * Method to create initial Eigenface recoginizer. Uses faces given to it through faces with the labels being in labels
         */
        public void CreateInitialRecognizer(Image<Gray, Byte>[] faces, int[] labels) {
            rec = new EigenFaceRecognizer();
            rec.Train(faces, labels);
        }
        /**
         * Save the EigenFaceRecognizer to filename
         */
        public void SaveRecognizer(String filename) {
            rec.Save(filename);
        }
        //TODO: Implement
        int authMethod.IdentifyUser(object A) {
            return 0;
        }

        //TODO: Implement
        Boolean authMethod.VerifyUser(object A, int user_id) {
            return false;
        }
    }
}