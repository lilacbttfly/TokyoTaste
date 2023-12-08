using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TokyoTasteCrud.Migrations
{
    /// <inheritdoc />
    public partial class atualizacao05 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Paises_Categoria_CategoriaId",
                table: "Paises");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Paises",
                table: "Paises");

            migrationBuilder.RenameTable(
                name: "Paises",
                newName: "Produtos");

            migrationBuilder.RenameIndex(
                name: "IX_Paises_CategoriaId",
                table: "Produtos",
                newName: "IX_Produtos_CategoriaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Produtos",
                table: "Produtos",
                column: "ProdutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Categoria_CategoriaId",
                table: "Produtos",
                column: "CategoriaId",
                principalTable: "Categoria",
                principalColumn: "ProdutoId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Categoria_CategoriaId",
                table: "Produtos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Produtos",
                table: "Produtos");

            migrationBuilder.RenameTable(
                name: "Produtos",
                newName: "Paises");

            migrationBuilder.RenameIndex(
                name: "IX_Produtos_CategoriaId",
                table: "Paises",
                newName: "IX_Paises_CategoriaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Paises",
                table: "Paises",
                column: "ProdutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Paises_Categoria_CategoriaId",
                table: "Paises",
                column: "CategoriaId",
                principalTable: "Categoria",
                principalColumn: "ProdutoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
