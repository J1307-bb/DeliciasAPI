using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliciasAPI.Migrations
{
    public partial class Terms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Terms",
                columns: table => new
                {
                    IdTerms = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terms", x => x.IdTerms);
                });

            migrationBuilder.CreateTable(
                name: "TermsItem",
                columns: table => new
                {
                    IdTermsItem = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Term = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TermsIdTerms = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TermsItem", x => x.IdTermsItem);
                    table.ForeignKey(
                        name: "FK_TermsItem_Terms_TermsIdTerms",
                        column: x => x.TermsIdTerms,
                        principalTable: "Terms",
                        principalColumn: "IdTerms");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TermsItem_TermsIdTerms",
                table: "TermsItem",
                column: "TermsIdTerms");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TermsItem");

            migrationBuilder.DropTable(
                name: "Terms");
        }
    }
}
