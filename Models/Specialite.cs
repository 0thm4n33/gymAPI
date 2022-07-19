namespace webAPi.Models
{
    public class Specialite    
    {
        public int? Id { get; set; }
        public string? Name { get; set; }

        public Specialite(int id, string? name)
        {
            Id = id;
            Name = name;
        }
        public Specialite()
        {
        }
    }
}
