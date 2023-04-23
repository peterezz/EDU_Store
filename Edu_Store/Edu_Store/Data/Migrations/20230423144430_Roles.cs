using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Edu_Store.Data.Migrations
{
    /// <inheritdoc />
    public partial class Roles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0f7194db-7e0c-4ffe-a16b-07bad0d69c63", "9828b72c-ce82-4541-9d19-faf63d31e456", "Teacher", "teacher" },
                    { "7fe801ec-0756-42a2-bb16-0ce7b8eecf14", "f5d18dd6-2f0d-4cb6-ad99-3b1b0440309b", "Student", "student" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0f7194db-7e0c-4ffe-a16b-07bad0d69c63");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fe801ec-0756-42a2-bb16-0ce7b8eecf14");
        }
    }
}
