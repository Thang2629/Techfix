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
	        base.OnConfiguring(options);
        }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            HandleView(modelBuilder);
            HandleList(modelBuilder);
            HandleRowVersion(modelBuilder);
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

    }
}
