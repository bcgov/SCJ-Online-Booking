using System.Globalization;
using DotEnv.Core;
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

            var mailQueueService = new MailQueueService(configuration, dbContext);

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


                // pause for 10 seconds
                Thread.Sleep(10000);
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

            return new ApplicationDbContext(connectionString, provider);
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
