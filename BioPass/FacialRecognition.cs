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
        private const double EIGEN_THRESHOLD = 2000.0;
        /**
         * Constructor if you already have a training set that you want to load in
         */
        public FacialRecognition(String filename) {
            rec = new EigenFaceRecognizer(80, double.PositiveInfinity);
            rec.Load(filename);
        }
        /**
         * Default constructor
         */
        public FacialRecognition() { }
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
        * Facial detection method. Calling this will take the photo given to it, detect a face,
        * cut out and return only the facial detection area. 
        * Note: Because the EigenFaceRecognizer requires you to have all the images the same size, 300x300 is hard coded
        */
        public static Bitmap DetectFace(Image image) {
            CascadeClassifier cascadeClassifier = new CascadeClassifier(@"C:\Emgu\emgucv-windesktop 3.1.0.2282\opencv\data\haarcascades\haarcascade_frontalface_default.xml");
            Image<Bgr, Byte> faces = new Image<Bgr, byte>((Bitmap)image);
            if (faces != null) {
                var detected = cascadeClassifier.DetectMultiScale(faces, 1.1, 10, Size.Empty);
                foreach (var face in detected) {
                    faces.Draw(face, new Bgr(0, double.MaxValue, 0), 3);
                }
                faces = faces.Resize(92, 112, 0);
                return faces.ToBitmap();
            }
            return null;
        }
        /**
         * Method to create initial Eigenface recoginizer. Uses faces given to it through faces with the labels being in labels
         */
        public void CreateInitialRecognizer(Image[] faces) {
            List<Image<Gray, Byte>> grayFaces = new List<Image<Gray, byte>>();
            List<int> labels = new List<int>();
            readCSV(@"C:\Users\james\Documents\Capstone\out.txt", ref grayFaces, ref labels);
            for(int i = 0; i < faces.Length; i++) {
                grayFaces.Add(new Image<Gray, byte>((Bitmap)faces[i]));
                labels.Add(40);
            }

            if (rec == null) { rec = new EigenFaceRecognizer(80, double.PositiveInfinity); }
            rec.Train(grayFaces.ToArray(), labels.ToArray());
        }
        //TODO: DELETE
        public int FakeRec(String filename) {
            Image<Gray, Byte> fake = new Image<Gray, Byte>(filename);
            fake = fake.Resize(92, 112, 0);
            return rec.Predict(fake).Label;
        }
        /**
         * Save the EigenFaceRecognizer to filename
         */
        public void SaveRecognizer(String filename) {
            rec.Save(filename);
        }
        //Prints out the label of the user to be identifed
        //2000 is currently set as the eigen threshold. The lower the better
        public int IdentifyUser(object A) {
            if (rec != null) {
                Image<Gray, Byte> recFace = new Image<Gray, byte>((Bitmap)(Image)A);
                recFace = recFace.Resize(92, 112, 0);
                var results = rec.Predict(recFace);
                if(results.Distance < EIGEN_THRESHOLD) {
                    return results.Label;
                }
            }
            return -1;
        }
        //TODO: DELETE
        public double GetDistance(Image a) {
            return rec.Predict(new Image<Gray, Byte>((Bitmap)a)).Distance;
        }
        //TODO: Implement
        public Boolean VerifyUser(object A, int user_id) {
            return false;
        }
    }
}