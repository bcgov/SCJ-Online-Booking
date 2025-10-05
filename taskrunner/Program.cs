using System.Globalization;
using DotEnv.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SCJ.Booking.Data;
using SCJ.Booking.TaskRunner.Services;
using SCJ.Booking.TaskRunner.Utils;
using Serilog;

namespace SCJ.Booking.TaskRunner
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-CA", false);
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            IConfiguration configuration = GetConfiguration();
            ILogger logger = LogHelper.GetLogger(configuration);
            ApplicationDbContext dbContext = GetDbContext(configuration);

            var mailQueueService = new MailQueueService(configuration, dbContext);
            var lotteryService = new LotteryService(configuration, dbContext);
            var lotteryCleanupService = new LotteryCleanupService(configuration, dbContext);

            var emailEnabled = configuration.GetValue<bool>("AppSettings:EmailEnabled");
            var lotteryEnabled = configuration.GetValue<bool>("AppSettings:LotteryEnabled");
            var cleanupEnabled = configuration.GetValue<bool>("AppSettings:PurgeEnabled");
            var pollingFrequencySeconds = configuration.GetValue<int>(
                "AppSettings:PollingFrequencySeconds"
            );

            logger.Information("SCJ.Booking.TaskRunner started");
            logger.Information(
                $"Checking emails and lottery requests every {pollingFrequencySeconds} seconds"
            );

            while (true)
            {
                // record pod liveness for openshift health check every time the job runs
                using (StreamWriter outputFile = new StreamWriter("lastrun.txt"))
                {
                    TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
                    int secondsSinceEpoch = (int)t.TotalSeconds;
                    outputFile.WriteLine(secondsSinceEpoch);
                }

                if (emailEnabled)
                {
                    await mailQueueService.SendEmailBatch();
                }

                if (lotteryEnabled)
                {
                    await lotteryService.RunNextLotteryStep();
                }

                if (cleanupEnabled)
                {
                    // remove old lottery requests
                    await lotteryCleanupService.RemoveOldLotteryRequests();
                    // remove names and phone numbers from processed lottery requests
                    await lotteryCleanupService.RemovePersonalInfo();
                }

                // pause for 3 seconds
                Thread.Sleep(pollingFrequencySeconds * 1000);
            }
        }

        private static ApplicationDbContext GetDbContext(IConfiguration configuration)
        {
            string connectionString;
            string provider;

            if (configuration["ConnectionString"] != null)
            {
                connectionString = configuration["ConnectionString"] ?? "";
                provider = ServiceConfig.DataProviderNpgsql;
            }
            else
            {
                provider = configuration[ServiceConfig.DataProviderKey.Replace("__", ":")] ?? "";
                connectionString =
                    configuration[ServiceConfig.ConnectionStringKey.Replace("__", ":")] ?? "";
            }

            var applicationDbContext = new ApplicationDbContext(connectionString, provider);

            // run migrations if needed
            applicationDbContext.Database.Migrate();

            return applicationDbContext;
        }

        private static IConfiguration GetConfiguration()
        {
            new EnvLoader().Load();

            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("servicesettings.json", true, true)
                .AddUserSecrets<Program>()
                .AddEnvironmentVariables();

            IConfiguration configuration = builder.Build();

            return configuration;
        }
    }
}
