using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class initDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TokenDocuments",
                columns: table => new
                {
                    TokenModelId = table.Column<int>(type: "int", nullable: false),
                    DocumentModelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TokenDocuments", x => new { x.TokenModelId, x.DocumentModelId });
                    table.ForeignKey(
                        name: "FK_TokenDocuments_Documents_DocumentModelId",
                        column: x => x.DocumentModelId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TokenDocuments_Tokens_TokenModelId",
                        column: x => x.TokenModelId,
                        principalTable: "Tokens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documents_Name",
                table: "Documents",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TokenDocuments_DocumentModelId",
                table: "TokenDocuments",
                column: "DocumentModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_Value",
                table: "Tokens",
                column: "Value",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TokenDocuments");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Tokens");
        }
    }
}
