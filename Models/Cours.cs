using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace webAPi.Models
{
    public class Cours
    {
        public int? Id { get; set; }
        public string? Nom { get; set; }
        public int ? ServiceId { get; set; }
        public int? Duree { get; set; }
        public string? Niveau { get; set; }
        public Service? Service { get; set; }
        public virtual ICollection<Materiel>? Materiels { get; set; }
        public virtual ICollection<Temps>? EmploiTemps { get; set; }


        public Cours(int id, string? nom, int? duree, string? niveau,Service? service):this()
        {
            Id = id;
            Nom = nom;
            Duree = duree;
            Niveau = niveau;
            Service = service;
        }
        public Cours()
        {
        }
    }
}
