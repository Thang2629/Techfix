using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Managerment.Migrations
{
    public partial class updategoodsreceipt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Id_supplier",
                table: "AppGoodsReceipts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppGoodsReceipts_Id_supplier",
                table: "AppGoodsReceipts",
                column: "Id_supplier");

            migrationBuilder.AddForeignKey(
                name: "FK_AppGoodsReceipts_AppSuppliers_Id_supplier",
                table: "AppGoodsReceipts",
                column: "Id_supplier",
                principalTable: "AppSuppliers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppGoodsReceipts_AppSuppliers_Id_supplier",
                table: "AppGoodsReceipts");

            migrationBuilder.DropIndex(
                name: "IX_AppGoodsReceipts_Id_supplier",
                table: "AppGoodsReceipts");

            migrationBuilder.DropColumn(
                name: "Id_supplier",
                table: "AppGoodsReceipts");
        }
    }
}
