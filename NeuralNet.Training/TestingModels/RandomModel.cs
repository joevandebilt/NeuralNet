using NeuralNet.Core.Entities.Layers;
using NeuralNet.Core.Extensions;
using NeuralNet.Training.TestingModels.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet.Training.TestingModels
{
    public class RandomModel : ITestingModel
    {
        public void RunTestingModel()
        {
            //Assemble our inputs
            Random random = new Random((int)DateTime.Now.Ticks);

            var sampleSize = random.Next(1, 10);
            var batchSize = random.Next(1, 10);

            Console.WriteLine("Creating a random input payload pf {0} samples of {1} inputs", batchSize, sampleSize);

            var inputs = new List<double[]>();
            for (int i = 0; i< batchSize;i++)
            {
                double[] sample = new double[sampleSize];
                for (int j = 0; j<sampleSize;j++)
                {
                    sample[j] = Maths.GetRandom(-10, 10);
                }
                inputs.Add(sample);
            }
            
            //Create a real Layer
            InputLayer inputLayer = new InputLayer(inputs, 3, 3, 5, 2);
        }
    }
}
