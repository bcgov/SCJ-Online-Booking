using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph.ExternalConnectors;
using SCJ.Booking.CourtBookingPrototype.Models;

namespace SCJ.Booking.CourtBookingPrototype
{
    public partial class SCJ_CourtBookingContext : DbContext
    {
        private string _connectionString;

        public SCJ_CourtBookingContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }

        public SCJ_CourtBookingContext(DbContextOptions<SCJ_CourtBookingContext> options, IConfiguration configuration)
            : base(options)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }

        public virtual DbSet<BookingPeriod> BookingPeriods { get; set; }
        public virtual DbSet<CaseBookingRequest> CaseBookingRequests { get; set; }
        public virtual DbSet<DateSelection> DateSelections { get; set; }
        public virtual DbSet<RegistrySetting> RegistrySettings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookingPeriod>(entity =>
            {
                entity.ToTable("BookingPeriod");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ClosingDate).HasColumnType("date");

                entity.Property(e => e.OpeningDate).HasColumnType("date");
            });

            modelBuilder.Entity<CaseBookingRequest>(entity =>
            {
                entity.ToTable("CaseBookingRequest");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.PhysicalFileId).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.TrialLength).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<DateSelection>(entity =>
            {
                entity.ToTable("DateSelection");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.HasOne(d => d.CaseBookingRequest)
                    .WithMany(p => p.DateSelections)
                    .HasForeignKey(d => d.CaseBookingRequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DateSelection_CaseBookingRequest");
            });

            modelBuilder.Entity<RegistrySetting>(entity =>
            {
                entity.ToTable("RegistrySetting");

                entity.Property(e => e.BookingEndTime).HasColumnType("datetime");

                entity.Property(e => e.BookingStartTime).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
