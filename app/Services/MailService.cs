using System;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Extensions.Configuration;
using SCJ.Booking.MVC.Utils;
using Serilog;
using Task = System.Threading.Tasks.Task;

namespace SCJ.Booking.MVC.Services
{
    public class MailService
    {
        private readonly IConfiguration _configuration;
        private readonly WebCredentials _emailCredentials;
        private readonly ILogger _logger;

        public MailService(string courtLevel, IConfiguration configuration, ILogger logger)
        {
            _configuration = configuration;
            _logger = logger;
            _emailCredentials = SetExchangeCredentials(courtLevel);
        }

        /// <summary>
        ///     Sends a confirmation email using an Exchange server
        /// </summary>
        public async Task SendEmail(string to, string subject, string body)
        {
            string exchangeUrl = _configuration["EXCHANGE_URL"] ?? "";

            // log the settings the the console
            _logger.Information($"EXCHANGE_URL={exchangeUrl}");

            //Do NULL checks to ensure we received all the settings
            if (!string.IsNullOrEmpty(exchangeUrl) && _emailCredentials != null)
            {
                var exchangeService = new ExchangeService
                {
                    Credentials = _emailCredentials,
                    Url = new Uri(exchangeUrl),
                    UseDefaultCredentials = false,
                    TraceEnabled = true,
                    TraceFlags = TraceFlags.All,
                    TraceListener = new ExchangeTraceListener(_logger)
                };

                var message = new EmailMessage(exchangeService)
                {
                    Subject = subject,
                    Body = new MessageBody(BodyType.Text, body)
                };

                message.ToRecipients.Add(new EmailAddress(to));

                await message.SendAndSaveCopy();
            }
        }

        /// <summary>
        ///     Sets the exchange credentials
        /// </summary>
        private WebCredentials SetExchangeCredentials(string courtLevel)
        {
            string emailUserName = _configuration[$"{courtLevel}_MAIL_USERNAME"] ?? "";
            string emailPassword = _configuration[$"{courtLevel}_MAIL_PASSWORD"] ?? "";
            string emailDomain = _configuration["MAIL_DOMAIN"] ?? "";

            if (emailDomain == "")
            {
                _logger.Error("MAIL_DOMAIN is not set");
            }

            if (emailPassword == "")
            {
                _logger.Error($"{courtLevel}_MAIL_PASSWORD is not set");
            }

            if (emailUserName == "")
            {
                _logger.Error($"{courtLevel}_MAIL_USERNAME is not set");
            }

            if (emailDomain == "" || emailPassword == "" || emailUserName == "")
            {
                return null;
            }

            return new WebCredentials(emailUserName, emailPassword, emailDomain);
        }
    }
}
