using System;
using System.Numerics;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;
using System.Data.SQLite;
using MathNet.Numerics.IntegralTransforms;

namespace BioPass
{
    class IrisAuth : authMethod
    {
        public int IdentifyUser(object image)
        {
            SQLiteConnection conn = new SQLiteConnection("Data Source=biopass.sqlite;Version=3");
            String select = "select iris_data, user_id from biometricProperties;";
            SQLiteCommand cmd = new SQLiteCommand(select, conn);
            SQLiteDataReader reader = cmd.ExecuteReader();
            int id = -1;
            double min = 1.0;
            while (reader.Read())
            {
                int[] template1 = Base64ToTemplate(reader.GetString(reader.GetOrdinal("iris_data")));
                double dist = HammingDistance(template1, GetTemplate(image), null, null);
                id = dist < min ? (int)reader.GetInt32(reader.GetOrdinal("user_id")) : id;
                min = dist < min ? dist : min;
            }
            return id;
        }

        public void UpdateUserTemplate(object image, int userId)
        {
            String data = templateToBase64(GetTemplate(image));
            String oldData = "";
            String select = "select iris_data from biometricProperties where user_id = " + userId + ";";
            SQLiteConnection conn = new SQLiteConnection("Data Source=biopass.sqlite;Version=3");
            SQLiteDataReader reader = new SQLiteCommand(select, conn).ExecuteReader();
            while (reader.Read()) //only 1 record should return lul
            {
                oldData = reader.GetString(reader.GetOrdinal("iris_data")) + ";";
            }
            data = oldData + data;
            String update = "update biometricProperties set iris_data = " + data +
                " where user_id = " + userId + ";";
            SQLiteCommand cmd = new SQLiteCommand(update, conn);
            cmd.ExecuteNonQuery();
        }

        public Boolean VerifyUser(object image, int userId)
        {
            SQLiteConnection conn = new SQLiteConnection("Data Source=biopass.sqlite;Version=3");
            String select = "select iris_data from biometricProperties where user_id = "
                + userId + ";";
            SQLiteCommand cmd = new SQLiteCommand(select, conn);
            SQLiteDataReader reader = cmd.ExecuteReader();
            int[] template2 = GetTemplate(image);
            while (reader.Read())
            {
                int[] template1 = Base64ToTemplate(reader.GetString(reader.GetOrdinal("iris_data")));
                return (HammingDistance(template2, template1, null, null) < .3);
            }
            return false;
        }

        private int[] GetTemplate(object img)
        {
            Image<Gray, byte> image = new Image<Gray, byte>((Bitmap)img);
            //Set ROI of eye region
            CascadeClassifier classifier = new CascadeClassifier(@"Support\haarcascade_eye.xml");
            image.ROI = Rectangle.Empty;
            try
            {
                image.ROI = classifier.DetectMultiScale(image)[0];
            }
            catch (Exception) { }
            Image<Gray, byte> showImage = image.Clone();
            //Set ROI of iris region
            CircleF circles = FindCircles(image);
            image.ROI = Rectangle.Empty;
            Rectangle roi = new Rectangle((int)circles.Center.X, (int)circles.Center.Y,
                (int)circles.Radius, (int)circles.Radius);
            image.ROI = roi;

            //Show segmentation
            CvInvoke.Circle(showImage, Point.Round(circles.Center), (int)circles.Radius, new Rgb(255, 0, 0).MCvScalar);
            CvInvoke.Imshow("Croped Image", showImage);
            CvInvoke.WaitKey(0);
            CvInvoke.DestroyAllWindows();

            //Normalize and encode features
            Image<Gray, byte> unraveled = Unravel(image, circles);
            Complex[][][] convolved = LogGabor(unraveled);
            return PhaseQuantifier(convolved[0]); //there's only 1 anyway
        }

        private CircleF FindCircles(Image<Gray, byte> image)
        {
            Gray canny = new Gray(200);
            int minAccumulator = 0;
            int currAcumulator = 200;
            double dp = 2.5;
            double minDistance = 10;
            int minSize = 0;
            int maxSize = 0;
            CircleF maxCircle = new CircleF();
            while (currAcumulator > minAccumulator)
            {
                Gray accumulator = new Gray(currAcumulator);
                CircleF[][] circles = image.Clone().SmoothGaussian(7).HoughCircles(
                    canny,
                    accumulator,
                    dp,
                    minDistance,
                    minSize,
                    maxSize);
                if (circles[0].Length > 0)
                    return circles[0][0];
                currAcumulator--;
            }
            return maxCircle;
        }//

        private Image<Gray, byte> Unravel(Image<Gray, byte> image, CircleF circle)
        {
            PointF center = circle.Center;
            double M = circle.Radius / Math.Log(circle.Radius);
            image = image.LogPolar(center, M);
            image = RemoveNull(image);
            image = image.Resize(20, 240, Emgu.CV.CvEnum.Inter.Linear);
            return image;
        }

        private Double HammingDistance(int[] template1, int[] template2, int[] mask1, int[] mask2)
        {
            double diffs = 0;
            double comps = 0;
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
                for (int j = 0; j < data[0].Length; j++)
                {
                    real[i * data[0].Length + j] = data[i][j].Real;
                    imag[i * data[0].Length + j] = data[i][j].Imaginary;
                }
            }

            int[] template = new int[real.Length * 2];
            for (int i = 0; i < real.Length; i++)
            {
                if (real[i] > 0) template[i * 2] = 1; else template[i * 2] = 0;
                if (imag[i] > 0) template[i * 2 + 1] = 1; else template[i * 2 + 1] = 0;
            }
            return template;
        }

        private Double[] GetFilter(Double sigma, int length, Double wavelength)
        {
            if (length % 2 == 0) length = length - 1;
            double[] radius = new double[length];
            for (int i = 0; i < radius.Length / 2 + 2; i++)
            {
                radius[i] = i * (0.5 / (length / 2 + 1));
            }
            double freq = 1.0 / wavelength;
            for (int i = 1; i < radius.Length / 2 + 2; i++)
            {
                radius[i] = Math.Exp((-1 * Math.Pow(Math.Log(radius[i] / freq), 2)) /
                    (2 * Math.Pow(Math.Log(sigma), 2)));
            }
            radius[0] = 1;
            return radius;
        }

        private Complex[][][] LogGabor(Image<Gray, byte> img)
        {
            int rotations = 1;
            int mult = 8;
            int wavelength = 18;
            double sigma = .5;
            Double[] filter = new Double[img.Height];
            Complex[][][] results = new Complex[rotations][][];
            for (int i = 0; i < rotations; i++)
            {
                Complex[][] response = new Complex[img.Height][];
                filter = GetFilter(sigma, img.Height, wavelength); //Should be 240
                for (int j = 0; j < img.Width; j++)  //should be 20
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
            for (int i = 0; i < filter.Length; i++)
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
            while (i < template.Length)
            {
                byte b = 0;
                for (int j = 0; j < 8; j++)
                {
                    if (i < template.Length)
                    {
                        b = (byte)(b | (template[i] << j));
                        i++;
                    }
                    else
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
            foreach (byte b in bytes)
            {
                for (int i = 0; i < 8; i++)
                {
                    template[j] = (b >> i) & 1;
                    j++;
                }
            }
            return template;
        }

        private Image<Gray, byte> RemoveNull(Image<Gray, byte> image)
        {
            image = image.Clone();
            int boundRight = 0;
            int boundLeft = image.Width;
            byte[,,] data = image.Data; //get data reference only once for optimization purposes
            //We only iterate through first row because it starts at top of image
            //This prevents  one of the four corners showing up as not-noise
            for (int i = image.Width - 1; i > 0; i--)
            {
                if (data[0, i, 0] > 10)
                {
                    boundRight = i > boundRight ? i : boundRight;
                    break;
                }
            }
            for (int i = 0; i < boundRight; i++)
            {
                if (data[0, i, 0] > 10)
                {
                    boundLeft = i < boundLeft ? i : boundLeft;
                    break;
                }
            }
            try
            {
                image.ROI = new Rectangle(boundLeft, 0, boundRight - boundLeft, image.Height);
            }
            catch (Exception) { }
            return image;
        }

        private Image<Gray, byte>[] GetEyeRegions(object img, int max)
        {
            Image<Gray, byte> image = new Image<Gray, byte>((Bitmap)img);
            CascadeClassifier classifier = new CascadeClassifier(@"Support\haarcascade_eye.xml");
            Image<Gray, byte>[] images = new Image<Gray, byte>[max];
            int i = 0;
            foreach (Rectangle c in classifier.DetectMultiScale(image))
            {
                Image<Gray, byte> clone = image.Clone();
                clone.ROI = c;
                images[i] = clone;
                i++;
                if (i >= max) break;
            }
            return images;
        }
    }
}