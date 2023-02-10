using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Managerment.Migrations
{
    public partial class detailproductrepair : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppDetailProductRepairs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PD_CPU = table.Column<int>(type: "int", nullable: false),
                    PD_HDD = table.Column<int>(type: "int", nullable: false),
                    PD_Ram = table.Column<int>(type: "int", nullable: false),
                    PD_Wifi = table.Column<int>(type: "int", nullable: false),
                    PD_Pin = table.Column<int>(type: "int", nullable: false),
                    PD_Adapter = table.Column<int>(type: "int", nullable: false),
                    PD_Keyboard = table.Column<int>(type: "int", nullable: false),
                    PD_PSU = table.Column<int>(type: "int", nullable: false),
                    PD_LCD = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDetailProductRepairs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppDetailProductRepairs");
        }
    }
}
