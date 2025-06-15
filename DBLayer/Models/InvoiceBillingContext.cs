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

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceProduct> InvoiceProducts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=InvoiceBilling;User Id=InvoiceUser;Password=invoice123;Encrypt=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64D86467035F");

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
            entity.HasKey(e => e.InvoiceId).HasName("PK__Invoice__D796AAB553E16F26");

            entity.ToTable("Invoice");

            entity.Property(e => e.CId)
                .HasDefaultValue(0)
                .HasColumnName("C_Id");
            entity.Property(e => e.CTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("C_Time");
            entity.Property(e => e.Ewaybill).HasMaxLength(100);
            entity.Property(e => e.InvoiceNo).HasMaxLength(10);
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("isActive");
            entity.Property(e => e.MId).HasColumnName("M_Id");
            entity.Property(e => e.MTime)
                .HasColumnType("datetime")
                .HasColumnName("M_Time");
            entity.Property(e => e.TotalCost).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.VehicelNo)
                .HasMaxLength(100)
                .HasColumnName("vehicelNo");

            entity.HasOne(d => d.Customer).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Invoice__Custome__09A971A2");
        });

        modelBuilder.Entity<InvoiceProduct>(entity =>
        {
            entity.HasKey(e => e.InvoiceProdId).HasName("PK__InvoiceP__624ED00A9BFF22EC");

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
            entity.Property(e => e.Quantity).HasDefaultValue(1);
            entity.Property(e => e.UnitCost).HasColumnType("numeric(18, 0)");

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceProducts)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__InvoicePr__Invoi__160F4887");

            entity.HasOne(d => d.Product).WithMany(p => p.InvoiceProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__InvoicePr__Produ__17036CC0");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__B40CC6CDD07A0043");

            entity.ToTable("Product");

            entity.HasIndex(e => e.Hsncode, "UQ__Product__7DEC4BAFE7104F64").IsUnique();

            entity.HasIndex(e => e.ProductName, "UQ__Product__DD5A978A62487805").IsUnique();

            entity.Property(e => e.CId)
                .HasDefaultValue(0)
                .HasColumnName("C_Id");
            entity.Property(e => e.CTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("C_Time");
            entity.Property(e => e.Cost).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.Hsncode)
                .HasMaxLength(100)
                .HasColumnName("HSNCode");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("isActive");
            entity.Property(e => e.MId).HasColumnName("M_Id");
            entity.Property(e => e.MTime)
                .HasColumnType("datetime")
                .HasColumnName("M_Time");
            entity.Property(e => e.ProductName).HasMaxLength(100);
            entity.Property(e => e.ProductNo).HasMaxLength(10);
            entity.Property(e => e.Quantity).HasDefaultValue(1);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
