using Microsoft.EntityFrameworkCore.Migrations;

namespace Siren.MobileAppService.Migrations
{
    public partial class UpdatedMessageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_SentById1",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_SentById1",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "SentById1",
                table: "Messages");

            migrationBuilder.AlterColumn<string>(
                name: "SentById",
                table: "Messages",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SentById",
                table: "Messages",
                column: "SentById");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_SentById",
                table: "Messages",
                column: "SentById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_SentById",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_SentById",
                table: "Messages");

            migrationBuilder.AlterColumn<int>(
                name: "SentById",
                table: "Messages",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SentById1",
                table: "Messages",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SentById1",
                table: "Messages",
                column: "SentById1");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_SentById1",
                table: "Messages",
                column: "SentById1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
