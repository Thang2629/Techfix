using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechFix.EntityModels.Migrations
{
    public partial class AddTemplateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("299efb5f-3831-4c43-850f-45a78d95041a"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("2ee38f31-e912-400c-865d-b25983258941"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("55257e22-2885-450f-a340-70fea2059fdf"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6237ac81-76f3-4dec-a039-febd35c6164f"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("7a120e18-59c7-4d0a-8554-71019f9697cb"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("819b5ad3-cc1c-441d-8a51-b391b546df6f"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("89d0fa4a-a2c1-43bb-bbf3-9d36929bbead"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a018b618-a03c-4f17-bed4-ec167b077f92"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b30b2908-4cf7-4f2f-93a4-8b7df7ebe9c6"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c2a19aa7-8b91-4f5b-b4c2-42f4d0db4f0a"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("f9e007a1-d798-4b90-9f65-a82a98efdfc9"));

            migrationBuilder.DeleteData(
                table: "ProductUnits",
                keyColumn: "Id",
                keyValue: new Guid("296e3602-3d0b-4506-84d8-e6618a55f8e1"));

            migrationBuilder.DeleteData(
                table: "ProductUnits",
                keyColumn: "Id",
                keyValue: new Guid("62196677-d19f-4626-869b-59afd4ce33be"));

            migrationBuilder.DeleteData(
                table: "ProductUnits",
                keyColumn: "Id",
                keyValue: new Guid("7f50d72d-cf1b-4874-8c37-6387c66a6523"));

            migrationBuilder.DeleteData(
                table: "ProductUnits",
                keyColumn: "Id",
                keyValue: new Guid("f576cb6f-c553-4cc8-91b5-17d036a6431c"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("15684798-d38e-4fdb-994b-72cf233f3bd1"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("4aa20fe5-44fa-4139-b4c9-cf7afcfddd36"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("99ca74bf-f5f3-4f13-8a0e-db5c0a6cf560"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a10c7723-c74a-4517-9dda-6dd764269465"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("ac291998-9ae4-4c0f-9bf1-097fb86d44b4"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("fb0ed40f-1133-4999-9721-83351d0194bc"));

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: new Guid("bbe4548a-ac32-4f02-bf89-3ae8d411282f"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("169960de-7eec-4915-bf68-d4c4920de1df"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("248d6baf-bf23-4f19-9f58-7cb82f650f92"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("377e531c-cbe8-42ec-a6ee-00adf0849224"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("51f4d540-fbd1-43e1-a261-add27f746319"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("695f0fca-eb4f-415e-90f8-ef1bce52d642"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("7e777353-5941-47a2-b25f-3be4c167ec55"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("c3b7a321-e2c6-4876-8ae5-64bb4d66af11"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("dd2e9146-ded2-4faa-a7ac-60c3191020b6"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("f11dca19-5d8f-49e8-83e6-6b0aae1cf459"));

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
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Templates", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "CreatedUser", "IsDeleted", "ModifiedDate", "ModifiedUser", "Name", "Path", "SearchData" },
                values: new object[,]
                {
                    { new Guid("213f9620-0eb8-4079-ad68-50a0c3d8e0c3"), null, null, false, null, null, "TOSHIBA", null, null },
                    { new Guid("29ca7ada-4598-440a-866a-d6553fa518de"), null, null, false, null, null, "ASUS", null, null },
                    { new Guid("3b369e55-89cf-41aa-9a94-a4acacc76412"), null, null, false, null, null, "CPU - Vi xử lý", null, null },
                    { new Guid("4b59ec63-82a0-4390-94c5-25479b440b3f"), null, null, false, null, null, "Màn hình AOC", null, null },
                    { new Guid("525af889-ec09-4bad-8bd7-caf38c62066a"), null, null, false, null, null, "ACER", null, null },
                    { new Guid("6050e331-c664-4945-8d16-99b82fdef9f5"), null, null, false, null, null, "LCD - Màn hình Laptop", null, null },
                    { new Guid("739d4145-2161-401e-9b5a-4b2f5692dec5"), null, null, false, null, null, "Macbook", null, null },
                    { new Guid("ac8061fd-c242-451c-811d-fb206afb491d"), null, null, false, null, null, "Phần mềm diệt virus - win", null, null },
                    { new Guid("cd4d2514-e62b-4ac6-a23c-8c831abf1ab6"), null, null, false, null, null, "SONY", null, null },
                    { new Guid("d10d0578-b70e-41ae-b86d-74b1e8d2ceec"), null, null, false, null, null, "VGA Laptop", null, null },
                    { new Guid("d8e36610-5b67-44f4-8dbb-3a141bc60c63"), null, null, false, null, null, "Keo tản nhiệt", null, null }
                });

            migrationBuilder.InsertData(
                table: "ProductUnits",
                columns: new[] { "Id", "CreatedDate", "CreatedUser", "IsDeleted", "ModifiedDate", "ModifiedUser", "Name", "SearchData" },
                values: new object[,]
                {
                    { new Guid("2d100dea-4cae-446c-ba26-ad9561ce3927"), null, null, false, null, null, "Cái", null },
                    { new Guid("9ac147ed-0045-4cc3-83e6-5a9fd400fa7c"), null, null, false, null, null, "Bộ", null },
                    { new Guid("b8b5843c-90d7-4f6f-a233-7d1774fe354a"), null, null, false, null, null, "Cm", null },
                    { new Guid("ed8b2466-f4b2-4fb0-8073-352cba25c8db"), null, null, false, null, null, "Con", null }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AllowNegativeSell", "CategoryId", "Code", "CreatedDate", "CreatedUser", "Description", "Discontinue", "FakePrice", "ImagePath", "IsDeleted", "IsInventoryTracking", "ManufacturerId", "MaximumNorm", "MinimumNorm", "ModifiedDate", "ModifiedUser", "Name", "OriginalPrice", "ProductConditionId", "ProductUnitId", "Quantity", "SearchData", "SupplierId", "Warranty", "WebPrice" },
                values: new object[,]
                {
                    { new Guid("130ed7f2-66d4-4989-a168-116245f42de4"), true, null, "SP0000032", null, null, "", false, 100000m, null, false, true, new Guid("e32e08d8-32d0-49a4-af6d-e9a8b02fa7ae"), 5, 1, null, null, "Caddybay mỏng 9.5mm Laptop", 35000m, new Guid("fcdf46b9-c1d6-4664-8ea4-7a1b25ab1875"), null, 99, null, new Guid("196d3520-8ffd-49e5-b0f1-f88d4b5f1b59"), "3TH", 35000m },
                    { new Guid("1b5f593e-77c1-4951-bfcb-74ce7479b9ea"), true, null, "SP0000011", null, null, "", false, 650000m, null, false, false, new Guid("e32e08d8-32d0-49a4-af6d-e9a8b02fa7ae"), 0, 0, null, null, "Sạc dự phòng Pisen Color Power Pro 10000mAh đỏ-đen (Dual USB 1A/2.4A Smart)", 550000m, new Guid("fcdf46b9-c1d6-4664-8ea4-7a1b25ab1875"), null, 0, null, new Guid("196d3520-8ffd-49e5-b0f1-f88d4b5f1b59"), "12TH", 550000m },
                    { new Guid("32446e56-85d1-4295-b291-7ee2d5d39250"), false, null, "SP0000003", null, null, "", false, 130000m, null, false, true, new Guid("3af759e8-b2e0-4b02-96b6-76c5609615f6"), 0, 0, null, null, "Cáp Pisen USB Type-C 3A 1m", 90000m, new Guid("cb98afde-54ed-416c-a73c-18eef6f0983b"), null, 8, null, new Guid("196d3520-8ffd-49e5-b0f1-f88d4b5f1b59"), "06TH", 90000m },
                    { new Guid("43b5ee3e-302a-429a-bea9-8ddc8fe2846f"), false, null, "SP0000041", null, null, "", false, 550000m, null, true, false, new Guid("e32e08d8-32d0-49a4-af6d-e9a8b02fa7ae"), 0, 0, null, null, "SSD Pioneer APS-SL3N 120GB 2.5in ( Read 520MB/s - Write 400MB/s )", 440000m, new Guid("fcdf46b9-c1d6-4664-8ea4-7a1b25ab1875"), null, 0, null, new Guid("196d3520-8ffd-49e5-b0f1-f88d4b5f1b59"), "12TH", 440000m },
                    { new Guid("6200100e-a62b-43b0-b264-9d034a0e5f3e"), true, null, "SP0000010", null, null, "", false, 70000m, null, false, false, new Guid("e32e08d8-32d0-49a4-af6d-e9a8b02fa7ae"), 10, 5, null, null, "Áo thun hai lỗ", 50000m, new Guid("9420681a-6d03-45bb-ab59-bd1af9e3054a"), null, 5, null, new Guid("196d3520-8ffd-49e5-b0f1-f88d4b5f1b59"), "Bảo hành 6Th", 50000m },
                    { new Guid("9fddec5a-0cfb-4230-8735-8092815f8a4a"), true, null, "SP0000022", null, null, "", false, 270000m, null, false, false, new Guid("70f6af9a-9725-47c5-b9e3-66aecd85e47b"), 5, 3, null, null, "Chuột Newmen F300 không dây", 215000m, new Guid("fcdf46b9-c1d6-4664-8ea4-7a1b25ab1875"), null, 3, null, new Guid("196d3520-8ffd-49e5-b0f1-f88d4b5f1b59"), "12TH", 215000m }
                });

            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "Id", "Address", "CreatedDate", "CreatedUser", "IsDeleted", "ModifiedDate", "ModifiedUser", "Name", "Phone", "SearchData" },
                values: new object[] { new Guid("c8f0adef-ece9-40a9-91e6-4dd1d7fce993"), "", null, null, false, null, null, "TechFix", "0123456778", null });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "Address", "Code", "CreatedDate", "CreatedUser", "Email", "ImagePath", "InDebt", "IsDeleted", "ModifiedDate", "ModifiedUser", "Name", "Note", "Phone", "PhoneNumber", "SearchData" },
                values: new object[,]
                {
                    { new Guid("16d237c8-51e0-48d7-94c7-2bbca61cab7d"), "", null, null, null, "", null, 0m, false, null, null, "LGT", "", "", null, null },
                    { new Guid("20af17bc-11ef-4ac4-8b5c-52e9dce47029"), "", null, null, null, "", null, 0m, false, null, null, "Chuẩn LTK", "", "", null, null },
                    { new Guid("225605ca-8ca5-4be5-9ba2-4c50f69b435a"), "", null, null, null, "", null, 0m, false, null, null, "Phát Đạt LTK", "", "", null, null },
                    { new Guid("27e99853-12be-448a-b633-bc5df6eb6d44"), "", null, null, null, "", null, 0m, false, null, null, "Huy Phát Kingmater", "", "", null, null },
                    { new Guid("66eec384-a26b-4fd0-9e40-88ab98cb953c"), "", null, null, null, "", null, 0m, false, null, null, "Văn Hải", "", "", null, null },
                    { new Guid("6b3a3388-5906-45b7-8cea-88079cdaad8f"), "", null, null, null, "", null, 0m, false, null, null, "Tuấn Hiền", "", "", null, null },
                    { new Guid("9dc6398b-9e0e-4ee2-abde-4d6c9a0e051b"), "", null, null, null, "", null, 0m, false, null, null, "Duy Tân", "", "", null, null },
                    { new Guid("b52378ba-1c1d-4091-b273-18f94f09b0e6"), "", null, null, null, "", null, 0m, false, null, null, "Tấn Phát LTK", "", "", null, null },
                    { new Guid("deae1070-6356-40c0-a8f1-02548905d317"), "", null, null, null, "", null, 0m, false, null, null, "Hải Việt", "", "", null, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Templates");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("213f9620-0eb8-4079-ad68-50a0c3d8e0c3"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("29ca7ada-4598-440a-866a-d6553fa518de"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("3b369e55-89cf-41aa-9a94-a4acacc76412"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("4b59ec63-82a0-4390-94c5-25479b440b3f"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("525af889-ec09-4bad-8bd7-caf38c62066a"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6050e331-c664-4945-8d16-99b82fdef9f5"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("739d4145-2161-401e-9b5a-4b2f5692dec5"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("ac8061fd-c242-451c-811d-fb206afb491d"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("cd4d2514-e62b-4ac6-a23c-8c831abf1ab6"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d10d0578-b70e-41ae-b86d-74b1e8d2ceec"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d8e36610-5b67-44f4-8dbb-3a141bc60c63"));

            migrationBuilder.DeleteData(
                table: "ProductUnits",
                keyColumn: "Id",
                keyValue: new Guid("2d100dea-4cae-446c-ba26-ad9561ce3927"));

            migrationBuilder.DeleteData(
                table: "ProductUnits",
                keyColumn: "Id",
                keyValue: new Guid("9ac147ed-0045-4cc3-83e6-5a9fd400fa7c"));

            migrationBuilder.DeleteData(
                table: "ProductUnits",
                keyColumn: "Id",
                keyValue: new Guid("b8b5843c-90d7-4f6f-a233-7d1774fe354a"));

            migrationBuilder.DeleteData(
                table: "ProductUnits",
                keyColumn: "Id",
                keyValue: new Guid("ed8b2466-f4b2-4fb0-8073-352cba25c8db"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("130ed7f2-66d4-4989-a168-116245f42de4"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("1b5f593e-77c1-4951-bfcb-74ce7479b9ea"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("32446e56-85d1-4295-b291-7ee2d5d39250"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("43b5ee3e-302a-429a-bea9-8ddc8fe2846f"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("6200100e-a62b-43b0-b264-9d034a0e5f3e"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("9fddec5a-0cfb-4230-8735-8092815f8a4a"));

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: new Guid("c8f0adef-ece9-40a9-91e6-4dd1d7fce993"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("16d237c8-51e0-48d7-94c7-2bbca61cab7d"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("20af17bc-11ef-4ac4-8b5c-52e9dce47029"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("225605ca-8ca5-4be5-9ba2-4c50f69b435a"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("27e99853-12be-448a-b633-bc5df6eb6d44"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("66eec384-a26b-4fd0-9e40-88ab98cb953c"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("6b3a3388-5906-45b7-8cea-88079cdaad8f"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("9dc6398b-9e0e-4ee2-abde-4d6c9a0e051b"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("b52378ba-1c1d-4091-b273-18f94f09b0e6"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("deae1070-6356-40c0-a8f1-02548905d317"));

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
                    { new Guid("248d6baf-bf23-4f19-9f58-7cb82f650f92"), "", null, null, null, "", null, 0m, false, null, null, "Tấn Phát LTK", "", "", null, null },
                    { new Guid("377e531c-cbe8-42ec-a6ee-00adf0849224"), "", null, null, null, "", null, 0m, false, null, null, "Hải Việt", "", "", null, null },
                    { new Guid("51f4d540-fbd1-43e1-a261-add27f746319"), "", null, null, null, "", null, 0m, false, null, null, "Chuẩn LTK", "", "", null, null },
                    { new Guid("695f0fca-eb4f-415e-90f8-ef1bce52d642"), "", null, null, null, "", null, 0m, false, null, null, "LGT", "", "", null, null },
                    { new Guid("7e777353-5941-47a2-b25f-3be4c167ec55"), "", null, null, null, "", null, 0m, false, null, null, "Huy Phát Kingmater", "", "", null, null },
                    { new Guid("c3b7a321-e2c6-4876-8ae5-64bb4d66af11"), "", null, null, null, "", null, 0m, false, null, null, "Văn Hải", "", "", null, null },
                    { new Guid("dd2e9146-ded2-4faa-a7ac-60c3191020b6"), "", null, null, null, "", null, 0m, false, null, null, "Duy Tân", "", "", null, null },
                    { new Guid("f11dca19-5d8f-49e8-83e6-6b0aae1cf459"), "", null, null, null, "", null, 0m, false, null, null, "Tuấn Hiền", "", "", null, null }
                });
        }
    }
}
