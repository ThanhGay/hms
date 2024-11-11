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

namespace HMS.WebAPI
{
    public class AuthorizationFilter : Attribute, IAuthorizationFilter
    {
        private readonly string Role;
        public AuthorizationFilter(string role)
        {
            Role = role;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            var claims = user.Claims.ToList();
            //if else
            var check = Role.Split(",").Contains(claims[4].Value);
            //var check = claims[4].Value.Split(", ").Contains(Role.Trim());
            if (!check)
            {
                context.Result = new UnauthorizedObjectResult(new { message = $"Bạn không có quyền" });
            }
        }
    }
}
