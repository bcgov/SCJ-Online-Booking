using SCJ.Booking.Data;
using SCJ.Booking.Data.Models;
using Task = System.Threading.Tasks.Task;

namespace SCJ.Booking.TaskManager.Services
{
    public class MailQueueService
    {
        private readonly ApplicationDbContext _dbContext;

        public MailQueueService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        ///     Queue an email
        /// </summary>
        public async Task QueueEmailAsync(
            string courtLevel,
            string toEmail,
            string subject,
            string body
        )
        {
            await _dbContext.EmailQueue.AddAsync(
                new QueuedEmail
                {
                    CourtLevel = courtLevel,
                    ToEmail = toEmail,
                    Subject = subject,
                    Body = body
                }
            );

            await _dbContext.SaveChangesAsync();
        }
    }
}
