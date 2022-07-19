namespace webAPi.Models
{
    public class Materiel
    {
        public int? Id { get; set; }
        public string? Marque { get; set; }
        public string? Designation { get; set; }
        public int Qte { get; set; }
        public virtual Statut Statut { get; set; }
        public virtual Categorie Categorie { get; set; }

        public Materiel(int id, string? marque, string? designation, int qte, Statut statut, Categorie categorie)
        {
            Id = id;
            Marque = marque;
            Designation = designation;
            Qte = qte;
            Statut = statut;
            Categorie = categorie;
        }
        public Materiel()
        {
        }
    }

}
