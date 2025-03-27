using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BusinessManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsersStatic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2fad9bca-3c0e-4289-8ded-2808b5e87d5c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("456650e8-2140-49b4-a95b-a53d706465c1"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "HashedPassword", "Role", "Username" },
                values: new object[,]
                {
                    { new Guid("a73e7d99-5d1b-44cd-b38d-31e9a726f0b7"), new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Utc), "$2b$12$LQpcArUo2Bi81N9s3kQTPuR/G99pC68xRnY5EkeT3FF9XxXy7FfQe", "Admin", "admin" },
                    { new Guid("b55e7d99-7a1b-44cd-b38d-31e9a726f0b8"), new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Utc), "$2b$10$2q8m6Ov45xaDhmiDeyKzyuW7B/QOfX4ar9JpgALcoajJJ9P1H7w5G", "IT", "it" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a73e7d99-5d1b-44cd-b38d-31e9a726f0b7"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b55e7d99-7a1b-44cd-b38d-31e9a726f0b8"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "HashedPassword", "Role", "Username" },
                values: new object[,]
                {
                    { new Guid("2fad9bca-3c0e-4289-8ded-2808b5e87d5c"), new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Utc), "$2b$12$Ux5PbmTPuQOchHpUQYVOZu6bch7a4DUE2dVP4Ly4BhQCmGLCzXKfe", "IT", "it" },
                    { new Guid("456650e8-2140-49b4-a95b-a53d706465c1"), new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Utc), "$2b$12$LQpcArUo2Bi81N9s3kQTPuR/G99pC68xRnY5EkeT3FF9XxXy7FfQe", "Admin", "admin" }
                });
        }
    }
}
