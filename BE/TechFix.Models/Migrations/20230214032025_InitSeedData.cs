using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechFix.EntityModels.Migrations
{
    public partial class InitSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "CreatedUser", "IsDeleted", "ModifiedDate", "ModifiedUser", "Name", "Path", "SearchData" },
                values: new object[,]
                {
                    { new Guid("065a68ed-a34e-4ae4-9b45-3b91f6742782"), null, null, false, null, null, "SONY", null, null },
                    { new Guid("33e8a1cd-a04b-4833-941b-b1cb8183d6ab"), null, null, false, null, null, "Macbook", null, null },
                    { new Guid("43b6c60d-960a-407a-aec3-a786527e7fd2"), null, null, false, null, null, "Màn hình AOC", null, null },
                    { new Guid("73a79910-c3ce-4cd6-a6e9-a48f4de8a727"), null, null, false, null, null, "Keo tản nhiệt", null, null },
                    { new Guid("762315dc-3db1-4bb3-aec9-492300290ce2"), null, null, false, null, null, "ASUS", null, null },
                    { new Guid("8064c62a-448c-4a92-bf0b-5087bd399ec5"), null, null, false, null, null, "LCD - Màn hình Laptop", null, null },
                    { new Guid("97204ff0-9cac-4dee-86a4-57c71d1016cf"), null, null, false, null, null, "TOSHIBA", null, null },
                    { new Guid("cd6b794a-2bbd-4345-882a-55f2bea37f01"), null, null, false, null, null, "VGA Laptop", null, null },
                    { new Guid("cd93fd2e-cf06-4893-b4a7-f01aa87cacb8"), null, null, false, null, null, "Phần mềm diệt virus - win", null, null },
                    { new Guid("ce55104e-1350-434f-b251-5c3d549c02fe"), null, null, false, null, null, "ACER", null, null },
                    { new Guid("d42d588d-e093-4241-a87b-57d39880e39d"), null, null, false, null, null, "CPU - Vi xử lý", null, null }
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
                    { new Guid("1f5cbdec-b821-40f3-80c3-6b7ef1961e2b"), null, null, false, null, null, "Cái", null },
                    { new Guid("569b970a-b72f-4859-a2ed-8afbbf5d54b8"), null, null, false, null, null, "Con", null },
                    { new Guid("9c48ea71-07c4-4ec4-b1a1-5c6e07ece1e4"), null, null, false, null, null, "Bộ", null },
                    { new Guid("da4e1b1b-0c4d-444e-9e17-d9b98da66fc8"), null, null, false, null, null, "Cm", null }
                });

            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "Id", "Address", "CreatedDate", "CreatedUser", "IsDeleted", "ModifiedDate", "ModifiedUser", "Name", "Phone", "SearchData" },
                values: new object[] { new Guid("006e356b-8dc4-4d15-941f-eaa5e2432da0"), "", null, null, false, null, null, "TechFix", "0123456778", null });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "Address", "CreatedDate", "CreatedUser", "Email", "InDebt", "IsDeleted", "ModifiedDate", "ModifiedUser", "Name", "Note", "Phone", "SearchData" },
                values: new object[,]
                {
                    { new Guid("0533febd-dec1-4593-90a9-5662eb6ff88b"), "", null, null, "", 0m, false, null, null, "Tấn Phát LTK", "", "", null },
                    { new Guid("196d3520-8ffd-49e5-b0f1-f88d4b5f1b59"), "", null, null, "", 0m, false, null, null, "NWH", "", "", null },
                    { new Guid("1fed613e-a858-40a5-bd81-28844f482218"), "", null, null, "", 0m, false, null, null, "Phát Đạt LTK", "", "", null },
                    { new Guid("33e3338c-7921-480a-89fe-c06eb62f0fd8"), "", null, null, "", 0m, false, null, null, "CTY PATECH", "", "", null }
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "Address", "CreatedDate", "CreatedUser", "Email", "InDebt", "IsDeleted", "ModifiedDate", "ModifiedUser", "Name", "Note", "Phone", "SearchData" },
                values: new object[,]
                {
                    { new Guid("3e0adebb-5484-4a33-9436-e69532db5fee"), "", null, null, "", 0m, false, null, null, "LGT", "", "", null },
                    { new Guid("57294d71-1b0a-4b29-b3e3-c4de1f7fa16f"), "", null, null, "", 0m, false, null, null, "Duy Tân", "", "", null },
                    { new Guid("67fe282c-92e2-420b-a185-c92243131768"), "", null, null, "", 0m, false, null, null, "Chuẩn LTK", "", "", null },
                    { new Guid("8639f97d-06d3-415d-836b-52abe02394b9"), "", null, null, "", 0m, false, null, null, "Tuấn Hiền", "", "", null },
                    { new Guid("aabacf68-91af-4ea3-8402-0e7b77dc2d87"), "", null, null, "", 0m, false, null, null, "Chỉnh LK", "", "", null },
                    { new Guid("e1d18951-2787-40d0-baec-ce0245aaa10f"), "", null, null, "", 0m, false, null, null, "Huy Phát Kingmater", "", "", null },
                    { new Guid("f17bddcc-18fa-453f-b539-862f78b4f32e"), "", null, null, "", 0m, false, null, null, "Hải Việt", "", "", null },
                    { new Guid("f630391a-5c7e-4156-a2b1-d748ffe13a27"), "", null, null, "", 0m, false, null, null, "Văn Hải", "", "", null }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AllowNegativeSell", "CategoryId", "Code", "CreatedDate", "CreatedUser", "Description", "IsDeleted", "IsInventoryTracking", "ManufacturerId", "MaximumNorm", "MinimumNorm", "ModifiedDate", "ModifiedUser", "Name", "OriginalCost", "ProductConditionId", "ProductUnitId", "Quantity", "SearchData", "SellIn", "SellOut", "StoreId", "SupplierId", "Warranty" },
                values: new object[,]
                {
                    { new Guid("2a34a331-95fd-4cd1-a77c-7a731c32e5b5"), false, null, "SP0000041", null, null, "", true, false, new Guid("e32e08d8-32d0-49a4-af6d-e9a8b02fa7ae"), 0, 0, null, null, "SSD Pioneer APS-SL3N 120GB 2.5in ( Read 520MB/s - Write 400MB/s )", 440000m, new Guid("fcdf46b9-c1d6-4664-8ea4-7a1b25ab1875"), null, 0, null, 440000m, 550000m, null, new Guid("196d3520-8ffd-49e5-b0f1-f88d4b5f1b59"), "12TH" },
                    { new Guid("2c56968f-07c0-4f55-bbcd-fa6f0f0e40b7"), true, null, "SP0000032", null, null, "", false, true, new Guid("e32e08d8-32d0-49a4-af6d-e9a8b02fa7ae"), 5, 1, null, null, "Caddybay mỏng 9.5mm Laptop", 35000m, new Guid("fcdf46b9-c1d6-4664-8ea4-7a1b25ab1875"), null, 99, null, 35000m, 100000m, null, new Guid("196d3520-8ffd-49e5-b0f1-f88d4b5f1b59"), "3TH" },
                    { new Guid("3815f587-d474-4f9e-890e-02f4facb49d9"), true, null, "SP0000011", null, null, "", false, false, new Guid("e32e08d8-32d0-49a4-af6d-e9a8b02fa7ae"), 0, 0, null, null, "Sạc dự phòng Pisen Color Power Pro 10000mAh đỏ-đen (Dual USB 1A/2.4A Smart)", 550000m, new Guid("fcdf46b9-c1d6-4664-8ea4-7a1b25ab1875"), null, 0, null, 550000m, 650000m, null, new Guid("196d3520-8ffd-49e5-b0f1-f88d4b5f1b59"), "12TH" },
                    { new Guid("8be4213c-68f4-49da-9232-08ee1c47c944"), false, null, "SP000003", null, null, "", false, true, new Guid("3af759e8-b2e0-4b02-96b6-76c5609615f6"), 0, 0, null, null, "Cáp Pisen USB Type-C 3A 1m", 90000m, new Guid("cb98afde-54ed-416c-a73c-18eef6f0983b"), null, 8, null, 90000m, 130000m, null, new Guid("196d3520-8ffd-49e5-b0f1-f88d4b5f1b59"), "06TH" },
                    { new Guid("a86e7ce5-86ef-4eef-a5c8-86fd07388622"), true, null, "SP0000010", null, null, "", false, false, new Guid("e32e08d8-32d0-49a4-af6d-e9a8b02fa7ae"), 10, 5, null, null, "Áo thun hai lỗ", 50000m, new Guid("9420681a-6d03-45bb-ab59-bd1af9e3054a"), null, 5, null, 50000m, 70000m, null, new Guid("196d3520-8ffd-49e5-b0f1-f88d4b5f1b59"), "Bảo hành 6Th" },
                    { new Guid("b936c681-c458-49d7-b4cb-0a9bfb760755"), true, null, "SP0000022", null, null, "", false, false, new Guid("70f6af9a-9725-47c5-b9e3-66aecd85e47b"), 5, 3, null, null, "Chuột Newmen F300 không dây", 215000m, new Guid("fcdf46b9-c1d6-4664-8ea4-7a1b25ab1875"), null, 3, null, 215000m, 270000m, null, new Guid("196d3520-8ffd-49e5-b0f1-f88d4b5f1b59"), "12TH" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("065a68ed-a34e-4ae4-9b45-3b91f6742782"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("33e8a1cd-a04b-4833-941b-b1cb8183d6ab"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("43b6c60d-960a-407a-aec3-a786527e7fd2"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("73a79910-c3ce-4cd6-a6e9-a48f4de8a727"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("762315dc-3db1-4bb3-aec9-492300290ce2"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("8064c62a-448c-4a92-bf0b-5087bd399ec5"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("97204ff0-9cac-4dee-86a4-57c71d1016cf"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("cd6b794a-2bbd-4345-882a-55f2bea37f01"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("cd93fd2e-cf06-4893-b4a7-f01aa87cacb8"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("ce55104e-1350-434f-b251-5c3d549c02fe"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d42d588d-e093-4241-a87b-57d39880e39d"));

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: new Guid("151ecbb0-dbe5-4a80-ab56-e443e145b5e0"));

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: new Guid("179f6f9c-33d7-4f43-8b36-b9668cba2cca"));

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: new Guid("18eacb75-8d7f-4833-87d9-97ba3971a46e"));

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: new Guid("242bf112-9304-4e64-a08a-f455cd132043"));

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: new Guid("36aa5768-87aa-4b7a-9fc7-c8066d136c55"));

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: new Guid("45341e2d-7e0f-45e7-ad84-f361cd69dc41"));

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: new Guid("49747ba2-8a30-4bb5-a2a1-49d6580e672a"));

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: new Guid("4ae0a384-443b-4901-85b1-cfa1976cf035"));

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: new Guid("5834857c-c47f-4995-abd0-9919515d8b35"));

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: new Guid("8657a192-250f-46f0-addc-0806460b4ea2"));

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: new Guid("b9ebb845-7dd3-4522-b2ba-5fc45f0459a0"));

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: new Guid("d6eb32c3-1339-4b15-b6bf-db38a8bb04ee"));

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: new Guid("f30a01bd-2a0c-4340-bfdb-6a497aecb648"));

            migrationBuilder.DeleteData(
                table: "ProductConditions",
                keyColumn: "Id",
                keyValue: new Guid("ba55e427-1ecd-4d44-a27a-a5be26b89fdd"));

            migrationBuilder.DeleteData(
                table: "ProductConditions",
                keyColumn: "Id",
                keyValue: new Guid("de2d4f17-b82c-47c0-9a88-a7bae70c8579"));

            migrationBuilder.DeleteData(
                table: "ProductConditions",
                keyColumn: "Id",
                keyValue: new Guid("f97327e2-d6aa-439f-991a-875460b45284"));

            migrationBuilder.DeleteData(
                table: "ProductUnits",
                keyColumn: "Id",
                keyValue: new Guid("1f5cbdec-b821-40f3-80c3-6b7ef1961e2b"));

            migrationBuilder.DeleteData(
                table: "ProductUnits",
                keyColumn: "Id",
                keyValue: new Guid("569b970a-b72f-4859-a2ed-8afbbf5d54b8"));

            migrationBuilder.DeleteData(
                table: "ProductUnits",
                keyColumn: "Id",
                keyValue: new Guid("9c48ea71-07c4-4ec4-b1a1-5c6e07ece1e4"));

            migrationBuilder.DeleteData(
                table: "ProductUnits",
                keyColumn: "Id",
                keyValue: new Guid("da4e1b1b-0c4d-444e-9e17-d9b98da66fc8"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("2a34a331-95fd-4cd1-a77c-7a731c32e5b5"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("2c56968f-07c0-4f55-bbcd-fa6f0f0e40b7"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3815f587-d474-4f9e-890e-02f4facb49d9"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("8be4213c-68f4-49da-9232-08ee1c47c944"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a86e7ce5-86ef-4eef-a5c8-86fd07388622"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b936c681-c458-49d7-b4cb-0a9bfb760755"));

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: new Guid("006e356b-8dc4-4d15-941f-eaa5e2432da0"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("0533febd-dec1-4593-90a9-5662eb6ff88b"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("1fed613e-a858-40a5-bd81-28844f482218"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("33e3338c-7921-480a-89fe-c06eb62f0fd8"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("3e0adebb-5484-4a33-9436-e69532db5fee"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("57294d71-1b0a-4b29-b3e3-c4de1f7fa16f"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("67fe282c-92e2-420b-a185-c92243131768"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("8639f97d-06d3-415d-836b-52abe02394b9"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("aabacf68-91af-4ea3-8402-0e7b77dc2d87"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("e1d18951-2787-40d0-baec-ce0245aaa10f"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("f17bddcc-18fa-453f-b539-862f78b4f32e"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("f630391a-5c7e-4156-a2b1-d748ffe13a27"));

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: new Guid("3af759e8-b2e0-4b02-96b6-76c5609615f6"));

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: new Guid("70f6af9a-9725-47c5-b9e3-66aecd85e47b"));

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: new Guid("e32e08d8-32d0-49a4-af6d-e9a8b02fa7ae"));

            migrationBuilder.DeleteData(
                table: "ProductConditions",
                keyColumn: "Id",
                keyValue: new Guid("9420681a-6d03-45bb-ab59-bd1af9e3054a"));

            migrationBuilder.DeleteData(
                table: "ProductConditions",
                keyColumn: "Id",
                keyValue: new Guid("cb98afde-54ed-416c-a73c-18eef6f0983b"));

            migrationBuilder.DeleteData(
                table: "ProductConditions",
                keyColumn: "Id",
                keyValue: new Guid("fcdf46b9-c1d6-4664-8ea4-7a1b25ab1875"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("196d3520-8ffd-49e5-b0f1-f88d4b5f1b59"));
        }
    }
}
