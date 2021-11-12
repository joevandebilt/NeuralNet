using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet.Core.Entities.Layers
{
    public class OutputLayer : HiddenLayer
    {
        public OutputLayer(IList<double[]> inputs, int neuronCount) : base(inputs, neuronCount) { }
    }
}
