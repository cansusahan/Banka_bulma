using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace son.Models;

public partial class ListeContext : DbContext
{
    public ListeContext()
    {
    }

    public ListeContext(DbContextOptions<ListeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Banka> Bankas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   => optionsBuilder.UseSqlServer("Server=LAPTOP-7JJP5TFO;Database=Liste;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Banka>(entity =>
        {
            entity.ToTable("banka");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AltTip)
                .HasMaxLength(50)
                .HasColumnName("alt_tip");
            entity.Property(e => e.BankaAdi)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("banka_adi");
            entity.Property(e => e.BankaKodu).HasColumnName("banka_kodu");
            entity.Property(e => e.Bin).HasColumnName("bin");
            
            entity.Property(e => e.Tip)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tip");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    internal object Where(Func<object, bool> value)
    {
        throw new NotImplementedException();
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
