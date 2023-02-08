using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Managerment.Migrations
{
    public partial class updatedatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppGoodsReceipts_AppPaymentHistories_Id_payment",
                table: "AppGoodsReceipts");

            migrationBuilder.DropIndex(
                name: "IX_AppGoodsReceipts_Id_payment",
                table: "AppGoodsReceipts");

            migrationBuilder.DropColumn(
                name: "Id_order",
                table: "AppGoodsReceipts");

            migrationBuilder.DropColumn(
                name: "Id_payment",
                table: "AppGoodsReceipts");

            migrationBuilder.AddColumn<int>(
                name: "Payment_method",
                table: "AppGoodsReceipts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Payment_method",
                table: "AppGoodsReceipts");

            migrationBuilder.AddColumn<Guid>(
                name: "Id_order",
                table: "AppGoodsReceipts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id_payment",
                table: "AppGoodsReceipts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppGoodsReceipts_Id_payment",
                table: "AppGoodsReceipts",
                column: "Id_payment");

            migrationBuilder.AddForeignKey(
                name: "FK_AppGoodsReceipts_AppPaymentHistories_Id_payment",
                table: "AppGoodsReceipts",
                column: "Id_payment",
                principalTable: "AppPaymentHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
