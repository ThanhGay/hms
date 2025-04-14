using HMS.Noti.ApplicationService.NotificationModule.Implements;
using HMS.Shared.ApplicationService.Notification;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

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
