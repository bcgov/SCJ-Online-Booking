using Microsoft.EntityFrameworkCore;
using SCJ.Booking.MVC.Models;

namespace SCJ.Booking.MVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<BookingHistory> BookingHistory { get; set; }
        public DbSet<BookingPeriod> BookingPeriods { get; set; }
        public DbSet<CaseBookingRequest> CaseBookingRequests { get; set; }
        public DbSet<DateSelection> DateSelections { get; set; }
        public DbSet<RegistrySetting> RegistrySettings { get; set; }
        public DbSet<CourtBookingEmail> CourtBookingEmails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookingHistory>()
                .HasKey(b => new { b.SmGovUserGuid, b.ContainerId, b.Timestamp });
        }
    }
}
