using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DBLayer.Models;

public partial class InvoiceBillingContext : DbContext
{
    public InvoiceBillingContext()
    {
    }

    public InvoiceBillingContext(DbContextOptions<InvoiceBillingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Constant> Constants { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceProduct> InvoiceProducts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionString");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Constant>(entity =>
        {
            entity.HasKey(e => e.ConstantCode).HasName("PK__Constant__4327FF3BE7EE5D53");

            entity.ToTable("Constant");

            entity.Property(e => e.ConstantCode).ValueGeneratedOnAdd();
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.ConstantName).HasMaxLength(50);
            entity.Property(e => e.CreatedBy).HasDefaultValue(1);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ShortName).HasMaxLength(50);
            entity.Property(e => e.Status).HasDefaultValue(true);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64D8F05D7341");

            entity.Property(e => e.Address)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.CId)
                .HasDefaultValue(0)
                .HasColumnName("C_Id");
            entity.Property(e => e.CTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("C_Time");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ContactNo)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.CustomerName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CustomerNo)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.EmailId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("emailId");
            entity.Property(e => e.Gstno)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("GSTNo");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("isActive");
            entity.Property(e => e.MId).HasColumnName("M_Id");
            entity.Property(e => e.MTime)
                .HasColumnType("datetime")
                .HasColumnName("M_Time");
            entity.Property(e => e.PinCode)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.State)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("PK__Invoice__D796AAB5C6E6D9E0");

            entity.ToTable("Invoice");

            entity.Property(e => e.CId)
                .HasDefaultValue(0)
                .HasColumnName("C_Id");
            entity.Property(e => e.CTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("C_Time");
            entity.Property(e => e.Ewaybill)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.InvoiceNo)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("isActive");
            entity.Property(e => e.MId).HasColumnName("M_Id");
            entity.Property(e => e.MTime)
                .HasColumnType("datetime")
                .HasColumnName("M_Time");
            entity.Property(e => e.TotalCost).HasColumnType("numeric(18, 0)");

            entity.HasOne(d => d.Customer).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Invoice__Custome__45F365D3");
        });

        modelBuilder.Entity<InvoiceProduct>(entity =>
        {
            entity.HasKey(e => e.InvoiceProdId).HasName("PK__InvoiceP__624ED00AE789E8A9");

            entity.Property(e => e.CId)
                .HasDefaultValue(0)
                .HasColumnName("C_Id");
            entity.Property(e => e.CTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("C_Time");
            entity.Property(e => e.Cost).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.MId).HasColumnName("M_Id");
            entity.Property(e => e.MTime)
                .HasColumnType("datetime")
                .HasColumnName("M_Time");

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceProducts)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__InvoicePr__Invoi__4AB81AF0");

            entity.HasOne(d => d.Product).WithMany(p => p.InvoiceProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__InvoicePr__Produ__4BAC3F29");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__B40CC6CD74E15BA6");

            entity.ToTable("Product");

            entity.HasIndex(e => e.Hsncode, "UQ__Product__7DEC4BAFD981B670").IsUnique();

            entity.HasIndex(e => e.ProductName, "UQ__Product__DD5A978A2C235DEF").IsUnique();

            entity.Property(e => e.CId)
                .HasDefaultValue(0)
                .HasColumnName("C_Id");
            entity.Property(e => e.CTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("C_Time");
            entity.Property(e => e.Cost).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.Hsncode)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("HSNCode");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("isActive");
            entity.Property(e => e.MId).HasColumnName("M_Id");
            entity.Property(e => e.MTime)
                .HasColumnType("datetime")
                .HasColumnName("M_Time");
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ProductNo)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CE4C0FB34");

            entity.HasIndex(e => e.Name, "UQ__Users__737584F6E8DB4FCC").IsUnique();

            entity.Property(e => e.CTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("C_time");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("isActive");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
