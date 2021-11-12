using NeuralNet.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet.Core.Entities.Layers
{
    public abstract class Layer
    {
        private readonly IList<Batch> _batches;
        public IList<double[]> Weights;
        public double[] Biases;

        public Layer(int singleSampleSize, int neuronCount)
        {
            _batches = new List<Batch>();
            Weights = Maths.GenerateMatrix(neuronCount, singleSampleSize, true);
            Biases = Maths.GenerateMatrix(1, neuronCount, false).First();
            
        }

        public Layer(IList<double[]> weights, double[] biases, bool randomAdjustment)
        {
            _batches = new List<Batch>();
            Weights = weights;
            Biases = biases;

            if (randomAdjustment)
            {
                for (int i =0; i< Weights.Count; i++)
                {
                    for (int j = 0; j< Weights[i].Length; j++)
                    {
                        Weights[i][j] += Maths.GetRandomNonZero(-3, 3);
                    }
                    Biases[i] += Maths.GetRandomNonZero(-3, 3);
                }
            }
        }

        public void Forward(double[] inputs)
        {
            if (Weights.Count == Biases.Length)
            {
                Batch batch = new Batch(Weights.Count);
                for (int i = 0; i < Weights.Count; i++)
                {
                    var weight = Weights.ElementAt(i);
                    var bias = Biases[i];

                    if (inputs.Length == weight.Length)
                    {
                        batch.AddNeuron(new Neuron(bias, inputs, weight));
                    }
                    else
                    {
                        throw new ArgumentException("Number of Weights and Inputs should be equal");
                    }
                }
                _batches.Add(batch);
            }
            else
            {
                throw new ArgumentException("Number of Weights and Biases should be equal");
            }
        }

        public Tuple<IList<double[]>, double[]> GetSettings()
        {
            return new Tuple<IList<double[]>, double[]>(Weights, Biases);
        }

        public IList<double[]> LayerOutput()
        {
            return _batches.Select(n => n.NeuronOutput()).ToArray();
        }

        public override string ToString()
        {
            return this.LayerOutput().MatrixToString();
        }
    }
}
