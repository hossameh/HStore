﻿// <auto-generated />
using System;
using HStore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HStore.Migrations
{
    [DbContext(typeof(HStoreDBContext))]
    [Migration("20210109111150_1st")]
    partial class _1st
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HStore.AspNetRoleClaims", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("HStore.AspNetRoles", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("([NormalizedName] IS NOT NULL)");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("HStore.AspNetUserClaims", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("HStore.AspNetUserLogins", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("HStore.AspNetUserRoles", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("HStore.AspNetUserTokens", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("HStore.AspNetUsers", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("([NormalizedUserName] IS NOT NULL)");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("HStore.Clients", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int?>("CreationBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime");

                    b.Property<bool?>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.Property<string>("Mobile")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<decimal?>("TotalPaid")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<decimal?>("TotalRemaining")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("HStore.ClientsPayments", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<int?>("CreationBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime");

                    b.Property<bool?>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.Property<string>("PaymentComment")
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime");

                    b.Property<decimal>("PaymentValue")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("UserId");

                    b.ToTable("ClientsPayments");
                });

            modelBuilder.Entity("HStore.PurchaseRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CreationBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime");

                    b.Property<string>("InvoiceNumber")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<bool?>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.Property<decimal?>("Paid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(18, 2)")
                        .HasDefaultValueSql("((0))");

                    b.Property<DateTime?>("PurchaseDate")
                        .HasColumnType("datetime");

                    b.Property<decimal?>("Remaining")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(18, 2)")
                        .HasDefaultValueSql("((0))");

                    b.Property<int>("SupplierId")
                        .HasColumnType("int");

                    b.Property<decimal?>("TotalAmount")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("decimal(19, 2)")
                        .HasComputedColumnSql("(isnull([Paid],(0))+isnull([Remaining],(0)))");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("SupplierId");

                    b.HasIndex("UserId");

                    b.ToTable("PurchaseRequest");
                });

            modelBuilder.Entity("HStore.PurchaseRequestDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CreationBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime");

                    b.Property<bool?>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<decimal?>("PurchasePrice")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(18, 2)")
                        .HasDefaultValueSql("((0))");

                    b.Property<int?>("PurchaseQuantity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("((0))");

                    b.Property<int>("PurchaseRequestId")
                        .HasColumnType("int");

                    b.Property<decimal?>("PurchaseTotalAmount")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("decimal(29, 2)")
                        .HasComputedColumnSql("(isnull([PurchasePrice],(0))*isnull([PurchaseQuantity],(0)))");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("PurchaseRequestId");

                    b.HasIndex("UserId");

                    b.ToTable("PurchaseRequestDetails");
                });

            modelBuilder.Entity("HStore.SellRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<int?>("CreationBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime");

                    b.Property<string>("InvoiceNumber")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<bool?>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.Property<decimal?>("Paid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(18, 2)")
                        .HasDefaultValueSql("((0))");

                    b.Property<decimal?>("Remaining")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(18, 2)")
                        .HasDefaultValueSql("((0))");

                    b.Property<DateTime?>("SellDate")
                        .HasColumnType("datetime");

                    b.Property<decimal?>("TotalAmount")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("decimal(19, 2)")
                        .HasComputedColumnSql("(isnull([Paid],(0))+isnull([Remaining],(0)))");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("UserId");

                    b.ToTable("SellRequest");
                });

            modelBuilder.Entity("HStore.SellRequestDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CreationBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime");

                    b.Property<bool?>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<decimal?>("SellPrice")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(18, 2)")
                        .HasDefaultValueSql("((0))");

                    b.Property<int?>("SellQuantity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("((0))");

                    b.Property<int>("SellRequestId")
                        .HasColumnType("int");

                    b.Property<decimal?>("SellTotalAmount")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("decimal(29, 2)")
                        .HasComputedColumnSql("(isnull([SellPrice],(0))*isnull([SellQuantity],(0)))");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("SellRequestId");

                    b.HasIndex("UserId");

                    b.ToTable("SellRequestDetails");
                });

            modelBuilder.Entity("HStore.StoreItems", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int?>("CreationBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<DateTime?>("ExpirationDate")
                        .HasColumnType("datetime");

                    b.Property<bool?>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(350)")
                        .HasMaxLength(350);

                    b.Property<DateTime?>("ProductionDate")
                        .HasColumnType("datetime");

                    b.Property<decimal?>("Quantity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(18, 0)")
                        .HasDefaultValueSql("((0))");

                    b.Property<decimal?>("StorePrice")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("decimal(37, 2)")
                        .HasComputedColumnSql("(isnull([TodayPrice],(0))*isnull([Quantity],(0)))");

                    b.Property<decimal?>("TodayPrice")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(18, 2)")
                        .HasDefaultValueSql("((0))");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("StoreItems");
                });

            modelBuilder.Entity("HStore.Suppliers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int?>("CreationBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime");

                    b.Property<bool?>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.Property<string>("Mobile")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<decimal?>("TotalPaid")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<decimal?>("TotalRemaining")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("HStore.SuppliersPayments", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CreationBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime");

                    b.Property<bool?>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.Property<string>("PaymentComment")
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime");

                    b.Property<decimal>("PaymentValue")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("SupplierId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("SupplierId");

                    b.HasIndex("UserId");

                    b.ToTable("SuppliersPayments");
                });

            modelBuilder.Entity("HStore.AspNetRoleClaims", b =>
                {
                    b.HasOne("HStore.AspNetRoles", "Role")
                        .WithMany("AspNetRoleClaims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HStore.AspNetUserClaims", b =>
                {
                    b.HasOne("HStore.AspNetUsers", "User")
                        .WithMany("AspNetUserClaims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HStore.AspNetUserLogins", b =>
                {
                    b.HasOne("HStore.AspNetUsers", "User")
                        .WithMany("AspNetUserLogins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HStore.AspNetUserRoles", b =>
                {
                    b.HasOne("HStore.AspNetRoles", "Role")
                        .WithMany("AspNetUserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HStore.AspNetUsers", "User")
                        .WithMany("AspNetUserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HStore.AspNetUserTokens", b =>
                {
                    b.HasOne("HStore.AspNetUsers", "User")
                        .WithMany("AspNetUserTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HStore.Clients", b =>
                {
                    b.HasOne("HStore.AspNetUsers", "User")
                        .WithMany("Clients")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("HStore.ClientsPayments", b =>
                {
                    b.HasOne("HStore.Clients", "Client")
                        .WithMany("ClientsPayments")
                        .HasForeignKey("ClientId")
                        .HasConstraintName("FK_ClientsPayments_Clients")
                        .IsRequired();

                    b.HasOne("HStore.AspNetUsers", "User")
                        .WithMany("ClientsPayments")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("HStore.PurchaseRequest", b =>
                {
                    b.HasOne("HStore.Suppliers", "Supplier")
                        .WithMany("PurchaseRequest")
                        .HasForeignKey("SupplierId")
                        .HasConstraintName("FK_PurchaseRequest_Suppliers")
                        .IsRequired();

                    b.HasOne("HStore.AspNetUsers", "User")
                        .WithMany("PurchaseRequests")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("HStore.PurchaseRequestDetails", b =>
                {
                    b.HasOne("HStore.StoreItems", "Item")
                        .WithMany("PurchaseRequestDetails")
                        .HasForeignKey("ItemId")
                        .HasConstraintName("FK_PurchaseRequestDetails_StoreItems")
                        .IsRequired();

                    b.HasOne("HStore.PurchaseRequest", "PurchaseRequest")
                        .WithMany("PurchaseRequestDetails")
                        .HasForeignKey("PurchaseRequestId")
                        .HasConstraintName("FK_PurchaseRequestDetails_PurchaseRequest")
                        .IsRequired();

                    b.HasOne("HStore.AspNetUsers", "User")
                        .WithMany("PurchaseRequestDetails")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("HStore.SellRequest", b =>
                {
                    b.HasOne("HStore.Clients", "Client")
                        .WithMany("SellRequest")
                        .HasForeignKey("ClientId")
                        .HasConstraintName("FK_SellRequest_Clients")
                        .IsRequired();

                    b.HasOne("HStore.AspNetUsers", "User")
                        .WithMany("SellRequests")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("HStore.SellRequestDetails", b =>
                {
                    b.HasOne("HStore.StoreItems", "Item")
                        .WithMany("SellRequestDetails")
                        .HasForeignKey("ItemId")
                        .HasConstraintName("FK_SellRequestDetails_StoreItems")
                        .IsRequired();

                    b.HasOne("HStore.SellRequest", "SellRequest")
                        .WithMany("SellRequestDetails")
                        .HasForeignKey("SellRequestId")
                        .HasConstraintName("FK_SellRequestDetails_SellRequest")
                        .IsRequired();

                    b.HasOne("HStore.AspNetUsers", "User")
                        .WithMany("SellRequestDetails")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("HStore.StoreItems", b =>
                {
                    b.HasOne("HStore.AspNetUsers", "User")
                        .WithMany("StoreItems")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("HStore.Suppliers", b =>
                {
                    b.HasOne("HStore.AspNetUsers", "User")
                        .WithMany("Suppliers")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("HStore.SuppliersPayments", b =>
                {
                    b.HasOne("HStore.Suppliers", "Supplier")
                        .WithMany("SuppliersPayments")
                        .HasForeignKey("SupplierId")
                        .HasConstraintName("FK_SuppliersPayments_Suppliers")
                        .IsRequired();

                    b.HasOne("HStore.AspNetUsers", "User")
                        .WithMany("SuppliersPayments")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
