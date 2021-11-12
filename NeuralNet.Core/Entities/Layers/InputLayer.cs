using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet.Core.Entities.Layers
{
    public class InputLayer : HiddenLayer
    {
        private IList<HiddenLayer> _layers;
        private OutputLayer _outputLayer;

        public InputLayer(IList<double[]> InputBatches, int NeuronSize, int HiddenLayers, int HiddenLayerNeuronSize, int OutputLayerNeuronSize) : base(InputBatches, NeuronSize)
        {
            IList<double[]> previousOutput = this.LayerOutput();
            Console.WriteLine("Input Layer Output\r\n{0}", this.ToString());

            _layers = new List<HiddenLayer>();
            for (int i = 0; i < HiddenLayers; i++)
            {
                var layer = new HiddenLayer(previousOutput, HiddenLayerNeuronSize);
                previousOutput = layer.LayerOutput();
                Console.WriteLine("Hidden Layer {1} Output\r\n{0}", layer.ToString(), i+1);
                _layers.Add(layer);
            }
            _outputLayer = new OutputLayer(previousOutput, OutputLayerNeuronSize);

            Console.WriteLine("Output Layer Output\r\n{0}", _outputLayer.ToString());
        }
    }
}
