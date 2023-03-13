using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechFix.EntityModels.Migrations
{
    public partial class HandleHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InDebt",
                table: "Suppliers",
                newName: "AmountOwed");

            migrationBuilder.AddColumn<decimal>(
                name: "ReturnAmount",
                table: "Bills",
                type: "decimal(38,16)",
                precision: 38,
                scale: 16,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "Returned",
                table: "BillItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "MoneyOutHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InputProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SearchData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CashierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PaymentMethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(38,16)", precision: 38, scale: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoneyOutHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoneyOutHistories_InputProducts_InputProductId",
                        column: x => x.InputProductId,
                        principalTable: "InputProducts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MoneyOutHistories_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MoneyOutHistories_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MoneyOutHistories_Users_CashierId",
                        column: x => x.CashierId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MoneyOutHistories_CashierId",
                table: "MoneyOutHistories",
                column: "CashierId");

            migrationBuilder.CreateIndex(
                name: "IX_MoneyOutHistories_InputProductId",
                table: "MoneyOutHistories",
                column: "InputProductId");

            migrationBuilder.CreateIndex(
                name: "IX_MoneyOutHistories_PaymentMethodId",
                table: "MoneyOutHistories",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_MoneyOutHistories_SupplierId",
                table: "MoneyOutHistories",
                column: "SupplierId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoneyOutHistories");

            migrationBuilder.DropColumn(
                name: "ReturnAmount",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "Returned",
                table: "BillItems");

            migrationBuilder.RenameColumn(
                name: "AmountOwed",
                table: "Suppliers",
                newName: "InDebt");
        }
    }
}
