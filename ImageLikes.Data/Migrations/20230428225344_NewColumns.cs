using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImageLikes.Data.Migrations
{
    public partial class NewColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Images",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Images");
        }
    }
}
