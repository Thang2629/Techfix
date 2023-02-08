using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Managerment.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppProcessRepairs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PR_date_order = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PR_status_fix = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PR_process_repair = table.Column<int>(type: "int", nullable: false),
                    Id_order_repair = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProcessRepairs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppProcessRepairs_AppOrderRepairs_Id_order_repair",
                        column: x => x.Id_order_repair,
                        principalTable: "AppOrderRepairs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppProcessRepairs_Id_order_repair",
                table: "AppProcessRepairs",
                column: "Id_order_repair");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppProcessRepairs");
        }
    }
}
