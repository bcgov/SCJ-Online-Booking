using System;
using System.Linq;
using System.Threading.Tasks;
using Azure.Identity;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using MimeKit;
using SendGrid;
using SendGrid.Helpers.Mail;
using Serilog;
using Task = System.Threading.Tasks.Task;

namespace SCJ.Booking.MVC.Services
{
    public class MailService
    {
        private readonly IConfiguration _configuration;
        private ClientSecretCredential _emailCredentials;
        private readonly ILogger _logger;
        private string _senderEmail;

        public MailService(string courtLevel, IConfiguration configuration, ILogger logger)
        {
            _configuration = configuration;
            _logger = logger;
            if (_configuration["TAG_NAME"] != "localdev")
            {
                _senderEmail = configuration[$"{courtLevel}_EMAIL"] ?? "";
                _emailCredentials = SetExchangeCredentials(courtLevel);
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
                        EmailAddress = new Microsoft.Graph.EmailAddress
                        {
                            Address = _senderEmail
                        }
                    },
                    Subject = subject,
                    Body = new ItemBody
                    {
                        ContentType = BodyType.Text,
                        Content = body
                    },
                    ToRecipients = new System.Collections.Generic.List<Recipient>()
                    {
                        new Recipient
                        {
                            EmailAddress = new Microsoft.Graph.EmailAddress
                            {
                                Address = to
                            }
                        }
                    }
                };

                var saveToSentItems = true;

                await graphClient.Users[_senderEmail]
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
        ///     Sets the exchange credentials
        /// </summary>
        private ClientSecretCredential SetExchangeCredentials(string courtLevel)
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

        /// <summary>
        ///     Sets the exchange credentials
        /// </summary>
        public void ChangeSenderEmail(string courtLevel)
        {
            _senderEmail = _configuration[$"{courtLevel}_EMAIL"] ?? "";
            _emailCredentials = SetExchangeCredentials(courtLevel);
        }
    }
}
