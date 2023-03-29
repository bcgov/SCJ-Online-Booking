
using Microsoft.Extensions.Configuration;
using Microsoft.Graph.ExternalConnectors;
using SCJ.Booking.MVC.Data;
using SCJ.Booking.MVC.Models;
using SCJ.Booking.MVC.Services;
using Serilog;

namespace SCJ.Booking.CourtBookingBulkEmailService
{
    public class Program
    {
        private static ILogger _logger;
        private static MailService _mailService;
        private static ApplicationDbContext _dbContext;

        public Program(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            _logger = LogHelper.GetLogger(configuration);
            _dbContext = dbContext;
            _mailService = new MailService("CA", configuration, _logger);
        }

        public static void Main(string[] args)
        {
            //get the successful court booking emails in batches of 50 to send out
            var courtBookingEmailBatch = _dbContext.Set<CourtBookingEmail>().Take(50);

            try
            {
                foreach (var email in courtBookingEmailBatch)
                {
                    string body = "";
                    if (email.BodyText.Length > 0 && email.BodyHtml.Length > 0)
                        body = email.BodyHtml + email.BodyText; //unsure of how to combine them atm
                    else if (email.BodyText.Length > 0)
                        body = email.BodyText;
                    else if (email.BodyHtml.Length > 0)
                        body = email.BodyHtml;

                    //update the sender email address
                    _mailService.ChangeSenderEmail(email.CourtLevel);
                    _mailService.ExchangeSendEmail(email.ToEmail, email.Subject, body).Wait();

                    //delete the email record
                    _dbContext.Remove(email);
                    _logger.Information($"Email sent to {email.ToEmail}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                _logger.Error(ex.Message);
            }
            finally
            {
                _dbContext.SaveChanges();
            }
        }
    }
}

