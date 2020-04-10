using Microsoft.EntityFrameworkCore.Migrations;

namespace Siren.MobileAppService.Migrations
{
    public partial class UpdatedUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrackId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TrackId",
                table: "AspNetUsers",
                column: "TrackId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Tracks_TrackId",
                table: "AspNetUsers",
                column: "TrackId",
                principalTable: "Tracks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Tracks_TrackId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TrackId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TrackId",
                table: "AspNetUsers");
        }
    }
}
