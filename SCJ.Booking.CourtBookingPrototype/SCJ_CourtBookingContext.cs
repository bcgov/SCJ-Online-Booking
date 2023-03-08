using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph.ExternalConnectors;
using SCJ.Booking.CourtBookingPrototype.Models;

namespace SCJ.Booking.CourtBookingPrototype
{
    public class SCJ_CourtBookingContext : DbContext
    {
        private string _connectionString;

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
    }
}
