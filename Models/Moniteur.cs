namespace webAPi.Models
{
    public class Moniteur
    {
        public int? Id { get; set; }
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public virtual List<Specialite>? Specialites { get; set; }
        public virtual Compte? Compte { get; set; }

        public Moniteur(int id, string? nom, string? prenom, Compte? compte):this()
        {
            Id = id;
            Nom = nom;
            Prenom = prenom;
            Compte = compte;
        }
        public Moniteur()
        {
            Specialites = new List<Specialite>();
        }
    }
}
