using Microsoft.Exchange.WebServices.Data;
using Microsoft.Extensions.Configuration;
using Serilog.Core;

namespace SCJ.Booking.MVC.Utils
{
    internal class ExchangeTraceListener : ITraceListener
    {
        private readonly IConfiguration _config;
        private readonly Logger _logger;

        public ExchangeTraceListener(Logger logger)
        {
            _logger = logger;
        }

        public void Trace(string traceType, string traceMessage)
        {
            _logger.Information(traceType);
            _logger.Information(traceMessage);
        }
    }
}
