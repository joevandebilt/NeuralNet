using NeuralNet.Core.Entities.ActivationFunctions;
using NeuralNet.Core.Entities.Layers;
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
    public class ReLUActivationFuntionModel : ITestingModel
    {
        public void RunTestingModel()
        {
            var X = Data.CreateSpiralData(100, 3);

            Console.WriteLine(X.DataPoints.MatrixToString());

            HiddenLayer hiddenLayer = new HiddenLayer(X.DataPoints, 5);
            hiddenLayer.LayerOutput();

            var activationFunction = new ActivationReLU(hiddenLayer.LayerOutput());

            Console.WriteLine(activationFunction.ToString());
        }
    }
}
