using NeuralNet.Core.Entities.ActivationFunctions;
using NeuralNet.Core.Entities.Layers;
using NeuralNet.Core.Entities.Loss.Base;
using NeuralNet.Core.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet.Core
{
    public class NeuralNetRunner
    {
        private IList<HiddenLayer> _layers;
        private IList<BaseActivationFunction> _activationFunctions;

        private Loss _lossCalulation;

        public int InputLayerNeurons { get; set; }
        public int OutputLayerNeurons { get; set; }
        public int Epochs { get; set; } = 1;

        private int _hiddenLayersCount;
        private IList<int> _hiddenLayerNeurons;
        private IList<Tuple<IList<double[]>, double[]>> _bestSettings;

        public IList<double[]> Input { get; set; }
        public IList<double[]> Predictions { get; set; }
        public IList<double[]> Output { get; set; }

        public double LossValue { get; set; } = 9999;
        public double Accuracy { get; set; }

        public void SetLossCalculation(Loss lossCulation)
        {
            _lossCalulation = lossCulation;
        }

        public void AddHiddenLayer(int NeuronCount)
        {
            this.AddHiddenLayer(NeuronCount, null);
        }

        public void AddHiddenLayer(int NeuronCount, BaseActivationFunction activationFunction)
        {
            _hiddenLayersCount++;

            if (_hiddenLayerNeurons == null)
                _hiddenLayerNeurons = new List<int>();

            if (_activationFunctions == null)
                _activationFunctions = new List<BaseActivationFunction>();

            _hiddenLayerNeurons.Add(NeuronCount);
            _activationFunctions.Add(activationFunction);
        }

        public void AddFinalActivation(BaseActivationFunction activationFunction)
        {
            _activationFunctions.Add(activationFunction);
        }

        public void RunModel()
        {
            ValidateModel();            
            for (int epoch = 0; epoch < Epochs; epoch++)
            {
                _layers = new List<HiddenLayer>();

                HiddenLayer firstLayer = null;
                HiddenLayer hiddenLayer = null;
                HiddenLayer finalLayer = null;

                if (_bestSettings == null)
                {
                    firstLayer = new HiddenLayer(Input, InputLayerNeurons);
                }
                else
                {
                    var weights = _bestSettings.First().Item1;
                    var biases = _bestSettings.First().Item2;
                    firstLayer = new HiddenLayer(Input, weights, biases, true);
                }
                _layers.Add(firstLayer);

                var previousLayerOutputs = firstLayer.LayerOutput();
                for (int i = 0; i < _hiddenLayersCount; i++)
                {
                    if (_bestSettings == null)
                    {
                        hiddenLayer = new HiddenLayer(previousLayerOutputs, _hiddenLayerNeurons[i]);
                    }
                    else
                    {
                        var settings = _bestSettings.Skip(1).ElementAt(i);
                        var weights = settings.Item1;
                        var biases = settings.Item2;
                        hiddenLayer = new HiddenLayer(previousLayerOutputs, weights, biases, true);
                    }
                    _layers.Add(hiddenLayer);

                    var activationFunction = _activationFunctions[i];
                    if (activationFunction != null)
                    {
                        activationFunction.Clear();
                        activationFunction.Forward(hiddenLayer.LayerOutput());
                        previousLayerOutputs = activationFunction.Output();
                    }
                    else
                        previousLayerOutputs = hiddenLayer.LayerOutput();
                }

                if (_bestSettings == null)
                {
                    finalLayer = new HiddenLayer(previousLayerOutputs, OutputLayerNeurons);
                }
                else
                {
                    var weights = _bestSettings.Last().Item1;
                    var biases = _bestSettings.Last().Item2;
                    finalLayer = new HiddenLayer(previousLayerOutputs, weights, biases, true);
                }
                _layers.Add(finalLayer);

                var finalActivationFunction = _activationFunctions.Last();
                finalActivationFunction.Clear();
                finalActivationFunction.Forward(finalLayer.LayerOutput());

                var lossVal = _lossCalulation.Calculate(Predictions, finalActivationFunction.Output());

                if (lossVal < LossValue)
                {
                    Console.WriteLine($"Improved Loss during Epoch {epoch}/{Epochs} from {LossValue} tp {lossVal}");
                    LossValue = lossVal;
                    _bestSettings = _layers.Select(l => l.GetSettings()).ToList();

                    Output = finalActivationFunction.Output();
                }
            }            
        }

        public void SaveTrainingModel(string filePath)
        {
            throw new NotImplementedException();
        }

        public void LoadTrainingModel(string filePath)
        {
            throw new NotImplementedException();
        }

        private void ValidateModel()
        {
            if (Input == null)
                throw new ArgumentNullException("Inputs cannot be null");

            if (Predictions == null)
                throw new ArgumentNullException("Predictions cannot be null");

            if (InputLayerNeurons <= 0)
                throw new ArgumentNullException("Input Layer Neurons cannot be less than 1");

            if (OutputLayerNeurons <= 0)
                throw new ArgumentNullException("Output Layer Neurons cannot be less than 1");

            if (_hiddenLayersCount != _hiddenLayerNeurons.Count)
                throw new ArgumentNullException($"Hidden Layer Neurons must be provided for each Hidden Layer. You have asked for {_hiddenLayersCount} Hidden Layers and provided {_hiddenLayerNeurons.Count} Neuron counts");

            if (_lossCalulation == null)
                throw new ArgumentNullException($"You must provide a model to calculate Loss");

            if (_activationFunctions == null)
                throw new ArgumentNullException($"You must provide an activation function");
        }
    }
}
