using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            new() {Id = Guid.NewGuid(), Name = "LCD - Màn hình Laptop"},
            new() {Id = Guid.NewGuid(), Name = "Màn hình AOC"},
            new() {Id = Guid.NewGuid(), Name = "Phần mềm diệt virus - win"},
            new() {Id = Guid.NewGuid(), Name = "VGA Laptop"},
            new() {Id = Guid.NewGuid(), Name = "CPU - Vi xử lý"},
            new() {Id = Guid.NewGuid(), Name = "ASUS"},
            new() {Id = Guid.NewGuid(), Name = "ACER"},
            new() {Id = Guid.NewGuid(), Name = "TOSHIBA"},
            new() {Id = Guid.NewGuid(), Name = "SONY"},
            new() {Id = Guid.NewGuid(), Name = "Macbook"},
            new() {Id = Guid.NewGuid(), Name = "Keo tản nhiệt"}
        });
        _context.SaveChanges();
    }

    private void SeedProductUnit()
    {
        _context.ProductUnits.AddRange(new List<ProductUnit>
        {
            new ProductUnit {Id = Guid.NewGuid(), Name = "Cái", IsDeleted = false},
            new ProductUnit {Id = Guid.NewGuid(), Name = "Bộ", IsDeleted = false},
            new ProductUnit {Id = Guid.NewGuid(), Name = "Con", IsDeleted = false},
            new ProductUnit {Id = Guid.NewGuid(), Name = "Cm", IsDeleted = false}
        });
        _context.SaveChanges();
    }

    private void SeedProductCondition()
    {
        _context.ProductConditions.AddRange(new List<ProductCondition>
        {
            new ProductCondition {Id = new Guid("9420681a-6d03-45bb-ab59-bd1af9e3054a"), Name = "Mới 100%", IsDeleted = false},
            new ProductCondition {Id = new Guid("cb98afde-54ed-416c-a73c-18eef6f0983b"), Name = "Mới 99%", IsDeleted = false},
            new ProductCondition {Id = new Guid("fcdf46b9-c1d6-4664-8ea4-7a1b25ab1875"), Name = "Mới 98%", IsDeleted = false},
            new ProductCondition {Id = new Guid("ba55e427-1ecd-4d44-a27a-a5be26b89fdd"), Name = "Cũ đã qua sửa chữa", IsDeleted = false},
            new ProductCondition {Id = new Guid("de2d4f17-b82c-47c0-9a88-a7bae70c8579"), Name = "Outlet", IsDeleted = false},
            new ProductCondition {Id = new Guid("f97327e2-d6aa-439f-991a-875460b45284"), Name = "New Outlet", IsDeleted = false}
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
