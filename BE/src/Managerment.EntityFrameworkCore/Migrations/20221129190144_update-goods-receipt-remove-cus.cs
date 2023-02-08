using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Managerment.Migrations
{
    public partial class updategoodsreceiptremovecus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppGoodsReceipts_AppCustomers_Id_cus",
                table: "AppGoodsReceipts");

            migrationBuilder.DropIndex(
                name: "IX_AppGoodsReceipts_Id_cus",
                table: "AppGoodsReceipts");

            migrationBuilder.DropColumn(
                name: "Id_cus",
                table: "AppGoodsReceipts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Id_cus",
                table: "AppGoodsReceipts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppGoodsReceipts_Id_cus",
                table: "AppGoodsReceipts",
                column: "Id_cus");

            migrationBuilder.AddForeignKey(
                name: "FK_AppGoodsReceipts_AppCustomers_Id_cus",
                table: "AppGoodsReceipts",
                column: "Id_cus",
                principalTable: "AppCustomers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
