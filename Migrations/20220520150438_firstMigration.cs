using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webAPi.Migrations
{
    public partial class firstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "comptes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pwd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comptes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Adherent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateNaissance = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Payement = table.Column<int>(type: "int", nullable: false),
                    CompteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adherent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Adherent_comptes_CompteId",
                        column: x => x.CompteId,
                        principalTable: "comptes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Moniteur",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moniteur", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Moniteur_comptes_CompteId",
                        column: x => x.CompteId,
                        principalTable: "comptes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Abonnement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dateDebut = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateFin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Montant = table.Column<double>(type: "float", nullable: true),
                    AdherentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abonnement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Abonnement_Adherent_AdherentId",
                        column: x => x.AdherentId,
                        principalTable: "Adherent",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contenu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AdherentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notification_Adherent_AdherentId",
                        column: x => x.AdherentId,
                        principalTable: "Adherent",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Specialite",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoniteurId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Specialite_Moniteur_MoniteurId",
                        column: x => x.MoniteurId,
                        principalTable: "Moniteur",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AbonnementId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Service_Abonnement_AbonnementId",
                        column: x => x.AbonnementId,
                        principalTable: "Abonnement",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cours_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "materiels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Marque = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qte = table.Column<int>(type: "int", nullable: false),
                    Statut = table.Column<int>(type: "int", nullable: false),
                    Categorie = table.Column<int>(type: "int", nullable: false),
                    CoursId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_materiels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_materiels_Cours_CoursId",
                        column: x => x.CoursId,
                        principalTable: "Cours",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Temps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HeureDebut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HeureFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Jour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoursId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Temps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Temps_Cours_CoursId",
                        column: x => x.CoursId,
                        principalTable: "Cours",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Abonnement_AdherentId",
                table: "Abonnement",
                column: "AdherentId");

            migrationBuilder.CreateIndex(
                name: "IX_Adherent_CompteId",
                table: "Adherent",
                column: "CompteId");

            migrationBuilder.CreateIndex(
                name: "IX_Cours_ServiceId",
                table: "Cours",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_materiels_CoursId",
                table: "materiels",
                column: "CoursId");

            migrationBuilder.CreateIndex(
                name: "IX_Moniteur_CompteId",
                table: "Moniteur",
                column: "CompteId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_AdherentId",
                table: "Notification",
                column: "AdherentId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_AbonnementId",
                table: "Service",
                column: "AbonnementId");

            migrationBuilder.CreateIndex(
                name: "IX_Specialite_MoniteurId",
                table: "Specialite",
                column: "MoniteurId");

            migrationBuilder.CreateIndex(
                name: "IX_Temps_CoursId",
                table: "Temps",
                column: "CoursId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "materiels");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "Specialite");

            migrationBuilder.DropTable(
                name: "Temps");

            migrationBuilder.DropTable(
                name: "Moniteur");

            migrationBuilder.DropTable(
                name: "Cours");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "Abonnement");

            migrationBuilder.DropTable(
                name: "Adherent");

            migrationBuilder.DropTable(
                name: "comptes");
        }
    }
}
