using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechFix.EntityModels.Migrations
{
    public partial class AddFKUser_ProductHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProductHistories_UserId",
                table: "ProductHistories",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductHistories_Users_UserId",
                table: "ProductHistories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductHistories_Users_UserId",
                table: "ProductHistories");

            migrationBuilder.DropIndex(
                name: "IX_ProductHistories_UserId",
                table: "ProductHistories");
        }
    }
}
