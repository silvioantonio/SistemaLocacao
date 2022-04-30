using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaLocacao.Migrations
{
    public partial class AtualizaModelo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientId1",
                table: "Locacao",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MovieId1",
                table: "Locacao",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locacao_ClientId1",
                table: "Locacao",
                column: "ClientId1");

            migrationBuilder.CreateIndex(
                name: "IX_Locacao_MovieId1",
                table: "Locacao",
                column: "MovieId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Locacao_Cliente_ClientId1",
                table: "Locacao",
                column: "ClientId1",
                principalTable: "Cliente",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Locacao_Filme_MovieId1",
                table: "Locacao",
                column: "MovieId1",
                principalTable: "Filme",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locacao_Cliente_ClientId1",
                table: "Locacao");

            migrationBuilder.DropForeignKey(
                name: "FK_Locacao_Filme_MovieId1",
                table: "Locacao");

            migrationBuilder.DropIndex(
                name: "IX_Locacao_ClientId1",
                table: "Locacao");

            migrationBuilder.DropIndex(
                name: "IX_Locacao_MovieId1",
                table: "Locacao");

            migrationBuilder.DropColumn(
                name: "ClientId1",
                table: "Locacao");

            migrationBuilder.DropColumn(
                name: "MovieId1",
                table: "Locacao");
        }
    }
}
