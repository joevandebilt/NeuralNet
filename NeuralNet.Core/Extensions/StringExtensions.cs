using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet.Core.Extensions
{
    public static class StringExtensions
    {
        public static string ArrayToString(this double[] Array)
        {
            return $"[ {string.Join(", ", Array)} ]";
        }

        public static string MatrixToString(this IList<double[]> Matrix)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[");
            foreach (var outputRow in Matrix)
            {
                sb.AppendLine($"\t[ {string.Join(", ", outputRow)} ]");
            }
            sb.AppendLine("]");

            return sb.ToString();
        }
    }
}
