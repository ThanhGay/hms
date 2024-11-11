using System.Security.Claims;
using HMS.Auth.Dtos;
namespace HMS.WebAPI
{
    public class CommonUntil
    {
        public static string GetCurrentUserId(IHttpContextAccessor httpContextAccessor)
        {
            var claims = httpContextAccessor.HttpContext?.User?.Identity as ClaimsIdentity;
                //nếu trong program dùng JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
                //thì các claim type sẽ không bị ghi đè tên nên phải dùng trực tiếp "sub"
            var claim = claims?.FindFirst(CustomClaimTypes.UserId) ?? claims?.FindFirst("user_id");
                if (claim == null)
                {
                    throw new Exception($"Tài khoản không chứa claim \"{System.Security.Claims.ClaimTypes.NameIdentifier}\"");
                }
                return claim.Value;
            }
        }

}
