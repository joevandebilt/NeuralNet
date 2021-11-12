using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet.Core.Entities
{
    public class Batch
    {
        private readonly IList<Neuron> _neurons; 

        public Batch(int inputSize)
        {
            _neurons = new List<Neuron>();
        }

        public void AddNeuron(Neuron neuron)
        {
            _neurons.Add(neuron);
        }

        public double[] NeuronOutput()
        {
            return _neurons.Select(n => n.Output()).ToArray();
        }
    }
}
