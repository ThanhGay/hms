using HMS.Auth.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Auth.ApplicationService.UserModule.Abstracts
{
    public interface IUserService
    {
        void AddToBlacklist(string token);
        Task ForgotPassword([FromForm] string email);
        List<string> GetFunctionCustomer();
        List<string> GetFunctionManager();
        List<string> GetFunctionReceptionist();
        bool IsTokenBlacklisted(string token);
        ResultLogin Login([FromQuery] LoginDto input);
        void ResetPassword(UpdatePassWordDto input);
        Task SendNotification(SendNotificationDto dto);
    }
}
