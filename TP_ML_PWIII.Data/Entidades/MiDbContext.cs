using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TP_ML_PWIII.Web.Models;

public partial class MiDbContext : DbContext
{
    public MiDbContext()
    {
    }

    public MiDbContext(DbContextOptions<MiDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cancione> Canciones { get; set; }

    public virtual DbSet<Interaccione> Interacciones { get; set; }

    public virtual DbSet<Recomendacione> Recomendaciones { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=DESKTOP-241GHB2\\SQLEXPRESS;Database=PWIII_TP;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cancione>(entity =>
        {
            entity.HasKey(e => e.IdCancion).HasName("PK__Cancione__A1358A2524436F81");
        });

        modelBuilder.Entity<Interaccione>(entity =>
        {
            entity.HasKey(e => e.IdInteraccion).HasName("PK__Interacc__ADBFF883CFCA94C9");

            entity.Property(e => e.FechaInteraccion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdCancionNavigation).WithMany(p => p.Interacciones).HasConstraintName("FK__Interacci__IdCan__4316F928");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Interacciones).HasConstraintName("FK__Interacci__IdUsu__440B1D61");
        });

        modelBuilder.Entity<Recomendacione>(entity =>
        {
            entity.HasKey(e => e.IdRecomendacion).HasName("PK__Recomend__190283E05AEFDBEC");

            entity.Property(e => e.FechaCalculo).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdUsuarioFuenteNavigation).WithMany(p => p.RecomendacioneIdUsuarioFuenteNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Recomenda__IdUsu__44FF419A");

            entity.HasOne(d => d.IdUsuarioRecomendadoNavigation).WithMany(p => p.RecomendacioneIdUsuarioRecomendadoNavigations).HasConstraintName("FK__Recomenda__IdUsu__45F365D3");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF97AF4E62CD");

            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
