using NeuralNet.Core.Entities;
using NeuralNet.Core.Entities.ActivationFunctions;
using NeuralNet.Core.Entities.Layers;
using NeuralNet.Core.Entities.Loss;
using NeuralNet.Core.Extensions;
using NeuralNet.Training.TestingModels.Interface;
using NeuralNet.Training.TestingModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet.Training.TestingModels
{
    public class CalculatingLossModel : ITestingModel
    {
        public void RunTestingModel()
        {
            var b = 5.2;
            Console.WriteLine(Math.Log(b));

            var softmax_output = new List<double[]> {
                new double[] {0.7, 0.1, 0.2 },
                new double[] {0.1,0.5,0.4},
                new double[] {0.02, 0.9, 0.08},
            };

            var target_output = new List<int[]> {
                new int[] {1, 0, 0 },
                new int[] {0, 1, 0},
                new int [] {0,1, 0}
            };

            var lossCalculation = new CategoricalCrossEntropyLoss();
            var loss = lossCalculation.Calculate(target_output, softmax_output);
            
            Console.WriteLine("{0} is this loss?", loss);
        }
    }
}
