﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TokyoTasteCrud.Migrations
{
    /// <inheritdoc />
    public partial class Atualizacao03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Imagem",
                table: "Usuarios",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagem",
                table: "Usuarios");
        }
    }
}
