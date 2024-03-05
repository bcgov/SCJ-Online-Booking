using DotEnv.Core;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace SCJ.Booking.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            new EnvLoader().Load();
            return WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
        }
    }
}
