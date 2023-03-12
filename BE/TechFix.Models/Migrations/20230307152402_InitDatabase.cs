using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechFix.EntityModels.Migrations
{
    public partial class InitDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SearchData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Team = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AmountOwed = table.Column<decimal>(type: "decimal(38,16)", precision: 38, scale: 16, nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SearchData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Manufacturers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SearchData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SearchData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductConditions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SearchData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductConditions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SearchData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SearchData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InDebt = table.Column<decimal>(type: "decimal(38,16)", precision: 38, scale: 16, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SearchData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Templates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SearchData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Templates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StaffCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BonusPercent = table.Column<decimal>(type: "decimal(38,16)", precision: 38, scale: 16, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordSalt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SearchData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Token = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpiredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SearchData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VlinkSequence",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SequenceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VlinkSequence", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ManufacturerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductConditionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    MinimumNorm = table.Column<int>(type: "int", nullable: false),
                    MaximumNorm = table.Column<int>(type: "int", nullable: false),
                    OriginalPrice = table.Column<decimal>(type: "decimal(38,16)", precision: 38, scale: 16, nullable: false),
                    WebPrice = table.Column<decimal>(type: "decimal(38,16)", precision: 38, scale: 16, nullable: false),
                    FakePrice = table.Column<decimal>(type: "decimal(38,16)", precision: 38, scale: 16, nullable: false),
                    Warranty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsInventoryTracking = table.Column<bool>(type: "bit", nullable: false),
                    AllowNegativeSell = table.Column<bool>(type: "bit", nullable: false),
                    Discontinue = table.Column<bool>(type: "bit", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SearchData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_Manufacturers_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_ProductConditions_ProductConditionId",
                        column: x => x.ProductConditionId,
                        principalTable: "ProductConditions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_ProductUnits_ProductUnitId",
                        column: x => x.ProductUnitId,
                        principalTable: "ProductUnits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PaymentMethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SellerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    SaleDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsReturn = table.Column<bool>(type: "bit", nullable: false),
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
                        name: "FK_Bills_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bills_Users_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FixOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CashierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceivedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsFixOrder = table.Column<bool>(type: "bit", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SearchData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FixOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FixOrders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FixOrders_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FixOrders_Users_CashierId",
                        column: x => x.CashierId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Funds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CashierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(38,16)", precision: 38, scale: 16, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsAdd = table.Column<bool>(type: "bit", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SearchData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Funds_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Funds_Users_CashierId",
                        column: x => x.CashierId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ImportHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(38,16)", precision: 38, scale: 16, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SearchData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImportHistories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IncomeTickets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    CashierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(38,16)", precision: 38, scale: 16, nullable: false),
                    Debt = table.Column<decimal>(type: "decimal(38,16)", precision: 38, scale: 16, nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaymentTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SearchData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeTickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IncomeTickets_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IncomeTickets_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IncomeTickets_Users_CashierId",
                        column: x => x.CashierId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InputProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PaymentMethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InputDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalGoodsMoney = table.Column<decimal>(type: "decimal(38,16)", precision: 38, scale: 16, nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(38,16)", precision: 38, scale: 16, nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(38,16)", precision: 38, scale: 16, nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(38,16)", precision: 38, scale: 16, nullable: false),
                    AmountOwed = table.Column<decimal>(type: "decimal(38,16)", precision: 38, scale: 16, nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SearchData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InputProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InputProducts_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InputProducts_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InputProducts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductConditionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Warranty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OriginalPrice = table.Column<decimal>(type: "decimal(38,16)", precision: 38, scale: 16, nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SearchData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductHistories_ProductConditions_ProductConditionId",
                        column: x => x.ProductConditionId,
                        principalTable: "ProductConditions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductHistories_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductHistories_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
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

            migrationBuilder.CreateTable(
                name: "InputProductItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InputProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    OriginalPrice = table.Column<int>(type: "int", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SearchData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InputProductItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InputProductItems_InputProducts_InputProductId",
                        column: x => x.InputProductId,
                        principalTable: "InputProducts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InputProductItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FixProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BillId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ErrorDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Condition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Process = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfTimes = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsFixOrder = table.Column<bool>(type: "bit", nullable: false),
                    EstimatedReturnDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinishDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Cpu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hdd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ram = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Wifi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adapter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Keyboard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Psu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lcd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Other = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FixStaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FixOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReceiptDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalMoney = table.Column<decimal>(type: "decimal(38,16)", precision: 38, scale: 16, nullable: false),
                    ProductSerial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarrantyPeriod = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsCreatedBill = table.Column<bool>(type: "bit", nullable: false),
                    BillItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SearchData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FixProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FixProducts_BillItems_BillItemId",
                        column: x => x.BillItemId,
                        principalTable: "BillItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FixProducts_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FixProducts_FixOrders_FixOrderId",
                        column: x => x.FixOrderId,
                        principalTable: "FixOrders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FixProducts_Users_FixStaffId",
                        column: x => x.FixStaffId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Bills_StoreId",
                table: "Bills",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_FixOrders_CashierId",
                table: "FixOrders",
                column: "CashierId");

            migrationBuilder.CreateIndex(
                name: "IX_FixOrders_CustomerId",
                table: "FixOrders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_FixOrders_StoreId",
                table: "FixOrders",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_FixProducts_BillId",
                table: "FixProducts",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_FixProducts_BillItemId",
                table: "FixProducts",
                column: "BillItemId");

            migrationBuilder.CreateIndex(
                name: "IX_FixProducts_FixOrderId",
                table: "FixProducts",
                column: "FixOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_FixProducts_FixStaffId",
                table: "FixProducts",
                column: "FixStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Funds_CashierId",
                table: "Funds",
                column: "CashierId");

            migrationBuilder.CreateIndex(
                name: "IX_Funds_StoreId",
                table: "Funds",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportHistories_UserId",
                table: "ImportHistories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeTickets_CashierId",
                table: "IncomeTickets",
                column: "CashierId");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeTickets_StoreId",
                table: "IncomeTickets",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeTickets_SupplierId",
                table: "IncomeTickets",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_InputProductItems_InputProductId",
                table: "InputProductItems",
                column: "InputProductId");

            migrationBuilder.CreateIndex(
                name: "IX_InputProductItems_ProductId",
                table: "InputProductItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_InputProducts_PaymentMethodId",
                table: "InputProducts",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_InputProducts_SupplierId",
                table: "InputProducts",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_InputProducts_UserId",
                table: "InputProducts",
                column: "UserId");

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

            migrationBuilder.CreateIndex(
                name: "IX_ProductHistories_ProductConditionId",
                table: "ProductHistories",
                column: "ProductConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductHistories_ProductId",
                table: "ProductHistories",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductHistories_StoreId",
                table: "ProductHistories",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ManufacturerId",
                table: "Products",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductConditionId",
                table: "Products",
                column: "ProductConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductUnitId",
                table: "Products",
                column: "ProductUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SupplierId",
                table: "Products",
                column: "SupplierId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FixProducts");

            migrationBuilder.DropTable(
                name: "Funds");

            migrationBuilder.DropTable(
                name: "ImportHistories");

            migrationBuilder.DropTable(
                name: "IncomeTickets");

            migrationBuilder.DropTable(
                name: "InputProductItems");

            migrationBuilder.DropTable(
                name: "MoneyInHistories");

            migrationBuilder.DropTable(
                name: "ProductHistories");

            migrationBuilder.DropTable(
                name: "Templates");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "VlinkSequence");

            migrationBuilder.DropTable(
                name: "BillItems");

            migrationBuilder.DropTable(
                name: "FixOrders");

            migrationBuilder.DropTable(
                name: "InputProducts");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Manufacturers");

            migrationBuilder.DropTable(
                name: "ProductConditions");

            migrationBuilder.DropTable(
                name: "ProductUnits");

            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}
