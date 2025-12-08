using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace SCJ.Booking.MVC.Middleware
{
    public class MaintenanceModeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public MaintenanceModeMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var maintenanceMode = _configuration["AppSettings:MaintenanceMode"] == "true";
            var requestPath = context.Request.Path.Value?.ToLower() ?? "";

            if (
                maintenanceMode
                && !requestPath.Contains("/maintenance")
                && !requestPath.Contains("/dist/")
                && !requestPath.Contains("/images/")
                && !requestPath.Contains("/js/")
            )
            {
                context.Request.Path = "/Maintenance";
            }

            await _next(context);
        }
    }
}
