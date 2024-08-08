using Microsoft.EntityFrameworkCore.Migrations;

namespace MuzikantenApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Muzikant",
                columns: table => new
                {
                    MuzikantId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Voornaam = table.Column<string>(maxLength: 40, nullable: false),
                    Achternaam = table.Column<string>(maxLength: 50, nullable: false),
                    StartJaar = table.Column<int>(nullable: false),
                    StopJaar = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Muzikant", x => x.MuzikantId);
                });

            migrationBuilder.CreateTable(
                name: "Instrument",
                columns: table => new
                {
                    InstrumentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(maxLength: 40, nullable: false),
                    Type = table.Column<string>(maxLength: 30, nullable: true),
                    Bijnaam = table.Column<string>(maxLength: 30, nullable: true),
                    Waarde = table.Column<int>(nullable: false),
                    MuzikantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instrument", x => x.InstrumentId);
                    table.ForeignKey(
                        name: "FK_Instrument_Muzikant_MuzikantId",
                        column: x => x.MuzikantId,
                        principalTable: "Muzikant",
                        principalColumn: "MuzikantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Instrument_MuzikantId",
                table: "Instrument",
                column: "MuzikantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Instrument");

            migrationBuilder.DropTable(
                name: "Muzikant");
        }
    }
}
