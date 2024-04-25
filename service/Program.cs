using System.Globalization;
using Microsoft.Extensions.Configuration;
using SCJ.Booking.Data;
using SCJ.Booking.Data.Models;
using SCJ.Booking.Data.Utils;
using SCJ.Booking.TaskRunner.Services;
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

            var mailService = new MailService(configuration, logger);

            while (true)
            {
                logger.Information("Checking mail queue");

                // record pod liveness for health check every time the job runs
                using (StreamWriter outputFile = new StreamWriter("lastrun.txt"))
                {
                    TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
                    int secondsSinceEpoch = (int)t.TotalSeconds;
                    outputFile.WriteLine(secondsSinceEpoch);
                }

                // get the successful court booking emails in batches of 5 to send out
                var emailBatch = dbContext.Set<QueuedEmail>().Take(5);

                try
                {
                    foreach (var email in emailBatch)
                    {
                        await mailService.SendEmailAsync(email);
                        dbContext.Remove(email);
                        logger.Information($"Email sent to {email.ToEmail}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    logger.Error(ex.Message);
                }
                finally
                {
                    await dbContext.SaveChangesAsync();
                }

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
