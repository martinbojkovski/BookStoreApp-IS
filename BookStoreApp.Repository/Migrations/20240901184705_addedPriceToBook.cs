using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedPriceToBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "price",
                table: "Books",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "price",
                table: "Books");
        }
    }
}
