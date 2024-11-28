using HMS.Noti.ApplicationService.NotificationModule.Abstracts;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
namespace HMS.Noti.ApplicationService.NotificationModule.Implements
{
    public class NotificationService : INotificationService
    {
        private readonly IConfiguration _configuration;
        public NotificationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmail(string receptor, string subject, string body)
        {
            var email = _configuration.GetValue<string>("configure_email:email");
            Console.WriteLine($"Email: {email} tồn tại");
            var password = _configuration.GetValue<string>("configure_email:password");
            Console.WriteLine($"Email: {password} tồn tại");
            var host = _configuration.GetValue<string>("configure_email:host");
            Console.WriteLine($"Email: {host} tồn tại");

            var port = _configuration.GetValue<int>("configure_email:port");
            Console.WriteLine($"Email: {port} tồn tại");

            var smtpClient = new SmtpClient(host, port);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(email, password);

            var message = new MailMessage(email!, receptor, subject, body);

            await smtpClient.SendMailAsync(message);

        }
    }
}
