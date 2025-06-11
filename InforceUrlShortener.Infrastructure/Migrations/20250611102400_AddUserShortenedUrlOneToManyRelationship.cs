using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InforceUrlShortener.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserShortenedUrlOneToManyRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "ShortenedUrls",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            // hardcoded in order to migrate db without need to make owner id nullable (admin Id)
            migrationBuilder.Sql("UPDATE ShortenedUrls Set OwnerId = '6e90536d-737a-4120-93e0-d3da91f7b3aa'");

            migrationBuilder.CreateIndex(
                name: "IX_ShortenedUrls_OwnerId",
                table: "ShortenedUrls",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShortenedUrls_AspNetUsers_OwnerId",
                table: "ShortenedUrls",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShortenedUrls_AspNetUsers_OwnerId",
                table: "ShortenedUrls");

            migrationBuilder.DropIndex(
                name: "IX_ShortenedUrls_OwnerId",
                table: "ShortenedUrls");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "ShortenedUrls");
        }
    }
}
