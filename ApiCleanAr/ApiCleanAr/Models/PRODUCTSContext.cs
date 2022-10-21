using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ApiCleanAr.Models
{
    public partial class PRODUCTSContext : DbContext
    {
        public PRODUCTSContext()
        {
        }

        public PRODUCTSContext(DbContextOptions<PRODUCTSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.IdCategory)
                    .HasName("PK__CATEGORY__CBD747067EEAD3D5");

                entity.ToTable("CATEGORY");

                entity.Property(e => e.DescriptionCategory)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Description_Category");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.IdProducts)
                    .HasName("PK__PRODUCTS__0988921CC4EA368F");

                entity.ToTable("PRODUCTS");

                entity.Property(e => e.DescriptionProduct)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Description_Product");

                entity.Property(e => e.NameProducts)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Sctok).HasColumnName("sctok");

                entity.HasOne(d => d.oCategory)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.IdCategory)
                    .HasConstraintName("FK_IDCATEGORY");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
