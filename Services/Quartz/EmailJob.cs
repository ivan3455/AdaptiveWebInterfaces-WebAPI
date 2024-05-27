using Microsoft.Extensions.Configuration;
using Quartz;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace AdaptiveWebInterfaces_WebAPI.Services.Quartz
{
    public class EmailJob : IJob
    {
        private readonly IConfiguration _configuration;

        public EmailJob(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var apiKey = _configuration["SendGrid:ApiKey"];
                var fromEmail = _configuration["SendGrid:FromEmail"];
                var toEmail = _configuration["SendGrid:ToEmail"];

                var client = new SendGridClient(apiKey);
                var from = new EmailAddress(fromEmail, "Your Name");
                var subject = "Scheduled Email";
                var to = new EmailAddress(toEmail, "Ivan Trunov");
                var plainTextContent = "This is a scheduled email sent by Quartz.NET job.";
                var htmlContent = "<strong>This is a scheduled email sent by Quartz.NET job.</strong>";
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
