using webAPi.Models;

namespace webAPi.ResourceModels
{
    public class AdherentSpecification
    {
        public int Id { get; set; }
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public DateTime? DateNaissance { get; set; }
        public virtual Payement Payement { get; set; }
        public int Compte { get; set; }
        public int Abonnement { get; set; }

    }
}
