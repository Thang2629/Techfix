using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechFix.EntityModels.Migrations
{
    public partial class UpdateTableInputProduct_InputProductItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StoreId",
                table: "InputProducts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "OriginalPrice",
                table: "InputProductItems",
                type: "decimal(38,16)",
                precision: 38,
                scale: 16,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_InputProducts_StoreId",
                table: "InputProducts",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_InputProducts_Stores_StoreId",
                table: "InputProducts",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InputProducts_Stores_StoreId",
                table: "InputProducts");

            migrationBuilder.DropIndex(
                name: "IX_InputProducts_StoreId",
                table: "InputProducts");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "InputProducts");

            migrationBuilder.AlterColumn<int>(
                name: "OriginalPrice",
                table: "InputProductItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(38,16)",
                oldPrecision: 38,
                oldScale: 16);
        }
    }
}
