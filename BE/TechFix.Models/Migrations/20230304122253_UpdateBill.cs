using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechFix.EntityModels.Migrations
{
    public partial class UpdateBill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExportHistories");

            migrationBuilder.RenameColumn(
                name: "Fullname",
                table: "Customers",
                newName: "FullName");

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentDate",
                table: "Bills",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "MoneyInHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BillId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CashierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PaymentMethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(38,16)", precision: 38, scale: 16, nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SearchData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoneyInHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoneyInHistories_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MoneyInHistories_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MoneyInHistories_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MoneyInHistories_Users_CashierId",
                        column: x => x.CashierId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MoneyInHistories_BillId",
                table: "MoneyInHistories",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_MoneyInHistories_CashierId",
                table: "MoneyInHistories",
                column: "CashierId");

            migrationBuilder.CreateIndex(
                name: "IX_MoneyInHistories_CustomerId",
                table: "MoneyInHistories",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_MoneyInHistories_PaymentMethodId",
                table: "MoneyInHistories",
                column: "PaymentMethodId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoneyInHistories");

            migrationBuilder.DropColumn(
                name: "PaymentDate",
                table: "Bills");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Customers",
                newName: "Fullname");

            migrationBuilder.CreateTable(
                name: "ExportHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(38,16)", precision: 38, scale: 16, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SearchData = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExportHistories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExportHistories_UserId",
                table: "ExportHistories",
                column: "UserId");
        }
    }
}
