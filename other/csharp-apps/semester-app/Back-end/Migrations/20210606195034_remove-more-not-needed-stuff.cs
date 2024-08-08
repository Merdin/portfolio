using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class removemorenotneededstuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Semesters_StudyPlans_StudyPlanId",
                table: "Semesters");

            migrationBuilder.DropIndex(
                name: "IX_Semesters_StudyPlanId",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "StudyPlanId",
                table: "Semesters");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudyPlanId",
                table: "Semesters",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_StudyPlanId",
                table: "Semesters",
                column: "StudyPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Semesters_StudyPlans_StudyPlanId",
                table: "Semesters",
                column: "StudyPlanId",
                principalTable: "StudyPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
