using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class englishfields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Periode",
                table: "Semesters");

            migrationBuilder.AddColumn<string>(
                name: "Period",
                table: "Semesters",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Period",
                table: "Semesters");

            migrationBuilder.AddColumn<string>(
                name: "Periode",
                table: "Semesters",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
