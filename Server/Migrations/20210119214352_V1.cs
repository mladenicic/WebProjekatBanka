using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banka",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Vrednost = table.Column<int>(type: "int", nullable: false),
                    Kapacitet = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banka", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Korisnik",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Prezime = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DatumRodjenja = table.Column<string>(name: "Datum Rodjenja", type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BankaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnik", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Korisnik_Banka_BankaID",
                        column: x => x.BankaID,
                        principalTable: "Banka",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Kredit",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Broj = table.Column<int>(type: "int", nullable: false),
                    Iznos = table.Column<int>(type: "int", nullable: false),
                    DatumPodizanja = table.Column<string>(name: "Datum Podizanja", type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DatumIsplate = table.Column<string>(name: "Datum Isplate", type: "nvarchar(255)", maxLength: 255, nullable: true),
                    VracenIznos = table.Column<int>(name: "Vracen Iznos", type: "int", nullable: false),
                    BankaID = table.Column<int>(type: "int", nullable: true),
                    KorisnikID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kredit", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Kredit_Banka_BankaID",
                        column: x => x.BankaID,
                        principalTable: "Banka",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Kredit_Korisnik_KorisnikID",
                        column: x => x.KorisnikID,
                        principalTable: "Korisnik",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Korisnik_BankaID",
                table: "Korisnik",
                column: "BankaID");

            migrationBuilder.CreateIndex(
                name: "IX_Kredit_BankaID",
                table: "Kredit",
                column: "BankaID");

            migrationBuilder.CreateIndex(
                name: "IX_Kredit_KorisnikID",
                table: "Kredit",
                column: "KorisnikID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kredit");

            migrationBuilder.DropTable(
                name: "Korisnik");

            migrationBuilder.DropTable(
                name: "Banka");
        }
    }
}
