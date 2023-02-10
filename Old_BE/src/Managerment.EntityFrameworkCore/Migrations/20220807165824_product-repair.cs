using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Managerment.Migrations
{
    public partial class productrepair : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppOrderRepairs_AppCustomers_CustomerId",
                table: "AppOrderRepairs");

            migrationBuilder.DropForeignKey(
                name: "FK_AppOrderRepairs_AppProductRepairs_ID_pr",
                table: "AppOrderRepairs");

            migrationBuilder.DropIndex(
                name: "IX_AppOrderRepairs_CustomerId",
                table: "AppOrderRepairs");

            migrationBuilder.DropIndex(
                name: "IX_AppOrderRepairs_ID_pr",
                table: "AppOrderRepairs");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "AppOrderRepairs");

            migrationBuilder.DropColumn(
                name: "ID_pr",
                table: "AppOrderRepairs");

            migrationBuilder.AddColumn<Guid>(
                name: "ID_order_repair",
                table: "AppProductRepairs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppProductRepairs_ID_detail",
                table: "AppProductRepairs",
                column: "ID_detail");

            migrationBuilder.CreateIndex(
                name: "IX_AppProductRepairs_ID_order_repair",
                table: "AppProductRepairs",
                column: "ID_order_repair");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProductRepairs_AppDetailProductRepairs_ID_detail",
                table: "AppProductRepairs",
                column: "ID_detail",
                principalTable: "AppDetailProductRepairs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProductRepairs_AppOrderRepairs_ID_order_repair",
                table: "AppProductRepairs",
                column: "ID_order_repair",
                principalTable: "AppOrderRepairs",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppProductRepairs_AppDetailProductRepairs_ID_detail",
                table: "AppProductRepairs");

            migrationBuilder.DropForeignKey(
                name: "FK_AppProductRepairs_AppOrderRepairs_ID_order_repair",
                table: "AppProductRepairs");

            migrationBuilder.DropIndex(
                name: "IX_AppProductRepairs_ID_detail",
                table: "AppProductRepairs");

            migrationBuilder.DropIndex(
                name: "IX_AppProductRepairs_ID_order_repair",
                table: "AppProductRepairs");

            migrationBuilder.DropColumn(
                name: "ID_order_repair",
                table: "AppProductRepairs");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "AppOrderRepairs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ID_pr",
                table: "AppOrderRepairs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppOrderRepairs_CustomerId",
                table: "AppOrderRepairs",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_AppOrderRepairs_ID_pr",
                table: "AppOrderRepairs",
                column: "ID_pr");

            migrationBuilder.AddForeignKey(
                name: "FK_AppOrderRepairs_AppCustomers_CustomerId",
                table: "AppOrderRepairs",
                column: "CustomerId",
                principalTable: "AppCustomers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppOrderRepairs_AppProductRepairs_ID_pr",
                table: "AppOrderRepairs",
                column: "ID_pr",
                principalTable: "AppProductRepairs",
                principalColumn: "Id");
        }
    }
}
