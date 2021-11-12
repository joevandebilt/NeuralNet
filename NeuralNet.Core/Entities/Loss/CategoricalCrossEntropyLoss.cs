using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralNet.Core.Entities.Loss.Base;

namespace NeuralNet.Core.Entities.Loss
{
    public class CategoricalCrossEntropyLoss : Base.Loss
    {
        public override double Calculate(double input)
        {
            if (input == 0)
            {
                return -(Math.Log(double.Epsilon));
            }
            else
            {
                return -(Math.Log(input));
            }
        }
    }
}
