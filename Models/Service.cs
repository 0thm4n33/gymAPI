namespace webAPi.Models
{
    public class Service
    {
        public int? Id { get; set; }
        public string? Nom { get; set; }
        public Abonnement ? Abonnement { get; set; }
        public int ? AbonnementId { get; set; }
        public virtual ICollection<Cours>? Cours { get; set; }
        public Service(int id, string? nom):this()
        {
            Id = id;
            Nom = nom;
        }
        public Service()
        {
        }
    }
}
