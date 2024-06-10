using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Npgsql;

#nullable disable

namespace SCJ.Booking.Data.Migrations
{
    public partial class SetTimeZone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder.ActiveProvider == "Npgsql.EntityFrameworkCore.PostgreSQL")
            {
                migrationBuilder.Sql(
                    $"ALTER DATABASE {GetDatabaseName()} SET TIMEZONE TO 'America/Vancouver'"
                );
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder.ActiveProvider == "Npgsql.EntityFrameworkCore.PostgreSQL")
            {
                migrationBuilder.Sql(
                    $"ALTER DATABASE {GetDatabaseName()} SET TIMEZONE TO 'Etc/UTC'"
                );
            }
        }

        /// <remarks>
        ///    There is probably an easier way to alter the current database without hardcoding
        ///    the database name, but parsing the name from the connection string seems to work
        /// </remarks>
        private static string GetDatabaseName()
        {
            var connectionString = GetConnectionString();
            var connectionStringBuilder = new NpgsqlConnectionStringBuilder(connectionString);
            return connectionStringBuilder.Database;
        }

        private static string GetConnectionString()
        {
            // the environment variable should always be present on our OpenShift deployments
            string connectionString = Environment.GetEnvironmentVariable("ConnectionString");

            if (!string.IsNullOrEmpty(connectionString))
            {
                return connectionString;
            }

            // get it from the json config files if the environment variable was not found
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile("servicesettings.json", true, true)
                .AddJsonFile("testsettings.json", true, true);
            IConfiguration configuration = builder.Build();
            return configuration["Data:DefaultConnection:ConnectionString"];
        }
    }
}
