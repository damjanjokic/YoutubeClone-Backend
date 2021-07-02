using Microsoft.EntityFrameworkCore.Migrations;

namespace YoutubeClone.Persistence.Migrations
{
    public partial class VideoToPostAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "VideoFileName",
                table: "Posts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VideoFileName",
                table: "Posts");

        }
    }
}
