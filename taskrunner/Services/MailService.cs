using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using SCJ.Booking.Data.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using Serilog;
using Task = System.Threading.Tasks.Task;

namespace SCJ.Booking.TaskRunner.Services
{
    public class MailService
    {
        private readonly IConfiguration _configuration;
        private ClientSecretCredential? _emailCredentials;
        private readonly ILogger _logger;
        private string? _senderEmail;
        private readonly bool _isLocalDevEnvironment;

        public MailService(IConfiguration configuration, ILogger logger)
        {
            _configuration = configuration;
            _logger = logger;
            _isLocalDevEnvironment = _configuration["TAG_NAME"] == "localdev";
        }

        /// <summary>
        ///     Send an email
        /// </summary>
        public async Task SendEmailAsync(QueuedEmail email)
        {
            if (_isLocalDevEnvironment)
            {
                var fromEmail = _configuration["SENDGRID_FROM_EMAIL"] ?? "";
                await SendGridSendEmail(fromEmail, email.ToEmail, email.Subject, email.Body);
            }
            else
            {
                _senderEmail = _configuration[$"{email.CourtLevel}_EMAIL"] ?? "";
                _emailCredentials = SetExchangeCredentials(email.CourtLevel);
                await ExchangeSendEmail(email.ToEmail, email.Subject, email.Body);
            }
        }

        /// <summary>
        ///     Sends a confirmation email using an Exchange server
        /// </summary>
        public async Task ExchangeSendEmail(string to, string subject, string body)
        {
            // log the settings the the console
            _logger.Information($"Sending email with Exchange");

            //Do NULL checks to ensure we received all the settings
            if (_emailCredentials != null)
            {
                GraphServiceClient graphClient = new GraphServiceClient(_emailCredentials);

                var message = new Message
                {
                    From = new Recipient
                    {
                        EmailAddress = new Microsoft.Graph.EmailAddress { Address = _senderEmail }
                    },
                    Subject = subject,
                    Body = new ItemBody { ContentType = BodyType.Html, Content = body },
                    ToRecipients = new List<Recipient>()
                    {
                        new() { EmailAddress = new Microsoft.Graph.EmailAddress { Address = to } }
                    }
                };

                var saveToSentItems = true;

                await graphClient
                    .Users[_senderEmail]
                    .SendMail(message, saveToSentItems)
                    .Request()
                    .PostAsync();
            }
        }

        /// <summary>
        ///     Sends a confirmation email using SendGrid
        /// </summary>
        /// <remarks>
        ///     For local development purposes only
        /// </remarks>
        public async Task<SendGrid.Response> SendGridSendEmail(
            string fromEmail,
            string toEmail,
            string subject,
            string body
        )
        {
            // log the settings the the console
            _logger.Information($"Sending email with SendGrid");

            var apiKey = _configuration["SENDGRID_API_KEY"];
            var client = new SendGridClient(apiKey);
            var from = new SendGrid.Helpers.Mail.EmailAddress(fromEmail);
            var to = new SendGrid.Helpers.Mail.EmailAddress(toEmail);
            var htmlContent = body;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, null, htmlContent);
            return await client.SendEmailAsync(msg);
        }

        /// <summary>
        ///     Sets the exchange credentials
        /// </summary>
        private ClientSecretCredential? SetExchangeCredentials(string courtLevel)
        {
            string tenantId = _configuration["EXCHANGE_TENANT_ID"] ?? "";
            string clientId = _configuration["EXCHANGE_CLIENT_ID"] ?? "";
            string clientSecret = _configuration["EXCHANGE_CLIENT_SECRET"] ?? "";

            if (tenantId == "")
            {
                _logger.Error("EXCHANGE_TENANT_ID is not set");
            }

            if (clientId == "")
            {
                _logger.Error("EXCHANGE_CLIENT_ID is not set");
            }

            if (clientSecret == "")
            {
                _logger.Error("EXCHANGE_CLIENT_SECRET is not set");
            }

            if (_senderEmail == "")
            {
                _logger.Error($"{courtLevel}_EMAIL is not set");
            }

            if (tenantId == "" || clientId == "" || clientSecret == "" || _senderEmail == "")
            {
                return null;
            }

            return new ClientSecretCredential(tenantId, clientId, clientSecret);
        }
    }
}
