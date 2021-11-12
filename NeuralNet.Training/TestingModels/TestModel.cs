using NeuralNet.Core.Entities.Layers;
using NeuralNet.Training.TestingModels.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet.Training.TestingModels
{
    public class TestModel : ITestingModel
    {
        public void RunTestingModel()
        {
            //Assemble our inputs
            var inputs = new List<double[]> {
                new double[]{ 1, 2, 3, 2.5 },
                new double[]{2.0, 5.0, -1.0, 2.0},
                new double[]{-1.5, 2.7, 3.3, -0.8}
            };

            List<double[]> weights = new List<double[]>
            {
                new double[] { 0.2, 0.8, -0.5, 1.0 },
                new double[] { 0.5, -0.91, 0.26, -0.5 },
                new double[] { -0.26, -0.27, 0.17, 0.87 }
            };

            double[] biases = new double[] { 2, 3, 0.5 };

            //Create our test layer
            TestLayer testLayer = new TestLayer(weights, biases);
            foreach (var input in inputs)
            {
                testLayer.Forward(input);
            }
            Console.WriteLine($"The output is\r\n {testLayer.ToString()}");

            //Create another testLayer
            List<double[]> weights2 = new List<double[]>
            {
                new double[] { 0.1, -0.14, 0.5 },
                new double[] { -0.5, 0.12, -0.33 },
                new double[] { -0.44, 0.73, -0.13 }
            };
            double[] biases2 = new double[] { -1, 2, -0.5 };

            TestLayer testLayer2 = new TestLayer(weights2, biases2);
            foreach (var input in testLayer.LayerOutput())
            {
                testLayer2.Forward(input);
            }
            Console.WriteLine($"The output is\r\n {testLayer2.ToString()}");
        }
    }
}
