using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class studyplanAdditions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "StudyPlans");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "StudyPlans",
                nullable: false);

            migrationBuilder.CreateTable(
                name: "StudyPlanSemester",
                columns: table => new
                {
                    StudyPlanId = table.Column<int>(nullable: false),
                    SemesterId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyPlanSemester", x => new { x.StudyPlanId, x.SemesterId });
                    table.ForeignKey(
                        name: "FK_StudyPlanSemester_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudyPlanSemester_StudyPlans_StudyPlanId",
                        column: x => x.StudyPlanId,
                        principalTable: "StudyPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudyPlanSemester_SemesterId",
                table: "StudyPlanSemester",
                column: "SemesterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudyPlanSemester");
            
            migrationBuilder.DropColumn(
                name: "Status",
                table: "StudyPlans");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "StudyPlans",
                nullable: false,
                defaultValue: "");
        }
    }
}
