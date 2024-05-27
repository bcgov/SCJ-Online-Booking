using System.IO;
using DotEnv.Core;
using Microsoft.Extensions.Configuration;

namespace SCJ.Booking.UnitTest.Helpers;

public class TestUtils
{
    public static IConfiguration GetConfiguration()
    {
        new EnvLoader().Load();

        IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .AddEnvironmentVariables();

        IConfiguration configuration = builder.Build();

        return configuration;
    }
}
