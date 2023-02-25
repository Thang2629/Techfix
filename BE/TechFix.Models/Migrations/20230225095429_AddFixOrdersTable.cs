using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechFix.EntityModels.Migrations
{
    public partial class AddFixOrdersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WarrantyOrderId",
                table: "FixProducts",
                newName: "FixOrderId");

            migrationBuilder.AddColumn<bool>(
                name: "IsFixOrder",
                table: "FixProducts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "FixOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CashierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceivedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsFixOrder = table.Column<bool>(type: "bit", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SearchData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FixOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FixOrders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FixOrders_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FixOrders_Users_CashierId",
                        column: x => x.CashierId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FixProducts_FixOrderId",
                table: "FixProducts",
                column: "FixOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_FixOrders_CashierId",
                table: "FixOrders",
                column: "CashierId");

            migrationBuilder.CreateIndex(
                name: "IX_FixOrders_CustomerId",
                table: "FixOrders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_FixOrders_StoreId",
                table: "FixOrders",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_FixProducts_FixOrders_FixOrderId",
                table: "FixProducts",
                column: "FixOrderId",
                principalTable: "FixOrders",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FixProducts_FixOrders_FixOrderId",
                table: "FixProducts");

            migrationBuilder.DropTable(
                name: "FixOrders");

            migrationBuilder.DropIndex(
                name: "IX_FixProducts_FixOrderId",
                table: "FixProducts");

            migrationBuilder.DropColumn(
                name: "IsFixOrder",
                table: "FixProducts");

            migrationBuilder.RenameColumn(
                name: "FixOrderId",
                table: "FixProducts",
                newName: "WarrantyOrderId");
        }
    }
}
