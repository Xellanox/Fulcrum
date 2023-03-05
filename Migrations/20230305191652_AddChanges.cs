using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fulcrum.Migrations
{
    /// <inheritdoc />
    public partial class AddChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ArtFilename",
                table: "Mediafiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArtPath",
                table: "Mediafiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MimeType",
                table: "Mediafiles",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArtFilename",
                table: "Mediafiles");

            migrationBuilder.DropColumn(
                name: "ArtPath",
                table: "Mediafiles");

            migrationBuilder.DropColumn(
                name: "MimeType",
                table: "Mediafiles");
        }
    }
}
