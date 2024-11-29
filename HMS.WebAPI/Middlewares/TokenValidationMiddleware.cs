using HMS.Auth.ApplicationService.UserModule.Abstracts;
using System.Threading.Tasks;

namespace HMS.WebAPI.Middlewares
{ 
public class TokenValidationMiddleware
{
    private readonly RequestDelegate _next;
    //private readonly IUserService _userService; // Dịch vụ mà bạn cần dùng

    public TokenValidationMiddleware(RequestDelegate next )
    {
        _next = next;
        //_userService = userService;
    }

   public async Task Invoke(HttpContext context, IUserService userService)
        {
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (userService.IsTokenBlacklisted(token))
            {
                context.Response.StatusCode = 401; // Unauthorized
                await context.Response.WriteAsync("Token đã bị thu hồi.");
                return;
            }

            await _next(context);
        }
    }
}