using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedMoreFieldsInEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "imageURL",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "imageURL",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "shortBiography",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imageURL",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "imageURL",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "shortBiography",
                table: "Authors");
        }
    }
}
