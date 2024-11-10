using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedOrdering : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ownerId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_AspNetUsers_ownerId",
                        column: x => x.ownerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BookInOrder",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    bookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    orderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookInOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookInOrder_Books_bookId",
                        column: x => x.bookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookInOrder_Order_orderId",
                        column: x => x.orderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookInOrder_bookId",
                table: "BookInOrder",
                column: "bookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookInOrder_orderId",
                table: "BookInOrder",
                column: "orderId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ownerId",
                table: "Order",
                column: "ownerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookInOrder");

            migrationBuilder.DropTable(
                name: "Order");
        }
    }
}
