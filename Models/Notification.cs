
namespace webAPi.Models
{
    public class Notification
    {

        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? Contenu { get; set; }
        public DateTime CreatedDate { get; set; }

        public Notification(int id, string? title, string? contenu, DateTime createdDate)
        {
            Id = id;
            Title = title;
            Contenu = contenu;
            CreatedDate = createdDate;
        }
        public Notification()
        {
        }
    }
}
