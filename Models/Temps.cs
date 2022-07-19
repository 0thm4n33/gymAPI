using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webAPi.Models
{
    public class Temps
    {
        [Key]
        public int? Id { get; set; }
        [Column(TypeName = "bigint")]
        public TimeSpan HeureDebut { get; set; }
        [Column(TypeName = "bigint")]
        public TimeSpan HeureFin { get; set; }
        public int? CoursId { get; set; }
        public string? Jour { get; set; }
        public Cours? Cours { get;set; }

        public Temps(int id, TimeSpan heureDebut, TimeSpan heureFin, string? jour, Cours? cours)
        {
            Id = id;
            HeureDebut = heureDebut;
            HeureFin = heureFin;
            Jour = jour;
            Cours = cours;
        }

        public Temps()
        {
        }

    }
}
