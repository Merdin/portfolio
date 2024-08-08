using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class SemesterModules : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modules_Semesters_SemesterId",
                table: "Modules");

            migrationBuilder.DropIndex(
                name: "IX_Modules_SemesterId",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "SemesterId",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Semesters");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Semesters",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Periode",
                table: "Semesters",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Semesters",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Expiration",
                table: "Semesters",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "SemesterModule",
                columns: table => new
                {
                    SemesterId = table.Column<int>(nullable: false),
                    ModuleId = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_SemesterModule_ModuleId",
                table: "SemesterModule",
                column: "ModuleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SemesterModule");

            migrationBuilder.DropColumn(
                name: "Expiration",
                table: "Semesters");
            
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Semesters");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Semesters",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Periode",
                table: "Semesters",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Semesters",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "SemesterId",
                table: "Modules",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Modules_SemesterId",
                table: "Modules",
                column: "SemesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Semesters_SemesterId",
                table: "Modules",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
