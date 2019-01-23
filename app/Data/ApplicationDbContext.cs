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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookingHistory>()
                .HasKey(b => new { b.SmGovUserGuid, b.ContainerId, b.Timestamp });
        }
    }
}
