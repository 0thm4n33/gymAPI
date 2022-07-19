using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webAPi.Migrations
{
    public partial class thirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Duree",
                table: "Cours",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Niveau",
                table: "Cours",
                type: "nvarchar(max)",
                nullable: true);
        }

        

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duree",
                table: "Cours");

            migrationBuilder.DropColumn(
                name: "Niveau",
                table: "Cours");
        }
    }
}
