using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConcessionariaApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Veiculos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Marca = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Modelo = table.Column<string>(type: "TEXT", maxLength: 80, nullable: false),
                    Ano = table.Column<int>(type: "INTEGER", nullable: false),
                    Preco = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false),
                    Quilometragem = table.Column<long>(type: "INTEGER", nullable: false),
                    Cor = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Combustivel = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Disponivel = table.Column<bool>(type: "INTEGER", nullable: false),
                    CriadoEmUtc = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculos", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Veiculos",
                columns: new[] { "Id", "Ano", "Combustivel", "Cor", "CriadoEmUtc", "Disponivel", "Marca", "Modelo", "Preco", "Quilometragem" },
                values: new object[,]
                {
                    { 1, 2024, "Flex", "Prata", new DateTime(2026, 9, 29, 12, 0, 0, 0, DateTimeKind.Utc), true, "Toyota", "Corolla Altis", 158900.00m, 12500L },
                    { 2, 2025, "Flex", "Branco", new DateTime(2026, 9, 29, 12, 0, 0, 0, DateTimeKind.Utc), true, "Volkswagen", "T-Cross Highline", 172490.00m, 8200L },
                    { 3, 2023, "Flex", "Azul", new DateTime(2026, 9, 29, 12, 0, 0, 0, DateTimeKind.Utc), false, "Chevrolet", "Onix Premier", 98900.00m, 31000L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Veiculos_Disponivel",
                table: "Veiculos",
                column: "Disponivel");

            migrationBuilder.CreateIndex(
                name: "IX_Veiculos_Marca_Modelo",
                table: "Veiculos",
                columns: new[] { "Marca", "Modelo" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Veiculos");
        }
    }
}
