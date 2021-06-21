using System;
using System.Collections.Generic;

#nullable disable

namespace AxoTourax.Models
{
    public partial class Calcul
    {
        public int IdCalcul { get; set; }
        public int IdBobine { get; set; }
        public double DiametreCable { get; set; }
        public double? PoidsCable { get; set; }
        public double? CoefficiantCorrection { get; set; }
        public double Longueur { get; set; }
        public string TypeCable { get; set; }
        public DateTime DateCalcul { get; set; }

        public virtual Bobine IdBobineNavigation { get; set; }
    }
}
