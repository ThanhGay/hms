using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Auth.Infrastructures;
using HMS.Auth.Dtos;
using System.Xml.Linq;
using HMS.Auth.ApplicationService.UserModule.Abstracts;

namespace HMS.WebAPI
{
    public class AuthorizationFilter : Attribute, IAuthorizationFilter
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly string _permissionKey;
        public AuthorizationFilter(IHttpContextAccessor contextAccessor, string permisstionKey)
        {
            _contextAccessor = contextAccessor;
            _permissionKey = permisstionKey;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // get dbcontext in filter Attribute, IAuthorizationFilter
            // hash mask
            var user = context.HttpContext.User;
            var claims = user.Claims.ToList();
            var dbContext = context.HttpContext.RequestServices.GetService<AuthDbContext>();
            var userId = CommonUntil.GetCurrentUserId(_contextAccessor);

            var listpermisstion = from au in dbContext.AuthUsers
                                  join ap in dbContext.AuthRolesPermissions on au.RoleId equals ap.RoleId
                                  where au.UserId == userId
                                  select new
                                  {
                                      roleId = au.RoleId,
                                      permissionKey = ap.PermissonKey
                                  };
            var permissionName = dbContext.AuthPermissions.FirstOrDefault(p => p.PermissonKey == _permissionKey);
            int checkPermission = 0;
            foreach (var item in listpermisstion)
            {
                if (item.permissionKey == _permissionKey)
                {
                    checkPermission++;
                }
            }
            if (checkPermission == 0)
            {
                context.Result = new UnauthorizedObjectResult(new { message = $"Bạn không có quyền: {permissionName.PermissionName}" });
            }

        }
    }
}
