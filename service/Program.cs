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
            IConfiguration configuration = GetConfiguration();
            ILogger logger = LogHelper.GetLogger(configuration);
            ApplicationDbContext dbContext = GetDbContext(configuration);
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            var mailQueueService = new MailQueueService(configuration, dbContext);
            var lotteryService = new LotteryService(configuration, dbContext);

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

                bool hasUnfinishedWork = await lotteryService.RunNextLotteryStep();

                // todo: we need another service that removes names and phone numbers (14 days after the lottery?)

                // pause for 3 seconds
                // todo: there should be a fast/infrequent mode an a slow/frequent mode for trial bookings based on whether or not there is incomplete work to be done
                Thread.Sleep(3000);
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
