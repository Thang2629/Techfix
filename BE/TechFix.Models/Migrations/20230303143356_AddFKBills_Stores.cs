using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechFix.EntityModels.Migrations
{
    public partial class AddFKBills_Stores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StoreId",
                table: "Bills",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bills_StoreId",
                table: "Bills",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Stores_StoreId",
                table: "Bills",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Stores_StoreId",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Bills_StoreId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "Bills");
        }
    }
}
