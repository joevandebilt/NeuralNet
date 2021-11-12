using NeuralNet.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet.Core.Entities.ActivationFunctions
{
    public abstract class BaseActivationFunction
    {
        protected IList<double[]> outputs = new List<double[]>();

        public BaseActivationFunction() { }

        public BaseActivationFunction(double[] inputs)
        {
            this.Forward(inputs);
        }

        public BaseActivationFunction(IList<double[]> inputs)
        {
            foreach (var input in inputs)
            {
                this.Forward(input);
            }
        }
        public void Forward(IList<double[]> inputs)
        {
            foreach (var input in inputs)
            {
                this.Forward(input);
            }
        }

        public abstract void Forward(double[] input);

        public void Clear()
        {
            outputs = new List<double[]>();
        }

        public IList<double[]> Output()
        {
            return outputs;
        }

        public override string ToString()
        {
            return outputs.MatrixToString();
        }
    }
}
