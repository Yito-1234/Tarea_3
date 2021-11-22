using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TrenCr.Models
{
    public partial class trencrContext : DbContext
    {
        public trencrContext()
        {
        }

        public trencrContext(DbContextOptions<trencrContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CompraBoleto> CompraBoletos { get; set; }
        public virtual DbSet<Estacion> Estacions { get; set; }
        public virtual DbSet<Ruta> Rutas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-ERSLUL0;Initial Catalog=trencr; User ID=sa;Password=1234;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<CompraBoleto>(entity =>
            {
                entity.HasKey(e => e.IdCompra);

                entity.ToTable("compra_boletos");

                entity.Property(e => e.IdCompra).HasColumnName("id_compra");

                entity.Property(e => e.CantBoletos).HasColumnName("cant_boletos");

                entity.Property(e => e.Estacion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("estacion");

                entity.Property(e => e.Horario)
                    .HasColumnType("datetime")
                    .HasColumnName("horario");

                entity.Property(e => e.IdEstacion).HasColumnName("id_estacion");

                entity.Property(e => e.IdRuta).HasColumnName("id_ruta");

                entity.HasOne(d => d.IdEstacionNavigation)
                    .WithMany(p => p.CompraBoletos)
                    .HasForeignKey(d => d.IdEstacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_compra_boletos_estacion");

                entity.HasOne(d => d.IdRutaNavigation)
                    .WithMany(p => p.CompraBoletos)
                    .HasForeignKey(d => d.IdRuta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_compra_boletos_rutas");
            });

            modelBuilder.Entity<Estacion>(entity =>
            {
                entity.HasKey(e => e.IdEstacion);

                entity.ToTable("estacion");

                entity.Property(e => e.IdEstacion).HasColumnName("id_estacion");

                entity.Property(e => e.CantBoletos).HasColumnName("cant_boletos");

                entity.Property(e => e.EspaciosDisponibles).HasColumnName("espacios_disponibles");

                entity.Property(e => e.Horario)
                    .HasColumnType("datetime")
                    .HasColumnName("horario");

                entity.Property(e => e.IdRuta).HasColumnName("id_ruta");

                entity.Property(e => e.NomEstacion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nom_estacion");

                entity.HasOne(d => d.IdRutaNavigation)
                    .WithMany(p => p.Estacions)
                    .HasForeignKey(d => d.IdRuta)
                    .HasConstraintName("FK_estacion_rutas");
            });

            modelBuilder.Entity<Ruta>(entity =>
            {
                entity.HasKey(e => e.IdRuta);

                entity.ToTable("rutas");

                entity.Property(e => e.IdRuta).HasColumnName("id_ruta");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
