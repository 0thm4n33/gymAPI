
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using webAPi.Models;
namespace webAPi.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options){}
        public DbSet<Compte> comptes { get; set; } = null!;
        public DbSet<Materiel> materiels { get; set; } = null!;
        public DbSet<Moniteur>? Moniteur { get; set; }
        public DbSet<Specialite>? Specialite { get; set; }
        public DbSet<Temps>? Temps { get; set; }
        public DbSet<Notification>? Notification { get; set; }
        public DbSet<Adherent>? Adherent { get; set; }
        public DbSet<Abonnement>? Abonnement { get; set; }
        public DbSet<Service>? Service { get; set; }
        public DbSet<Cours>? Cours { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Cours>().
                    HasOne(c => c.Service)
                    .WithMany(s => s.Cours).HasForeignKey("ServiceId");
            builder.Entity<Service>()
                .HasOne(s => s.Abonnement)
                .WithMany(a => a.Services).HasForeignKey("AbonnementId");
            builder.Entity<Cours>().OwnsMany(c => c.EmploiTemps, e =>
            {
                e.WithOwner().HasForeignKey("CoursId");
                e.HasKey("Id");
            });
            #region Abonnement
                builder.Entity<Abonnement>().HasData(new Abonnement { Id = 1, Designation = "Classique", Montant = 250 });
                builder.Entity<Abonnement>().HasData(new Abonnement { Id = 2, Designation = "Etudiant", Montant = 100 });
            #endregion
            #region services
                int counter = 0;
                String[] services = { "Fitness", "Cycling" };
                foreach(var service in services)
                {
                    builder.Entity<Service>().HasData(new Service { Id = ++counter, AbonnementId = 1, Nom = service });
                }
            #endregion
            #region cours
                Dictionary<string, string[]> cours = new Dictionary<string, string[]>();
                cours.Add("fitness", new string[]{ "Pilate", "Body Bump", "Body Attack", "Body Combat" });
                cours.Add("cycling", new string[] { "SPRINT", "RPM" });
                string[] jours = { "Lundi", "Mardi", "Mercredi", "Jeudi", "Vendredi", "Samedi" };
                counter = 0;
                int tempsId = 0;
                for(int i = 0; i < cours.Count; i++)
                {
                    string key = cours.ElementAt(i).Key;
                    string[] cs = cours[key];
                    foreach(var cour in cs)
                    {
                        int serviceId = i + 1;
                        builder.Entity<Cours>().
                        HasData(new Cours { Id = ++counter, Nom = cour, Duree = 45, Niveau = "Debutant", ServiceId = serviceId });
                        foreach(var jour in jours)
                        {
                        builder.Entity<Cours>().OwnsMany(c => c.EmploiTemps).HasData(
                          new Temps
                          {
                              Id = ++tempsId,
                              Jour = jour,
                              HeureDebut = new TimeSpan(08, 00, 00),
                              HeureFin = new TimeSpan(09, 00, 00),
                              CoursId = counter
                          });
                        }
                    }
                }
            #endregion

        }
    }
}