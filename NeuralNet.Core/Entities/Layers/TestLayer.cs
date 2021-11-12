using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet.Core.Entities.Layers
{
    public class TestLayer : Layer
    {
        public TestLayer(IList<double[]> weights, double[] biases) : base(weights, biases, false)
        {
        }
    }
}
