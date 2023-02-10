using Managerment.CashReceipts;
using Managerment.CashRecieptOthers;
using Managerment.Categories;
using Managerment.CustomCodes;
using Managerment.Customers;
using Managerment.DetailProductRepairs;
using Managerment.GoodsReceiptProducts;
using Managerment.GoodsReceipts;
using Managerment.Manufacturers;
using Managerment.Notifications;
using Managerment.OrderRepairs;
using Managerment.Orders;
using Managerment.OrderWarranties;
using Managerment.PaymentHistories;
using Managerment.PaymentMethods;
using Managerment.ProcessRepairs;
using Managerment.ProcessWarranties;
using Managerment.ProductConditions;
using Managerment.ProductRepairs;
using Managerment.Products;
using Managerment.ProductUnits;
using Managerment.ProductWarranties;
using Managerment.Roles;
using Managerment.Stores;
using Managerment.Suppliers;
using Managerment.Users;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Managerment.EntityFrameworkCore;

[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class ManagermentDbContext :AbpDbContext<ManagermentDbContext>,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

   

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }
    public DbSet<CashReceiptOther> CashReceiptOthers { get; set; }
    public DbSet<CashReceipt> CashReceipts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Customer> Customers { get; set; }
    //public DbSet<DetailProductRepair> DetailProductRepairs { get; set; }
    public DbSet<GoodsReceipt> GoodReceipts { get; set; }
    public DbSet<Manufacturer> Manufacturers { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<OrderRepair> OrderRepairs { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderWarranty> OrderWarranties { get; set; }
    public DbSet<PaymentHistory> PaymentHistories { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    public DbSet<ProductCondition> ProductConditions { get; set; }
    public DbSet<ProductRepair> ProductRepairs { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductUnit> ProductUnits { get; set; }
    public DbSet<ProductWarranty> ProductWarranties { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Store> Stores { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<CustomCode> CustomCodes { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<DetailProductRepair> DetailProductRepairs { get; set; }
    public DbSet<ProcessRepair> ProcessRepairs { get; set; }
    public DbSet<ProcessWarranty> ProcessWarranties { get; set; }
    public DbSet<GoodsReceiptProduct> GoodsReceiptProducts { get; set; }
    #endregion

    public ManagermentDbContext(DbContextOptions<ManagermentDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

  
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureTenantManagement();

        /* Configure your own tables/entities inside here */

        builder.Entity<CashReceiptOther>(b =>
        {
            b.ToTable(ManagermentConsts.DbTablePrefix + "CashReceiptOther",
                ManagermentConsts.DbSchema);

            b.ConfigureByConvention();

            b.Property(x => x.CRO_code).IsRequired().HasMaxLength(10);

            b.Property(x => x.CRO_date).IsRequired();
        });
        builder.Entity<CashReceipt>(b =>
        {
            b.ToTable(ManagermentConsts.DbTablePrefix + "CashReceipts",
                ManagermentConsts.DbSchema);

            b.ConfigureByConvention();

            b.Property(x => x.CR_code).IsRequired().HasMaxLength(10);

            b.Property(x => x.CR_date).IsRequired();
        });
        builder.Entity<Product>(b =>
        {
            b.ToTable(ManagermentConsts.DbTablePrefix + "Products",
                ManagermentConsts.DbSchema);

            b.ConfigureByConvention();

            b.Property(x => x.Pro_code).IsRequired().HasMaxLength(10);

            b.Property(x => x.Pro_name).IsRequired().HasMaxLength(255);
            b.Property(x => x.Pro_quanlity).IsRequired();
            b.Property(x => x.Pro_min).IsRequired();
            b.Property(x => x.Pro_max).IsRequired();
            b.Property(x => x.Pro_original_cost).IsRequired();
            b.Property(x => x.Pro_sell_in).IsRequired();
            b.Property(x => x.Pro_sell_out).IsRequired();
            b.Property(x => x.Pro_warranty).IsRequired().HasMaxLength(100);
            b.HasIndex(x => x.Pro_name);
            b.HasOne<ProductCondition>().WithMany().HasForeignKey(x => x.Id_con).OnDelete(DeleteBehavior.Cascade);
            b.HasOne<ProductUnit>().WithMany().HasForeignKey(x => x.Id_unit).OnDelete(DeleteBehavior.Cascade);
            b.HasOne<Supplier>().WithMany().HasForeignKey(x => x.Id_sup).OnDelete(DeleteBehavior.Cascade);
            b.HasOne<Category>().WithMany().HasForeignKey(x => x.Id_cat).OnDelete(DeleteBehavior.Cascade);
            b.HasOne<Manufacturer>().WithMany().HasForeignKey(x => x.Id_manu).OnDelete(DeleteBehavior.Cascade);
        });
        builder.Entity<Manufacturer>(b =>
        {
            b.ToTable(ManagermentConsts.DbTablePrefix + "Manufacturers",
                ManagermentConsts.DbSchema);

            b.ConfigureByConvention();

            b.Property(x => x.Name).IsRequired().HasMaxLength(255);
        });
        builder.Entity<Supplier>(b =>
        {
            b.ToTable(ManagermentConsts.DbTablePrefix + "Suppliers",
                ManagermentConsts.DbSchema);

            b.ConfigureByConvention();

            b.Property(x => x.Name).IsRequired().HasMaxLength(255);
            b.Property(x => x.Phone).IsRequired().HasMaxLength(11);
            b.Property(x => x.Address).HasMaxLength(255);
            b.Property(x => x.Email).HasMaxLength(255);
            b.Property(x => x.Tax).HasMaxLength(255);
        });
        builder.Entity<Category>(b =>
        {
            b.ToTable(ManagermentConsts.DbTablePrefix + "Categories",
                ManagermentConsts.DbSchema);

            b.ConfigureByConvention();

            b.Property(x => x.Cat_name).IsRequired().HasMaxLength(255);
            b.Property(x => x.Parent_Id).IsRequired();
        });
        builder.Entity<ProductCondition>(b =>
        {
            b.ToTable(ManagermentConsts.DbTablePrefix + "ProductConditions",
                ManagermentConsts.DbSchema);

            b.ConfigureByConvention();

            b.Property(x => x.Cond_name).IsRequired().HasMaxLength(255);
        });
        builder.Entity<ProductUnit>(b =>
        {
            b.ToTable(ManagermentConsts.DbTablePrefix + "ProductUnits",
                ManagermentConsts.DbSchema);

            b.ConfigureByConvention();

            b.Property(x => x.Unit_name).IsRequired().HasMaxLength(255);
        });

        //custom code

        builder.Entity<CustomCode>(b =>
        {
            b.ToTable(ManagermentConsts.DbTablePrefix + "CustomCodes",
                ManagermentConsts.DbSchema);

            b.ConfigureByConvention();
            b.Property(x => x.CodeName).IsRequired();
            b.Property(x => x.CodeValue).IsRequired();
        });
        builder.Entity<Store>(b =>
        {
            b.ToTable(ManagermentConsts.DbTablePrefix + "Stores",
                ManagermentConsts.DbSchema);

            b.ConfigureByConvention();
            b.Property(x => x.Store_name).IsRequired().HasMaxLength(100);
        });

        builder.Entity<Role>(b =>
        {
            b.ToTable(ManagermentConsts.DbTablePrefix + "Roles",
                ManagermentConsts.DbSchema);

            b.ConfigureByConvention();
            b.Property(x => x.Roles_name).IsRequired().HasMaxLength(100);
            b.Property(x => x.Permission).IsRequired();

        });

        builder.Entity<User>(b =>
        {
            b.ToTable(ManagermentConsts.DbTablePrefix + "Users",
                ManagermentConsts.DbSchema);

            b.ConfigureByConvention();
            b.Property(x => x.U_username).IsRequired().HasMaxLength(255);
            b.Property(x => x.U_password).IsRequired().HasMaxLength(255);
            b.Property(x => x.U_salt).IsRequired().HasMaxLength(255);
            b.Property(x => x.U_name).IsRequired().HasMaxLength(255);
            b.Property(x => x.U_email).IsRequired().HasMaxLength(255);
            b.Property(x => x.U_status).IsRequired().HasMaxLength(255);
            b.Property(x => x.U_commission).IsRequired();
            b.HasOne<Store>().WithMany().HasForeignKey(x => x.Id_store).OnDelete(DeleteBehavior.Cascade);
            b.HasOne<Role>().WithMany().HasForeignKey(x => x.Id_roles).OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Notification>(b =>
        {
            b.ToTable(ManagermentConsts.DbTablePrefix + "Notifications",
                ManagermentConsts.DbSchema);

            b.ConfigureByConvention();
            b.Property(x => x.Description).IsRequired().HasMaxLength(255);

        });
        builder.Entity<PaymentHistory>(b =>
        {
            b.ToTable(ManagermentConsts.DbTablePrefix + "PaymentHistories",
                ManagermentConsts.DbSchema);

            b.ConfigureByConvention();
            b.Property(x => x.Date_pay).IsRequired();
            b.Property(x => x.Money).IsRequired();
            b.HasOne<PaymentMethod>().WithMany().HasForeignKey(x => x.ID_payment).OnDelete(DeleteBehavior.Cascade);
        });
        //builder.Entity<DetailProductRepair>(b =>
        //{
        //    b.ToTable(ManagermentConsts.DbTablePrefix + "DetailProductRepairs",
        //        ManagermentConsts.DbSchema);

        //    b.ConfigureByConvention();
        //});
        builder.Entity<ProductRepair>(b =>
        {
            b.ToTable(ManagermentConsts.DbTablePrefix + "ProductRepairs",
                ManagermentConsts.DbSchema);

            b.ConfigureByConvention();
            b.Property(x => x.PR_code).IsRequired().HasMaxLength(10);
            b.Property(x => x.PR_name).IsRequired().HasMaxLength(255);
            b.HasOne<OrderRepair>().WithMany().HasForeignKey(x => x.ID_order_repair).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<DetailProductRepair>().WithMany().HasForeignKey(x => x.ID_detail).OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<ProductWarranty>(b =>
        {
            b.ToTable(ManagermentConsts.DbTablePrefix + "ProductWarranties",
                ManagermentConsts.DbSchema);

            b.ConfigureByConvention();
            b.Property(x => x.PW_code).IsRequired().HasMaxLength(10);
            b.Property(x => x.PW_name).IsRequired().HasMaxLength(255);
            b.HasOne<DetailProductRepair>().WithMany().HasForeignKey(x => x.ID_detail).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<OrderWarranty>().WithMany().HasForeignKey(x => x.ID_order_warranty).OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<OrderWarranty>(b =>
        {
            b.ToTable(ManagermentConsts.DbTablePrefix + "OrderWarranties",
                ManagermentConsts.DbSchema);

            b.ConfigureByConvention();
            b.Property(x => x.OW_code).IsRequired().HasMaxLength(10);
            b.HasOne<Customer>().WithMany().HasForeignKey(x => x.ID_cus).OnDelete(DeleteBehavior.Cascade);
            b.HasOne<User>().WithMany().HasForeignKey(x => x.ID_user).OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<Order>(b =>
        {
            b.ToTable(ManagermentConsts.DbTablePrefix + "Orders",
                ManagermentConsts.DbSchema);

            b.ConfigureByConvention();
            b.Property(x => x.Order_code).IsRequired().HasMaxLength(10);
            b.Property(x => x.Order_date).IsRequired();
            b.Property(x => x.Order_code).IsRequired();
            b.Property(x => x.Order_quantity).IsRequired();
            b.Property(x => x.Total_money).IsRequired();
            b.Property(x => x.Paid).IsRequired();
            b.Property(x => x.Debt).IsRequired().HasMaxLength(10);
            b.HasOne<Store>().WithMany().HasForeignKey(x => x.Id_store).OnDelete(DeleteBehavior.Cascade);
            b.HasOne<Product>().WithMany().HasForeignKey(x => x.Id_prod).OnDelete(DeleteBehavior.Cascade);
            b.HasOne<User>().WithMany().HasForeignKey(x => x.Id_user).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<Customer>().WithMany().HasForeignKey(x => x.Id_cus).OnDelete(DeleteBehavior.Cascade);
            b.HasOne<PaymentHistory>().WithMany().HasForeignKey(x => x.Id_history).OnDelete(DeleteBehavior.Cascade);
            b.HasOne<OrderRepair>().WithMany().HasForeignKey(x => x.Id_repair).OnDelete(DeleteBehavior.Cascade);
        });
        builder.Entity<GoodsReceipt>(b =>
        {
            b.ToTable(ManagermentConsts.DbTablePrefix + "GoodsReceipts",
                ManagermentConsts.DbSchema);

            b.ConfigureByConvention();
            b.Property(x => x.Receipt_code).IsRequired().HasMaxLength(10);
            b.Property(x => x.Receipt_date).IsRequired();
            b.Property(x => x.Total_price).IsRequired();
            b.Property(x => x.Total_money).IsRequired();
            b.Property(x => x.Paid).IsRequired();
            b.Property(x => x.Debt).IsRequired();
            b.HasOne<Store>().WithMany().HasForeignKey(x => x.Id_store).OnDelete(DeleteBehavior.Cascade);
            b.HasOne<User>().WithMany().HasForeignKey(x => x.Id_user).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<Supplier>().WithMany().HasForeignKey(x => x.Id_supplier).OnDelete(DeleteBehavior.NoAction);
        });
        //customer
        builder.Entity<Customer>(b =>
        {
            b.ToTable(ManagermentConsts.DbTablePrefix + "Customers",
                ManagermentConsts.DbSchema);

            b.ConfigureByConvention();

            b.Property(x => x.C_code).IsRequired().HasMaxLength(10);

            b.Property(x => x.C_name).IsRequired().HasMaxLength(255);
            b.Property(x => x.C_phone).IsRequired().HasMaxLength(11);
            //b.Property(x => x.C_address).IsRequired().HasMaxLength(255);
            //b.Property(x => x.C_gender).IsRequired();
            //b.Property(x => x.C_birthday).IsRequired();
            //b.Property(x => x.Customer_type).IsRequired();
        });
        builder.Entity<OrderRepair>(b =>
        {
            b.ToTable(ManagermentConsts.DbTablePrefix + "OrderRepairs",
                ManagermentConsts.DbSchema);

            b.ConfigureByConvention();

            b.Property(x => x.OR_code).IsRequired().HasMaxLength(10);
            b.HasOne<Customer>().WithMany().HasForeignKey(x => x.ID_cus).OnDelete(DeleteBehavior.NoAction);
            b.HasOne<User>().WithMany().HasForeignKey(x => x.ID_user).OnDelete(DeleteBehavior.NoAction);
        });
        builder.Entity<DetailProductRepair>(b =>
        {
            b.ToTable(ManagermentConsts.DbTablePrefix + "DetailProductRepairs",
                ManagermentConsts.DbSchema);

            b.ConfigureByConvention();
        });
        builder.Entity<ProcessRepair>(b =>
        {
            b.ToTable(ManagermentConsts.DbTablePrefix + "ProcessRepairs",
                ManagermentConsts.DbSchema);

            b.ConfigureByConvention();
            b.HasOne<OrderRepair>().WithMany().HasForeignKey(x => x.Id_order_repair).OnDelete(DeleteBehavior.Cascade);
        });
        builder.Entity<ProcessWarranty>(b =>
        {
            b.ToTable(ManagermentConsts.DbTablePrefix + "ProcessWarranties",
                ManagermentConsts.DbSchema);

            b.ConfigureByConvention();
            b.HasOne<OrderWarranty>().WithMany().HasForeignKey(x => x.Order_warranty_id).OnDelete(DeleteBehavior.Cascade);
        });
        builder.Entity<GoodsReceiptProduct>(b =>
        {
            b.ToTable(ManagermentConsts.DbTablePrefix + "GoodsReceiptProducts",
                ManagermentConsts.DbSchema);

            b.ConfigureByConvention();
            b.HasOne<Product>().WithMany().HasForeignKey(x => x.Product_id).OnDelete(DeleteBehavior.Cascade);
            b.HasOne<GoodsReceipt>().WithMany().HasForeignKey(x => x.Goods_receipt_id).OnDelete(DeleteBehavior.Cascade);
        });
    }
}
