using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet.Core.Entities.ActivationFunctions
{
    public class ActivationSigmoid : BaseActivationFunction
    {
        public ActivationSigmoid() : base() { }

        public ActivationSigmoid(double[] inputs) : base(inputs)
        {
        }

        public ActivationSigmoid(IList<double[]> inputs) : base(inputs)
        {
        }

        public override void Forward(double[] input)
        {
            var output = new double[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                var negativeVal = -input[i];
                var exponent = Math.Pow(Math.E, negativeVal); 
                var outVal = 1 / (1 + exponent);
                output[i] = outVal;

            }

            outputs.Add(output);
        }
    }
}
