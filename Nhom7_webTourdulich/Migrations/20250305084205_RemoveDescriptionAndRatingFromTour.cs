using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nhom7_webTourdulich.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDescriptionAndRatingFromTour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Tour");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Tour");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Tour",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Tour",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
