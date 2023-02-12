﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TechFix.EntityModels;

#nullable disable

namespace TechFix.EntityModels.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230212170509_AddProductAndAssociatedTables")]
    partial class AddProductAndAssociatedTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TechFix.EntityModels.Category", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ModifiedUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SearchData")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("TechFix.EntityModels.Manufacturer", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ModifiedUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SearchData")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Manufacturers");
                });

            modelBuilder.Entity("TechFix.EntityModels.Product", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("AllowNegativeSell")
                        .HasColumnType("bit");

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsInventoryTracking")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ManufacturerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("MaximumNorm")
                        .HasColumnType("int");

                    b.Property<int>("MinimumNorm")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ModifiedUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("OriginalCost")
                        .HasPrecision(38, 16)
                        .HasColumnType("decimal(38,16)");

                    b.Property<Guid?>("ProductConditionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProductUnitId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("SearchData")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("SellIn")
                        .HasPrecision(38, 16)
                        .HasColumnType("decimal(38,16)");

                    b.Property<decimal>("SellOut")
                        .HasPrecision(38, 16)
                        .HasColumnType("decimal(38,16)");

                    b.Property<Guid?>("StoreId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SupplierId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Warranty")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ManufacturerId");

                    b.HasIndex("ProductConditionId");

                    b.HasIndex("ProductUnitId");

                    b.HasIndex("StoreId");

                    b.HasIndex("SupplierId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("TechFix.EntityModels.ProductCondition", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ModifiedUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SearchData")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ProductConditions");
                });

            modelBuilder.Entity("TechFix.EntityModels.ProductUnit", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ModifiedUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SearchData")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ProductUnits");
                });

            modelBuilder.Entity("TechFix.EntityModels.Store", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ModifiedUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SearchData")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Stores");
                });

            modelBuilder.Entity("TechFix.EntityModels.Supplier", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("InDebt")
                        .HasPrecision(38, 16)
                        .HasColumnType("decimal(38,16)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ModifiedUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SearchData")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("TechFix.EntityModels.User", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("BonusPercent")
                        .HasPrecision(38, 16)
                        .HasColumnType("decimal(38,16)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ModifiedUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordSalt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SearchData")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StaffCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("StoreId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TechFix.EntityModels.UserToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ExpiredDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("IpAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ModifiedUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SearchData")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("Token")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("TechFix.EntityModels.VlinkSequence", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("SequenceName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("VlinkSequence");
                });

            modelBuilder.Entity("TechFix.EntityModels.Product", b =>
                {
                    b.HasOne("TechFix.EntityModels.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("TechFix.EntityModels.Manufacturer", "Manufacturer")
                        .WithMany()
                        .HasForeignKey("ManufacturerId");

                    b.HasOne("TechFix.EntityModels.ProductCondition", "ProductCondition")
                        .WithMany()
                        .HasForeignKey("ProductConditionId");

                    b.HasOne("TechFix.EntityModels.ProductUnit", "ProductUnit")
                        .WithMany()
                        .HasForeignKey("ProductUnitId");

                    b.HasOne("TechFix.EntityModels.Store", "Store")
                        .WithMany()
                        .HasForeignKey("StoreId");

                    b.HasOne("TechFix.EntityModels.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierId");

                    b.Navigation("Category");

                    b.Navigation("Manufacturer");

                    b.Navigation("ProductCondition");

                    b.Navigation("ProductUnit");

                    b.Navigation("Store");

                    b.Navigation("Supplier");
                });
#pragma warning restore 612, 618
        }
    }
}
