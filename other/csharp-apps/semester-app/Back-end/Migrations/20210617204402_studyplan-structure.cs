using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class studyplanstructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudyPlanLearningUnit");

            migrationBuilder.AddColumn<int>(
                name: "ExploringItId",
                table: "StudyPlans",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FreeChoiceOneId",
                table: "StudyPlans",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FreeChoiceThreeId",
                table: "StudyPlans",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FreeChoiceTwoId",
                table: "StudyPlans",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProfileChoiceOneId",
                table: "StudyPlans",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProfileChoiceTwoId",
                table: "StudyPlans",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StudyPlans_ExploringItId",
                table: "StudyPlans",
                column: "ExploringItId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyPlans_FreeChoiceOneId",
                table: "StudyPlans",
                column: "FreeChoiceOneId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyPlans_FreeChoiceThreeId",
                table: "StudyPlans",
                column: "FreeChoiceThreeId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyPlans_FreeChoiceTwoId",
                table: "StudyPlans",
                column: "FreeChoiceTwoId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyPlans_ProfileChoiceOneId",
                table: "StudyPlans",
                column: "ProfileChoiceOneId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyPlans_ProfileChoiceTwoId",
                table: "StudyPlans",
                column: "ProfileChoiceTwoId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudyPlans_LearningUnits_ExploringItId",
                table: "StudyPlans",
                column: "ExploringItId",
                principalTable: "LearningUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudyPlans_LearningUnits_FreeChoiceOneId",
                table: "StudyPlans",
                column: "FreeChoiceOneId",
                principalTable: "LearningUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudyPlans_LearningUnits_FreeChoiceThreeId",
                table: "StudyPlans",
                column: "FreeChoiceThreeId",
                principalTable: "LearningUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudyPlans_LearningUnits_FreeChoiceTwoId",
                table: "StudyPlans",
                column: "FreeChoiceTwoId",
                principalTable: "LearningUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudyPlans_LearningUnits_ProfileChoiceOneId",
                table: "StudyPlans",
                column: "ProfileChoiceOneId",
                principalTable: "LearningUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudyPlans_LearningUnits_ProfileChoiceTwoId",
                table: "StudyPlans",
                column: "ProfileChoiceTwoId",
                principalTable: "LearningUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudyPlans_LearningUnits_ExploringItId",
                table: "StudyPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_StudyPlans_LearningUnits_FreeChoiceOneId",
                table: "StudyPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_StudyPlans_LearningUnits_FreeChoiceThreeId",
                table: "StudyPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_StudyPlans_LearningUnits_FreeChoiceTwoId",
                table: "StudyPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_StudyPlans_LearningUnits_ProfileChoiceOneId",
                table: "StudyPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_StudyPlans_LearningUnits_ProfileChoiceTwoId",
                table: "StudyPlans");

            migrationBuilder.DropIndex(
                name: "IX_StudyPlans_ExploringItId",
                table: "StudyPlans");

            migrationBuilder.DropIndex(
                name: "IX_StudyPlans_FreeChoiceOneId",
                table: "StudyPlans");

            migrationBuilder.DropIndex(
                name: "IX_StudyPlans_FreeChoiceThreeId",
                table: "StudyPlans");

            migrationBuilder.DropIndex(
                name: "IX_StudyPlans_FreeChoiceTwoId",
                table: "StudyPlans");

            migrationBuilder.DropIndex(
                name: "IX_StudyPlans_ProfileChoiceOneId",
                table: "StudyPlans");

            migrationBuilder.DropIndex(
                name: "IX_StudyPlans_ProfileChoiceTwoId",
                table: "StudyPlans");

            migrationBuilder.DropColumn(
                name: "ExploringItId",
                table: "StudyPlans");

            migrationBuilder.DropColumn(
                name: "FreeChoiceOneId",
                table: "StudyPlans");

            migrationBuilder.DropColumn(
                name: "FreeChoiceThreeId",
                table: "StudyPlans");

            migrationBuilder.DropColumn(
                name: "FreeChoiceTwoId",
                table: "StudyPlans");

            migrationBuilder.DropColumn(
                name: "ProfileChoiceOneId",
                table: "StudyPlans");

            migrationBuilder.DropColumn(
                name: "ProfileChoiceTwoId",
                table: "StudyPlans");

            migrationBuilder.CreateTable(
                name: "StudyPlanLearningUnit",
                columns: table => new
                {
                    StudyPlanId = table.Column<int>(type: "integer", nullable: false),
                    LearningUnitId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyPlanLearningUnit", x => new { x.StudyPlanId, x.LearningUnitId });
                    table.ForeignKey(
                        name: "FK_StudyPlanLearningUnit_LearningUnits_LearningUnitId",
                        column: x => x.LearningUnitId,
                        principalTable: "LearningUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudyPlanLearningUnit_StudyPlans_StudyPlanId",
                        column: x => x.StudyPlanId,
                        principalTable: "StudyPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudyPlanLearningUnit_LearningUnitId",
                table: "StudyPlanLearningUnit",
                column: "LearningUnitId");
        }
    }
}
