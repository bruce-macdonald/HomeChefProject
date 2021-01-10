using Microsoft.EntityFrameworkCore.Migrations;

namespace SoloCapstone.Data.Migrations
{
    public partial class UpdatedRecipeFavoritesAndChefIngredients : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Item",
                table: "RecipeFavorites");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "RecipeFavorites",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "RecipeFavorites",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageType",
                table: "RecipeFavorites",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "RecipeFavorites",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "RecipeFavorites",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "RecipeFavorites");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "RecipeFavorites");

            migrationBuilder.DropColumn(
                name: "ImageType",
                table: "RecipeFavorites");

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "RecipeFavorites");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "RecipeFavorites");

            migrationBuilder.AddColumn<string>(
                name: "Item",
                table: "RecipeFavorites",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
