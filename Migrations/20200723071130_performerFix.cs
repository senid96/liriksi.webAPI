using Microsoft.EntityFrameworkCore.Migrations;

namespace liriksi.WebAPI.Migrations
{
    public partial class performerFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Song_Performer_PerformerId",
                table: "Song");

            migrationBuilder.DropIndex(
                name: "IX_Song_PerformerId",
                table: "Song");

            migrationBuilder.DropColumn(
                name: "PerformerId",
                table: "Song");

            migrationBuilder.AddColumn<int>(
                name: "PerformerId",
                table: "Album",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Album_PerformerId",
                table: "Album",
                column: "PerformerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Album_Performer_PerformerId",
                table: "Album",
                column: "PerformerId",
                principalTable: "Performer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Album_Performer_PerformerId",
                table: "Album");

            migrationBuilder.DropIndex(
                name: "IX_Album_PerformerId",
                table: "Album");

            migrationBuilder.DropColumn(
                name: "PerformerId",
                table: "Album");

            migrationBuilder.AddColumn<int>(
                name: "PerformerId",
                table: "Song",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Song_PerformerId",
                table: "Song",
                column: "PerformerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Song_Performer_PerformerId",
                table: "Song",
                column: "PerformerId",
                principalTable: "Performer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
