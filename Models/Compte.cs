using webAPi.Models.GestClub.Models;

namespace webAPi.Models
{
    public class Compte
    {
        public int? Id { get; set; }
        public string? Login { get; set; }
        public string? Pwd { get; set; }
        public virtual Role Role { get; set; }

        public Compte(int id, string? login, string? pwd, Role role)
        {
            Id = id;
            Login = login;
            Pwd = pwd;
            Role = role;
        }

        public Compte()
        {
        }
    }
}
