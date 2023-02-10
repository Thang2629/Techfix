using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Managerment.Migrations
{
    public partial class orderwarranty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppOrderWarranties_AppProductRepairs_ID_pr",
                table: "AppOrderWarranties");

            migrationBuilder.DropIndex(
                name: "IX_AppOrderWarranties_ID_pr",
                table: "AppOrderWarranties");

            migrationBuilder.DropColumn(
                name: "ID_pr",
                table: "AppOrderWarranties");

            migrationBuilder.RenameColumn(
                name: "ID_type",
                table: "AppProductWarranties",
                newName: "ID_order_warranty");

            migrationBuilder.RenameColumn(
                name: "QR_code",
                table: "AppOrderWarranties",
                newName: "OW_code");

            migrationBuilder.AddColumn<int>(
                name: "Product_warranty_type",
                table: "AppProductWarranties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Total_count",
                table: "AppProductWarranties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Warranty_times",
                table: "AppProductWarranties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "C_gender",
                table: "AppCustomers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "C_birthday",
                table: "AppCustomers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "C_address",
                table: "AppCustomers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.CreateIndex(
                name: "IX_AppProductWarranties_ID_detail",
                table: "AppProductWarranties",
                column: "ID_detail");

            migrationBuilder.CreateIndex(
                name: "IX_AppProductWarranties_ID_order_warranty",
                table: "AppProductWarranties",
                column: "ID_order_warranty");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProductWarranties_AppDetailProductRepairs_ID_detail",
                table: "AppProductWarranties",
                column: "ID_detail",
                principalTable: "AppDetailProductRepairs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProductWarranties_AppOrderWarranties_ID_order_warranty",
                table: "AppProductWarranties",
                column: "ID_order_warranty",
                principalTable: "AppOrderWarranties",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppProductWarranties_AppDetailProductRepairs_ID_detail",
                table: "AppProductWarranties");

            migrationBuilder.DropForeignKey(
                name: "FK_AppProductWarranties_AppOrderWarranties_ID_order_warranty",
                table: "AppProductWarranties");

            migrationBuilder.DropIndex(
                name: "IX_AppProductWarranties_ID_detail",
                table: "AppProductWarranties");

            migrationBuilder.DropIndex(
                name: "IX_AppProductWarranties_ID_order_warranty",
                table: "AppProductWarranties");

            migrationBuilder.DropColumn(
                name: "Product_warranty_type",
                table: "AppProductWarranties");

            migrationBuilder.DropColumn(
                name: "Total_count",
                table: "AppProductWarranties");

            migrationBuilder.DropColumn(
                name: "Warranty_times",
                table: "AppProductWarranties");

            migrationBuilder.RenameColumn(
                name: "ID_order_warranty",
                table: "AppProductWarranties",
                newName: "ID_type");

            migrationBuilder.RenameColumn(
                name: "OW_code",
                table: "AppOrderWarranties",
                newName: "QR_code");

            migrationBuilder.AddColumn<Guid>(
                name: "ID_pr",
                table: "AppOrderWarranties",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "C_gender",
                table: "AppCustomers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "C_birthday",
                table: "AppCustomers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "C_address",
                table: "AppCustomers",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppOrderWarranties_ID_pr",
                table: "AppOrderWarranties",
                column: "ID_pr");

            migrationBuilder.AddForeignKey(
                name: "FK_AppOrderWarranties_AppProductRepairs_ID_pr",
                table: "AppOrderWarranties",
                column: "ID_pr",
                principalTable: "AppProductRepairs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
