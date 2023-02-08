using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Managerment.Migrations
{
    public partial class updatedb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppOrders_OrderRepairs_Id_repair",
                table: "AppOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_AppProductRepairs_AppTypeRepairs_ID_type",
                table: "AppProductRepairs");

            migrationBuilder.DropForeignKey(
                name: "FK_AppProductWarranties_AppTypeRepairs_ID_type",
                table: "AppProductWarranties");

            migrationBuilder.DropTable(
                name: "AppCashReceiptTypes");

            migrationBuilder.DropTable(
                name: "AppTypeRepairs");

            migrationBuilder.DropIndex(
                name: "IX_AppProductWarranties_ID_type",
                table: "AppProductWarranties");

            migrationBuilder.DropIndex(
                name: "IX_AppProductRepairs_ID_type",
                table: "AppProductRepairs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderRepairs",
                table: "OrderRepairs");

            migrationBuilder.DropColumn(
                name: "ID_type",
                table: "AppProductRepairs");

            migrationBuilder.DropColumn(
                name: "ID_reciept_type",
                table: "AppCashReceiptOther");

            migrationBuilder.RenameTable(
                name: "OrderRepairs",
                newName: "AppOrderRepairs");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "AppOrderRepairs",
                newName: "Notes");

            migrationBuilder.AddColumn<string>(
                name: "U_code",
                table: "AppUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Product_repair_type",
                table: "AppProductRepairs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Cash_receipt_type",
                table: "AppCashReceiptOther",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "OR_code",
                table: "AppOrderRepairs",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "AppOrderRepairs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppOrderRepairs",
                table: "AppOrderRepairs",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppPaymentHistories_ID_payment",
                table: "AppPaymentHistories",
                column: "ID_payment");

            migrationBuilder.CreateIndex(
                name: "IX_AppOrderRepairs_CustomerId",
                table: "AppOrderRepairs",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_AppOrderRepairs_ID_cus",
                table: "AppOrderRepairs",
                column: "ID_cus");

            migrationBuilder.CreateIndex(
                name: "IX_AppOrderRepairs_ID_pr",
                table: "AppOrderRepairs",
                column: "ID_pr");

            migrationBuilder.CreateIndex(
                name: "IX_AppOrderRepairs_ID_user",
                table: "AppOrderRepairs",
                column: "ID_user");

            migrationBuilder.AddForeignKey(
                name: "FK_AppOrderRepairs_AppCustomers_CustomerId",
                table: "AppOrderRepairs",
                column: "CustomerId",
                principalTable: "AppCustomers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppOrderRepairs_AppCustomers_ID_cus",
                table: "AppOrderRepairs",
                column: "ID_cus",
                principalTable: "AppCustomers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppOrderRepairs_AppProductRepairs_ID_pr",
                table: "AppOrderRepairs",
                column: "ID_pr",
                principalTable: "AppProductRepairs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppOrderRepairs_AppUsers_ID_user",
                table: "AppOrderRepairs",
                column: "ID_user",
                principalTable: "AppUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppOrders_AppOrderRepairs_Id_repair",
                table: "AppOrders",
                column: "Id_repair",
                principalTable: "AppOrderRepairs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppPaymentHistories_PaymentMethods_ID_payment",
                table: "AppPaymentHistories",
                column: "ID_payment",
                principalTable: "PaymentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppOrderRepairs_AppCustomers_CustomerId",
                table: "AppOrderRepairs");

            migrationBuilder.DropForeignKey(
                name: "FK_AppOrderRepairs_AppCustomers_ID_cus",
                table: "AppOrderRepairs");

            migrationBuilder.DropForeignKey(
                name: "FK_AppOrderRepairs_AppProductRepairs_ID_pr",
                table: "AppOrderRepairs");

            migrationBuilder.DropForeignKey(
                name: "FK_AppOrderRepairs_AppUsers_ID_user",
                table: "AppOrderRepairs");

            migrationBuilder.DropForeignKey(
                name: "FK_AppOrders_AppOrderRepairs_Id_repair",
                table: "AppOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_AppPaymentHistories_PaymentMethods_ID_payment",
                table: "AppPaymentHistories");

            migrationBuilder.DropIndex(
                name: "IX_AppPaymentHistories_ID_payment",
                table: "AppPaymentHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppOrderRepairs",
                table: "AppOrderRepairs");

            migrationBuilder.DropIndex(
                name: "IX_AppOrderRepairs_CustomerId",
                table: "AppOrderRepairs");

            migrationBuilder.DropIndex(
                name: "IX_AppOrderRepairs_ID_cus",
                table: "AppOrderRepairs");

            migrationBuilder.DropIndex(
                name: "IX_AppOrderRepairs_ID_pr",
                table: "AppOrderRepairs");

            migrationBuilder.DropIndex(
                name: "IX_AppOrderRepairs_ID_user",
                table: "AppOrderRepairs");

            migrationBuilder.DropColumn(
                name: "U_code",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "Product_repair_type",
                table: "AppProductRepairs");

            migrationBuilder.DropColumn(
                name: "Cash_receipt_type",
                table: "AppCashReceiptOther");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "AppOrderRepairs");

            migrationBuilder.RenameTable(
                name: "AppOrderRepairs",
                newName: "OrderRepairs");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "OrderRepairs",
                newName: "Text");

            migrationBuilder.AddColumn<Guid>(
                name: "ID_type",
                table: "AppProductRepairs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ID_reciept_type",
                table: "AppCashReceiptOther",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "OR_code",
                table: "OrderRepairs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderRepairs",
                table: "OrderRepairs",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AppCashReceiptTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CRT_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCashReceiptTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppTypeRepairs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppTypeRepairs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppProductWarranties_ID_type",
                table: "AppProductWarranties",
                column: "ID_type");

            migrationBuilder.CreateIndex(
                name: "IX_AppProductRepairs_ID_type",
                table: "AppProductRepairs",
                column: "ID_type");

            migrationBuilder.AddForeignKey(
                name: "FK_AppOrders_OrderRepairs_Id_repair",
                table: "AppOrders",
                column: "Id_repair",
                principalTable: "OrderRepairs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppProductRepairs_AppTypeRepairs_ID_type",
                table: "AppProductRepairs",
                column: "ID_type",
                principalTable: "AppTypeRepairs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppProductWarranties_AppTypeRepairs_ID_type",
                table: "AppProductWarranties",
                column: "ID_type",
                principalTable: "AppTypeRepairs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
