using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Managerment.Migrations
{
    public partial class processwarranty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppProcessWarranties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Warranty_status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Warranty_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Warranty_process = table.Column<int>(type: "int", nullable: false),
                    Order_warranty_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProcessWarranties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppProcessWarranties_AppOrderWarranties_Order_warranty_id",
                        column: x => x.Order_warranty_id,
                        principalTable: "AppOrderWarranties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppProcessWarranties_Order_warranty_id",
                table: "AppProcessWarranties",
                column: "Order_warranty_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppProcessWarranties");
        }
    }
}
