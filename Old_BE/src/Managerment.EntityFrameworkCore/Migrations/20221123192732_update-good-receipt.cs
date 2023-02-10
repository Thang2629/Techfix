using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Managerment.Migrations
{
    public partial class updategoodreceipt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppGoodsReceipts_AppProducts_Id_prod",
                table: "AppGoodsReceipts");

            migrationBuilder.DropIndex(
                name: "IX_AppGoodsReceipts_Id_prod",
                table: "AppGoodsReceipts");

            migrationBuilder.DropColumn(
                name: "Id_prod",
                table: "AppGoodsReceipts");

            migrationBuilder.AddColumn<Guid>(
                name: "Id_good_receipt",
                table: "AppProducts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "AppGoodsReceiptProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Goods_receipt_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Import_price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppGoodsReceiptProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppGoodsReceiptProducts_AppGoodsReceipts_Goods_receipt_id",
                        column: x => x.Goods_receipt_id,
                        principalTable: "AppGoodsReceipts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppGoodsReceiptProducts_AppProducts_Product_id",
                        column: x => x.Product_id,
                        principalTable: "AppProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppGoodsReceiptProducts_Goods_receipt_id",
                table: "AppGoodsReceiptProducts",
                column: "Goods_receipt_id");

            migrationBuilder.CreateIndex(
                name: "IX_AppGoodsReceiptProducts_Product_id",
                table: "AppGoodsReceiptProducts",
                column: "Product_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppGoodsReceiptProducts");

            migrationBuilder.DropColumn(
                name: "Id_good_receipt",
                table: "AppProducts");

            migrationBuilder.AddColumn<Guid>(
                name: "Id_prod",
                table: "AppGoodsReceipts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppGoodsReceipts_Id_prod",
                table: "AppGoodsReceipts",
                column: "Id_prod");

            migrationBuilder.AddForeignKey(
                name: "FK_AppGoodsReceipts_AppProducts_Id_prod",
                table: "AppGoodsReceipts",
                column: "Id_prod",
                principalTable: "AppProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
