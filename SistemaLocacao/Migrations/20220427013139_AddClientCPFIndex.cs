using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaLocacao.Migrations
{
    public partial class AddClientCPFIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Cliente_cpf",
                table: "Cliente",
                column: "cpf",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cliente_cpf",
                table: "Cliente");
        }
    }
}
