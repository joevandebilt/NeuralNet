using NeuralNet.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet.Core.Entities
{
    public class Neuron
    {
        public double Bias { get; set; }
        public double[] Inputs { get; set; }
        public double[] Weights { get; set; }
                
        public Neuron(double bias, double[] inputs, double[] weights)
        {
            Bias = bias;
            Inputs = inputs;
            Weights = weights;
        }

        public double Output()
        {
            return Maths.Dot(Inputs, Weights) + Bias;
        }
    }
}
