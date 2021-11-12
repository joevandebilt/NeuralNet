using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet.Core.Entities.ActivationFunctions
{
    public class ActivationSoftmax : BaseActivationFunction
    {
        public ActivationSoftmax() : base() { }

        public ActivationSoftmax(double[] inputs) : base(inputs)
        {
        }

        public ActivationSoftmax(IList<double[]> inputs) : base(inputs)
        {
        }

        public override void Forward(double[] inputs)
        {
            var output = new double[inputs.Length];
            var maxVal = inputs.Max();
            var normalisedValues = inputs.Select(i => i - maxVal).ToArray();
            var normalisedBase = normalisedValues.Sum(n => Math.Exp(n));

            for (int i = 0; i < normalisedValues.Length; i++)
            {
                output[i] = Math.Exp(normalisedValues[i]) / normalisedBase;
            }

            outputs.Add(output);
        }
    }
}
