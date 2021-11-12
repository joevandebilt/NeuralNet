using NeuralNet.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet.Core.Entities.Loss.Base
{
    public abstract class Loss
    {
        public double Calculate(IList<int[]> targetClasses, IList<double[]> inputs)
        {
            var matchedType = targetClasses.Select(t => t.Select(v => (double)v).ToArray()).ToList();
            return this.Calculate(matchedType, inputs);
        }

        public double Calculate(IList<double[]> targetClasses, IList<double[]> inputs)
        {
            try
            {
                var outputMatrix = Maths.MatricesDot(targetClasses, inputs);
                double[] predictions = outputMatrix.Select(om => om.Single(row => row > 0)).ToArray();
                var loss = predictions.Average(om => this.Calculate(om));
                return loss;
            }
            catch(Exception ex)
            {
                Console.WriteLine(targetClasses.MatrixToString());
                Console.WriteLine(inputs.MatrixToString());
            }
            return 9999;
        }

        public double Calculate(IEnumerable<int> targetClasses, IList<double[]> inputs)
        {
            return this.Calculate(targetClasses.ToArray(), inputs);
        }

        public double Calculate(int[] targetClasses, IList<double[]> inputs)
        {
            double[] outputs = null;
            if (inputs.Count == targetClasses.Length)
            {
                outputs = new double[targetClasses.Length];
                for (int i = 0; i < targetClasses.Length; i++)
                {
                    outputs[i] = this.Calculate(targetClasses[i], inputs[i]);
                }
            }
            else
            {
                throw new Exception("Failed to calculate loss");
            }
            return outputs.Average();
        }

        public double Calculate(int targetClass, double[] inputs)
        {
            return this.Calculate(inputs[targetClass]);
        }

        public abstract double Calculate(double input);
    }
}
