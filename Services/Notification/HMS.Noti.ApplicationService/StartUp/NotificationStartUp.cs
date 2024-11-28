using HMS.Noti.ApplicationService.NotificationModule.Abstracts;
using HMS.Noti.ApplicationService.NotificationModule.Implements;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Noti.ApplicationService.StartUp
{
    public static class NotificationStartUp
    {
        public static void ConfigureNotification(this WebApplicationBuilder builder, string? assemblyName)
        {
            builder.Services.AddScoped<INotificationService, NotificationService>();
        }

    }
}
