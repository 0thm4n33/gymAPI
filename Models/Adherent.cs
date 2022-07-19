

namespace webAPi.Models
{
    public class Adherent
    {

        public int? Id { get; set; }
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public DateTime? DateNaissance { get; set; }
        public virtual Payement Payement { get; set; }
        public virtual Compte Compte { get; set; }
        public virtual List<Notification>? Notifications { get; set; }
        public virtual Abonnement? Abonnement { get; set; }

        public Adherent(int id, string? name, string? prenom, Payement payement, Compte compte):this()
        {
            Id = id;
            Nom = name;
            Prenom = prenom;
            Payement = payement;
            Compte = compte;
            
        }

        public Adherent()
        {
            Notifications = new List<Notification>();
        }
    }
}
