using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using NeuralNet.Db.Entities;

namespace NeuralNet.Db
{
    public partial class NeuralNetTrainingContext : DbContext
    {
        public NeuralNetTrainingContext()
        {
        }

        public NeuralNetTrainingContext(DbContextOptions<NeuralNetTrainingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<NGram> Ngrams { get; set; } = null!;
        public virtual DbSet<NGramCategory> NgramCategories { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                throw new Exception("Configure Services you dingus");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NGram>(entity =>
            {
                entity.ToTable("NGram");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Value)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.NGrams)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NGram_NGramCategory");
            });

            modelBuilder.Entity<NGramCategory>(entity =>
            {
                entity.ToTable("NGramCategory");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
