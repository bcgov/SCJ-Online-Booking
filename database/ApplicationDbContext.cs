using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SCJ.Booking.Data.Models;

namespace SCJ.Booking.Data
{
    public class ApplicationDbContext : DbContext, IDataProtectionKeyContext
    {
        private readonly string? _connectionString;
        private readonly string? _provider;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public ApplicationDbContext(string connectionString, string provider)
        {
            _connectionString = connectionString;
            _provider = provider;
        }

        public DbSet<BookingHistory> BookingHistory => Set<BookingHistory>();

        public DbSet<OidcUser> Users => Set<OidcUser>();

        public DbSet<ScTrialBookingRequest> ScTrialBookingRequests => Set<ScTrialBookingRequest>();

        public DbSet<ScTrialDateSelection> ScTrialDateSelections => Set<ScTrialDateSelection>();

        public DbSet<QueuedEmail> EmailQueue => Set<QueuedEmail>();

        public DbSet<DataProtectionKey> DataProtectionKeys => Set<DataProtectionKey>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrEmpty(_connectionString))
            {
                if (_provider == "sqlite")
                {
                    optionsBuilder.UseSqlite(_connectionString);
                }

                if (_provider == "npgsql")
                {
                    optionsBuilder.UseNpgsql(_connectionString);
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<OidcUser>()
                .HasIndex(u => new { u.UniqueIdentifier, u.CredentialType })
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
