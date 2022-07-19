using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webAPi.Migrations
{
    public partial class secondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Abonnement_Adherent_AdherentId",
                table: "Abonnement");

            migrationBuilder.DropForeignKey(
                name: "FK_Adherent_comptes_CompteId",
                table: "Adherent");

            migrationBuilder.DropIndex(
                name: "IX_Abonnement_AdherentId",
                table: "Abonnement");

            migrationBuilder.DropColumn(
                name: "AdherentId",
                table: "Abonnement");

            migrationBuilder.AlterColumn<int>(
                name: "CompteId",
                table: "Adherent",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AbonnementId",
                table: "Adherent",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Adherent_AbonnementId",
                table: "Adherent",
                column: "AbonnementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Adherent_Abonnement_AbonnementId",
                table: "Adherent",
                column: "AbonnementId",
                principalTable: "Abonnement",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Adherent_comptes_CompteId",
                table: "Adherent",
                column: "CompteId",
                principalTable: "comptes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adherent_Abonnement_AbonnementId",
                table: "Adherent");

            migrationBuilder.DropForeignKey(
                name: "FK_Adherent_comptes_CompteId",
                table: "Adherent");

            migrationBuilder.DropIndex(
                name: "IX_Adherent_AbonnementId",
                table: "Adherent");

            migrationBuilder.DropColumn(
                name: "AbonnementId",
                table: "Adherent");

            migrationBuilder.AlterColumn<int>(
                name: "CompteId",
                table: "Adherent",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AdherentId",
                table: "Abonnement",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Abonnement_AdherentId",
                table: "Abonnement",
                column: "AdherentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Abonnement_Adherent_AdherentId",
                table: "Abonnement",
                column: "AdherentId",
                principalTable: "Adherent",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Adherent_comptes_CompteId",
                table: "Adherent",
                column: "CompteId",
                principalTable: "comptes",
                principalColumn: "Id");
        }
    }
}
