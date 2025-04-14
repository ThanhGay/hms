namespace HMS.Shared.ApplicationService.Notification
{
    public interface INotificationService
    {
        Task SendEmail(string receptor, string subject, string body);
    }
}
