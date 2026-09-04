using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace InvoiceSystemApp.Models;

public partial class InvoiceSystemDbContext : DbContext
{
    public InvoiceSystemDbContext()
    {
    }

    public InvoiceSystemDbContext(DbContextOptions<InvoiceSystemDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Tblcustomer> Tblcustomers { get; set; }

    public virtual DbSet<TblinvoiceDetail> TblinvoiceDetails { get; set; }

    public virtual DbSet<TblinvoicePayment> TblinvoicePayments { get; set; }

    public virtual DbSet<TblinvoiceProduct> TblinvoiceProducts { get; set; }

    public virtual DbSet<Tblproduct> Tblproducts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-VRMFV23\\SQLSERVER;Database=InvoiceSystemDB;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tblcustomer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__tblcusto__A4AE64D83CD64F01");

            entity.ToTable("tblcustomers");

            entity.Property(e => e.City)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.CustomerName)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblinvoiceDetail>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("PK__tblinvoi__D796AAB51BB6B1F7");

            entity.ToTable("tblinvoice_details");

            entity.HasOne(d => d.Customer).WithMany(p => p.TblinvoiceDetails)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkcustid");
        });

        modelBuilder.Entity<TblinvoicePayment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__tblinvoi__9B556A38B2104899");

            entity.ToTable("tblinvoice_payments");

            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentDescription)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PaymentMode)
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.HasOne(d => d.Invoice).WithMany(p => p.TblinvoicePayments)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkinvoiceidpayment");
        });

        modelBuilder.Entity<TblinvoiceProduct>(entity =>
        {
            entity.HasKey(e => e.InvoiceProductId).HasName("PK__tblinvoi__D032D0C901B75A42");

            entity.ToTable("tblinvoice_products");

            entity.HasOne(d => d.Invoice).WithMany(p => p.TblinvoiceProducts)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkinvoiceid");

            entity.HasOne(d => d.Product).WithMany(p => p.TblinvoiceProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pkprid");
        });

        modelBuilder.Entity<Tblproduct>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__tblprodu__B40CC6CD76A66FCC");

            entity.ToTable("tblproducts");

            entity.HasIndex(e => e.ProductName, "UQ__tblprodu__DD5A978A1C0F0A47").IsUnique();

            entity.Property(e => e.ProductName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
