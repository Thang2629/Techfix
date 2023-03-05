using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechFix.EntityModels.Migrations
{
    public partial class UpdateFunds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cashier",
                table: "Funds",
                newName: "CashierId");

            migrationBuilder.AddColumn<bool>(
                name: "IsReturn",
                table: "Bills",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Funds_CashierId",
                table: "Funds",
                column: "CashierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Funds_Users_CashierId",
                table: "Funds",
                column: "CashierId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Funds_Users_CashierId",
                table: "Funds");

            migrationBuilder.DropIndex(
                name: "IX_Funds_CashierId",
                table: "Funds");

            migrationBuilder.DropColumn(
                name: "IsReturn",
                table: "Bills");

            migrationBuilder.RenameColumn(
                name: "CashierId",
                table: "Funds",
                newName: "Cashier");
        }
    }
}
