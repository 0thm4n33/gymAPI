namespace webAPi.Models
{
    public class Abonnement
    {
        public int? Id { get; set; }
        public string? Designation { get; set; }
        public DateTime? dateDebut { get; set; }
        public DateTime? dateFin { get; set; }
        public double? Montant { get; set; }
        public virtual ICollection<Service>? Services { get; set; }

        public Abonnement(int id, string? designation, DateTime? dateDebut, DateTime? dateFin, double? montant):this()
        {
            Id = id;
            Designation = designation;
            this.dateDebut = dateDebut;
            this.dateFin = dateFin;
            Montant = montant;
        }
        public Abonnement()
        {
            Services = new List<Service>();
        }
    }
}
