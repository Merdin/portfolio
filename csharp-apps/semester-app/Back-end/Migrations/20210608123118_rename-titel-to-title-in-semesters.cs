using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class renametiteltotitleinsemesters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Titel",
                table: "Semesters");
            
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Semesters",
                nullable: true,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Semesters");
            
            migrationBuilder.AddColumn<string>(
                name: "Titel",
                table: "Semesters",
                nullable: true,
                defaultValue: "");
        }
    }
}
