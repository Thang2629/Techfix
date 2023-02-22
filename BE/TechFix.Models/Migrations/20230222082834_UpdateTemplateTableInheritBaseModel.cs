using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechFix.EntityModels.Migrations
{
    public partial class UpdateTemplateTableInheritBaseModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<string>(
                name: "SearchData",
                table: "Templates",
                type: "nvarchar(max)",
                nullable: true);

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
                    { new Guid("36dcab9b-b6ac-45e3-bd70-0d4dd5cc0973"), "", null, null, null, "", null, 0m, false, null, null, "Văn Hải", "", "", null, null },
                    { new Guid("51e57f1b-ed47-4fe9-9cdb-3866b0ca4fbb"), "", null, null, null, "", null, 0m, false, null, null, "Tuấn Hiền", "", "", null, null },
                    { new Guid("673aad6d-4b27-4760-95b5-7b1c6cf553bd"), "", null, null, null, "", null, 0m, false, null, null, "Chuẩn LTK", "", "", null, null },
                    { new Guid("a9324c9b-545c-4de3-b4c6-990721e2a130"), "", null, null, null, "", null, 0m, false, null, null, "Huy Phát Kingmater", "", "", null, null },
                    { new Guid("e358f54b-bbba-473b-93f4-8308331a3c7d"), "", null, null, null, "", null, 0m, false, null, null, "Tấn Phát LTK", "", "", null, null },
                    { new Guid("f6c5bbd4-701c-43f9-a4fb-df6c887ad0b9"), "", null, null, null, "", null, 0m, false, null, null, "LGT", "", "", null, null },
                    { new Guid("fe0ac497-c6b3-41a4-9242-acb95d075a17"), "", null, null, null, "", null, 0m, false, null, null, "Hải Việt", "", "", null, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("04e05d4d-a304-4106-91a4-dededd677cd9"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("4865a6c6-a56d-4f8e-9961-b062af262426"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("586a934a-a843-4730-aa38-7ad8486959ea"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("743f02c1-eb00-4f9f-aaa3-b10385c8ae58"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("74d78077-f391-4762-95eb-b29699db7af9"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("7a62c105-e475-4113-a176-b0e283581e12"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("7aff1aba-1ca0-42ff-8627-0aa7c2d039af"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9c8fc9f7-25a6-44b2-82fc-e303184449ec"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("ce403e65-ef53-4a2e-8515-06de75436b9e"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("e9cf44cb-0410-4470-8f5f-62d0265a4b91"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("f7b5685e-acca-435f-9681-2175c0054d77"));

            migrationBuilder.DeleteData(
                table: "ProductUnits",
                keyColumn: "Id",
                keyValue: new Guid("0a30fca8-bc21-407e-9264-8ea2e5f41575"));

            migrationBuilder.DeleteData(
                table: "ProductUnits",
                keyColumn: "Id",
                keyValue: new Guid("7035167e-2ec1-46cb-a346-9f6a20eac535"));

            migrationBuilder.DeleteData(
                table: "ProductUnits",
                keyColumn: "Id",
                keyValue: new Guid("92c5a297-4635-46ad-8c9e-b5f28fd44e75"));

            migrationBuilder.DeleteData(
                table: "ProductUnits",
                keyColumn: "Id",
                keyValue: new Guid("eb4a1712-88ac-4694-8941-5e7305bbc4b1"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("29f334ee-2f62-483c-b882-804b5a356dc6"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("342798ba-e601-498b-ac36-963df8173c3e"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("5624153b-9302-4246-b42f-29bd6a06ec46"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b328ddfd-516d-49a3-9fd4-8e0d6354d069"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("e4f6c2a6-1510-49c6-834d-22b8f518a41e"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("e64c481d-a4fa-45ca-902c-f18fb6f37681"));

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: new Guid("ea9f5a2d-4d11-45bc-8ec4-ea7b7cd9334b"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("02f2f867-eab4-4711-8190-ed5e5f0601cf"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("03acff81-9ddd-4cb7-8d7b-de0fc9e51c9e"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("36dcab9b-b6ac-45e3-bd70-0d4dd5cc0973"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("51e57f1b-ed47-4fe9-9cdb-3866b0ca4fbb"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("673aad6d-4b27-4760-95b5-7b1c6cf553bd"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("a9324c9b-545c-4de3-b4c6-990721e2a130"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("e358f54b-bbba-473b-93f4-8308331a3c7d"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("f6c5bbd4-701c-43f9-a4fb-df6c887ad0b9"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("fe0ac497-c6b3-41a4-9242-acb95d075a17"));

            migrationBuilder.DropColumn(
                name: "SearchData",
                table: "Templates");

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
    }
}
