using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookmarkTracker.Data.Migrations
{
    public partial class changedbookmarkprops : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookmarkCount",
                table: "Bookmarks",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookmarkCount",
                table: "Bookmarks");
        }
    }
}
