using System;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Extensions.Configuration;
using MimeKit;
using SCJ.Booking.MVC.Utils;
using SendGrid;
using SendGrid.Helpers.Mail;
using Serilog;
using EmailAddress = Microsoft.Exchange.WebServices.Data.EmailAddress;
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
            if (_configuration["TAG_NAME"] != "localdev")
            {
                _emailCredentials = SetExchangeCredentials(courtLevel);
            }
        }

        /// <summary>
        ///     Sends a confirmation email using an Exchange server
        /// </summary>
        public async Task ExchangeSendEmail(string to, string subject, string body)
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
        ///     Sends a confirmation email using SendGrid
        /// </summary>
        /// <remarks>
        ///     For local development purposes only
        /// </remarks>
        public async Task<Response> SendGridSendEmail(
            string fromEmail, string toEmail, string subject, string body)
        {
            // log the settings the the console
            _logger.Information($"Sending email with SendGrid");

            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(apiKey);
            var from = new SendGrid.Helpers.Mail.EmailAddress(fromEmail);
            var to = new SendGrid.Helpers.Mail.EmailAddress(toEmail);
            var plainTextContent = body;
            var htmlContent = body;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            return await client.SendEmailAsync(msg);
        }

        /// <summary>
        ///     Sends an email using SMTP
        /// </summary>
        /// <remarks>
        ///     Temporary workaround until Office365 Graph API is supported
        /// </remarks>
        public async Task SmtpSendEmail(string toEmail, string subject, string body)
        {
            // log the settings the the console
            _logger.Information($"Sending email with SMTP");

            var email = new MimeMessage();

            email.Sender = MailboxAddress.Parse("mike.olund@gov.bc.ca");
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.From.Add(email.Sender);

            var builder = new BodyBuilder
            {
                TextBody = body
            };
            email.Body = builder.ToMessageBody();
            email.Subject = subject;

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync("apps.smtp.gov.bc.ca", 25, SecureSocketOptions.None);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
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
