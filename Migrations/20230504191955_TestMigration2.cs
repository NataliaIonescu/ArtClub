using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtClub.Migrations
{
    /// <inheritdoc />
    public partial class TestMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Events_ResourceId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Availability",
                table: "Resources");

            migrationBuilder.CreateIndex(
                name: "IX_Events_ResourceId",
                table: "Events",
                column: "ResourceId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Events_ResourceId",
                table: "Events");

            migrationBuilder.AddColumn<string>(
                name: "Availability",
                table: "Resources",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Events_ResourceId",
                table: "Events",
                column: "ResourceId");
        }
    }
}
