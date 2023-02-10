using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Managerment.Migrations
{
    public partial class addpricewarranty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Warranty_price",
                table: "AppProcessWarranties",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Warranty_price",
                table: "AppProcessWarranties");
        }
    }
}
