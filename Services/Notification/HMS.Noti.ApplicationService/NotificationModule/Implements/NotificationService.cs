using HMS.Shared.ApplicationService.Notification;
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

            var password = _configuration.GetValue<string>("configure_email:password");

            var host = _configuration.GetValue<string>("configure_email:host");

            var port = _configuration.GetValue<int>("configure_email:port");

            var smtpClient = new SmtpClient(host, port);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(email, password);

            var message = new MailMessage(email!, receptor, subject, body);

            await smtpClient.SendMailAsync(message);

        }
    }
}
