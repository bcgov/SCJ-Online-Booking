using DotEnv.Core;
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
            IConfiguration configuration = GetConfiguration();

            var emailLotteryResultEnabled = configuration.GetValue<bool>(
                "AppSettings:EmailLotteryResultEnabled"
            );
            var mailService = new MailService(_configuration, _logger);

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

        private static IConfiguration GetConfiguration()
        {
            new EnvLoader().Load();

            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("servicesettings.json", true, true)
                .AddEnvironmentVariables();

            IConfiguration configuration = builder.Build();

            return configuration;
        }
    }
}
