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
    public class SoftmaxActivationFunctionModel : ITestingModel
    {
        public void RunTestingModel()
        {
            
            var X = Data.CreateSpiralData(100, 3);

            HiddenLayer dense1 = new HiddenLayer(X.DataPoints, 3);
            var ReLUactivationFunction = new ActivationReLU(dense1.LayerOutput());

            HiddenLayer desnse2 = new HiddenLayer(ReLUactivationFunction.Output(), 3);
            var activationFunction = new ActivationSoftmax(desnse2.LayerOutput());
            
            Console.WriteLine(activationFunction.ToString());
        }
    }
}
