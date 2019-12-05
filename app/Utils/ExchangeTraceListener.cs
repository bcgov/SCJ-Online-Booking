using Microsoft.Exchange.WebServices.Data;
using Serilog;

namespace SCJ.Booking.MVC.Utils
{
    internal class ExchangeTraceListener : ITraceListener
    {
        private readonly ILogger _logger;

        public ExchangeTraceListener(ILogger logger)
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
