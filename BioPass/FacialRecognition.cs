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

//TODO: change rec so that it creates a new thread inside of this class 
// instead of having to call the thread outside of the class

//TODO: Save all face data so that it can be loaded in again. 

//TODO: Go through and delete methods that are no longer needed

//TODO: Write csv for file structure?
namespace BioPass {
    class FacialRecognition : authMethod {
        private EigenFaceRecognizer rec;
        private Capture cap;
        private BackgroundWorker _recTrainerWorker;
        private BackgroundWorker _detectFaceWorker;
        private CascadeClassifier cascadeClassifier;
        private List<Image<Gray, Byte>> _detectedFaces;
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
        public FacialRecognition() {
            cascadeClassifier = new CascadeClassifier(@"Support\haarcascade_frontalface_default.xml");
            _detectedFaces = new List<Image<Gray, Byte>>();

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
        * Facial detection method. Calling this will take the photo given to it, detect a face,
        * cut out and return only the facial detection area. 
        * Note: Because the EigenFaceRecognizer requires you to have all the images the same size, 300x300 is hard coded
        */
        public void DetectFace(Image image) {
            //idk if this is the right way to reference the file 
                _detectFaceWorker = new BackgroundWorker();
                _detectFaceWorker.DoWork += detectFace_DoWork;
                _detectFaceWorker.RunWorkerCompleted += detectFace_RunWorkerCompleted;
                _detectFaceWorker.RunWorkerAsync(image);

        }

        private void detectFace_DoWork(Object sender, DoWorkEventArgs args) {
            Image<Gray, Byte> faces = new Image<Gray, byte>((Bitmap)args.Argument);
            Rectangle[] detected = cascadeClassifier.DetectMultiScale(faces, 1.1, 10, Size.Empty);
            if (detected.Length > 0) {
                //Array.Sort(detected, detected.Length);
                //TODO sort array

                faces = faces.Copy(detected[0]);
                faces = faces.Resize(92, 112, 0);
                faces.Save(_detectedFaces.Count + ".jpg");
                args.Result = faces;
            }
        }

        private void detectFace_RunWorkerCompleted(object Sender, RunWorkerCompletedEventArgs args) {
            _detectedFaces.Add((Image<Gray, Byte>)args.Result);
            Console.WriteLine("Detected Face Thread Completed. Number of faces:" + _detectedFaces.Count);
        }

        /**
         * Method to create initial Eigenface recoginizer. Uses faces given to it through faces with the labels being in labels
         */
        public void CreateInitialRecognizer() {
           if (rec == null) { rec = new EigenFaceRecognizer(80, double.PositiveInfinity); }
            if (_detectedFaces.Count >= 10) {
                _recTrainerWorker = new BackgroundWorker();
                _recTrainerWorker.DoWork += recTrainer_DoWork;
                _recTrainerWorker.RunWorkerAsync(_detectedFaces);
                //_detectedFaces.Clear();
            } else {
                Console.WriteLine("Not enough faces");
            }
         
        }

        private void recTrainer_DoWork(object Sender, DoWorkEventArgs args) {
            // Look into how arguments work
            // may need to pack everything into a multi-dimensional array?
            // How to ensure safety? 
            List<Image<Gray, Byte>> grayFaces = new List<Image<Gray, byte>>();
            List<int> labels = new List<int>();
            Console.WriteLine("Detected faces length: " + _detectedFaces.ToArray());
            Image<Gray,Byte>[] facesArray = _detectedFaces.ToArray();

            readCSV(@"Support\face_directory.txt", ref grayFaces, ref labels);
            Console.WriteLine("Gray faces count: " + grayFaces.Count);
            Console.WriteLine("Labels count: " + labels.Count);
            Console.WriteLine(facesArray.Length);
            for (int i = 0; i < facesArray.Length; i++) {
                grayFaces.Add(facesArray[i]);
                labels.Add(40);
            }
            Console.WriteLine("Gray faces count: " + grayFaces.ToArray().Length);
            Console.WriteLine("Labels count: " + labels.ToArray().Length);
            rec.Train(grayFaces.ToArray(), labels.ToArray());
            Console.WriteLine("Rec is trained");
            _detectedFaces.Clear();
        }

        public Boolean IsRecognizerCreated() {
            return (rec != null && (_recTrainerWorker != null && !_recTrainerWorker.IsBusy));
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
                //DetectFace((Bitmap)A);
                //This is breaking because detect face is now async. need to wait somehow. 
                //create some form of callback?
                var results = rec.Predict(_detectedFaces[0]);
                Console.WriteLine(results.Distance);
                if(results.Distance < EIGEN_THRESHOLD) {
                    _detectedFaces.Clear();
                    return results.Label;
                }
                //TODO: DELETE. DANGEROUS CALL
                _detectedFaces.Clear();
            }
            return -1;
        }
        //TODO: Implement
        public Boolean VerifyUser(object A, int user_id) {
            return false;
        }
    }
}