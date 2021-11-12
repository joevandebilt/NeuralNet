using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet.Core.Entities.Layers
{
    public class HiddenLayer : Layer
    {
        public HiddenLayer(IList<double[]> inputs, IList<double[]> weights, double[] biases, bool randomAdjustment) : base(weights, biases, randomAdjustment)
        {
            foreach (var input in inputs)
            {
                this.Forward(input);
            }
        }

        public HiddenLayer(IList<double[]> inputs, int neuronCount) : base(inputs.First().Length, neuronCount)
        {
            foreach(var input in inputs)
            {
                this.Forward(input);
            }
        }

        public HiddenLayer(int singleSampleSize, int neuronCount) : base(singleSampleSize, neuronCount)
        {

        }
    }
}
