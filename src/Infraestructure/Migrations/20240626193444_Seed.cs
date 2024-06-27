using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class Seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "CreatedAt", "Email", "Name", "UpdatedAt" },
                values: new object[] { new Guid("12081264-5645-407a-ae37-78d5da96fe59"), "Rua Exemplo 1, 123", new DateTime(2024, 6, 26, 0, 0, 0, 0, DateTimeKind.Utc), "cliente1@example.com", "Cliente Exemplo 1", new DateTime(2024, 6, 26, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("48c6dc20-a943-4f8f-83ca-1e1cf094a612"), new DateTime(2024, 6, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Produto 2", new DateTime(2024, 6, 26, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("48c6dc20-a943-4f8f-83ca-1e1cf094a683"), new DateTime(2024, 6, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Produto 1", new DateTime(2024, 6, 26, 0, 0, 0, 0, DateTimeKind.Utc) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("12081264-5645-407a-ae37-78d5da96fe59"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("48c6dc20-a943-4f8f-83ca-1e1cf094a612"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("48c6dc20-a943-4f8f-83ca-1e1cf094a683"));
        }
    }
}
