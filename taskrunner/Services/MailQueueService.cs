using Microsoft.Extensions.Configuration;
using SCJ.Booking.Data;
using SCJ.Booking.Data.Models;
using SCJ.Booking.TaskRunner.Utils;
using Serilog;
using Task = System.Threading.Tasks.Task;

namespace SCJ.Booking.TaskRunner.Services
{
    public class MailQueueService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public MailQueueService(IConfiguration configuration, ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _logger = LogHelper.GetLogger(configuration);
            _configuration = configuration;
        }

        /// <summary>
        ///     Generates a booking success email for a trial and adds it to the
        ///     mail queue
        /// </summary>
        public async Task QueueTrialSuccessEmail(ScLotteryBookingRequest entry)
        {
            var model = new LotteryEmailViewModel(entry);
            string emailText = await RazorHelper.RenderTemplate(
                "Trial-Lottery-Success.cshtml",
                model
            );
            string subject =
                $"Trial booking for {model.FullCaseNumber} starting on {model.AllocatedStartDate}";

            await QueueEmailAsync(
                "SC",
                model.EmailAddress,
                subject,
                emailText,
                isLotteryResult: true
            );
        }

        /// <summary>
        ///     Generates an unsuccessful booking email for a trial and adds it
        ///     to the mail queue
        /// </summary>
        public async Task QueueTrialFailureEmail(ScLotteryBookingRequest entry)
        {
            var model = new LotteryEmailViewModel(entry);
            string emailText = await RazorHelper.RenderTemplate(
                "Trial-Lottery-Failure.cshtml",
                model
            );
            string subject = $"No trial booking for {model.FullCaseNumber}";

            await QueueEmailAsync(
                "SC",
                model.EmailAddress,
                subject,
                emailText,
                isLotteryResult: true
            );
        }

        /// <summary>
        ///     Generates a booking success email for a long chambers hearing and adds
        ///     it to the mail queue
        /// </summary>
        public async Task QueueLongChambersSuccessEmail(ScLotteryBookingRequest entry)
        {
            var model = new LotteryEmailViewModel(entry);
            string emailText = await RazorHelper.RenderTemplate(
                "LongChambers-Lottery-Success.cshtml",
                model
            );
            string subject =
                $"Chambers hearing booking for {model.FullCaseNumber} starting on {model.AllocatedStartDate}";

            await QueueEmailAsync(
                "SC",
                model.EmailAddress,
                subject,
                emailText,
                isLotteryResult: true
            );
        }

        /// <summary>
        ///     Generates an unsuccessful booking email for a long chambers hearing and adds
        ///     it to the mail queue
        /// </summary>
        public async Task QueueLongChambersFailureEmail(ScLotteryBookingRequest entry)
        {
            var model = new LotteryEmailViewModel(entry);
            string emailText = await RazorHelper.RenderTemplate(
                "LongChambers-Lottery-Failure.cshtml",
                model
            );
            string subject = $"No chambers hearing booking for {model.FullCaseNumber}";

            await QueueEmailAsync(
                "SC",
                model.EmailAddress,
                subject,
                emailText,
                isLotteryResult: true
            );
        }

        /// <summary>
        ///     Queue an email
        /// </summary>
        public async Task QueueEmailAsync(
            string courtLevel,
            string toEmail,
            string subject,
            string body,
            bool isLotteryResult = false
        )
        {
            await _dbContext.EmailQueue.AddAsync(
                new QueuedEmail
                {
                    CourtLevel = courtLevel,
                    ToEmail = toEmail,
                    Subject = subject,
                    Body = body,
                    IsLotteryResult = isLotteryResult
                }
            );

            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        ///
        /// </summary>
        public async Task SendEmailBatch()
        {
            var mailService = new MailService(_configuration, _logger);
            var emailLotteryResultEnabled = _configuration.GetValue<bool>(
                "AppSettings:EmailLotteryResultEnabled"
            );

            _logger.Debug("Checking mail queue");

            IQueryable<QueuedEmail> emailBatch;

            // If lottery booking emails are disabled, exclude them from the batch
            if (!emailLotteryResultEnabled)
            {
                emailBatch = _dbContext
                    .Set<QueuedEmail>()
                    .Where(email => !email.IsLotteryResult)
                    .Take(5);
            }
            else
            {
                emailBatch = _dbContext.Set<QueuedEmail>().Take(5);
            }

            try
            {
                foreach (var email in emailBatch)
                {
                    await mailService.SendEmailAsync(email);
                    _dbContext.Remove(email);
                    _logger.Information($"Email sent to {email.ToEmail}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                _logger.Error(ex.Message);
            }
            finally
            {
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
