using NeuralNet.Core.Entities;
using NeuralNet.Core.Entities.Layers;
using NeuralNet.Training.TestingModels;
using NeuralNet.Training.TestingModels.Interface;
using System;
using System.Collections.Generic;

namespace NeuralNet.Training
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            ITestingModel model = new TextModel();
            model.RunTestingModel();


        }
    }
}
