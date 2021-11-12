using NeuralNet.Training.TestingModels.Interface;
using NeuralNet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralNet.Core;
using NeuralNet.Core.Entities.Loss;
using NeuralNet.Core.Entities.ActivationFunctions;
using NeuralNet.Core.Extensions;

namespace NeuralNet.Training.TestingModels
{
    public class TextModel : ITestingModel
    {
        Random rnd = new Random();
        Dictionary<string, int> WordMapping = new Dictionary<string, int>();
        int _batchSize = 3;

        public void RunTestingModel()
        {
            var neuralNet = new NeuralNetRunner();

            neuralNet.InputLayerNeurons = _batchSize;

            neuralNet.AddHiddenLayer(5, new ActivationSoftmax());

            neuralNet.OutputLayerNeurons = _batchSize;

            neuralNet.AddFinalActivation(new ActivationSigmoid());
            neuralNet.SetLossCalculation(new CategoricalCrossEntropyLoss());

            var dataModel = GetTextModel();

            neuralNet.Epochs = 1000;

            neuralNet.Input = dataModel.Item1;
            neuralNet.Predictions = dataModel.Item2;

            neuralNet.RunModel();

            var SampledOutput = neuralNet.Output;

            Console.WriteLine("Neural Net Ran {1} times and final loss was {0}", neuralNet.LossValue, neuralNet.Epochs);
            Console.WriteLine(neuralNet.Output.MatrixToString());

            Console.WriteLine("{0}{0}I made my AI try to write the script for a scene in Star Trek, this is what it gave me:{0}{0}\t", Environment.NewLine);

            double maxValue = (double)WordMapping.Count;
            foreach(var row in SampledOutput)
            {
                foreach(var col in row)
                {
                    int index = (int)Math.Round(maxValue * col);
                    //Console.WriteLine("Get word at index {0}", index);

                    Console.Write("{0} ", WordMapping.Single(word => word.Value == index).Key);
                }
            }
            Console.WriteLine("{0}{0}End of line...{0}{0}", Environment.NewLine);
        }

        private Tuple<List<double[]>, List<double[]>> GetTextModel()
        {
            string script = System.IO.File.ReadAllText(@"F:\Personal\NeuralNetTraining\Data\103_TheNakedNow.txt");
            List<string> words = script.Split(new string[] { " ", "\r\n"}, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Distinct().ToList();
            List<string> predictionWords = words.Skip(1).ToList();
            predictionWords.Add(words.First());

            int i = 1;
            WordMapping = words.OrderBy(word => word).Select(word => word.ToLower()).Distinct().ToDictionary(word => word, word => i++);
            WordMapping.Add("\r\n", 0);

            List<double[]> inputs = new List<double[]>();
            List<double[]> predictions = new List<double[]>();
            for (i=0;i< _batchSize; i++)
            {
                double[] inputSample = words.Skip(_batchSize * inputs.Count).Take(_batchSize).Select(i => ((double)(WordMapping[i.ToLower()]))).ToArray();
                for (int j = 0; j < inputSample.Length; j++) 
                {
                    double sample = inputSample[j] / WordMapping.Count;
                    inputSample[j] = sample; 
                }
                
                double[] predictionSampleModel = new double[inputSample.Length];                
                predictionSampleModel[rnd.Next(0, inputSample.Length-1)] = 1;

                inputs.Add(inputSample);
                predictions.Add(predictionSampleModel);
            }

            return new Tuple<List<double[]>, List<double[]>>(inputs, predictions);
        }

    }
}
