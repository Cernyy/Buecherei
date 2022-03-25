using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bücherei.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblBuch",
                columns: table => new
                {
                    Buchnummer = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    Sachgebiet = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Titel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Autor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ort = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Erscheinungsjahr = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblBuch", x => x.Buchnummer);
                });

            migrationBuilder.CreateTable(
                name: "TblSchuelerIn",
                columns: table => new
                {
                    Ausweisnummer = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vorname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nachname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblSchuelerIn", x => x.Ausweisnummer);
                });

            migrationBuilder.CreateTable(
                name: "TblAusleihe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Buchnummer = table.Column<string>(type: "nvarchar(6)", nullable: false),
                    Ausweisnummer = table.Column<int>(type: "int", nullable: false),
                    Ausleihdatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Retourdatum = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblAusleihe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblAusleihe_TblBuch_Buchnummer",
                        column: x => x.Buchnummer,
                        principalTable: "TblBuch",
                        principalColumn: "Buchnummer",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TblAusleihe_TblSchuelerIn_Ausweisnummer",
                        column: x => x.Ausweisnummer,
                        principalTable: "TblSchuelerIn",
                        principalColumn: "Ausweisnummer",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblAusleihe_Ausweisnummer",
                table: "TblAusleihe",
                column: "Ausweisnummer");

            migrationBuilder.CreateIndex(
                name: "IX_TblAusleihe_Buchnummer",
                table: "TblAusleihe",
                column: "Buchnummer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblAusleihe");

            migrationBuilder.DropTable(
                name: "TblBuch");

            migrationBuilder.DropTable(
                name: "TblSchuelerIn");
        }
    }
}
