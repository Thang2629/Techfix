using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TechFix.Common.AppSetting;

namespace TechFix.EntityModels.Persistence;

public class DataContextInitialize
{
    private readonly ILogger<DataContextInitialize> _logger;
    private readonly DataContext _context;
    private readonly IOptions<AppSettings> _appSettings;
    public DataContextInitialize(
        ILogger<DataContextInitialize> logger,
        DataContext context,
        IOptions<AppSettings> appSetting)
    {
        _logger = logger;
        _context = context;
        _appSettings = appSetting;
    }

    public async void SeedData()
    {
        try
        {
            SeedSequence();
            if(!_context.Manufacturers.Any())
            {
                SeedMannufacturer();
            }

            if(!_context.Suppliers.Any())
            {
                SeedSupplier();
            }

            if(!_context.Stores.Any())
            {
                SeedStore();
            }

            if (!_context.Users.Any())
            {
                SeedUser();
            }

            if (!_context.Categories.Any())
            {
                SeedCategory();
            }

            if (!_context.ProductUnits.Any())
            {
                SeedProductUnit();
            }

            if (!_context.ProductConditions.Any())
            {
                SeedProductCondition();
            }

            if (!_context.PaymentMethods.Any())
            {
                SeedPaymentMethods();
            }

            await SeedView();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    private async Task SeedView()
    {
        if (!await CheckExistTableInDatabase("RepairProductByCustomerView"))
        {
            var repairProductByCustomer = "CREATE VIEW [RepairProductByCustomerView] AS" +
                                          " SELECT c.Id," +
                                          "c.Fullname as CustomerName," +
                                          "fo.Code as FixOrderCode," +
                                          "CONCAT_WS(' | ', c.Fullname, c.PhoneNumber, fo.Code, fp.Code) as SearchData," +
                                          "fp.Id as FixProductId," +
                                          "fp.Code as FixProductCode," +
                                          "fp.Name as FixProductName," +
                                          "fp.ErrorDescription as FixProductErrorDescription," +
                                          "fp.Condition as FixProductCondition," +
                                          "fp.ReceiptDate as FixProductReceiptDate," +
                                          "fp.TotalMoney," +
                                          "fp.EstimatedReturnDate as FixProductEstimatedReturnDate," +
                                          "fp.FinishDate as FixProductFinishDate," +
                                          "fp.ReturnDate as FixProductReturnDate," +
                                          "fp.Process as FixProductProcess," +
                                          "u.FullName as FixStaffName," +
                                          "fp.Cpu," +
                                          "fp.Hdd," +
                                          "fp.Ram," +
                                          "fp.Wifi," +
                                          "fp.Pin," +
                                          "fp.Adapter," +
                                          "fp.Keyboard," +
                                          "fp.Psu," +
                                          "fp.Lcd," +
                                          "fp.Other," +
                                          "c.IsDeleted" +
                                          " FROM Users u JOIN FixProducts fp ON u.Id = fp.FixStaffId" +
                                          " JOIN FixOrders fo ON fo.Id = fp.FixOrderId" +
                                          " JOIN Customers c ON c.Id = fo.CustomerId";

            _context.Database.ExecuteSqlRaw(repairProductByCustomer);
        }

        if (!await CheckExistTableInDatabase("RepairProductByFixStaffView"))
        {
            var repairProductByFixStaff = "CREATE VIEW [RepairProductByFixStaffView] AS" +
                                          " SELECT u.Id," +
                                          "u.FullName as FixStaffName," +
                                          "CONCAT_WS(' | ', c.Fullname, c.PhoneNumber, fo.Code, fp.Code) as SearchData," +
                                          "c.Fullname as CustomerName," +
                                          "fp.Id as FixProductId," +
                                          "fo.Code as FixOrderCode," +
                                          "fp.Code as FixProductCode," +
                                          "fp.Name as FixProductName," +
                                          "fp.ErrorDescription as FixProductErrorDescription," +
                                          "fp.Condition as FixProductCondition," +
                                          "fp.ReceiptDate as FixProductReceiptDate," +
                                          "fp.TotalMoney," +
                                          "fp.EstimatedReturnDate as FixProductEstimatedReturnDate," +
                                          "fp.FinishDate as FixProductFinishDate," +
                                          "fp.ReturnDate as FixProductReturnDate," +
                                          "fp.Process as FixProductProcess," +
                                          "fp.Cpu," +
                                          "fp.Hdd," +
                                          "fp.Ram," +
                                          "fp.Wifi," +
                                          "fp.Pin," +
                                          "fp.Adapter," +
                                          "fp.Keyboard," +
                                          "fp.Psu," +
                                          "fp.Lcd," +
                                          "fp.Other," +
                                          "c.IsDeleted" +
                                          " FROM Users u JOIN FixProducts fp ON u.Id = fp.FixStaffId" +
                                          " JOIN FixOrders fo ON fo.Id = fp.FixOrderId" +
                                          " JOIN Customers c ON c.Id = fo.CustomerId";

            _context.Database.ExecuteSqlRaw(repairProductByFixStaff);
        }

        if (!await CheckExistTableInDatabase("RepairProductReportView"))
        {
            var repairProductReport = "CREATE VIEW [RepairProductReportView] AS" +
                                      " SELECT b.Id," +
                                      "b.Code," +
                                      "c.Fullname as CustomerName," +
                                      "s.Name as StoreName," +
                                      "b.CreatedDate," +
                                      "u.FullName as FixStaffName," +
                                      "b.TotalQuantity," +
                                      "b.TotalAmount," +
                                      "b.AmountPaid," +
                                      "b.AmountOwed," +
                                      "b.IsDeleted," +
                                      "fp.Id as FixProductId," +
                                      "fp.Code as FixProductCode," +
                                      "fp.Name as FixProductName," +
                                      "fp.ProductSerial," +
                                      "fp.Condition," +
                                      "fp.WarrantyPeriod," +
                                      "fp.TotalMoney" +
                                      " FROM Bills b" +
                                      " JOIN Customers c ON b.CustomerId = c.Id" +
                                      " JOIN Users u ON b.SellerId = u.Id" +
                                      " JOIN PaymentMethods pm ON b.PaymentMethodId = pm.Id" +
                                      " JOIN FixProducts fp ON b.Id = fp.BillId" +
                                      " JOIN Stores s ON s.Id = b.StoreId";

            _context.Database.ExecuteSqlRaw(repairProductReport);
        }

        if (!await CheckExistTableInDatabase("BillView"))
        {
            var query = @"IF OBJECT_ID('BillView') IS NULL
                            BEGIN   
                                EXECUTE('CREATE VIEW BillView AS
                            SELECT b.Id,
                                   c.PhoneNumber,
                                   c.Fullname CustomerName,
                                   b.Code,
                                   s.Name StoreName,
                                   b.Note,
                                   b.SaleDate,
                                   u.FullName SaleName,
                                   b.TotalQuantity,
                                   b.TotalAmount,
                                   b.AmountOwed,
	                               b.IsDeleted,
	                               b.IsReturn,
	                               u.Id SellerId,
	                               c.Team,
	                               CONCAT_WS('' | '', c.PhoneNumber, c.FullName, b.Code) SearchData
                            FROM Bills b
                            LEFT JOIN Customers c ON b.CustomerId = c.Id
                            LEFT JOIN Stores s ON b.StoreId = c.Id
                            LEFT JOIN Users u ON b.SellerId = u.Id;')
                            END";

            _context.Database.ExecuteSqlRaw(query);
        }
    }

    private void SeedPaymentMethods()
    {
        _context.PaymentMethods.AddRange(new List<PaymentMethod>
        {
            new (){Id = Guid.Parse("5F88B1B9-090F-4ADE-B7B3-377626F985B8"), Name = "Tiền mặt", CreatedUser = null, ModifiedUser = null, IsDeleted = false },
            new (){Id = Guid.Parse("649312B0-80D7-4573-B70F-E99F673FB2A2"), Name = "Thẻ", CreatedUser = null, ModifiedUser = null, IsDeleted = false },
            new (){Id = Guid.Parse("50F73E3A-C353-48FE-82E5-4A9187252D77"), Name = "CK", CreatedUser = null, ModifiedUser = null, IsDeleted = false },
        });
        _context.SaveChanges();
    }

    private void SeedStore()
    {
        _context.Stores.Add(new Store {Id = new Guid("4f056739-e1dd-4f6d-9d95-1682c857110a"), Name = "TechFix", Address = "", Phone = "0123456778", CreatedUser = null, ModifiedUser = null, IsDeleted = false});
        _context.SaveChanges();
    }

    private void SeedCategory()
    {
        _context.Categories.AddRange(new List<Category>
        {
            new() { Id = new Guid("5937b3fe-a1ef-4d83-9c91-2927a0c00185"), Name = "LCD - Màn hình Laptop", Path = "/lcd", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("3e107806-2a75-4eed-9987-dc88a69a031a"), Name = "Màn hình AOC", Path = "/lcd/aoc", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("1406750b-1862-4c8f-8eb0-cc6299a7e28e"), Name = "Phần mềm Virus - Win", Path = "/virus-win", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("51ccd309-6ac0-4ecd-814b-c7d06abca651"), Name = "Card VGA Laptop", Path = "/vga-laptop", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("9f119605-7dcc-4d80-ac69-f07f860a93e8"), Name = "CPU Vi xử lý", Path = "/cpu", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("a09590c7-fa52-446b-a3a4-5768e4fd4e95"), Name = "Box HDD - CaddyBay Laptop", Path = "/hdd-caddybay", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("76879d8a-5602-4840-96a6-769d83816a54"), Name = "Cáp chuyển đổi tín hiệu", Path = "/cable", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("b31a8971-b446-43f2-b983-ee106455f3a0"), Name = "Adapter Laptop", Path = "/adapter", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("57b2461d-353e-49ce-a5d3-8a089324d151"), Name = "DELL", Path = "/adapter/dell", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("67425823-7017-4e67-aa50-0527636e6a4d"), Name = "HP", Path = "/adapter/hp", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("dfca494b-325f-4ccc-9d98-c33fc5b3173c"), Name = "HP", Path = "/adapter/hp", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("50793467-3d5e-42e0-aa5a-11f515d66e6b"), Name = "Lenovo", Path = "/adapter/lenovo", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("52d70768-b31e-42ed-870d-01325b72ddc1"), Name = "ASUS", Path = "/adapter/asus", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("5b7288dc-4045-4d69-bb8a-1e463b64db8d"), Name = "ACER", Path = "/adapter/acer", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("d3d142c9-74e2-4a24-ad16-328fc6ae2b59"), Name = "TOSHIBA", Path = "/adapter/toshiba", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("429f224b-bf56-4c2e-830a-79f0ed3ae3f3"), Name = "SONY", Path = "/adapter/sony", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("c44fdaa4-e6ac-4a58-80df-537baee6ab1e"), Name = "Macbook", Path = "/adapter/macbook", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("8debca51-ea31-4699-9b92-d72d1c4d990f"), Name = "Keo tản nhiệt", Path = "/thermal-paste", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("e3ff15cd-6adc-4f01-a0a0-10079aeb4af1"), Name = "HDD Laptop - PC", Path = "/hdd", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("2ffddc6a-52c1-4b25-8632-0eccbbd0bdc9"), Name = "HDD Laptop", Path = "/hdd/laptop", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("43f7ca1d-c33d-448c-8e8e-29d91329bfc8"), Name = "HDD PC", Path = "/hdd/pc", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("b83c2713-74b5-4610-b8e4-f05d4c89c2f1"), Name = "SSD - Ổ cứng rắn", Path = "/ssd", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("a00039d2-39c8-4ff6-98ef-3c83183e7d7f"), Name = "SSD 2.5in", Path = "/ssd/2.5", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("190d2d13-85d9-40ca-be84-007df211792e"), Name = "SSD mSata", Path = "/ssd/msata", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("9ddfcbb3-bc48-4b66-abe9-05b361dad875"), Name = "SSD M2 Sata3", Path = "/ssd/m2-sata3", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("f68ab117-976f-4ccf-bfd7-2bda43f72e2b"), Name = "SSD NVMe - PCIE", Path = "/ssd/nvme", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("eb4c1945-0218-41ae-a9d3-e24030a6f9f8"), Name = "SSD - Macbook", Path = "/ssd/macbook", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("aec6ad12-c89d-4f6d-a323-f2e507dbe2d4"), Name = "RAM - Bộ nhớ trong", Path = "/ram", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("97e44c8d-c398-4452-8a9a-66e3f5a54f46"), Name = "RAM - PC", Path = "/ram/pc", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("246a4eb8-716c-4210-bf35-014e5b37440e"), Name = "RAM - Laptop", Path = "/ram/laptop", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("7549ac63-367e-4dd8-b0d9-f2f527ed0bbb"), Name = "Pin Laptop - Macbook", Path = "/pin", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("32610e85-c98b-4640-a7cd-4ffdb8531cb4"), Name = "Pin Laptop", Path = "/pin/laptop", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("62e4b9b9-2d05-4e1a-a535-7e7f50097710"), Name = "Pin Macbook", Path = "/pin/macbook", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("3673731b-2fc1-4d3d-9a0b-ac52a0ac9fb8"), Name = "PSU - Nguồn PC", Path = "/psu", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("2b3e5af1-c218-4e1c-b6a0-5e54d0905223"), Name = "Nguồn CH New", Path = "/psu/ch-new", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("089aac0a-789b-4339-8e56-062f29e6ef27"), Name = "Nguồn Dell", Path = "/psu/dell", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("16a9ba40-b861-4062-9203-2f7d57aabd97"), Name = "Nguồn HP", Path = "/psu/hp", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("a06abcfe-fb3c-4b2a-8c39-d287a7963867"), Name = "Nguồn Lenovo", Path = "/psu/lenovo", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("7e26d06a-e329-4bb2-9e3b-0c3b93732589"), Name = "Nguồn tổng hợp", Path = "/psu/tong-hop", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("b4555973-9779-4fa0-b564-ca3368eaa33e"), Name = "Phím - chuột PC", Path = "/phim-chuot", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("5288443a-d3d9-4a6b-bd83-8af3b41837be"), Name = "Combo K + M", Path = "/phim-chuot/combo-km", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("af2f3d60-c5c0-4989-aa3c-36c4694ede03"), Name = "Bàn Phím", Path = "/phim-chuot/ban-phim", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("d691aaf6-0129-4ff1-8c4c-74dc6bc29cc5"), Name = "Chuột USB", Path = "/phim-chuot/chuot-usb", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("c4c231e5-b021-4924-b591-3fce093af85b"), Name = "Pad Mouse", Path = "/phim-chuot/mousepad", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("11342b7f-a61a-4224-8703-b60fb926c770"), Name = "Card màn hình PC", Path = "/vga-pc", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("daca376d-1508-4a12-b976-5f9412a8f67c"), Name = "Nvidia Quadro", Path = "/vga-pc/quadro", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("ce6e6b7d-680c-44c4-9c85-87989dbfeb95"), Name = "VGA GTX", Path = "/vga-pc/gtx", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("86d093b8-d21b-4f01-87be-c9d7c8b368bd"), Name = "VGA RTX", Path = "/vga-pc/rtx", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("f235b8a7-7378-4026-9af5-7d33f5970eb9"), Name = "Linh kiện điện thoại", Path = "/accessory", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("97600878-aa5b-4ac4-8e3f-fbf2ddaacf43"), Name = "Cáp sạc", Path = "/accessory/cable", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("9757b9fd-e278-439b-af23-90a00d7b790f"), Name = "Pin iphone", Path = "/accessory/pin-iphone", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("73bcdabc-a59c-46a2-844e-d4d6ad7db96d"), Name = "Pin dự phòng", Path = "/accessory/pin-du-phong", CreatedUser = null, ModifiedUser = null },
            new() { Id = new Guid("a1da03e6-25f4-4c58-9f7a-24f762327c94"), Name = "Docking laptop", Path = "/docking-laptop", CreatedUser = null, ModifiedUser = null },

        });
        _context.SaveChanges();
    }

    private void SeedProductUnit()
    {
        _context.ProductUnits.AddRange(new List<ProductUnit>
        {
            new ProductUnit {Id = new Guid("EB4A1712-88AC-4694-8941-5E7305BBC4B1"), Name = "Bộ", IsDeleted = false, CreatedUser = null, ModifiedUser = null },
            new ProductUnit {Id = new Guid("0A30FCA8-BC21-407E-9264-8EA2E5F41575"), Name = "Con", IsDeleted = false, CreatedUser = null, ModifiedUser = null },
            new ProductUnit {Id = new Guid("7035167E-2EC1-46CB-A346-9F6A20EAC535"), Name = "Cái", IsDeleted = false, CreatedUser = null, ModifiedUser = null },
            new ProductUnit {Id = new Guid("92C5A297-4635-46AD-8C9E-B5F28FD44E75"), Name = "Cm", IsDeleted = false, CreatedUser = null, ModifiedUser = null },
        });
        _context.SaveChanges();
    }

    private void SeedProductCondition()
    {
        _context.ProductConditions.AddRange(new List<ProductCondition>
        {
            new ProductCondition {Id = new Guid("9420681A-6D03-45BB-AB59-BD1AF9E3054A"), Name = "New", IsDeleted = false, CreatedUser = null, ModifiedUser = null },
            new ProductCondition {Id = new Guid("CB98AFDE-54ED-416C-A73C-18EEF6F0983B"), Name = "Like New", IsDeleted = false, CreatedUser = null, ModifiedUser = null },
            new ProductCondition {Id = new Guid("FCDF46B9-C1D6-4664-8EA4-7A1B25AB1875"), Name = "Like New Zin", IsDeleted = false, CreatedUser = null, ModifiedUser = null },
        });
        _context.SaveChanges();
    }

    private void SeedSupplier()
    {
        _context.Suppliers.AddRange(new List<Supplier>
        {
            new() {Id = new Guid("196d3520-8ffd-49e5-b0f1-f88d4b5f1b59"), Name = "NWH", Address = "", Email = "", AmountOwed = 0, Phone = "", Note = "", IsDeleted = false, CreatedUser = null, ModifiedUser = null },
            new() {Id = new Guid("33e3338c-7921-480a-89fe-c06eb62f0fd8"), Name = "CTY PATECH", Address = "", Email = "", AmountOwed = 0, Phone = "", Note = "", IsDeleted = false, CreatedUser = null, ModifiedUser = null },
            new() {Id = new Guid("aabacf68-91af-4ea3-8402-0e7b77dc2d87"), Name = "Chỉnh LK", Address = "", Email = "", AmountOwed = 0, Phone = "", Note = "", IsDeleted = false, CreatedUser = null, ModifiedUser = null },
            new() {Id = new Guid("5e4b4217-4c79-4201-98f1-3b4580b6fbec"), Name = "Tấn Phát LTK", Address = "", Email = "", AmountOwed = 0, Phone = "", Note = "", IsDeleted = false, CreatedUser = null, ModifiedUser = null },
            new() {Id = new Guid("6935c438-9cdc-421d-9105-073d6d4348b7"), Name = "Chuẩn LTK", Address = "", Email = "", AmountOwed = 0, Phone = "", Note = "", IsDeleted = false, CreatedUser = null, ModifiedUser = null },
            new() {Id = new Guid("87b15f2c-2108-4f9a-8814-7e00c98db977"), Name = "Duy Tân", Address = "", Email = "", AmountOwed = 0, Phone = "", Note = "", IsDeleted = false, CreatedUser = null, ModifiedUser = null },
            new() {Id = new Guid("69a4a5d2-4ab1-445f-8553-3ed8b6687348"), Name = "Hải Việt", Address = "", Email = "", AmountOwed = 0, Phone = "", Note = "", IsDeleted = false, CreatedUser = null, ModifiedUser = null },
            new() {Id = new Guid("9198201d-f59f-45f9-8e49-117a0e94d628"), Name = "LGT", Address = "", Email = "", AmountOwed = 0, Phone = "", Note = "", IsDeleted = false, CreatedUser = null, ModifiedUser = null },
            new() {Id = new Guid("71071d3e-2186-4949-9a41-47424c05d9d7"), Name = "Huy Phát Kingmater", Address = "", Email = "", AmountOwed = 0, Phone = "", Note = "", IsDeleted = false, CreatedUser = null, ModifiedUser = null },
            new() {Id = new Guid("f9df20ef-50ae-4834-a48a-f0724d9a8f42"), Name = "Tuấn Hiền", Address = "", Email = "", AmountOwed = 0, Phone = "", Note = "", IsDeleted = false, CreatedUser = null, ModifiedUser = null },
            new() {Id = new Guid("16bcb4bb-6181-47b3-b29b-ba51d6551a25"), Name = "Văn Hải", Address = "", Email = "", AmountOwed = 0, Phone = "", Note = "", IsDeleted = false, CreatedUser = null, ModifiedUser = null },
            new() {Id = new Guid("25064f1d-53b0-46a9-b1b5-a53d3acc17a4"), Name = "Phát Đạt LTK", Address = "", Email = "", AmountOwed = 0, Phone = "", Note = "", IsDeleted = false, CreatedUser = null, ModifiedUser = null },
        });
        _context.SaveChanges();
    }

    private void SeedMannufacturer()
    {
        _context.Manufacturers.AddRange(new List<Manufacturer>
        {
            new() {Id = new Guid("e32e08d8-32d0-49a4-af6d-e9a8b02fa7ae"), Name = "Pioneer", CreatedUser = null, ModifiedUser = null },
            new() {Id = new Guid("3af759e8-b2e0-4b02-96b6-76c5609615f6"), Name = "Colorful", CreatedUser = null, ModifiedUser = null },
            new() {Id = new Guid("8657a192-250f-46f0-addc-0806460b4ea2"), Name = "Pisen", CreatedUser = null, ModifiedUser = null },
            new() {Id = new Guid("70f6af9a-9725-47c5-b9e3-66aecd85e47b"), Name = "Newmen", CreatedUser = null, ModifiedUser = null },
            new() {Id = new Guid("179f6f9c-33d7-4f43-8b36-b9668cba2cca"), Name = "Genius", CreatedUser = null, ModifiedUser = null },
            new() {Id = new Guid("f30a01bd-2a0c-4340-bfdb-6a497aecb648"), Name = "HP", CreatedUser = null, ModifiedUser = null },
            new() {Id = new Guid("242bf112-9304-4e64-a08a-f455cd132043"), Name = "Unitek", CreatedUser = null, ModifiedUser = null },
            new() {Id = new Guid("5834857c-c47f-4995-abd0-9919515d8b35"), Name = "Energizer", CreatedUser = null, ModifiedUser = null },
            new() {Id = new Guid("36aa5768-87aa-4b7a-9fc7-c8066d136c55"), Name = "Western", CreatedUser = null, ModifiedUser = null },
            new() {Id = new Guid("49747ba2-8a30-4bb5-a2a1-49d6580e672a"), Name = "Addlink", CreatedUser = null, ModifiedUser = null },
            new() {Id = new Guid("4ae0a384-443b-4901-85b1-cfa1976cf035"), Name = "Caddy Bay", CreatedUser = null, ModifiedUser = null },
            new() {Id = new Guid("45341e2d-7e0f-45e7-ad84-f361cd69dc41"), Name = "Samsung", CreatedUser = null, ModifiedUser = null },
            new() {Id = new Guid("b9ebb845-7dd3-4522-b2ba-5fc45f0459a0"), Name = "Hoco", CreatedUser = null, ModifiedUser = null },
            new() {Id = new Guid("d6eb32c3-1339-4b15-b6bf-db38a8bb04ee"), Name = "Kingmax", CreatedUser = null, ModifiedUser = null },
            new() {Id = new Guid("18eacb75-8d7f-4833-87d9-97ba3971a46e"), Name = "Kingston", CreatedUser = null, ModifiedUser = null },
            new() {Id = new Guid("151ecbb0-dbe5-4a80-ab56-e443e145b5e0"), Name = "Seagate", CreatedUser = null, ModifiedUser = null }
        });
        _context.SaveChanges();
    }

    private void SeedUser()
    {
        _context.Users.AddRange(new List<User>
        {
            new User() {Id = new Guid("A61BB43E-5D71-416A-0187-08DB13F9E709"), FullName = "thanh", StaffCode = "thanh01", Email = "THANH01@GMAIL.COM", BonusPercent = 0, Role = "ADMIN", StoreId = new Guid("4f056739-e1dd-4f6d-9d95-1682c857110a"), Status = 0, PasswordHash = "LFmADxsBxvdZLpLEc0A8WhzQWUBolP00Khirx2E0hT98JTwBZxeyvZl09EQE5dB+GDYIyw4oBK+HqEL8spVxvA==", PasswordSalt = "tTgHdl7OVvhCf88DjPkce8E6i1BwFxJzA35UCZuv4BB873eAFRktggIIfx6Hvj3q7Nm0ZXSoHrKsmvhGLR8fVZVhS7sDISbRbdpk8stLnY5acOocELCKkirD7xrNdmPjTD79U/UPMSf+gDXe4tBuZbJe6+kzYZ48UGgkHQwvahY=", IsDeleted = false, CreatedUser = null, ModifiedUser = null }
        });
    }

    private void SeedSequence()
    {
        CreateSequence("ProductCode", 1000000);
        CreateSequence("FundCode", 1000000);
        CreateSequence("BillCode", 1000000);
        CreateSequence("FixOrderCode", 1000000);
        CreateSequence("FixProductCode", 1000000);
        CreateSequence("CustomerCode", 1000000);
    }

    private void CreateSequence(string sequenceName, int startNumber)
    {
        var script = $@"IF NOT EXISTS(SELECT * FROM sys.objects
                        WHERE object_id = OBJECT_ID(N'[dbo].[{sequenceName}]') AND type = 'SO')
                        CREATE SEQUENCE [dbo].[{sequenceName}] 
                            START WITH {startNumber}
                            INCREMENT BY 1;";
        _context.Database.ExecuteSqlRaw(script);
    }

    private async Task<bool> CheckExistTableInDatabase(string tableName)
    {
        var conn = _context.Database.GetDbConnection();
        if (conn.State.Equals(ConnectionState.Closed)) await conn.OpenAsync();
        using (var command = conn.CreateCommand())
        {
            command.CommandText = @"SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '" + _appSettings.Value.DBSchema + "' AND TABLE_NAME = '" + tableName + "'";
            return await command.ExecuteScalarAsync() != null;
        }
    }
}
