using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class renamedidToIdInBaseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_Authorid",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "Authorid",
                table: "Books",
                newName: "AuthorId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Books",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Books_Authorid",
                table: "Books",
                newName: "IX_Books_AuthorId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Authors",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_AuthorId",
                table: "Books",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_AuthorId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Books",
                newName: "Authorid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Books",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                newName: "IX_Books_Authorid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Authors",
                newName: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_Authorid",
                table: "Books",
                column: "Authorid",
                principalTable: "Authors",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
