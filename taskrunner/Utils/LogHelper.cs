using Microsoft.Extensions.Configuration;
using Serilog;

namespace SCJ.Booking.TaskRunner.Utils
{
    public static class LogHelper
    {
        public static ILogger GetLogger(IConfiguration configuration)
        {
            return new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .WriteTo.Console()
                .CreateLogger();
        }
    }
}
