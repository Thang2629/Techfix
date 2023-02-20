using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechFix.EntityModels.Migrations
{
    public partial class InitDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "FundCodeIncrement",
                startValue: 1000001L);

            migrationBuilder.CreateSequence<int>(
                name: "ProductCodeIncrement",
                startValue: 1000001L);

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
                    Fullname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "Funds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(38,16)", precision: 38, scale: 16, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cashier = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsAdd = table.Column<bool>(type: "bit", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_Funds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Funds_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ProductStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Warranty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                        name: "FK_ProductHistories_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "'SP' + CAST( NEXT VALUE FOR ProductCodeIncrement AS nvarchar(50) ) "),
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
                name: "ExportHistories",
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
                    table.PrimaryKey("PK_ExportHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExportHistories_Users_UserId",
                        column: x => x.UserId,
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
                name: "Orders",
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
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SearchData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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

            migrationBuilder.CreateTable(
                name: "FixProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ErrorDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Condition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Process = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    NumberOfTimes = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    WarrantyOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReceiptDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalMoney = table.Column<decimal>(type: "decimal(38,16)", precision: 38, scale: 16, nullable: false),
                    OrderItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                        name: "FK_FixProducts_OrderItems_OrderItemId",
                        column: x => x.OrderItemId,
                        principalTable: "OrderItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FixProducts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FixProducts_Users_FixStaffId",
                        column: x => x.FixStaffId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "CreatedUser", "IsDeleted", "ModifiedDate", "ModifiedUser", "Name", "Path", "SearchData" },
                values: new object[,]
                {
                    { new Guid("299efb5f-3831-4c43-850f-45a78d95041a"), null, null, false, null, null, "CPU - Vi xử lý", null, null },
                    { new Guid("2ee38f31-e912-400c-865d-b25983258941"), null, null, false, null, null, "ACER", null, null },
                    { new Guid("55257e22-2885-450f-a340-70fea2059fdf"), null, null, false, null, null, "ASUS", null, null },
                    { new Guid("6237ac81-76f3-4dec-a039-febd35c6164f"), null, null, false, null, null, "VGA Laptop", null, null },
                    { new Guid("7a120e18-59c7-4d0a-8554-71019f9697cb"), null, null, false, null, null, "Macbook", null, null },
                    { new Guid("819b5ad3-cc1c-441d-8a51-b391b546df6f"), null, null, false, null, null, "LCD - Màn hình Laptop", null, null },
                    { new Guid("89d0fa4a-a2c1-43bb-bbf3-9d36929bbead"), null, null, false, null, null, "TOSHIBA", null, null },
                    { new Guid("a018b618-a03c-4f17-bed4-ec167b077f92"), null, null, false, null, null, "SONY", null, null },
                    { new Guid("b30b2908-4cf7-4f2f-93a4-8b7df7ebe9c6"), null, null, false, null, null, "Keo tản nhiệt", null, null },
                    { new Guid("c2a19aa7-8b91-4f5b-b4c2-42f4d0db4f0a"), null, null, false, null, null, "Phần mềm diệt virus - win", null, null },
                    { new Guid("f9e007a1-d798-4b90-9f65-a82a98efdfc9"), null, null, false, null, null, "Màn hình AOC", null, null }
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
                    { new Guid("296e3602-3d0b-4506-84d8-e6618a55f8e1"), null, null, false, null, null, "Con", null },
                    { new Guid("62196677-d19f-4626-869b-59afd4ce33be"), null, null, false, null, null, "Cái", null },
                    { new Guid("7f50d72d-cf1b-4874-8c37-6387c66a6523"), null, null, false, null, null, "Cm", null },
                    { new Guid("f576cb6f-c553-4cc8-91b5-17d036a6431c"), null, null, false, null, null, "Bộ", null }
                });

            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "Id", "Address", "CreatedDate", "CreatedUser", "IsDeleted", "ModifiedDate", "ModifiedUser", "Name", "Phone", "SearchData" },
                values: new object[] { new Guid("bbe4548a-ac32-4f02-bf89-3ae8d411282f"), "", null, null, false, null, null, "TechFix", "0123456778", null });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "Address", "Code", "CreatedDate", "CreatedUser", "Email", "ImagePath", "InDebt", "IsDeleted", "ModifiedDate", "ModifiedUser", "Name", "Note", "Phone", "PhoneNumber", "SearchData" },
                values: new object[,]
                {
                    { new Guid("169960de-7eec-4915-bf68-d4c4920de1df"), "", null, null, null, "", null, 0m, false, null, null, "Phát Đạt LTK", "", "", null, null },
                    { new Guid("196d3520-8ffd-49e5-b0f1-f88d4b5f1b59"), "", null, null, null, "", null, 0m, false, null, null, "NWH", "", "", null, null },
                    { new Guid("248d6baf-bf23-4f19-9f58-7cb82f650f92"), "", null, null, null, "", null, 0m, false, null, null, "Tấn Phát LTK", "", "", null, null },
                    { new Guid("33e3338c-7921-480a-89fe-c06eb62f0fd8"), "", null, null, null, "", null, 0m, false, null, null, "CTY PATECH", "", "", null, null }
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "Address", "Code", "CreatedDate", "CreatedUser", "Email", "ImagePath", "InDebt", "IsDeleted", "ModifiedDate", "ModifiedUser", "Name", "Note", "Phone", "PhoneNumber", "SearchData" },
                values: new object[,]
                {
                    { new Guid("377e531c-cbe8-42ec-a6ee-00adf0849224"), "", null, null, null, "", null, 0m, false, null, null, "Hải Việt", "", "", null, null },
                    { new Guid("51f4d540-fbd1-43e1-a261-add27f746319"), "", null, null, null, "", null, 0m, false, null, null, "Chuẩn LTK", "", "", null, null },
                    { new Guid("695f0fca-eb4f-415e-90f8-ef1bce52d642"), "", null, null, null, "", null, 0m, false, null, null, "LGT", "", "", null, null },
                    { new Guid("7e777353-5941-47a2-b25f-3be4c167ec55"), "", null, null, null, "", null, 0m, false, null, null, "Huy Phát Kingmater", "", "", null, null },
                    { new Guid("aabacf68-91af-4ea3-8402-0e7b77dc2d87"), "", null, null, null, "", null, 0m, false, null, null, "Chỉnh LK", "", "", null, null },
                    { new Guid("c3b7a321-e2c6-4876-8ae5-64bb4d66af11"), "", null, null, null, "", null, 0m, false, null, null, "Văn Hải", "", "", null, null },
                    { new Guid("dd2e9146-ded2-4faa-a7ac-60c3191020b6"), "", null, null, null, "", null, 0m, false, null, null, "Duy Tân", "", "", null, null },
                    { new Guid("f11dca19-5d8f-49e8-83e6-6b0aae1cf459"), "", null, null, null, "", null, 0m, false, null, null, "Tuấn Hiền", "", "", null, null }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AllowNegativeSell", "CategoryId", "Code", "CreatedDate", "CreatedUser", "Description", "Discontinue", "FakePrice", "ImagePath", "IsDeleted", "IsInventoryTracking", "ManufacturerId", "MaximumNorm", "MinimumNorm", "ModifiedDate", "ModifiedUser", "Name", "OriginalPrice", "ProductConditionId", "ProductUnitId", "Quantity", "SearchData", "SupplierId", "Warranty", "WebPrice" },
                values: new object[,]
                {
                    { new Guid("15684798-d38e-4fdb-994b-72cf233f3bd1"), true, null, "SP0000022", null, null, "", false, 270000m, null, false, false, new Guid("70f6af9a-9725-47c5-b9e3-66aecd85e47b"), 5, 3, null, null, "Chuột Newmen F300 không dây", 215000m, new Guid("fcdf46b9-c1d6-4664-8ea4-7a1b25ab1875"), null, 3, null, new Guid("196d3520-8ffd-49e5-b0f1-f88d4b5f1b59"), "12TH", 215000m },
                    { new Guid("4aa20fe5-44fa-4139-b4c9-cf7afcfddd36"), true, null, "SP0000032", null, null, "", false, 100000m, null, false, true, new Guid("e32e08d8-32d0-49a4-af6d-e9a8b02fa7ae"), 5, 1, null, null, "Caddybay mỏng 9.5mm Laptop", 35000m, new Guid("fcdf46b9-c1d6-4664-8ea4-7a1b25ab1875"), null, 99, null, new Guid("196d3520-8ffd-49e5-b0f1-f88d4b5f1b59"), "3TH", 35000m },
                    { new Guid("99ca74bf-f5f3-4f13-8a0e-db5c0a6cf560"), false, null, "SP0000003", null, null, "", false, 130000m, null, false, true, new Guid("3af759e8-b2e0-4b02-96b6-76c5609615f6"), 0, 0, null, null, "Cáp Pisen USB Type-C 3A 1m", 90000m, new Guid("cb98afde-54ed-416c-a73c-18eef6f0983b"), null, 8, null, new Guid("196d3520-8ffd-49e5-b0f1-f88d4b5f1b59"), "06TH", 90000m },
                    { new Guid("a10c7723-c74a-4517-9dda-6dd764269465"), false, null, "SP0000041", null, null, "", false, 550000m, null, true, false, new Guid("e32e08d8-32d0-49a4-af6d-e9a8b02fa7ae"), 0, 0, null, null, "SSD Pioneer APS-SL3N 120GB 2.5in ( Read 520MB/s - Write 400MB/s )", 440000m, new Guid("fcdf46b9-c1d6-4664-8ea4-7a1b25ab1875"), null, 0, null, new Guid("196d3520-8ffd-49e5-b0f1-f88d4b5f1b59"), "12TH", 440000m },
                    { new Guid("ac291998-9ae4-4c0f-9bf1-097fb86d44b4"), true, null, "SP0000010", null, null, "", false, 70000m, null, false, false, new Guid("e32e08d8-32d0-49a4-af6d-e9a8b02fa7ae"), 10, 5, null, null, "Áo thun hai lỗ", 50000m, new Guid("9420681a-6d03-45bb-ab59-bd1af9e3054a"), null, 5, null, new Guid("196d3520-8ffd-49e5-b0f1-f88d4b5f1b59"), "Bảo hành 6Th", 50000m },
                    { new Guid("fb0ed40f-1133-4999-9721-83351d0194bc"), true, null, "SP0000011", null, null, "", false, 650000m, null, false, false, new Guid("e32e08d8-32d0-49a4-af6d-e9a8b02fa7ae"), 0, 0, null, null, "Sạc dự phòng Pisen Color Power Pro 10000mAh đỏ-đen (Dual USB 1A/2.4A Smart)", 550000m, new Guid("fcdf46b9-c1d6-4664-8ea4-7a1b25ab1875"), null, 0, null, new Guid("196d3520-8ffd-49e5-b0f1-f88d4b5f1b59"), "12TH", 550000m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExportHistories_UserId",
                table: "ExportHistories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FixProducts_FixStaffId",
                table: "FixProducts",
                column: "FixStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_FixProducts_OrderId",
                table: "FixProducts",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_FixProducts_OrderItemId",
                table: "FixProducts",
                column: "OrderItemId");

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
                name: "ExportHistories");

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
                name: "ProductHistories");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "VlinkSequence");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "InputProducts");

            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

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

            migrationBuilder.DropSequence(
                name: "FundCodeIncrement");

            migrationBuilder.DropSequence(
                name: "ProductCodeIncrement");
        }
    }
}
