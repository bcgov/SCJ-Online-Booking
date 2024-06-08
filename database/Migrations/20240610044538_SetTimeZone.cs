using DotEnv.Core;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace SCJ.Booking.Data.Migrations
{
    public partial class SetTimeZone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder.ActiveProvider == "Npgsql.EntityFrameworkCore.PostgreSQL")
            {
                new EnvLoader().Load();

                IConfigurationBuilder builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .AddEnvironmentVariables();

                IConfiguration configuration = builder.Build();

                var connectionString = configuration["Data:DefaultConnection:ConnectionString"];
                var connInfo = new Npgsql.NpgsqlConnectionStringBuilder(connectionString);

                migrationBuilder.Sql(
                    $"ALTER DATABASE {connInfo.Database} SET TIMEZONE TO 'America/Vancouver'"
                );
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder) { }
    }
}
