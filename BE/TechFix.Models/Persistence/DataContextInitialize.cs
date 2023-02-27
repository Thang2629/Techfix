using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TechFix.EntityModels.Persistence;

public class DataContextInitialize
{
    private readonly ILogger<DataContextInitialize> _logger;
    private readonly DataContext _context;

    public DataContextInitialize(
        ILogger<DataContextInitialize> logger,
        DataContext context)
    {
        _logger = logger;
        _context = context;
    }

    public void SeedData()
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

            if(!_context.Categories.Any())
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
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    private void SeedStore()
    {
        _context.Stores.Add(new Store {Id = new Guid("4f056739-e1dd-4f6d-9d95-1682c857110a"), Name = "TechFix", Address = "", Phone = "0123456778", IsDeleted = false});
        _context.SaveChanges();
    }

    private void SeedCategory()
    {
        _context.Categories.AddRange(new List<Category>
        {
            new() { Id = new Guid("5937b3fe-a1ef-4d83-9c91-2927a0c00185"), Name = "LCD - Màn hình Laptop", Path = "/lcd" },
            new() { Id = new Guid("3e107806-2a75-4eed-9987-dc88a69a031a"), Name = "Màn hình AOC", Path = "/lcd/aoc" },
            new() { Id = new Guid("1406750b-1862-4c8f-8eb0-cc6299a7e28e"), Name = "Phần mềm Virus - Win", Path = "/virus-win" },
            new() { Id = new Guid("51ccd309-6ac0-4ecd-814b-c7d06abca651"), Name = "Card VGA Laptop", Path = "/vga-laptop" },
            new() { Id = new Guid("9f119605-7dcc-4d80-ac69-f07f860a93e8"), Name = "CPU Vi xử lý", Path = "/cpu" },
            new() { Id = new Guid("a09590c7-fa52-446b-a3a4-5768e4fd4e95"), Name = "Box HDD - CaddyBay Laptop", Path = "/hdd-caddybay" },
            new() { Id = new Guid("76879d8a-5602-4840-96a6-769d83816a54"), Name = "Cáp chuyển đổi tín hiệu", Path = "/cable" },
            new() { Id = new Guid("b31a8971-b446-43f2-b983-ee106455f3a0"), Name = "Adapter Laptop", Path = "/adapter" },
            new() { Id = new Guid("57b2461d-353e-49ce-a5d3-8a089324d151"), Name = "DELL", Path = "/adapter/dell" },
            new() { Id = new Guid("67425823-7017-4e67-aa50-0527636e6a4d"), Name = "HP", Path = "/adapter/hp" },
            new() { Id = new Guid("dfca494b-325f-4ccc-9d98-c33fc5b3173c"), Name = "HP", Path = "/adapter/hp" },
            new() { Id = new Guid("50793467-3d5e-42e0-aa5a-11f515d66e6b"), Name = "Lenovo", Path = "/adapter/lenovo" },
            new() { Id = new Guid("62e44abf-e3cb-4c83-81e9-7867c13ad4a3"), Name = "L1", Path = "/adapter/lenovo/l1" },
            new() { Id = new Guid("c905a591-3349-4aed-b8fc-0e20dc7aec43"), Name = "L1.1", Path = "/adapter/lenovo/l1/l1.1" },
            new() { Id = new Guid("0bef5e3a-fd1d-4690-8af1-3c4756617778"), Name = "L1.2", Path = "/adapter/lenovo/l1/l1.2" },
            new() { Id = new Guid("52d70768-b31e-42ed-870d-01325b72ddc1"), Name = "ASUS", Path = "/adapter/asus" },
            new() { Id = new Guid("5b7288dc-4045-4d69-bb8a-1e463b64db8d"), Name = "ACER", Path = "/adapter/acer" },
            new() { Id = new Guid("d3d142c9-74e2-4a24-ad16-328fc6ae2b59"), Name = "TOSHIBA", Path = "/adapter/toshiba" },
            new() { Id = new Guid("429f224b-bf56-4c2e-830a-79f0ed3ae3f3"), Name = "SONY", Path = "/adapter/sony" },
            new() { Id = new Guid("c44fdaa4-e6ac-4a58-80df-537baee6ab1e"), Name = "Macbook", Path = "/adapter/macbook" },
            new() { Id = new Guid("8debca51-ea31-4699-9b92-d72d1c4d990f"), Name = "Keo tản nhiệt", Path = "/thermal-paste" },
            new() { Id = new Guid("e3ff15cd-6adc-4f01-a0a0-10079aeb4af1"), Name = "HDD Laptop - PC", Path = "/hdd" },
            new() { Id = new Guid("2ffddc6a-52c1-4b25-8632-0eccbbd0bdc9"), Name = "HDD Laptop", Path = "/hdd/laptop" },
            new() { Id = new Guid("43f7ca1d-c33d-448c-8e8e-29d91329bfc8"), Name = "HDD PC", Path = "/hdd/pc" },
            new() { Id = new Guid("b83c2713-74b5-4610-b8e4-f05d4c89c2f1"), Name = "SSD - Ổ cứng rắn", Path = "/ssd" },
            new() { Id = new Guid("a00039d2-39c8-4ff6-98ef-3c83183e7d7f"), Name = "SSD 2.5in", Path = "/ssd/2.5" },
            new() { Id = new Guid("190d2d13-85d9-40ca-be84-007df211792e"), Name = "SSD mSata", Path = "/ssd/msata" },
            new() { Id = new Guid("9ddfcbb3-bc48-4b66-abe9-05b361dad875"), Name = "SSD M2 Sata3", Path = "/ssd/m2-sata3" },
            new() { Id = new Guid("f68ab117-976f-4ccf-bfd7-2bda43f72e2b"), Name = "SSD NVMe - PCIE", Path = "/ssd/nvme" },
            new() { Id = new Guid("eb4c1945-0218-41ae-a9d3-e24030a6f9f8"), Name = "SSD - Macbook", Path = "/ssd/macbook" },
            new() { Id = new Guid("aec6ad12-c89d-4f6d-a323-f2e507dbe2d4"), Name = "RAM - Bộ nhớ trong", Path = "/ram" },
            new() { Id = new Guid("97e44c8d-c398-4452-8a9a-66e3f5a54f46"), Name = "RAM - PC", Path = "/ram/pc" },
            new() { Id = new Guid("246a4eb8-716c-4210-bf35-014e5b37440e"), Name = "RAM - Laptop", Path = "/ram/laptop" },
            new() { Id = new Guid("7549ac63-367e-4dd8-b0d9-f2f527ed0bbb"), Name = "Pin Laptop - Macbook", Path = "/pin" },
            new() { Id = new Guid("32610e85-c98b-4640-a7cd-4ffdb8531cb4"), Name = "Pin Laptop", Path = "/pin/laptop" },
            new() { Id = new Guid("62e4b9b9-2d05-4e1a-a535-7e7f50097710"), Name = "Pin Macbook", Path = "/pin/macbook" },
            new() { Id = new Guid("3673731b-2fc1-4d3d-9a0b-ac52a0ac9fb8"), Name = "PSU - Nguồn PC", Path = "/psu" },
            new() { Id = new Guid("2b3e5af1-c218-4e1c-b6a0-5e54d0905223"), Name = "Nguồn CH New", Path = "/psu/ch-new" },
            new() { Id = new Guid("089aac0a-789b-4339-8e56-062f29e6ef27"), Name = "Nguồn Dell", Path = "/psu/dell" },
            new() { Id = new Guid("16a9ba40-b861-4062-9203-2f7d57aabd97"), Name = "Nguồn HP", Path = "/psu/hp" },
            new() { Id = new Guid("a06abcfe-fb3c-4b2a-8c39-d287a7963867"), Name = "Nguồn Lenovo", Path = "/psu/lenovo" },
            new() { Id = new Guid("7e26d06a-e329-4bb2-9e3b-0c3b93732589"), Name = "Nguồn tổng hợp", Path = "/psu/tong-hop" },
            new() { Id = new Guid("b4555973-9779-4fa0-b564-ca3368eaa33e"), Name = "Phím - chuột PC", Path = "/phim-chuot" },
            new() { Id = new Guid("5288443a-d3d9-4a6b-bd83-8af3b41837be"), Name = "Combo K + M", Path = "/phim-chuot/combo-km" },
            new() { Id = new Guid("af2f3d60-c5c0-4989-aa3c-36c4694ede03"), Name = "Bàn Phím", Path = "/phim-chuot/ban-phim" },
            new() { Id = new Guid("d691aaf6-0129-4ff1-8c4c-74dc6bc29cc5"), Name = "Chuột USB", Path = "/phim-chuot/chuot-usb" },
            new() { Id = new Guid("c4c231e5-b021-4924-b591-3fce093af85b"), Name = "Pad Mouse", Path = "/phim-chuot/mousepad" },
            new() { Id = new Guid("11342b7f-a61a-4224-8703-b60fb926c770"), Name = "Card màn hình PC", Path = "/vga-pc" },
            new() { Id = new Guid("daca376d-1508-4a12-b976-5f9412a8f67c"), Name = "Nvidia Quadro", Path = "/vga-pc/quadro" },
            new() { Id = new Guid("ce6e6b7d-680c-44c4-9c85-87989dbfeb95"), Name = "VGA GTX", Path = "/vga-pc/gtx" },
            new() { Id = new Guid("86d093b8-d21b-4f01-87be-c9d7c8b368bd"), Name = "VGA RTX", Path = "/vga-pc/rtx" },
            new() { Id = new Guid("f235b8a7-7378-4026-9af5-7d33f5970eb9"), Name = "Linh kiện điện thoại", Path = "/accessory" },
            new() { Id = new Guid("97600878-aa5b-4ac4-8e3f-fbf2ddaacf43"), Name = "Cáp sạc", Path = "/accessory/cable" },
            new() { Id = new Guid("9757b9fd-e278-439b-af23-90a00d7b790f"), Name = "Pin iphone", Path = "/accessory/pin-iphone" },
            new() { Id = new Guid("73bcdabc-a59c-46a2-844e-d4d6ad7db96d"), Name = "Pin dự phòng", Path = "/accessory/pin-du-phong" },
            new() { Id = new Guid("a1da03e6-25f4-4c58-9f7a-24f762327c94"), Name = "Docking laptop", Path = "/docking-laptop" },

        });
        _context.SaveChanges();
    }

    private void SeedProductUnit()
    {
        _context.ProductUnits.AddRange(new List<ProductUnit>
        {
            new ProductUnit {Id = new Guid("EB4A1712-88AC-4694-8941-5E7305BBC4B1"), Name = "Bộ", IsDeleted = false},
            new ProductUnit {Id = new Guid("0A30FCA8-BC21-407E-9264-8EA2E5F41575"), Name = "Con", IsDeleted = false},
            new ProductUnit {Id = new Guid("7035167E-2EC1-46CB-A346-9F6A20EAC535"), Name = "Cái", IsDeleted = false},
            new ProductUnit {Id = new Guid("92C5A297-4635-46AD-8C9E-B5F28FD44E75"), Name = "Cm", IsDeleted = false}
        });
        _context.SaveChanges();
    }

    private void SeedProductCondition()
    {
        _context.ProductConditions.AddRange(new List<ProductCondition>
        {
            new ProductCondition {Id = new Guid("9420681A-6D03-45BB-AB59-BD1AF9E3054A"), Name = "Mới 100%", IsDeleted = false},
            new ProductCondition {Id = new Guid("CB98AFDE-54ED-416C-A73C-18EEF6F0983B"), Name = "Mới 99%", IsDeleted = false},
            new ProductCondition {Id = new Guid("FCDF46B9-C1D6-4664-8EA4-7A1B25AB1875"), Name = "Mới 98%", IsDeleted = false},
            new ProductCondition {Id = new Guid("BA55E427-1ECD-4D44-A27A-A5BE26B89FDD"), Name = "Cũ đã qua sửa chữa", IsDeleted = false},
            new ProductCondition {Id = new Guid("DE2D4F17-B82C-47C0-9A88-A7BAE70C8579"), Name = "Outlet", IsDeleted = false},
            new ProductCondition {Id = new Guid("F97327E2-D6AA-439F-991A-875460B45284"), Name = "New Outlet", IsDeleted = false}
        });
        _context.SaveChanges();
    }

    private void SeedSupplier()
    {
        _context.Suppliers.AddRange(new List<Supplier>
        {
            new() {Id = new Guid("196d3520-8ffd-49e5-b0f1-f88d4b5f1b59"), Name = "NWH", Address = "", Email = "", InDebt = 0, Phone = "", Note = "", IsDeleted = false},
            new() {Id = new Guid("33e3338c-7921-480a-89fe-c06eb62f0fd8"), Name = "CTY PATECH", Address = "", Email = "", InDebt = 0, Phone = "", Note = "", IsDeleted = false},
            new() {Id = new Guid("aabacf68-91af-4ea3-8402-0e7b77dc2d87"), Name = "Chỉnh LK", Address = "", Email = "", InDebt = 0, Phone = "", Note = "", IsDeleted = false},
            new() {Id = new Guid("5e4b4217-4c79-4201-98f1-3b4580b6fbec"), Name = "Tấn Phát LTK", Address = "", Email = "", InDebt = 0, Phone = "", Note = "", IsDeleted = false},
            new() {Id = new Guid("6935c438-9cdc-421d-9105-073d6d4348b7"), Name = "Chuẩn LTK", Address = "", Email = "", InDebt = 0, Phone = "", Note = "", IsDeleted = false},
            new() {Id = new Guid("87b15f2c-2108-4f9a-8814-7e00c98db977"), Name = "Duy Tân", Address = "", Email = "", InDebt = 0, Phone = "", Note = "", IsDeleted = false},
            new() {Id = new Guid("69a4a5d2-4ab1-445f-8553-3ed8b6687348"), Name = "Hải Việt", Address = "", Email = "", InDebt = 0, Phone = "", Note = "", IsDeleted = false},
            new() {Id = new Guid("9198201d-f59f-45f9-8e49-117a0e94d628"), Name = "LGT", Address = "", Email = "", InDebt = 0, Phone = "", Note = "", IsDeleted = false},
            new() {Id = new Guid("71071d3e-2186-4949-9a41-47424c05d9d7"), Name = "Huy Phát Kingmater", Address = "", Email = "", InDebt = 0, Phone = "", Note = "", IsDeleted = false},
            new() {Id = new Guid("f9df20ef-50ae-4834-a48a-f0724d9a8f42"), Name = "Tuấn Hiền", Address = "", Email = "", InDebt = 0, Phone = "", Note = "", IsDeleted = false},
            new() {Id = new Guid("16bcb4bb-6181-47b3-b29b-ba51d6551a25"), Name = "Văn Hải", Address = "", Email = "", InDebt = 0, Phone = "", Note = "", IsDeleted = false},
            new() {Id = new Guid("25064f1d-53b0-46a9-b1b5-a53d3acc17a4"), Name = "Phát Đạt LTK", Address = "", Email = "", InDebt = 0, Phone = "", Note = "", IsDeleted = false}
        });
        _context.SaveChanges();
    }

    private void SeedMannufacturer()
    {
        _context.Manufacturers.AddRange(new List<Manufacturer>
        {
            new() {Id = new Guid("e32e08d8-32d0-49a4-af6d-e9a8b02fa7ae"), Name = "Pioneer"},
            new() {Id = new Guid("3af759e8-b2e0-4b02-96b6-76c5609615f6"), Name = "Colorful"},
            new() {Id = new Guid("8657a192-250f-46f0-addc-0806460b4ea2"), Name = "Pisen"},
            new() {Id = new Guid("70f6af9a-9725-47c5-b9e3-66aecd85e47b"), Name = "Newmen"},
            new() {Id = new Guid("179f6f9c-33d7-4f43-8b36-b9668cba2cca"), Name = "Genius"},
            new() {Id = new Guid("f30a01bd-2a0c-4340-bfdb-6a497aecb648"), Name = "HP"},
            new() {Id = new Guid("242bf112-9304-4e64-a08a-f455cd132043"), Name = "Unitek"},
            new() {Id = new Guid("5834857c-c47f-4995-abd0-9919515d8b35"), Name = "Energizer"},
            new() {Id = new Guid("36aa5768-87aa-4b7a-9fc7-c8066d136c55"), Name = "Western"},
            new() {Id = new Guid("49747ba2-8a30-4bb5-a2a1-49d6580e672a"), Name = "Addlink"},
            new() {Id = new Guid("4ae0a384-443b-4901-85b1-cfa1976cf035"), Name = "Caddy Bay"},
            new() {Id = new Guid("45341e2d-7e0f-45e7-ad84-f361cd69dc41"), Name = "Samsung"},
            new() {Id = new Guid("b9ebb845-7dd3-4522-b2ba-5fc45f0459a0"), Name = "Hoco"},
            new() {Id = new Guid("d6eb32c3-1339-4b15-b6bf-db38a8bb04ee"), Name = "Kingmax"},
            new() {Id = new Guid("18eacb75-8d7f-4833-87d9-97ba3971a46e"), Name = "Kingston"},
            new() {Id = new Guid("151ecbb0-dbe5-4a80-ab56-e443e145b5e0"), Name = "Seagate"}
        });
        _context.SaveChanges();
    }


    private void SeedSequence()
    {
        CreateSequence("ProductCode", 100000);
        CreateSequence("BillCode", 1000000);
        CreateSequence("FixOrderCode", 1000000);
        CreateSequence("FixProductCode", 1000000);
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
}
