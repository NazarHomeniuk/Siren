using Microsoft.EntityFrameworkCore.Migrations;

namespace Siren.MobileAppService.Migrations
{
    public partial class UpdatedTracksTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Artist",
                table: "Tracks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Tracks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Artist",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Tracks");
        }
    }
}
