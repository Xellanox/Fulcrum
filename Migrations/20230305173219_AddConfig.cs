using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fulcrum.Migrations
{
    /// <inheritdoc />
    public partial class AddConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SysConfiguration",
                columns: table => new
                {
                    SysConfigId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LibraryBasePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImportBasePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArtBasePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LibraryStructureScheme = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstRunComplete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysConfiguration", x => x.SysConfigId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SysConfiguration");
        }
    }
}
