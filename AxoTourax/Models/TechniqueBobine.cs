using System;
using System.Collections.Generic;

#nullable disable

namespace AxoTourax.Models
{
    public partial class TechniqueBobine
    {
        public TechniqueBobine()
        {
            Bobines = new HashSet<Bobine>();
        }

        public int IdTechnique { get; set; }
        public string Libelle { get; set; }

        public virtual ICollection<Bobine> Bobines { get; set; }
    }
}
