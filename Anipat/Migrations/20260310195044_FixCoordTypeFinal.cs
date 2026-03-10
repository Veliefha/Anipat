using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anipat.Migrations
{
    /// <inheritdoc />
    public partial class FixCoordTypeFinal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "Pets",
                type: "decimal(18,10)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "Pets",
                type: "decimal(18,10)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Longitude",
                table: "Pets",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,10)");

            migrationBuilder.AlterColumn<double>(
                name: "Latitude",
                table: "Pets",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,10)");
        }
    }
}
