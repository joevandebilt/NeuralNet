using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet.Core.Extensions
{
    public static class Maths
    {
        private static readonly Random rnd = new Random((int)DateTime.Now.Ticks);

        public static IEnumerable<T[]> MatricesDot<T>(IList<T[]> matrix1, IList<T[]> matrix2)
        {
            List<T[]> output = null;
            if (matrix1.Count() == matrix2.Count())
            {
                output = new List<T[]>();
                for (int row = 0; row < matrix1.Count(); row++)
                {
                    if (matrix1[row].Length == matrix2[row].Length)
                    {
                        T[] rowValues = new T[matrix1[row].Length];
                        for(int col = 0; col < matrix1[row].Length; col++)
                        {
                            rowValues[col] = (dynamic)matrix1[row][col] * matrix2[row][col];
                        }
                        output.Add(rowValues);
                    }
                    else
                    {
                        output.Add(null);
                    }
                }
            }
            return output;
        }

        public static T[] MatrixDot<T>(IEnumerable<T[]> matrix, T[] input)
        {
            int i = 0;
            T[] output = new T[matrix.Count()];
            foreach (var vector in matrix)
            {
                if (vector.Length == input.Length)
                {
                    output[i++] = Maths.Dot<T>(vector, input);

                }
            }
            return output;
        }

        public static T Dot<T>(T[] input1, T[] input2)
        {
            var output = default(T);
            try
            {
                if (input1.Length == input2.Length)
                {
                    for (int i = 0; i < input1.Length; i++)
                    {
                        output += (dynamic)input1[i] * input2[i];
                    }
                }
            }
            catch { }
            return output;
        }

        public static IList<double[]> GenerateMatrix(int Rows, int Cols, bool allowZero, int Min, int Max)
        {
            IList<double[]> matrix = new List<double[]>();
            for (int i = 0; i < Rows; i++)
            {
                double[] vector = new double[Cols];
                for (int j = 0; j < Cols; j++)
                {
                    vector[j] = (allowZero ? GetRandom(Min, Max) : GetRandomNonZero(Min, Max));
                }
                matrix.Add(vector);
            }
            return matrix;
        }

        public static IList<double[]> GenerateMatrix(int Rows, int Cols, bool allowZero)
        {
            return GenerateMatrix(Rows, Cols, allowZero, -1, 1);
        }

        public static IList<double[]> GenerateMatrix(int Rows, int Cols)
        {
            return GenerateMatrix(Rows, Cols, true, -1, 1);
        }

        public static double GetRandom(int Min, int Max)
        {
            return (rnd.NextDouble() * (Max + 1)) - Math.Abs(Min);
        }

        public static double GetRandomNonZero(int Min, int Max)
        {
            double output = GetRandom(Min, Max);
            while (output == 0)
            {
                output = GetRandom(Min, Max);
            }
            return output;
        }

        public static double[] Linspace(double start, double stop, int num = 50)
        {
            double[] sample = new double[num];
            double difference = (stop - start) / num;            

            double sampleValue = start;
            for (int i = 0; i<num;i++)
            {
                sample[i] = sampleValue;
                sampleValue = sampleValue + difference;
            }

            return sample;
        }

    }
}
