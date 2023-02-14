using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using TechFix.EntityModels.Handle;

namespace TechFix.EntityModels
{
    public class UserInfo
    {
        public Guid? CurrentUserId { get; set; }
        public string IpAddress { get; set; }
        public string Username { get; set; }
        public Guid? StoreId { get; set; }
    }

    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public UserInfo UserInfo { get; set; } = new();

		public DataContext()
		{
		}

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
	        var connectionString = Configuration.GetConnectionString("WebApiDatabase");
            options.UseSqlServer(connectionString);
            //options.EnableSensitiveDataLogging();
            base.OnConfiguring(options);
        }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            HandleView(modelBuilder);
            HandleList(modelBuilder);
            HandleRowVersion(modelBuilder);

            //Apply seed data
            modelBuilder.Entity<Manufacturer>().HasData(
                new Manufacturer { Id = new Guid("e32e08d8-32d0-49a4-af6d-e9a8b02fa7ae"), Name = "Pioneer" },
                new Manufacturer { Id = new Guid("3af759e8-b2e0-4b02-96b6-76c5609615f6"), Name = "Colorful" },
                new Manufacturer { Id = new Guid("8657a192-250f-46f0-addc-0806460b4ea2"), Name = "Pisen" },
                new Manufacturer { Id = new Guid("70f6af9a-9725-47c5-b9e3-66aecd85e47b"), Name = "Newmen" },
                new Manufacturer { Id = new Guid("179f6f9c-33d7-4f43-8b36-b9668cba2cca"), Name = "Genius" },
                new Manufacturer { Id = new Guid("f30a01bd-2a0c-4340-bfdb-6a497aecb648"), Name = "HP" },
                new Manufacturer { Id = new Guid("242bf112-9304-4e64-a08a-f455cd132043"), Name = "Unitek" },
                new Manufacturer { Id = new Guid("5834857c-c47f-4995-abd0-9919515d8b35"), Name = "Energizer" },
                new Manufacturer { Id = new Guid("36aa5768-87aa-4b7a-9fc7-c8066d136c55"), Name = "Western" },
                new Manufacturer { Id = new Guid("49747ba2-8a30-4bb5-a2a1-49d6580e672a"), Name = "Addlink" },
                new Manufacturer { Id = new Guid("4ae0a384-443b-4901-85b1-cfa1976cf035"), Name = "Caddy Bay" },
                new Manufacturer { Id = new Guid("45341e2d-7e0f-45e7-ad84-f361cd69dc41"), Name = "Samsung" },
                new Manufacturer { Id = new Guid("b9ebb845-7dd3-4522-b2ba-5fc45f0459a0"), Name = "Hoco" },
                new Manufacturer { Id = new Guid("d6eb32c3-1339-4b15-b6bf-db38a8bb04ee"), Name = "Kingmax" },
                new Manufacturer { Id = new Guid("18eacb75-8d7f-4833-87d9-97ba3971a46e"), Name = "Kingston" },
                new Manufacturer { Id = new Guid("151ecbb0-dbe5-4a80-ab56-e443e145b5e0"), Name = "Seagate" }
            );

            modelBuilder.Entity<Supplier>().HasData(
                new Supplier { Id = new Guid("196d3520-8ffd-49e5-b0f1-f88d4b5f1b59"), Name = "NWH", Address = "", Email = "", InDebt = 0, Phone = "", Note = "", IsDeleted = false },
                new Supplier { Id = new Guid("33e3338c-7921-480a-89fe-c06eb62f0fd8"), Name = "CTY PATECH", Address = "", Email = "", InDebt = 0, Phone = "", Note = "", IsDeleted = false },
                new Supplier { Id = new Guid("aabacf68-91af-4ea3-8402-0e7b77dc2d87"), Name = "Chỉnh LK", Address = "", Email = "", InDebt = 0, Phone = "", Note = "", IsDeleted = false },
                new Supplier { Id = Guid.NewGuid(), Name = "Tấn Phát LTK", Address = "", Email = "", InDebt = 0, Phone = "", Note = "", IsDeleted = false },
                new Supplier { Id = Guid.NewGuid(), Name = "Chuẩn LTK", Address = "", Email = "", InDebt = 0, Phone = "", Note = "", IsDeleted = false },
                new Supplier { Id = Guid.NewGuid(), Name = "Duy Tân", Address = "", Email = "", InDebt = 0, Phone = "", Note = "", IsDeleted = false },
                new Supplier { Id = Guid.NewGuid(), Name = "Hải Việt", Address = "", Email = "", InDebt = 0, Phone = "", Note = "", IsDeleted = false },
                new Supplier { Id = Guid.NewGuid(), Name = "LGT", Address = "", Email = "", InDebt = 0, Phone = "", Note = "", IsDeleted = false },
                new Supplier { Id = Guid.NewGuid(), Name = "Huy Phát Kingmater", Address = "", Email = "", InDebt = 0, Phone = "", Note = "", IsDeleted = false },
                new Supplier { Id = Guid.NewGuid(), Name = "Tuấn Hiền", Address = "", Email = "", InDebt = 0, Phone = "", Note = "", IsDeleted = false },
                new Supplier { Id = Guid.NewGuid(), Name = "Văn Hải", Address = "", Email = "", InDebt = 0, Phone = "", Note = "", IsDeleted = false },
                new Supplier { Id = Guid.NewGuid(), Name = "Phát Đạt LTK", Address = "", Email = "", InDebt = 0, Phone = "", Note = "", IsDeleted = false }
            );

            modelBuilder.Entity<Store>().HasData(new Store { Id = Guid.NewGuid(), Name = "TechFix", Address = "", Phone = "0123456778", IsDeleted = false });

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = Guid.NewGuid(), Name = "LCD - Màn hình Laptop" },
                new Category { Id = Guid.NewGuid(), Name = "Màn hình AOC" },
                new Category { Id = Guid.NewGuid(), Name = "Phần mềm diệt virus - win" },
                new Category { Id = Guid.NewGuid(), Name = "VGA Laptop" },
                new Category { Id = Guid.NewGuid(), Name = "CPU - Vi xử lý" },
                new Category { Id = Guid.NewGuid(), Name = "ASUS" },
                new Category { Id = Guid.NewGuid(), Name = "ACER" },
                new Category { Id = Guid.NewGuid(), Name = "TOSHIBA" },
                new Category { Id = Guid.NewGuid(), Name = "SONY" },
                new Category { Id = Guid.NewGuid(), Name = "Macbook" },
                new Category { Id = Guid.NewGuid(), Name = "Keo tản nhiệt" }
            );

            modelBuilder.Entity<ProductUnit>().HasData(
                new ProductUnit { Id = Guid.NewGuid(), Name = "Cái", IsDeleted = false },
                new ProductUnit { Id = Guid.NewGuid(), Name = "Bộ", IsDeleted = false },
                new ProductUnit { Id = Guid.NewGuid(), Name = "Con", IsDeleted = false },
                new ProductUnit { Id = Guid.NewGuid(), Name = "Cm", IsDeleted = false }
            );

            modelBuilder.Entity<ProductCondition>().HasData(
                new ProductCondition { Id = new Guid("9420681a-6d03-45bb-ab59-bd1af9e3054a"), Name = "Mới 100%", IsDeleted = false },
                new ProductCondition { Id = new Guid("cb98afde-54ed-416c-a73c-18eef6f0983b"), Name = "Mới 99%", IsDeleted = false },
                new ProductCondition { Id = new Guid("fcdf46b9-c1d6-4664-8ea4-7a1b25ab1875"), Name = "Mới 98%", IsDeleted = false },
                new ProductCondition { Id = new Guid("ba55e427-1ecd-4d44-a27a-a5be26b89fdd"), Name = "Cũ đã qua sửa chữa", IsDeleted = false },
                new ProductCondition { Id = new Guid("de2d4f17-b82c-47c0-9a88-a7bae70c8579"), Name = "Outlet", IsDeleted = false },
                new ProductCondition { Id = new Guid("f97327e2-d6aa-439f-991a-875460b45284"), Name = "New Outlet", IsDeleted = false }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { 
                    Id = Guid.NewGuid(),
                    ManufacturerId = new Guid("e32e08d8-32d0-49a4-af6d-e9a8b02fa7ae"),
                    SupplierId = new Guid("196d3520-8ffd-49e5-b0f1-f88d4b5f1b59"),
                    ProductConditionId = new Guid("9420681a-6d03-45bb-ab59-bd1af9e3054a"),
                    Code = "SP0000010",
                    Name = "Áo thun hai lỗ",
                    Description = "",
                    OriginalCost = 50000,
                    Quantity = 5,
                    Warranty = "Bảo hành 6Th",
                    MinimumNorm = 5,
                    MaximumNorm = 10,
                    SellIn = 50000,
                    SellOut = 70000,
                    AllowNegativeSell = true,
                    IsInventoryTracking = false,
                    IsDeleted = false 
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    ManufacturerId = new Guid("3af759e8-b2e0-4b02-96b6-76c5609615f6"),
                    SupplierId = new Guid("196d3520-8ffd-49e5-b0f1-f88d4b5f1b59"),
                    ProductConditionId = new Guid("cb98afde-54ed-416c-a73c-18eef6f0983b"),
                    Code = "SP000003",
                    Name = "Cáp Pisen USB Type-C 3A 1m",
                    Description = "",
                    OriginalCost = 90000,
                    SellIn = 90000,
                    SellOut = 130000,
                    Quantity = 8,
                    Warranty = "06TH",
                    MinimumNorm = 0,
                    MaximumNorm = 0,
                    AllowNegativeSell = false,
                    IsInventoryTracking = true,
                    IsDeleted = false
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    ManufacturerId = new Guid("e32e08d8-32d0-49a4-af6d-e9a8b02fa7ae"),
                    SupplierId = new Guid("196d3520-8ffd-49e5-b0f1-f88d4b5f1b59"),
                    ProductConditionId = new Guid("fcdf46b9-c1d6-4664-8ea4-7a1b25ab1875"),
                    Code = "SP0000011",
                    Name = "Sạc dự phòng Pisen Color Power Pro 10000mAh đỏ-đen (Dual USB 1A/2.4A Smart)",
                    Description = "",
                    OriginalCost = 550000,
                    SellIn = 550000,
                    SellOut = 650000,
                    Quantity = 0,
                    Warranty = "12TH",
                    MinimumNorm = 0,
                    MaximumNorm = 0,
                    AllowNegativeSell = true,
                    IsInventoryTracking = false,
                    IsDeleted = false
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    ManufacturerId = new Guid("70f6af9a-9725-47c5-b9e3-66aecd85e47b"),
                    SupplierId = new Guid("196d3520-8ffd-49e5-b0f1-f88d4b5f1b59"),
                    ProductConditionId = new Guid("fcdf46b9-c1d6-4664-8ea4-7a1b25ab1875"),
                    Code = "SP0000022",
                    Name = "Chuột Newmen F300 không dây",
                    Description = "",
                    OriginalCost = 215000,
                    SellIn = 215000,
                    SellOut = 270000,
                    Quantity = 3,
                    Warranty = "12TH",
                    MinimumNorm = 3,
                    MaximumNorm = 5,
                    AllowNegativeSell = true,
                    IsInventoryTracking = false,
                    IsDeleted = false
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    ManufacturerId = new Guid("e32e08d8-32d0-49a4-af6d-e9a8b02fa7ae"),
                    SupplierId = new Guid("196d3520-8ffd-49e5-b0f1-f88d4b5f1b59"),
                    ProductConditionId = new Guid("fcdf46b9-c1d6-4664-8ea4-7a1b25ab1875"),
                    Code = "SP0000032",
                    Name = "Caddybay mỏng 9.5mm Laptop",
                    Description = "",
                    OriginalCost = 35000,
                    SellIn = 35000,
                    SellOut = 100000,
                    Quantity = 99,
                    Warranty = "3TH",
                    MinimumNorm = 1,
                    MaximumNorm = 5,
                    AllowNegativeSell = true,
                    IsInventoryTracking = true,
                    IsDeleted = false
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    ManufacturerId = new Guid("e32e08d8-32d0-49a4-af6d-e9a8b02fa7ae"),
                    SupplierId = new Guid("196d3520-8ffd-49e5-b0f1-f88d4b5f1b59"),
                    ProductConditionId = new Guid("fcdf46b9-c1d6-4664-8ea4-7a1b25ab1875"),
                    Code = "SP0000041",
                    Name = "SSD Pioneer APS-SL3N 120GB 2.5in ( Read 520MB/s - Write 400MB/s )",
                    Description = "",
                    OriginalCost = 440000,
                    SellIn = 440000,
                    SellOut = 550000,
                    Quantity = 0,
                    Warranty = "12TH",
                    MinimumNorm = 0,
                    MaximumNorm = 0,
                    AllowNegativeSell = false,
                    IsInventoryTracking = false,
                    IsDeleted = true
                }
            );
        }

        private void HandleList(ModelBuilder modelBuilder)
        {
        }

        private static void HandleView(ModelBuilder modelBuilder)
        {
          
        }

        private void HandleRowVersion(ModelBuilder modelBuilder)
        {
            
        }

        public void TrackChangedEntity()
		{

			var entries = ChangeTracker
				.Entries()
				.Where(e => e.Entity is BaseModel && (
					e.State == EntityState.Added || e.State == EntityState.Modified));

			foreach (var entityEntry in entries)
			{

				var baseModel = (BaseModel) entityEntry.Entity;
				var classCustomAttribute = GetCustomAttribute(entityEntry.Entity.GetType(), typeof(EntityClassAttribute));
				if (classCustomAttribute != null && ((EntityClassAttribute) classCustomAttribute).FullTextSearch)
				{
					baseModel.SearchData = GetSearchDataValue(entityEntry, baseModel);
				}
                else
                {
                    baseModel.SearchData = "";
                }

                baseModel.ModifiedDate = DateTime.Now;
				baseModel.ModifiedUser = UserInfo.CurrentUserId ?? Guid.Empty;
				if (entityEntry.State == EntityState.Added)
				{
					baseModel.CreatedDate = DateTime.Now;
					baseModel.CreatedUser = UserInfo.CurrentUserId ?? Guid.Empty;
				}
			}
		}

		private static string GetSearchDataValue(EntityEntry entityEntry, BaseModel baseModel)
		{
			var properties = entityEntry.Entity.GetType().GetProperties();
			var values = new List<string>();
			foreach (var propertyInfo in properties)
			{
				if (IsIgnoreType(propertyInfo))
					continue;

				var value = propertyInfo.GetValue(baseModel, null);
				if (value != null && propertyInfo.Name != "SearchData")
				{
					values.Add(value.ToString());
				}
			}

			return string.Join("`", values);
		}

		private static bool IsIgnoreType(PropertyInfo propertyInfo)
		{
			var ignoredTypes = new List<Type> {typeof(bool), typeof(bool?), typeof(List<Guid>), typeof(Guid), typeof(Guid?), typeof(DateTime), typeof(DateTime?), typeof(decimal)};

			//Ignore List
			if (propertyInfo.PropertyType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType))
				return true;
			//Ignore no need type
			if (propertyInfo.PropertyType.FullName != null && (propertyInfo.PropertyType.FullName.StartsWith("VlinkNet") || ignoredTypes.Any(i => propertyInfo.PropertyType.FullName == i.FullName)))
				return true;

			//Ignore fields setup ignore by custom attribute
			var customAttribute = propertyInfo.GetCustomAttributes(typeof(DataColumnAttribute), false).FirstOrDefault();
            if (customAttribute != null)
            {
                var dataColumnInfo = (DataColumnAttribute) customAttribute;
                if (dataColumnInfo.IgnoreSearch)
                    return true;
            }

            return false;
		}

		private static object GetCustomAttribute(Type entityType, Type attributeType, bool inherit = false)
        {
	        var attribute = entityType.GetCustomAttributes(attributeType, inherit).FirstOrDefault();
	        return attribute;
        }

        public override int SaveChanges()
        {
            TrackChangedEntity();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            TrackChangedEntity();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            TrackChangedEntity();
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void ConfigureConventions(
            ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<decimal>()
                .HavePrecision(38, 16);
        }

        public int? GetNextSequenceValue(string sequenceName)
        {
            try
            {
                //todo
                return 1;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<VlinkSequence> VlinkSequence { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCondition> ProductConditions { get; set; }
        public DbSet<ProductUnit> ProductUnits { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
