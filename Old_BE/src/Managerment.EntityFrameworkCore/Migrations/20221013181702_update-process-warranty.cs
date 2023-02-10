using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Managerment.Migrations
{
    public partial class updateprocesswarranty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "User_warranty",
                table: "AppProcessWarranties",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "User_warranty",
                table: "AppProcessWarranties");
        }
    }
}
