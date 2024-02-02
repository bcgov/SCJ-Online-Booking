using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SCJ.Booking.Data.Models;

namespace SCJ.Booking.Data
{
    public class ApplicationDbContext : DbContext, IDataProtectionKeyContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<BookingHistory> BookingHistory => Set<BookingHistory>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<BookingHistory>()
                .HasKey(
                    b =>
                        new
                        {
                            b.SmGovUserGuid,
                            b.ContainerId,
                            b.Timestamp
                        }
                );
        }

        public DbSet<DataProtectionKey> DataProtectionKeys => Set<DataProtectionKey>();
    }
}
