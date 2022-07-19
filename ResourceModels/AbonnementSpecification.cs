namespace webAPi.ResourceModels
{
    public class AbonnementSpecification
    {
        public int? Id { get; set; }
        public string? Designation { get; set; }
        public DateTime? dateDebut { get; set; }
        public DateTime? dateFin { get; set; }
        public double? Montant { get; set; }
    }
}
