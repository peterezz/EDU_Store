using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Edu_Store.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedModuleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalHour",
                table: "Modules",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<decimal>(
                name: "LectureTime_MS",
                table: "lectures",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LectureTime_MS",
                table: "lectures");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalHour",
                table: "Modules",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");
        }
    }
}
