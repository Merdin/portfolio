using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class removesemesteridfromconstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Constraints_Semesters_SemesterId",
                table: "Constraints");

            migrationBuilder.DropIndex(
                name: "IX_Constraints_SemesterId",
                table: "Constraints");

            migrationBuilder.DropColumn(
                name: "SemesterId",
                table: "Constraints");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SemesterId",
                table: "Constraints",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Constraints_SemesterId",
                table: "Constraints",
                column: "SemesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Constraints_Semesters_SemesterId",
                table: "Constraints",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
