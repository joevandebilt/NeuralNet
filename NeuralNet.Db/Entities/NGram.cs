using System;
using System.Collections.Generic;

namespace NeuralNet.Db.Entities
{
    public partial class NGram
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Value { get; set; } = null!;
        public int Frequency { get; set; }

        public virtual NGramCategory Category { get; set; } = null!;
    }
}
