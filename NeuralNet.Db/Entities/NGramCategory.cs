using System;
using System.Collections.Generic;

namespace NeuralNet.Db.Entities
{
    public partial class NGramCategory
    {
        public NGramCategory()
        {
            NGrams = new HashSet<NGram>();
        }

        public int Id { get; set; }
        public string Description { get; set; } = null!;

        public virtual ICollection<NGram> NGrams { get; set; }
    }
}
