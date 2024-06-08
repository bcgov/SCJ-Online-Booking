using System.Diagnostics;
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

        public DbSet<ScLottery> ScLotteries => Set<ScLottery>();

        public DbSet<QueuedEmail> EmailQueue => Set<QueuedEmail>();

        public DbSet<DataProtectionKey> DataProtectionKeys => Set<DataProtectionKey>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrEmpty(_connectionString))
            {
                if (_provider == "sqlite")
                {
                    var cs = SwapDevSqliteConnectionString();
                    optionsBuilder.UseSqlite(cs);
                }

                if (_provider == "npgsql")
                {
                    optionsBuilder.UseNpgsql(_connectionString);
                    optionsBuilder.EnableSensitiveDataLogging();
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<OidcUser>()
                .HasIndex(u => new { u.UniqueIdentifier, u.CredentialType })
                .IsUnique();

            modelBuilder
                .Entity<ScTrialBookingRequest>()
                .HasIndex(r => new { r.CaseNumber }, "IX_CaseNumber_Ascending");

            modelBuilder
                .Entity<ScTrialBookingRequest>()
                .HasIndex(
                    r => new
                    {
                        r.LotteryStartDate,
                        r.BookingLocationId,
                        r.BookHearingCode
                    },
                    "IX_TaskRunner_Lottery"
                );

            modelBuilder
                .Entity<ScTrialBookingRequest>()
                .HasIndex(r => new { r.LotteryStartDate }, "IX_LotteryStartDate_Ascending");

            base.OnModelCreating(modelBuilder);
        }

        private string SwapDevSqliteConnectionString()
        {
            var slash = Path.DirectorySeparatorChar;
            var process = Process.GetCurrentProcess().MainModule?.FileName;
            if (process != null && _connectionString != null)
            {
                var path = process.Split(@$"{slash}service{slash}bin");
                var fileName = _connectionString.Split('=');
                if (fileName.Length > 1 && path.Length > 1)
                {
                    return $@"data source={path[0]}{slash}app{slash}{fileName[1]}";
                }
            }
            return _connectionString ?? "";
        }
    }
}
