using NeuralNet.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet.Training.TestingModels.Shared
{
    public class SampleData
    {
        public SampleData()
        {
            DataPoints = new List<double[]>();
            Predictions = new List<int>();
        }

        public IList<double[]> DataPoints { get; set; }
        public IList<int> Predictions { get; set; } 

        public bool IsValid
        {
            get
            {
                return this.DataPoints.Count == this.Predictions.Count;
            }
        }
    }

    public static class Data
    {
        private static Random random = new Random(0);
        public static SampleData CreateSpiralData(int points, int classes)
        {
            SampleData sampleData = new SampleData();
            List<double[]> dataPoints = new List<double[]>();
            const float dtheta = (float)(5 * Math.PI / 180);    // Five degrees.

            int currentClass = 0;
            int breakPoint = points / classes;

            double theta = 0;
            while (sampleData.DataPoints.Count < points)
            {
                double X;
                double Y;

                // Calculate r.
                double r = classes * theta;

                // Convert to Cartesian coordinates.
                PolarToCartesian(r, theta, out X, out Y);

                // Center.
                X += 0.5;
                Y += 0.5;

                sampleData.DataPoints.Add(new double[] { X, Y });

                theta += dtheta;
                if (sampleData.DataPoints.Count % breakPoint == 0)
                {
                    currentClass++;
                    if (currentClass == classes)
                    {
                        currentClass--;
                    }
                }
                sampleData.Predictions.Add(currentClass);
            }
            return sampleData;
        }

        // Convert polar coordinates into Cartesian coordinates.
        private static void PolarToCartesian(double r, double theta, out double x, out double y)
        {
            x = (double)(r * Math.Cos(theta))+(random.NextDouble()*2)-1;
            y = (double)(r * Math.Sin(theta))+(random.NextDouble()*2)-1;
        }
    }
}
