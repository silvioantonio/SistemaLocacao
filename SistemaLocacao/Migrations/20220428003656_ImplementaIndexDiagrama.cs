using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaLocacao.Migrations
{
    public partial class ImplementaIndexDiagrama : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cliente_cpf",
                table: "Cliente");

            migrationBuilder.CreateIndex(
                name: "IX_Filme_lancamento_titulo",
                table: "Filme",
                columns: new[] { "lancamento", "titulo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_cpf_nome",
                table: "Cliente",
                columns: new[] { "cpf", "nome" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Filme_lancamento_titulo",
                table: "Filme");

            migrationBuilder.DropIndex(
                name: "IX_Cliente_cpf_nome",
                table: "Cliente");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_cpf",
                table: "Cliente",
                column: "cpf",
                unique: true);
        }
    }
}
