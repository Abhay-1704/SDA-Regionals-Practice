using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FleetDb.Models;

public partial class FleetDbContext : DbContext
{
    public FleetDbContext()
    {
    }

    public FleetDbContext(DbContextOptions<FleetDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=FleetDb;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasIndex(e => e.LicensePlate, "IX_Vehicles").IsUnique();

            entity.Property(e => e.VehicleId).HasColumnName("VehicleID");
            entity.Property(e => e.LastServiceDate).HasColumnType("datetime");
            entity.Property(e => e.LicensePlate)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModelName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
