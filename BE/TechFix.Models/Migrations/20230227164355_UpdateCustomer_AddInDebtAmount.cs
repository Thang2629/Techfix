using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechFix.EntityModels.Migrations
{
    public partial class UpdateCustomer_AddInDebtAmount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "InDebtAmount",
                table: "Customers",
                type: "decimal(38,16)",
                precision: 38,
                scale: 16,
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InDebtAmount",
                table: "Customers");
        }
    }
}
