using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Noti.ApplicationService.NotificationModule.Abstracts
{
    public interface INotificationService
    {
        Task SendEmail(string receptor, string subject, string body);
    }
}
