using System;
using System.Collections.Generic;

#nullable disable

namespace AxoTourax.Models
{
    public partial class MatiereBobine
    {
        public MatiereBobine()
        {
            Bobines = new HashSet<Bobine>();
        }

        public int IdMatiere { get; set; }
        public string Libelle { get; set; }

        public virtual ICollection<Bobine> Bobines { get; set; }
    }
}
