using System;
using System.Collections.Generic;

#nullable disable

namespace AxoTourax.Models
{
    public partial class Bobine
    {
        public Bobine()
        {
            Calculs = new HashSet<Calcul>();
        }

        public int IdBobine { get; set; }
        public string Reference { get; set; }
        public int? IdMatiere { get; set; }
        public int? IdTechnique { get; set; }
        public double? Poids { get; set; }
        public double? PoidsMaximum { get; set; }
        public int? Stock { get; set; }
        public decimal? Prix { get; set; }
        public double? DiametreExterieur { get; set; }
        public double? DiametreInterieur { get; set; }
        public double? DiametreTrouAxeCentral { get; set; }
        public double? LargeurInterieur { get; set; }
        public double? LargeurExterieur { get; set; }
        public bool? Consigne { get; set; }
        public string Photo { get; set; }

        public virtual MatiereBobine IdMatiereNavigation { get; set; }
        public virtual TechniqueBobine IdTechniqueNavigation { get; set; }
        public virtual ICollection<Calcul> Calculs { get; set; }
    }
}
