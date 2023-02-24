using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
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

            //Apply auto increment sequence AS ProductCodeIncrement for Products.Code
            modelBuilder.HasSequence<int>("ProductCodeIncrement")
                .StartsAt(1000001)
                .IncrementsBy(1);

            modelBuilder.Entity<Product>()
                .Property(o => o.Code)
                .HasDefaultValueSql("'SP' + CAST( NEXT VALUE FOR ProductCodeIncrement AS nvarchar(50) ) ");

            //Apply auto increment sequence AS FundCodeIncrement for Funds.Code
            modelBuilder.HasSequence<int>("FundCodeIncrement")
                .StartsAt(1000001)
                .IncrementsBy(1);
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
                return !dataColumnInfo.AllowSearch;
            }

            return true;
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

        public async Task<int> GetNextSequenceValue(string sequenceName)
        {
            await using SqlConnection connection = new(Database.GetConnectionString());
            connection.Open();
            await using var cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = $"SELECT NEXT VALUE FOR {sequenceName}";
            cmd.CommandType = CommandType.Text;
            var value = cmd.ExecuteScalar();
            connection.Close();
            int.TryParse(value.ToString(), out var result);

            return result;
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
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Fund> Funds { get; set; }
        public DbSet<IncomeTicket> IncomeTickets { get; set; }
        public DbSet<InputProduct> InputProducts { get; set; }
        public DbSet<InputProductItem> InputProductItems { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillItem> BillItems { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<FixProduct> FixProducts { get; set; }
        public DbSet<ExportHistory> ExportHistories { get; set; }
        public DbSet<ImportHistory> ImportHistories { get; set; }
        public DbSet<ProductHistory> ProductHistories { get; set; }
        public DbSet<Template> Templates { get; set; }

    }
}
