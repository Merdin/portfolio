using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Backend.Migrations
{
    public partial class renamesemestertolearningunit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SemesterModule");

            migrationBuilder.DropTable(
                name: "StudyPlanSemester");

            migrationBuilder.DropTable(
                name: "Semesters");

            migrationBuilder.CreateTable(
                name: "LearningUnits",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Period = table.Column<string>(nullable: false),
                    ContactPersonId = table.Column<int>(nullable: true),
                    Visible = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Expiration = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearningUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LearningUnits_Users_ContactPersonId",
                        column: x => x.ContactPersonId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LearningUnitModule",
                columns: table => new
                {
                    LearningUnitId = table.Column<int>(nullable: false),
                    ModuleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearningUnitModule", x => new { x.LearningUnitId, x.ModuleId });
                    table.ForeignKey(
                        name: "FK_LearningUnitModule_LearningUnits_LearningUnitId",
                        column: x => x.LearningUnitId,
                        principalTable: "LearningUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LearningUnitModule_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudyPlanLearningUnit",
                columns: table => new
                {
                    StudyPlanId = table.Column<int>(nullable: false),
                    LearningUnitId = table.Column<int>(nullable: false)
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
                name: "IX_LearningUnitModule_ModuleId",
                table: "LearningUnitModule",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_LearningUnits_ContactPersonId",
                table: "LearningUnits",
                column: "ContactPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyPlanLearningUnit_LearningUnitId",
                table: "StudyPlanLearningUnit",
                column: "LearningUnitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LearningUnitModule");

            migrationBuilder.DropTable(
                name: "StudyPlanLearningUnit");

            migrationBuilder.DropTable(
                name: "LearningUnits");

            migrationBuilder.CreateTable(
                name: "Semesters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContactPersonId = table.Column<int>(type: "integer", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Expiration = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Period = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Visible = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semesters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Semesters_Users_ContactPersonId",
                        column: x => x.ContactPersonId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SemesterModule",
                columns: table => new
                {
                    SemesterId = table.Column<int>(type: "integer", nullable: false),
                    ModuleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemesterModule", x => new { x.SemesterId, x.ModuleId });
                    table.ForeignKey(
                        name: "FK_SemesterModule_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SemesterModule_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudyPlanSemester",
                columns: table => new
                {
                    StudyPlanId = table.Column<int>(type: "integer", nullable: false),
                    SemesterId = table.Column<int>(type: "integer", nullable: false)
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
                name: "IX_SemesterModule_ModuleId",
                table: "SemesterModule",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_ContactPersonId",
                table: "Semesters",
                column: "ContactPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyPlanSemester_SemesterId",
                table: "StudyPlanSemester",
                column: "SemesterId");
        }
    }
}
