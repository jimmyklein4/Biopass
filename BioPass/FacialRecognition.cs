using System;
using System.Collections.Generic;
using System.IO;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.Face;
using System.Drawing;
using System.ComponentModel;
using System.Threading;


namespace BioPass {
    public class FacialRecognition : authMethod {
        private LBPHFaceRecognizer rec;
        private BackgroundWorker _recTrainerWorker;
        private BackgroundWorker _detectFaceWorker;
        private BackgroundWorker _addNewUserWorker;
        private CascadeClassifier cascadeClassifier;
        private List<Image<Gray, Byte>> _detectedFaces;
        private Mat testFaceMat;
        private const double LBPH_THRESHOLD = 100.0;
        /**
         * Constructor if you already have a training set that you want to load in
         */
        public FacialRecognition(String filename) {
            rec = new LBPHFaceRecognizer();
            rec.Load(filename);
            cascadeClassifier = new CascadeClassifier(@"Support\haarcascade_frontalface_default.xml");
            _detectedFaces = new List<Image<Gray, Byte>>();
        }
        /**
         * Default constructor. Loads in the default training set (pre-trained)
         */
        public FacialRecognition() {
            cascadeClassifier = new CascadeClassifier(@"Support\haarcascade_frontalface_default.xml");
            _detectedFaces = new List<Image<Gray, Byte>>();
            rec = new LBPHFaceRecognizer();
            //Imports default training set from AT faces. Faces were trained using LBPH with default values
            rec.Load(@"Support\default_training_set.xml");
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

        public void AddNewUser(Image[] faces, int label) {
            Image[] facesCopy = new Image[faces.Length];

            faces.CopyTo(facesCopy, 0);
            var args = new Tuple<Image[], int>(facesCopy, label);

            _addNewUserWorker = new BackgroundWorker();
            _addNewUserWorker.DoWork += addNewUser_DoWork;
            _addNewUserWorker.RunWorkerAsync(args);
        }

        private void addNewUser_DoWork(Object sender, DoWorkEventArgs args) {
            Tuple<Image[], int> sentArgs = (Tuple<Image[], int>)args.Argument;
            _detectedFaces = new List<Image<Gray, byte>>();
            for (int i = 0; i < sentArgs.Item1.Length; i++) {
                Image<Gray, Byte> face = new Image<Gray, Byte>((Bitmap)sentArgs.Item1[i]);
                Rectangle[] detected = cascadeClassifier.DetectMultiScale(face, 1.1, 10, Size.Empty);
                if (detected.Length > 0) {
                    //Array.Sort(detected, detected.Length);
                    //TODO sort array
                    face = face.Copy(detected[0]);
                    face = face.Resize(92, 112, 0);
                    _detectedFaces.Add(face);
                }
            }
            TrainRecognizer(sentArgs.Item2);
        }
        /**
         * DEPRECATED 
        * Facial detection method. Calling this will take the photo given to it, detect a face,
        * cut out and return only the facial detection area. 
        * Note: Because the LBPHFaceRecognizer requires you to have all the images the same size, 92x112 is hard coded
        */
        public void DetectFace(Image image) {
                _detectFaceWorker = new BackgroundWorker();
                _detectFaceWorker.DoWork += detectFace_DoWork;
                _detectFaceWorker.RunWorkerCompleted += detectFace_RunWorkerCompleted;
                _detectFaceWorker.RunWorkerAsync(image.Clone());

        }
        //DEPRECATED
        private void detectFace_DoWork(Object sender, DoWorkEventArgs args) {
            Image<Gray, Byte> faces = new Image<Gray, byte>((Bitmap)args.Argument);
            Rectangle[] detected = cascadeClassifier.DetectMultiScale(faces, 1.1, 10, Size.Empty);
            if (detected.Length > 0) {
                //Array.Sort(detected, detected.Length);
                //TODO sort array

                faces = faces.Copy(detected[0]);
                faces = faces.Resize(92, 112, 0);
                testFaceMat = faces.Mat;
                args.Result = faces;
            }
        }
        //DEPRECATED
        private void detectFace_RunWorkerCompleted(object Sender, RunWorkerCompletedEventArgs args) {
            _detectedFaces.Add((Image<Gray, Byte>)args.Result);
            Console.WriteLine("Detected Face Thread Completed. Number of faces:" + _detectedFaces.Count);
        }

        /**
         * Method to create initial LBPHface recoginizer. Uses faces given to it through faces with the labels being in labels
         */
        public void TrainRecognizer(int label) {
            if (_detectedFaces.Count >= 10) {
                _recTrainerWorker = new BackgroundWorker();
                _recTrainerWorker.DoWork += recTrainer_DoWork;
                _recTrainerWorker.RunWorkerAsync(label);
            } else {
                Console.WriteLine("Not enough faces");
            }

        }

        private void recTrainer_DoWork(object Sender, DoWorkEventArgs args) {
            List<int> labels = new List<int>();
            Image<Gray,Byte>[] facesArray = _detectedFaces.ToArray();
            for (int i = 0; i < facesArray.Length; i++) {
                labels.Add((int)args.Argument);
            }
            rec.Update(facesArray, labels.ToArray());
            Console.WriteLine("Rec is updated");
            _detectedFaces.Clear();
        }

        public Boolean IsRecognizerCreated() {
            return (rec != null && (_recTrainerWorker != null && !_recTrainerWorker.IsBusy));
        }

        /**
         * Save the LBPHFaceRecognizer to filename
         * XML file
         */
        public void SaveRecognizer(String filename) {
            rec.Save(filename);
        }

        /**
         * Update the recognizer. Call will still be from _detectedfaces 
         * may not work i have no idea
         */ 
        public void UpdateRecognizer(int label) {
            _recTrainerWorker = new BackgroundWorker();
            _recTrainerWorker.DoWork += recTrainer_DoWork;
            _recTrainerWorker.RunWorkerAsync(label);
        }

        //Returns the label for the user being detected
        public int IdentifyUser(object A) {
            Image<Gray, Byte> faces = new Image<Gray, byte>((Bitmap)A);
            Rectangle[] detected = cascadeClassifier.DetectMultiScale(faces, 1.1, 10, Size.Empty);
            if (detected.Length > 0) {
                //Array.Sort(detected, detected.Length);
                //TODO sort array

                faces = faces.Copy(detected[0]);
                faces = faces.Resize(92, 112, 0);
                return rec.Predict(faces).Label;
            }
            return -1;
        }
        //TODO: Implement
        public Boolean VerifyUser(object A, int user_id) {
            return false;
        }
    }
}