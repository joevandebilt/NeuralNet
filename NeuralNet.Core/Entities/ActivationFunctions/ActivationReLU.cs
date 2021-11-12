using NeuralNet.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet.Core.Entities.ActivationFunctions
{
    public class ActivationReLU : BaseActivationFunction
    {
        public ActivationReLU() : base() { }

        public ActivationReLU(double[] inputs) : base(inputs)
        {
        }

        public ActivationReLU(IList<double[]> inputs) : base(inputs)
        {
        }

        public override void Forward(double[] inputs)
        {
            double[] output = new double[inputs.Length];
            for (int i = 0; i < inputs.Length; i++)
            {
                if (inputs[i] > 0)
                {
                    output[i] = inputs[i];
                }
                else
                {
                    output[i] = 0;
                }
            }
            outputs.Add(output);
        }        
    }
}
