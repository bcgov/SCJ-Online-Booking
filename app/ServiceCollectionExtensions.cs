using System;
using app.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace app
{
    internal static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            string provider;
            string connectionString;

            // For a nicer example, setting ConnectionString will use PostgreSQL
            if (configuration["ConnectionString"] != null)
            {
                provider = ServiceConfig.DataProviderNpgsql;
                connectionString = configuration["ConnectionString"];
            }
            else
            {
                provider = configuration[ServiceConfig.DataProviderKey.Replace("__", ":")];
                connectionString =
                    configuration[ServiceConfig.ConnectionStringKey.Replace("__", ":")];
            }

            if (string.IsNullOrEmpty(provider))
            {
                provider = ServiceConfig.DataProviderPlatform;
            }

            if (provider == ServiceConfig.DataProviderPlatform)
            {
                var platform = new Platform();
                provider = platform.UseInMemoryStore
                    ? ServiceConfig.DataProviderMemory
                    : ServiceConfig.DataProviderSqlServer;
            }

            Console.WriteLine($"Using data provider: {provider}");
            switch (provider)
            {
                case ServiceConfig.DataProviderMemory:
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseInMemoryDatabase("Scratch"));
                    break;
                case ServiceConfig.DataProviderSqlServer:
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer(connectionString));
                    break;
                case ServiceConfig.DataProviderNpgsql:
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseNpgsql(connectionString));
                    break;
                case ServiceConfig.DataProviderSqlite:
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlite(connectionString));
                    break;
                case ServiceConfig.DataProviderMysql:
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseMySql(connectionString));
                    break;
                default:
                    throw new ArgumentException("Unknown data provider",
                        ServiceConfig.DataProviderKey);
            }

            return services;
        }
    }
}
