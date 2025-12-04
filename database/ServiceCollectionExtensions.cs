using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SCJ.Booking.Data
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationDbContext(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            string provider;
            string connectionString;

            // For a nicer example, setting ConnectionString will use PostgreSQL
            if (configuration["ConnectionString"] != null)
            {
                provider = ServiceConfig.DataProviderNpgsql;
                connectionString =
                    configuration["ConnectionString"]
                    ?? throw new InvalidOperationException(
                        "ConnectionString configuration value is missing."
                    );
            }
            else
            {
                provider =
                    configuration[ServiceConfig.DataProviderKey.Replace("__", ":")]
                    ?? throw new InvalidOperationException(
                        "DataProvider configuration value is missing."
                    );
                connectionString =
                    configuration[ServiceConfig.ConnectionStringKey.Replace("__", ":")]
                    ?? throw new InvalidOperationException(
                        "ConnectionString configuration value is missing."
                    );
            }

            Console.WriteLine($"Using data provider: {provider}");
            switch (provider)
            {
                case ServiceConfig.DataProviderNpgsql:
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseNpgsql(connectionString)
                    );
                    break;
                case ServiceConfig.DataProviderSqlite:
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlite(connectionString)
                    );
                    break;
                default:
                    throw new ArgumentException(
                        "Unknown data provider",
                        ServiceConfig.DataProviderKey
                    );
            }

            return services;
        }
    }
}
