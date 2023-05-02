using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Edu_Store.Data.Migrations
{
    /// <inheritdoc />
    public partial class CartTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0f7194db-7e0c-4ffe-a16b-07bad0d69c63");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fe801ec-0756-42a2-bb16-0ce7b8eecf14");

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseID = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_AspNetUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Carts_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "ac466e28-fcae-4405-bfe4-c5232c7148a4", "1d073965-3b77-4a93-bffa-95bc91d25b97", "Student", "student" },
                    { "f6e25cd1-9f62-4647-9cb4-a1644cb9f647", "18ef4c6b-cade-4213-89d5-30d6a12e8965", "Teacher", "teacher" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carts_CourseID",
                table: "Carts",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_StudentId",
                table: "Carts",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ac466e28-fcae-4405-bfe4-c5232c7148a4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f6e25cd1-9f62-4647-9cb4-a1644cb9f647");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0f7194db-7e0c-4ffe-a16b-07bad0d69c63", "9828b72c-ce82-4541-9d19-faf63d31e456", "Teacher", "teacher" },
                    { "7fe801ec-0756-42a2-bb16-0ce7b8eecf14", "f5d18dd6-2f0d-4cb6-ad99-3b1b0440309b", "Student", "student" }
                });
        }
    }
}
