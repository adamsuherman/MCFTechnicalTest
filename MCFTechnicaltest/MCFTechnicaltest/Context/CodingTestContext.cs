using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MCFTechnicaltest.Models.CodingTest;

namespace MCFTechnicaltest.Context
{
    public partial class CodingTestContext : DbContext
    {
        public CodingTestContext(DbContextOptions<CodingTestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ms_storage_location> ms_storage_location { get; set; } = null!;
        public virtual DbSet<ms_user> ms_user { get; set; } = null!;
        public virtual DbSet<tr_bpkb> tr_bpkb { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ms_storage_location>(entity =>
            {
                entity.HasKey(e => e.location_id);

                entity.Property(e => e.location_id)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.location_name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ms_user>(entity =>
            {
                entity.HasKey(e => e.user_id);

                entity.Property(e => e.user_id).ValueGeneratedNever();

                entity.Property(e => e.password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.user_name)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<tr_bpkb>(entity =>
            {
                entity.HasKey(e => e.agreement_number);

                entity.Property(e => e.agreement_number)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.bpkb_date).HasColumnType("datetime");

                entity.Property(e => e.bpkb_date_in).HasColumnType("datetime");

                entity.Property(e => e.bpkb_no)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.branch_id)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.created_by)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.created_on).HasColumnType("datetime");

                entity.Property(e => e.faktur_date).HasColumnType("datetime");

                entity.Property(e => e.faktur_no)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.last_updated_by)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.last_updated_on).HasColumnType("datetime");

                entity.Property(e => e.location_id)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.policy_no)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
