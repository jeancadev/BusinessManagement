using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixDecimalColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a73e7d99-5d1b-44cd-b38d-31e9a726f0b7"),
                columns: new[] { "CreatedAt", "HashedPassword" },
                values: new object[] { new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), "$2a$11$QoGpjBRRFdALcvpS1STwCues386.ao6sIdHq3SAo/4k3UE3/i.dFe" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b55e7d99-7a1b-44cd-b38d-31e9a726f0b8"),
                columns: new[] { "CreatedAt", "HashedPassword" },
                values: new object[] { new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), "$2a$11$cTTeqtMiGtmyob0Ku2Rx5eQGbVurEKphzvWmrVC3dnWivAnWLX0za" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a73e7d99-5d1b-44cd-b38d-31e9a726f0b7"),
                columns: new[] { "CreatedAt", "HashedPassword" },
                values: new object[] { new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Utc), "$2b$12$LQpcArUo2Bi81N9s3kQTPuR/G99pC68xRnY5EkeT3FF9XxXy7FfQe" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b55e7d99-7a1b-44cd-b38d-31e9a726f0b8"),
                columns: new[] { "CreatedAt", "HashedPassword" },
                values: new object[] { new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Utc), "$2b$10$2q8m6Ov45xaDhmiDeyKzyuW7B/QOfX4ar9JpgALcoajJJ9P1H7w5G" });
        }
    }
}
