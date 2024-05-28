using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace AdaptiveWebInterfaces_WebAPI.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailNotificationAsync(int entryId)
        {
            try
            {
                var apiKey = _configuration["SendGrid:ApiKey"];
                var fromEmail = _configuration["SendGrid:FromEmail"];
                var toEmail = _configuration["SendGrid:ToEmail"];

                var client = new SendGridClient(apiKey);
                var from = new EmailAddress(fromEmail, "Your Name");
                var subject = "New Database Entry";
                var to = new EmailAddress(toEmail, "Recipient Name");
                var plainTextContent = $"A new entry with ID {entryId} has been added to the database.";
                var htmlContent = $"<strong>A new entry with ID {entryId} has been added to the database.</strong>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

                var response = await client.SendEmailAsync(msg);
                Console.WriteLine(response.IsSuccessStatusCode
                    ? "Email sent successfully."
                    : $"Failed to send email: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email: {ex.Message}");
            }
        }
    }
}
