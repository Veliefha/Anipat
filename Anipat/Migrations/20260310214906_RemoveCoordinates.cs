using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anipat.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCoordinates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Pets");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Latitude",
                table: "Pets",
                type: "decimal(9,6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Longitude",
                table: "Pets",
                type: "decimal(9,6)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
