using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SCJ.Booking.Data;
using SCJ.Booking.TaskRunner.Utils;
using Serilog;
using Task = System.Threading.Tasks.Task;

namespace SCJ.Booking.TaskRunner.Services
{
    public class LotteryCleanupService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public LotteryCleanupService(IConfiguration configuration, ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _logger = LogHelper.GetLogger(configuration);
            _configuration = configuration;
        }

        /// <summary>
        ///    Removes names and phone numbers from processed lottery entries
        /// </summary>
        public async Task RemovePersonalInfo()
        {
            string anonymizedName = "_anonymized";
            string anonymizedEmail = "_anonymized@example.com";

            _logger.Information("Removing personal info from lottery entries");

            // calculate the oldest lottery entry to keep
            int maxDays = _configuration.GetValue<int>(
                "AppSettings:PurgeLotteryContactInfoAfterDays"
            );
            DateTime oldestDate = DateTime.Now.AddDays(-maxDays);

            // get entries from ScTrialBookingRequests where ProcessingTimestamp is older than oldestDate
            var entriesToUpdate = await _dbContext
                .ScTrialBookingRequests.Where(e => e.ProcessingTimestamp < oldestDate)
                .Where(e => e.Email != anonymizedEmail && e.RequestedByName != anonymizedName)
                .ToListAsync();

            if (entriesToUpdate.Count == 0)
            {
                _logger.Information("No entries to update");
                return;
            }

            _logger.Information(
                "Updating {count} old entries from ScTrialBookingRequests with anonymized details",
                entriesToUpdate.Count
            );

            // Update each entry
            foreach (var entry in entriesToUpdate)
            {
                entry.Email = anonymizedEmail;
                entry.RequestedByName = anonymizedName;
                entry.Phone = "";
            }

            // Save changes to the database
            _dbContext.SaveChanges();

            _logger.Information(
                "Updated {count} entries from ScTrialBookingRequests",
                entriesToUpdate.Count
            );
        }

        /// <summary>
        ///    Removes old trial booking lottery requests
        /// </summary>
        public async Task RemoveOldLotteryRequests()
        {
            _logger.Information("Removing old lottery requests");

            // calculate the oldest trial booking request to keep
            int maxDays = _configuration.GetValue<int>("AppSettings:PurgeLotteryRequestsAfterDays");
            DateTime oldestDate = DateTime.Now.AddDays(-maxDays);

            // get entries from ScTrialBookingRequests where ProcessingTimestamp is older than oldestDate
            var entriesToDelete = await _dbContext
                .ScTrialBookingRequests.Where(e => e.ProcessingTimestamp < oldestDate)
                .ToListAsync();

            if (entriesToDelete.Count == 0)
            {
                _logger.Information("No entries to delete");
                return;
            }

            _logger.Information(
                "Deleting {count} old entries from ScTrialBookingRequests",
                entriesToDelete.Count
            );

            // delete rows in dependent tables that reference entriesToDelete
            foreach (var entry in entriesToDelete)
            {
                var childEntries = _dbContext
                    .ScTrialDateSelections.Where(c => c.BookingRequest == entry)
                    .ToList();
                _dbContext.ScTrialDateSelections.RemoveRange(childEntries);
            }

            // save changes to the database
            _dbContext.SaveChanges();

            // delete each expired lottery entry
            foreach (var entry in entriesToDelete)
            {
                _dbContext.ScTrialBookingRequests.Remove(entry);
            }

            // save changes to the database
            _dbContext.SaveChanges();

            _logger.Information(
                "Deleted {count} entries from ScTrialBookingRequests",
                entriesToDelete.Count
            );
        }
    }
}
