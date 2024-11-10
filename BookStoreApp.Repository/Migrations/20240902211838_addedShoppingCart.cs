using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedShoppingCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShoppingCart",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ownerId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCart_AspNetUsers_ownerId",
                        column: x => x.ownerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BookInShoppingCart",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    bookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    shoppingCartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookInShoppingCart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookInShoppingCart_Books_bookId",
                        column: x => x.bookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookInShoppingCart_ShoppingCart_shoppingCartId",
                        column: x => x.shoppingCartId,
                        principalTable: "ShoppingCart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookInShoppingCart_bookId",
                table: "BookInShoppingCart",
                column: "bookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookInShoppingCart_shoppingCartId",
                table: "BookInShoppingCart",
                column: "shoppingCartId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCart_ownerId",
                table: "ShoppingCart",
                column: "ownerId",
                unique: true,
                filter: "[ownerId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookInShoppingCart");

            migrationBuilder.DropTable(
                name: "ShoppingCart");
        }
    }
}
