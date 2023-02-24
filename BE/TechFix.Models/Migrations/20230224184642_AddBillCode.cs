using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechFix.EntityModels.Migrations
{
    public partial class AddBillCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FixProducts_OrderItems_OrderItemId",
                table: "FixProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_FixProducts_Orders_OrderId",
                table: "FixProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductHistories_Stores_StoreId",
                table: "ProductHistories");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductStatus",
                table: "ProductHistories");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "FixProducts");

            migrationBuilder.RenameColumn(
                name: "OrderItemId",
                table: "FixProducts",
                newName: "BillItemId");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "FixProducts",
                newName: "BillId");

            migrationBuilder.RenameIndex(
                name: "IX_FixProducts_OrderItemId",
                table: "FixProducts",
                newName: "IX_FixProducts_BillItemId");

            migrationBuilder.RenameIndex(
                name: "IX_FixProducts_OrderId",
                table: "FixProducts",
                newName: "IX_FixProducts_BillId");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "ProductHistories",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "StoreId",
                table: "ProductHistories",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "ProductHistories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "OriginalPrice",
                table: "ProductHistories",
                type: "decimal(38,16)",
                precision: 38,
                scale: 16,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductConditionId",
                table: "ProductHistories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "ProductHistories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCreatedBill",
                table: "FixProducts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ProductSerial",
                table: "FixProducts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "WarrantyPeriod",
                table: "FixProducts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PaymentMethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SellerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vat = table.Column<decimal>(type: "decimal(38,16)", precision: 38, scale: 16, nullable: false),
                    TotalGoodsMoney = table.Column<decimal>(type: "decimal(38,16)", precision: 38, scale: 16, nullable: false),
                    TotalQuantity = table.Column<int>(type: "int", nullable: false),
                    DiscountPerItem = table.Column<decimal>(type: "decimal(38,16)", precision: 38, scale: 16, nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(38,16)", precision: 38, scale: 16, nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(38,16)", precision: 38, scale: 16, nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(38,16)", precision: 38, scale: 16, nullable: false),
                    AmountOwed = table.Column<decimal>(type: "decimal(38,16)", precision: 38, scale: 16, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SearchData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bills_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bills_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bills_Users_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BillItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BillId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductSerial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(38,16)", precision: 38, scale: 16, nullable: false),
                    WarrantyPeriod = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SearchData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillItems_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BillItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductHistories_ProductConditionId",
                table: "ProductHistories",
                column: "ProductConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductHistories_ProductId",
                table: "ProductHistories",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_BillItems_BillId",
                table: "BillItems",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_BillItems_ProductId",
                table: "BillItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_CustomerId",
                table: "Bills",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_PaymentMethodId",
                table: "Bills",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_SellerId",
                table: "Bills",
                column: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_FixProducts_BillItems_BillItemId",
                table: "FixProducts",
                column: "BillItemId",
                principalTable: "BillItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FixProducts_Bills_BillId",
                table: "FixProducts",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductHistories_ProductConditions_ProductConditionId",
                table: "ProductHistories",
                column: "ProductConditionId",
                principalTable: "ProductConditions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductHistories_Products_ProductId",
                table: "ProductHistories",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductHistories_Stores_StoreId",
                table: "ProductHistories",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FixProducts_BillItems_BillItemId",
                table: "FixProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_FixProducts_Bills_BillId",
                table: "FixProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductHistories_ProductConditions_ProductConditionId",
                table: "ProductHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductHistories_Products_ProductId",
                table: "ProductHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductHistories_Stores_StoreId",
                table: "ProductHistories");

            migrationBuilder.DropTable(
                name: "BillItems");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_ProductHistories_ProductConditionId",
                table: "ProductHistories");

            migrationBuilder.DropIndex(
                name: "IX_ProductHistories_ProductId",
                table: "ProductHistories");

            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "ProductHistories");

            migrationBuilder.DropColumn(
                name: "OriginalPrice",
                table: "ProductHistories");

            migrationBuilder.DropColumn(
                name: "ProductConditionId",
                table: "ProductHistories");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductHistories");

            migrationBuilder.DropColumn(
                name: "IsCreatedBill",
                table: "FixProducts");

            migrationBuilder.DropColumn(
                name: "ProductSerial",
                table: "FixProducts");

            migrationBuilder.DropColumn(
                name: "WarrantyPeriod",
                table: "FixProducts");

            migrationBuilder.RenameColumn(
                name: "BillItemId",
                table: "FixProducts",
                newName: "OrderItemId");

            migrationBuilder.RenameColumn(
                name: "BillId",
                table: "FixProducts",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_FixProducts_BillItemId",
                table: "FixProducts",
                newName: "IX_FixProducts_OrderItemId");

            migrationBuilder.RenameIndex(
                name: "IX_FixProducts_BillId",
                table: "FixProducts",
                newName: "IX_FixProducts_OrderId");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "ProductHistories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "StoreId",
                table: "ProductHistories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductStatus",
                table: "ProductHistories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "FixProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PaymentMethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SellerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AmountOwed = table.Column<decimal>(type: "decimal(38,16)", precision: 38, scale: 16, nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(38,16)", precision: 38, scale: 16, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Discount = table.Column<decimal>(type: "decimal(38,16)", precision: 38, scale: 16, nullable: false),
                    DiscountPerItem = table.Column<decimal>(type: "decimal(38,16)", precision: 38, scale: 16, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SearchData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(38,16)", precision: 38, scale: 16, nullable: false),
                    TotalGoodsMoney = table.Column<decimal>(type: "decimal(38,16)", precision: 38, scale: 16, nullable: false),
                    TotalQuantity = table.Column<int>(type: "int", nullable: false),
                    Vat = table.Column<decimal>(type: "decimal(38,16)", precision: 38, scale: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Users_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(38,16)", precision: 38, scale: 16, nullable: false),
                    ProductSerial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SearchData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarrantyPeriod = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "CreatedUser", "IsDeleted", "ModifiedDate", "ModifiedUser", "Name", "Path", "SearchData" },
                values: new object[,]
                {
                    { new Guid("04e05d4d-a304-4106-91a4-dededd677cd9"), null, null, false, null, null, "ASUS", null, null },
                    { new Guid("4865a6c6-a56d-4f8e-9961-b062af262426"), null, null, false, null, null, "ACER", null, null },
                    { new Guid("586a934a-a843-4730-aa38-7ad8486959ea"), null, null, false, null, null, "SONY", null, null },
                    { new Guid("743f02c1-eb00-4f9f-aaa3-b10385c8ae58"), null, null, false, null, null, "Màn hình AOC", null, null },
                    { new Guid("74d78077-f391-4762-95eb-b29699db7af9"), null, null, false, null, null, "CPU - Vi xử lý", null, null },
                    { new Guid("7a62c105-e475-4113-a176-b0e283581e12"), null, null, false, null, null, "Macbook", null, null },
                    { new Guid("7aff1aba-1ca0-42ff-8627-0aa7c2d039af"), null, null, false, null, null, "VGA Laptop", null, null },
                    { new Guid("9c8fc9f7-25a6-44b2-82fc-e303184449ec"), null, null, false, null, null, "LCD - Màn hình Laptop", null, null },
                    { new Guid("ce403e65-ef53-4a2e-8515-06de75436b9e"), null, null, false, null, null, "Phần mềm diệt virus - win", null, null },
                    { new Guid("e9cf44cb-0410-4470-8f5f-62d0265a4b91"), null, null, false, null, null, "TOSHIBA", null, null },
                    { new Guid("f7b5685e-acca-435f-9681-2175c0054d77"), null, null, false, null, null, "Keo tản nhiệt", null, null }
                });

            migrationBuilder.InsertData(
                table: "Manufacturers",
                columns: new[] { "Id", "CreatedDate", "CreatedUser", "IsDeleted", "ModifiedDate", "ModifiedUser", "Name", "SearchData" },
                values: new object[,]
                {
                    { new Guid("151ecbb0-dbe5-4a80-ab56-e443e145b5e0"), null, null, false, null, null, "Seagate", null },
                    { new Guid("179f6f9c-33d7-4f43-8b36-b9668cba2cca"), null, null, false, null, null, "Genius", null },
                    { new Guid("18eacb75-8d7f-4833-87d9-97ba3971a46e"), null, null, false, null, null, "Kingston", null },
                    { new Guid("242bf112-9304-4e64-a08a-f455cd132043"), null, null, false, null, null, "Unitek", null },
                    { new Guid("36aa5768-87aa-4b7a-9fc7-c8066d136c55"), null, null, false, null, null, "Western", null },
                    { new Guid("3af759e8-b2e0-4b02-96b6-76c5609615f6"), null, null, false, null, null, "Colorful", null },
                    { new Guid("45341e2d-7e0f-45e7-ad84-f361cd69dc41"), null, null, false, null, null, "Samsung", null },
                    { new Guid("49747ba2-8a30-4bb5-a2a1-49d6580e672a"), null, null, false, null, null, "Addlink", null },
                    { new Guid("4ae0a384-443b-4901-85b1-cfa1976cf035"), null, null, false, null, null, "Caddy Bay", null },
                    { new Guid("5834857c-c47f-4995-abd0-9919515d8b35"), null, null, false, null, null, "Energizer", null },
                    { new Guid("70f6af9a-9725-47c5-b9e3-66aecd85e47b"), null, null, false, null, null, "Newmen", null },
                    { new Guid("8657a192-250f-46f0-addc-0806460b4ea2"), null, null, false, null, null, "Pisen", null },
                    { new Guid("b9ebb845-7dd3-4522-b2ba-5fc45f0459a0"), null, null, false, null, null, "Hoco", null },
                    { new Guid("d6eb32c3-1339-4b15-b6bf-db38a8bb04ee"), null, null, false, null, null, "Kingmax", null },
                    { new Guid("e32e08d8-32d0-49a4-af6d-e9a8b02fa7ae"), null, null, false, null, null, "Pioneer", null },
                    { new Guid("f30a01bd-2a0c-4340-bfdb-6a497aecb648"), null, null, false, null, null, "HP", null }
                });

            migrationBuilder.InsertData(
                table: "ProductConditions",
                columns: new[] { "Id", "CreatedDate", "CreatedUser", "IsDeleted", "ModifiedDate", "ModifiedUser", "Name", "SearchData" },
                values: new object[,]
                {
                    { new Guid("9420681a-6d03-45bb-ab59-bd1af9e3054a"), null, null, false, null, null, "Mới 100%", null },
                    { new Guid("ba55e427-1ecd-4d44-a27a-a5be26b89fdd"), null, null, false, null, null, "Cũ đã qua sửa chữa", null },
                    { new Guid("cb98afde-54ed-416c-a73c-18eef6f0983b"), null, null, false, null, null, "Mới 99%", null },
                    { new Guid("de2d4f17-b82c-47c0-9a88-a7bae70c8579"), null, null, false, null, null, "Outlet", null },
                    { new Guid("f97327e2-d6aa-439f-991a-875460b45284"), null, null, false, null, null, "New Outlet", null },
                    { new Guid("fcdf46b9-c1d6-4664-8ea4-7a1b25ab1875"), null, null, false, null, null, "Mới 98%", null }
                });

            migrationBuilder.InsertData(
                table: "ProductUnits",
                columns: new[] { "Id", "CreatedDate", "CreatedUser", "IsDeleted", "ModifiedDate", "ModifiedUser", "Name", "SearchData" },
                values: new object[,]
                {
                    { new Guid("0a30fca8-bc21-407e-9264-8ea2e5f41575"), null, null, false, null, null, "Con", null },
                    { new Guid("7035167e-2ec1-46cb-a346-9f6a20eac535"), null, null, false, null, null, "Cái", null },
                    { new Guid("92c5a297-4635-46ad-8c9e-b5f28fd44e75"), null, null, false, null, null, "Cm", null },
                    { new Guid("eb4a1712-88ac-4694-8941-5e7305bbc4b1"), null, null, false, null, null, "Bộ", null }
                });

            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "Id", "Address", "CreatedDate", "CreatedUser", "IsDeleted", "ModifiedDate", "ModifiedUser", "Name", "Phone", "SearchData" },
                values: new object[] { new Guid("ea9f5a2d-4d11-45bc-8ec4-ea7b7cd9334b"), "", null, null, false, null, null, "TechFix", "0123456778", null });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "Address", "Code", "CreatedDate", "CreatedUser", "Email", "ImagePath", "InDebt", "IsDeleted", "ModifiedDate", "ModifiedUser", "Name", "Note", "Phone", "PhoneNumber", "SearchData" },
                values: new object[,]
                {
                    { new Guid("02f2f867-eab4-4711-8190-ed5e5f0601cf"), "", null, null, null, "", null, 0m, false, null, null, "Duy Tân", "", "", null, null },
                    { new Guid("03acff81-9ddd-4cb7-8d7b-de0fc9e51c9e"), "", null, null, null, "", null, 0m, false, null, null, "Phát Đạt LTK", "", "", null, null },
                    { new Guid("196d3520-8ffd-49e5-b0f1-f88d4b5f1b59"), "", null, null, null, "", null, 0m, false, null, null, "NWH", "", "", null, null },
                    { new Guid("33e3338c-7921-480a-89fe-c06eb62f0fd8"), "", null, null, null, "", null, 0m, false, null, null, "CTY PATECH", "", "", null, null }
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "Address", "Code", "CreatedDate", "CreatedUser", "Email", "ImagePath", "InDebt", "IsDeleted", "ModifiedDate", "ModifiedUser", "Name", "Note", "Phone", "PhoneNumber", "SearchData" },
                values: new object[,]
                {
                    { new Guid("36dcab9b-b6ac-45e3-bd70-0d4dd5cc0973"), "", null, null, null, "", null, 0m, false, null, null, "Văn Hải", "", "", null, null },
                    { new Guid("51e57f1b-ed47-4fe9-9cdb-3866b0ca4fbb"), "", null, null, null, "", null, 0m, false, null, null, "Tuấn Hiền", "", "", null, null },
                    { new Guid("673aad6d-4b27-4760-95b5-7b1c6cf553bd"), "", null, null, null, "", null, 0m, false, null, null, "Chuẩn LTK", "", "", null, null },
                    { new Guid("a9324c9b-545c-4de3-b4c6-990721e2a130"), "", null, null, null, "", null, 0m, false, null, null, "Huy Phát Kingmater", "", "", null, null },
                    { new Guid("aabacf68-91af-4ea3-8402-0e7b77dc2d87"), "", null, null, null, "", null, 0m, false, null, null, "Chỉnh LK", "", "", null, null },
                    { new Guid("e358f54b-bbba-473b-93f4-8308331a3c7d"), "", null, null, null, "", null, 0m, false, null, null, "Tấn Phát LTK", "", "", null, null },
                    { new Guid("f6c5bbd4-701c-43f9-a4fb-df6c887ad0b9"), "", null, null, null, "", null, 0m, false, null, null, "LGT", "", "", null, null },
                    { new Guid("fe0ac497-c6b3-41a4-9242-acb95d075a17"), "", null, null, null, "", null, 0m, false, null, null, "Hải Việt", "", "", null, null }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AllowNegativeSell", "CategoryId", "Code", "CreatedDate", "CreatedUser", "Description", "Discontinue", "FakePrice", "ImagePath", "IsDeleted", "IsInventoryTracking", "ManufacturerId", "MaximumNorm", "MinimumNorm", "ModifiedDate", "ModifiedUser", "Name", "OriginalPrice", "ProductConditionId", "ProductUnitId", "Quantity", "SearchData", "SupplierId", "Warranty", "WebPrice" },
                values: new object[,]
                {
                    { new Guid("29f334ee-2f62-483c-b882-804b5a356dc6"), true, null, "SP0000011", null, null, "", false, 650000m, null, false, false, new Guid("e32e08d8-32d0-49a4-af6d-e9a8b02fa7ae"), 0, 0, null, null, "Sạc dự phòng Pisen Color Power Pro 10000mAh đỏ-đen (Dual USB 1A/2.4A Smart)", 550000m, new Guid("fcdf46b9-c1d6-4664-8ea4-7a1b25ab1875"), null, 0, null, new Guid("196d3520-8ffd-49e5-b0f1-f88d4b5f1b59"), "12TH", 550000m },
                    { new Guid("342798ba-e601-498b-ac36-963df8173c3e"), true, null, "SP0000010", null, null, "", false, 70000m, null, false, false, new Guid("e32e08d8-32d0-49a4-af6d-e9a8b02fa7ae"), 10, 5, null, null, "Áo thun hai lỗ", 50000m, new Guid("9420681a-6d03-45bb-ab59-bd1af9e3054a"), null, 5, null, new Guid("196d3520-8ffd-49e5-b0f1-f88d4b5f1b59"), "Bảo hành 6Th", 50000m },
                    { new Guid("5624153b-9302-4246-b42f-29bd6a06ec46"), false, null, "SP0000041", null, null, "", false, 550000m, null, true, false, new Guid("e32e08d8-32d0-49a4-af6d-e9a8b02fa7ae"), 0, 0, null, null, "SSD Pioneer APS-SL3N 120GB 2.5in ( Read 520MB/s - Write 400MB/s )", 440000m, new Guid("fcdf46b9-c1d6-4664-8ea4-7a1b25ab1875"), null, 0, null, new Guid("196d3520-8ffd-49e5-b0f1-f88d4b5f1b59"), "12TH", 440000m },
                    { new Guid("b328ddfd-516d-49a3-9fd4-8e0d6354d069"), false, null, "SP0000003", null, null, "", false, 130000m, null, false, true, new Guid("3af759e8-b2e0-4b02-96b6-76c5609615f6"), 0, 0, null, null, "Cáp Pisen USB Type-C 3A 1m", 90000m, new Guid("cb98afde-54ed-416c-a73c-18eef6f0983b"), null, 8, null, new Guid("196d3520-8ffd-49e5-b0f1-f88d4b5f1b59"), "06TH", 90000m },
                    { new Guid("e4f6c2a6-1510-49c6-834d-22b8f518a41e"), true, null, "SP0000022", null, null, "", false, 270000m, null, false, false, new Guid("70f6af9a-9725-47c5-b9e3-66aecd85e47b"), 5, 3, null, null, "Chuột Newmen F300 không dây", 215000m, new Guid("fcdf46b9-c1d6-4664-8ea4-7a1b25ab1875"), null, 3, null, new Guid("196d3520-8ffd-49e5-b0f1-f88d4b5f1b59"), "12TH", 215000m },
                    { new Guid("e64c481d-a4fa-45ca-902c-f18fb6f37681"), true, null, "SP0000032", null, null, "", false, 100000m, null, false, true, new Guid("e32e08d8-32d0-49a4-af6d-e9a8b02fa7ae"), 5, 1, null, null, "Caddybay mỏng 9.5mm Laptop", 35000m, new Guid("fcdf46b9-c1d6-4664-8ea4-7a1b25ab1875"), null, 99, null, new Guid("196d3520-8ffd-49e5-b0f1-f88d4b5f1b59"), "3TH", 35000m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentMethodId",
                table: "Orders",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SellerId",
                table: "Orders",
                column: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_FixProducts_OrderItems_OrderItemId",
                table: "FixProducts",
                column: "OrderItemId",
                principalTable: "OrderItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FixProducts_Orders_OrderId",
                table: "FixProducts",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductHistories_Stores_StoreId",
                table: "ProductHistories",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
