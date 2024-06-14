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
        private const int PollingFrequencySeconds = 3;

        public static async Task Main(string[] args)
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-CA", false);
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            IConfiguration configuration = GetConfiguration();
            ILogger logger = LogHelper.GetLogger(configuration);
            ApplicationDbContext dbContext = GetDbContext(configuration);

            var mailQueueService = new MailQueueService(configuration, dbContext);
            var lotteryService = new LotteryService(configuration, dbContext);

            logger.Information("SCJ.Booking.TaskRunner started");
            logger.Information(
                $"Checking emails and lottery requests every {PollingFrequencySeconds} seconds"
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

                await mailQueueService.SendEmailBatch();

                await lotteryService.RunNextLotteryStep();

                // todo: we need another service that removes names and phone numbers (14 days after the lottery?)

                // pause for 3 seconds
                Thread.Sleep(PollingFrequencySeconds * 1000);
            }
        }

        private static ApplicationDbContext GetDbContext(IConfiguration configuration)
        {
            string connectionString;
            string provider;

            if (configuration["ConnectionString"] != null)
            {
                connectionString = configuration["ConnectionString"];
                provider = ServiceConfig.DataProviderNpgsql;
            }
            else
            {
                provider = configuration[ServiceConfig.DataProviderKey.Replace("__", ":")];
                connectionString = configuration[
                    ServiceConfig.ConnectionStringKey.Replace("__", ":")
                ];
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