﻿using System;
using MathNet.Numerics.IntegralTransforms;
using System.Numerics;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;
using System.Data.SQLite;

namespace BioPass
{
    class IrisAuth : authMethod
    {
        public int IdentifyUser(object image){
            SQLiteConnection.CreateFile("test.db");
            SQLiteConnection conn = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3");
            String select = "select iris_data, user_id from biometricProperties;";
            SQLiteCommand cmd = new SQLiteCommand(select, conn);
            SQLiteDataReader reader = cmd.ExecuteReader();
            int id = -1;
            double min = 1.0;
            while (reader.Read())
            {
                int[] template1 = Base64ToTemplate(reader["iris_data"].ToString());
                double dist = HammingDistance(template1, GetTemplate(image), null, null);
                id  = dist < min ? (int) reader["user_id"] : id;
                min = dist < min ? dist : min;
            }
            return id;
        }

        public void UpdateUserTemplate(int userId, object image)
        {
            String data = templateToBase64(GetTemplate(image));
            SQLiteConnection.CreateFile("test.db");
            SQLiteConnection conn = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3");
            String update = "update biometricProperties set iris_data = " + data + 
                " where user_id = " + userId + ";";
            SQLiteCommand cmd = new SQLiteCommand(update, conn);
            cmd.ExecuteNonQuery();
        }

        public Boolean VerifyUser(object image, int userId){
            SQLiteConnection.CreateFile("test.db");
            SQLiteConnection conn = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3");
            String select = "select iris_data from biometricProperties where user_id = "
                + userId + ";";
            SQLiteCommand cmd = new SQLiteCommand(select, conn);
            SQLiteDataReader reader = cmd.ExecuteReader();
            int[] template2 = GetTemplate(image);
            while (reader.Read())
            {
                int[] template1 = Base64ToTemplate(reader["iris_data"].ToString());
                return (HammingDistance(template1, template2, null, null) < .3);
            }
            return false;
        }
        
        private int[] GetTemplate(object img)
        {
            Image<Gray, byte> image = (Image<Gray, byte>) img;
            //Set ROI of eye region
            CascadeClassifier classifier = new CascadeClassifier(@"Support\haarcascade_eye.xml");
            image.ROI = Rectangle.Empty;
            image.ROI = classifier.DetectMultiScale(image)[0];
            CvInvoke.Imshow("Croped Image", image);
            CvInvoke.WaitKey(0);
            CvInvoke.DestroyAllWindows();
            //Set ROI of iris region
            CircleF circles = FindCircles(image);
            image.ROI = Rectangle.Empty;
            Rectangle roi = new Rectangle((int) circles.Center.X, (int) circles.Center.Y, 
                (int) circles.Radius, (int) circles.Radius);
            image.ROI = roi;
            Image<Gray, byte> unraveled = Unravel(image, circles);
            Complex[][][] convolved = LogGabor(unraveled);
            return PhaseQuantifier(convolved[0]); //there's only 1 anyway
        }

        private CircleF FindCircles(Image<Gray, byte> image)
        {
            Gray canny = new Gray(16);
            int minAccumulator = 0;
            int currAcumulator = 200;
            double dp = 12;
            double minDistance = 10;
            int minSize = 0;
            int maxSize = 0;
            CircleF maxCircle = new CircleF();
            while (currAcumulator > minAccumulator)
            {
                Gray accumulator = new Gray(currAcumulator);
                CircleF[][] circles = image.Clone().HoughCircles(
                    canny,
                    accumulator,
                    dp,
                    minDistance,
                    minSize,
                    maxSize);
                if (circles.Length > 0)
                    return circles[0][0];
                currAcumulator--;
            }
            return maxCircle;
        }

        private Image<Gray, byte> Unravel(Image<Gray, byte> image, CircleF circle)
        {
            PointF center = circle.Center;
            double M = image.Width / Math.Log(image.Height);
            //This may cause errors based on how EmguCV handles these methods.
            //If so, just create new Images to store the return from these methods
            image = image.LogPolar(center, M);
            //Eliminate null space here
            image = image.Resize(20, 240, Emgu.CV.CvEnum.Inter.Linear);
            return image;
        }

        private Double HammingDistance(int[] template1, int[] template2, int[] mask1, int[] mask2)
        {
            int diffs = 0;
            int comps = 0;
            mask1 = mask1 == null ? new int[template1.Length] : mask1;
            mask2 = mask2 == null ? new int[template1.Length] : mask2;
            for (int i = 0; i < template1.Length; i++)
            {
                if (mask1[i] != 1 && mask2[i] != 1)
                {
                    if (template1[i] != template2[i])
                    {
                        diffs++;
                    }
                    comps++;
                }
            }
            return diffs / comps;
        }

        private int[] PhaseQuantifier(Complex[][] data)
        {
            double[] real = new double[data.Length * data[0].Length];
            double[] imag = new double[data.Length * data[0].Length];
            for (int i = 0; i < data.Length; i++)
            {
                for(int j = 0; j < data[0].Length; j++)
                {
                    real[i * data[0].Length + j] = data[i][j].Real;
                    imag[i * data[0].Length + j] = data[i][j].Imaginary;
                }
            }

            int[] template = new int[real.Length * 2];
            for (int i = 0; i < real.Length; i++)
            {
                if (real[i] > 0) template[i * 2] = 1;     else template[i * 2] = 0;
                if (imag[i] > 0) template[i * 2 + 1] = 1; else template[i * 2 + 1] = 0; 
            }
            return template;
        }

        private Double[] GetFilter(Double sigma, int length, Double wavelength)
        {
            double[] radius = new double[length];
            if (length % 2 == 0) length = length - 1;
            for(int i = 0; i < radius.Length; i++)
            {
                radius[i] = i * (0.5 / length);
            }
            radius[0] = 1;
            double freq = 1.0 / wavelength;
            for(int i = 0; i < radius.Length; i++)
            {
                radius[i] = Math.Exp((-1 * Math.Pow(Math.Log(radius[i] / freq), 2)) /
                    (2 * Math.Pow(Math.Log(sigma), 2)));
            }
            return radius;
        }

        private Complex[][][] LogGabor(Image<Gray, byte> img)
        {
            int rotations = 1;
            int mult = 8;
            int wavelength = 8;
            double sigma = .5;
            Double[] filter = new Double[img.Height];
            Complex[][][] results = new Complex[rotations][][];
            for(int i = 0; i < rotations; i++)
            {
                Complex[][] response = new Complex[img.Height][];
                filter = GetFilter(sigma, img.Height, wavelength);
                for(int j = 0; j < img.Width; j++)
                {
                    //idk if there's a method for this
                    Complex[] signal = new Complex[img.Height];
                    for (int k = 0; i < img.Height; k++)
                    {
                        signal[k] = img[k, j].Intensity;
                    }
                    Fourier.Forward(signal);
                    signal = ApplyFilter(filter, signal);
                    Fourier.Inverse(signal);
                    response[j] = signal;
                }
                results[i] = response;
                wavelength = wavelength * mult;
            }
            return results;
        }

        private Complex[] ApplyFilter(double[] filter, Complex[] signal)
        {
            for(int i = 0; i < filter.Length; i++)
            {
                signal[i] = signal[i] * filter[i];
            }
            return signal;
        }

        private String templateToBase64(int[] template)
        {
            byte[] bytes = new byte[template.Length / 8 + 1];
            int i = 0;
            int k = 0;
            while(i < template.Length)
            {
                byte b = 0;
                for(int j=0; j<8; j++)
                {
                    if (i < template.Length)
                    {
                        b = (byte)(b | (template[i] << j));
                        i++;
                    } else
                    {
                        break;
                    }
                }
                bytes[k] = b;
                k++;
            }
            return Convert.ToBase64String(bytes);
        }
        
        private int[] Base64ToTemplate(String base64)
        {
            byte[] bytes = Convert.FromBase64String(base64);
            int[] template = new int[bytes.Length * 8];
            int j = 0;
            foreach(byte b in bytes)
            {
                for(int i=0; i<8; i++)
                {
                    template[j] = (b >> i) & 1;
                }
                j++;
            }
            return template;
        }

    }
}
