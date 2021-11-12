using NeuralNet.Core.Entities.ActivationFunctions;
using NeuralNet.Core.Entities.Layers;
using NeuralNet.Core.Entities.Loss;
using NeuralNet.Training.TestingModels.Interface;
using NeuralNet.Training.TestingModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet.Training.TestingModels
{
    public class EndToEndTestModel : ITestingModel
    {
        public void RunTestingModel()
        {
            var sampleData = Data.CreateSpiralData(100, 3);
            if (sampleData.IsValid)
            {
                double bestLoss = 999999;
                for (int i = 0; i < 1000; i++)
                {
                    var dense1 = new HiddenLayer(sampleData.DataPoints, 3);
                    var activation1 = new ActivationReLU(dense1.LayerOutput());
                    var dense2 = new HiddenLayer(activation1.Output(), 3);
                    var activation2 = new ActivationSoftmax(dense2.LayerOutput());

                    var lossFunction = new CategoricalCrossEntropyLoss();
                    var loss = lossFunction.Calculate(sampleData.Predictions, activation2.Output());

                    if (loss < bestLoss)
                    {
                        bestLoss = loss;
                        Console.WriteLine("Found a new best loss at iteration {1}/1000 {0}", loss, i);
                    }                    
                }
            }
        }
            
    }
}
