using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class removenotneededcolumnsforsemester : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PointsRequired",
                table: "Semesters");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PointsRequired",
                table: "Semesters",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
