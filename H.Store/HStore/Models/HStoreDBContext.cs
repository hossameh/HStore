using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HStore
{
    public partial class HStoreDBContext : DbContext
    {
        public HStoreDBContext()
        {
        }

        public HStoreDBContext(DbContextOptions<HStoreDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<ClientsPayments> ClientsPayments { get; set; }
        public virtual DbSet<PurchaseRequest> PurchaseRequest { get; set; }
        public virtual DbSet<PurchaseRequestDetails> PurchaseRequestDetails { get; set; }
        public virtual DbSet<SellRequest> SellRequest { get; set; }
        public virtual DbSet<SellRequestDetails> SellRequestDetails { get; set; }
        public virtual DbSet<StoreItems> StoreItems { get; set; }
        public virtual DbSet<Suppliers> Suppliers { get; set; }
        public virtual DbSet<SuppliersPayments> SuppliersPayments { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=N-5CG9225F8M;Database=HStoreDB;Trusted_Connection=True;MultipleActiveResultSets=true");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Clients>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Mobile).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.TotalPaid).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalRemaining).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<ClientsPayments>(entity =>
            {
                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.PaymentComment).HasMaxLength(2000);

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentValue).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientsPayments)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientsPayments_Clients");
            });

            modelBuilder.Entity<PurchaseRequest>(entity =>
            {
                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.InvoiceNumber).HasMaxLength(50);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Paid)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PurchaseDate).HasColumnType("datetime");

                entity.Property(e => e.Remaining)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TotalAmount)
                    .HasColumnType("decimal(19, 2)")
                    .HasComputedColumnSql("(isnull([Paid],(0))+isnull([Remaining],(0)))");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.PurchaseRequest)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PurchaseRequest_Suppliers");
            });

            modelBuilder.Entity<PurchaseRequestDetails>(entity =>
            {
                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.PurchasePrice)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PurchaseQuantity).HasDefaultValueSql("((0))");

                entity.Property(e => e.PurchaseTotalAmount)
                    .HasColumnType("decimal(29, 2)")
                    .HasComputedColumnSql("(isnull([PurchasePrice],(0))*isnull([PurchaseQuantity],(0)))");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.PurchaseRequestDetails)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PurchaseRequestDetails_StoreItems");

                entity.HasOne(d => d.PurchaseRequest)
                    .WithMany(p => p.PurchaseRequestDetails)
                    .HasForeignKey(d => d.PurchaseRequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PurchaseRequestDetails_PurchaseRequest");
            });

            modelBuilder.Entity<SellRequest>(entity =>
            {
                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.InvoiceNumber).HasMaxLength(50);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Paid)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Remaining)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SellDate).HasColumnType("datetime");

                entity.Property(e => e.TotalAmount)
                    .HasColumnType("decimal(19, 2)")
                    .HasComputedColumnSql("(isnull([Paid],(0))+isnull([Remaining],(0)))");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.SellRequest)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SellRequest_Clients");
            });

            modelBuilder.Entity<SellRequestDetails>(entity =>
            {
                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.SellPrice)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SellQuantity).HasDefaultValueSql("((0))");

                entity.Property(e => e.SellTotalAmount)
                    .HasColumnType("decimal(29, 2)")
                    .HasComputedColumnSql("(isnull([SellPrice],(0))*isnull([SellQuantity],(0)))");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.SellRequestDetails)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SellRequestDetails_StoreItems");

                entity.HasOne(d => d.SellRequest)
                    .WithMany(p => p.SellRequestDetails)
                    .HasForeignKey(d => d.SellRequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SellRequestDetails_SellRequest");
            });

            modelBuilder.Entity<StoreItems>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).HasMaxLength(350);

                entity.Property(e => e.ProductionDate).HasColumnType("datetime");

                entity.Property(e => e.Quantity)
                    .HasColumnType("decimal(18, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.StorePrice)
                    .HasColumnType("decimal(37, 2)")
                    .HasComputedColumnSql("(isnull([TodayPrice],(0))*isnull([Quantity],(0)))");

                entity.Property(e => e.TodayPrice)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Suppliers>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Mobile).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.TotalPaid).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalRemaining).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<SuppliersPayments>(entity =>
            {
                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.PaymentComment).HasMaxLength(2000);

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentValue).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.SuppliersPayments)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SuppliersPayments_Suppliers");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
