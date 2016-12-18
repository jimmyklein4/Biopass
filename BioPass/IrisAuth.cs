using System;
using System.Numerics;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;
using System.Data.SQLite;
using MathNet.Numerics.IntegralTransforms;
using System.IO;

namespace BioPass
{
    class IrisAuth : authMethod
    {
        const double TEMPLATE_NULL = 99;

        public int IdentifyUser(object image)
        {
            String select = "select iris_data, user_id from iris;";
            SQLiteCommand cmd = new SQLiteCommand(select, Program.db.dbConn);
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
            int[] template = GetTemplate(image);
            if (template == null) return;
            String data = templateToBase64(GetTemplate(image));
            String oldData = "";
            String select = "select iris_data from iris where user_id = " + userId + ";";
            SQLiteDataReader reader = new SQLiteCommand(select, Program.db.dbConn).ExecuteReader();
            while (reader.Read()) //only 1 record should return lul
            {
                oldData = reader.GetString(reader.GetOrdinal("iris_data")) + ";";
            }
            data = oldData + data;
            String update = "update iris set iris_data = " + data +
                " where user_id = " + userId + ";";
            SQLiteCommand cmd = new SQLiteCommand(update, Program.db.dbConn);
            cmd.ExecuteNonQuery();
        }

        public Boolean VerifyUser(object image, int userId)
        {
            String select = "select iris_data from iris where user_id = "
                + userId + ";";
            SQLiteCommand cmd = new SQLiteCommand(select, Program.db.dbConn);
            SQLiteDataReader reader = cmd.ExecuteReader();
            int[] template2 = GetTemplate(image);
            while (reader.Read())
            {
                int[] template1 = Base64ToTemplate(reader.GetString(reader.GetOrdinal("iris_data")));
                return (HammingDistance(template2, template1, null, null) < .45);
            }
            return false;
        }

        public void AddUser(object image, long userId) {
            int[] template2 = GetTemplate(image);
            if (template2 == null) return;
            String data = templateToBase64(template2);
            long iid = Program.db.addIrisData(data, userId);
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
            Image<Gray, byte> baseImage = image.Copy();
            baseImage = RemoveThresholdNoise(baseImage);
            image = image.Copy();

            image = RemoveThresholdNoise(image);
            image = image.ThresholdBinary(SigmaThreshold(image, -1), new Gray(255));

            CvInvoke.Imshow("Detected Eye", baseImage);
            CvInvoke.WaitKey(0);
            CvInvoke.DestroyAllWindows();
            Image<Gray, byte> showImage = image.Copy();
            //Set ROI of iris region
            CircleF circles = FindCircles(image);
            Image<Gray, byte> mask = new Image<Gray, byte>(baseImage.Size);
            CvInvoke.Circle(mask, new Point((int)circles.Center.X, (int)circles.Center.Y), (int)circles.Radius,
                new MCvScalar(255, 255, 255), -1, Emgu.CV.CvEnum.LineType.AntiAlias, 0);
            baseImage = baseImage.And(baseImage, mask.Not());
            CvInvoke.Imshow("Detected Pupil", baseImage);
            CvInvoke.WaitKey(0);
            CvInvoke.DestroyAllWindows();
            if (circles.Area == 0)
                return null;

            circles = GetIrisFromPupilImage(baseImage, circles);
            if (circles.Radius == 0) return null;
            Rectangle roi = new Rectangle((int)(circles.Center.X - circles.Radius), (int)(circles.Center.Y - circles.Radius),
                (int)circles.Radius * 2, (int)circles.Radius* 2);
            baseImage.ROI = roi;
            //Show segmentation
            CvInvoke.Circle(baseImage, new Point(baseImage.Width / 2, baseImage.Height / 2),
                (int)circles.Radius, new Rgb(255, 0, 0).MCvScalar);
            CvInvoke.Imshow("Detected Iris", baseImage);
            CvInvoke.WaitKey(0);
            CvInvoke.DestroyAllWindows();
            //Normalize and encode features
            Image<Gray, byte> unraveled = Unravel(baseImage, circles);
            Complex[][][] convolved = LogGabor(unraveled);
            return PhaseQuantifier(convolved[0]); //there's only 1 anyway
        }

        private Image<Gray, byte> RemoveThresholdNoise(Image<Gray, byte> image)
        {//original param: 235, testing param: 210
            Image<Gray, byte> lightToBlack = image.ThresholdBinary(new Gray(210), new Gray(255)).Not();
            image = image.And(lightToBlack, image);
            Image<Gray, byte> blackToBlack = image.ThresholdBinary(new Gray(15), new Gray(255));
            image = image.And(blackToBlack, image);
            return image;
        }

        private CircleF GetIrisFromPupilImage(Image<Gray, byte> image, CircleF pupil)
        {
            Image<Gray, byte> startImage = image.Copy();
            image = image.Copy();
            image._EqualizeHist();
            int beg_right = (int)(pupil.Center.X + (pupil.Radius * 1.5));
            int beg_left = (int)(pupil.Center.X - (pupil.Radius * 1.5));
            CvInvoke.Circle(image, Point.Round(pupil.Center), (int)pupil.Radius, new Rgb(178, 0, 0).MCvScalar);
            int end = image.Width;
            int height = (int)pupil.Center.Y;
            int hits = 0;
            int start = 0;
            int currGray = 200;
            for (; currGray >= 100; currGray -= 5)
            {
                image = image.ThresholdBinary(new Gray(currGray), new Gray(255));
                byte[,,] data = image.Data;
                byte[,,] data2 = startImage.Data;
                for (int i = beg_right; i < end; i++)
                {
                    if ((data[height, i, 0] >= 245 || data2[height, i, 0] == 0) && hits == 0) { hits++; start = i; }
                    else if (data[height, i, 0] >= 245 || data2[height, i, 0] == 0) hits++;
                    else { hits = 0; start = 0; }
                    if (hits > 20) return new CircleF(pupil.Center, start - pupil.Center.X);
                }
                hits = 0;
                start = 0;
                for (int i = beg_left; i > 0; i--)
                {
                    if ((data[height, i, 0] >= 245 || data2[height, i, 0] == 0) && hits == 0) { hits++; start = i; }
                    else if (data[height, i, 0] >= 245 || data2[height, i, 0] == 0) hits++;
                    else { hits = 0; start = 0; }
                    if (hits > 20) return new CircleF(pupil.Center, (start - pupil.Center.X) * -1);
                }
                image = startImage;
            }
            return new CircleF();
        }

        private CircleF FindCircles(Image<Gray, byte> image)
        {
            Gray canny = new Gray(200);
            int minAccumulator = 0;
            int currAcumulator = 200;
            double dp = 1;
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
            PointF center = new PointF(image.Width / 2, image.Height / 2);
            Image<Gray, byte> mask = new Image<Gray, byte>(image.Width, image.Height);
            Point center2 = new Point(image.Width / 2, image.Height / 2);
            CvInvoke.Circle(mask, center2, (int)circle.Radius, new MCvScalar(255, 255, 255), -1, Emgu.CV.CvEnum.LineType.AntiAlias, 0);
            image = image.And(image, mask);
            double M = circle.Radius / Math.Log(circle.Radius);
            image = image.LogPolar(center, M);
            image = RemoveNull(image);
            image = image.Resize(20, 240, Emgu.CV.CvEnum.Inter.Linear);
            CvInvoke.Imshow("Normalized Iris Strip", image);
            CvInvoke.WaitKey(0);
            CvInvoke.DestroyAllWindows();
            return image;
        }

        private Double HammingDistance(int[] template1, int[] template2, int[] mask1, int[] mask2)
        {
            double diffs = 0;
            double comps = 0;
            if (template1 == null || template2 == null) return TEMPLATE_NULL;
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
            double[] radius = new double[length];
            if (length % 2 == 0) length = length - 1;
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
            radius[0] = 0;
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
                Complex[][] response = new Complex[img.Width][];
                filter = GetFilter(sigma, img.Height, wavelength); //Should be 240
                for (int j = 0; j < img.Width; j++)  //should be 20
                {
                    //idk if there's a method for this
                    Complex[] signal = new Complex[img.Height];
                    for (int k = 0; k < img.Height; k++)
                    {
                        signal[k] = img[k, j].Intensity;
                    }
                    System.Console.Out.WriteLine("");
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

        private Gray SigmaThreshold(Image<Gray, byte> image, double sigma)
        {
            MCvScalar std;
            Gray avg;
            image.AvgSdv(out avg, out std);
            return new Gray(avg.Intensity+std.V0*sigma);
        }
    }
}