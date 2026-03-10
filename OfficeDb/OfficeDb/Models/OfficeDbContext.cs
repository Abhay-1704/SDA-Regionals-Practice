using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OfficeDb.Models;

public partial class OfficeDbContext : DbContext
{
    public OfficeDbContext()
    {
    }

    public OfficeDbContext(DbContextOptions<OfficeDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<OfficeSupply> OfficeSupplies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=OfficeDb;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OfficeSupply>(entity =>
        {
            entity.HasKey(e => e.ItemId);

            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.Department).HasMaxLength(100);
            entity.Property(e => e.ItemName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
