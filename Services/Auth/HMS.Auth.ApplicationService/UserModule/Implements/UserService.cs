using BCrypt.Net;
using HMS.Auth.ApplicationService.Common;
using HMS.Auth.ApplicationService.UserModule.Abstracts;
using HMS.Auth.Domain;
using HMS.Auth.Dtos;
using HMS.Auth.Infrastructures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HMS.Auth.ApplicationService.UserModule.Implements
{
    public class UserService : AuthServiceBase, IUserService
    {
        private readonly IConfiguration _configuration;
        public UserService(ILogger<UserService> logger, AuthDbContext dbContext,IConfiguration configuration) : base(logger, dbContext) 
        {
            _configuration = configuration;
        }

        private string Createtokens(UserDto input,int role)
        {

            var claims = new[]
            {
                new Claim(CustomClaimTypes.UserId, $"{input.UserId}"),
                new Claim(CustomClaimTypes.FullName, input.FirstName + input.LastName),
                new Claim(CustomClaimTypes.DateOfBirth, $"{input.DateOfBirth}"),
                new Claim(CustomClaimTypes.PhoneNumber, input.PhoneNumber),
                new Claim(CustomClaimTypes.Role, $"{role}")


            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims, 
                expires: DateTime.Now.AddMinutes(_configuration.GetValue<int>("JWT:ExpiryMinutes")),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ResultLogin Login([FromQuery] LoginDto input)
        {
            var checkDelete = _dbContext.AuthUsers.Any(e => e.Email == input.Email && e.IsDeleted == true);
            if (checkDelete) { throw new UserExceptions("Tài khoản đã bị xóa"); };
            var resultAuth = _dbContext.AuthUsers.FirstOrDefault(a => a.Email == input.Email)
                ?? throw new UserExceptions("Không tồn tại Email");
            var checkPassword = BCrypt.Net.BCrypt.Verify(input.Password, resultAuth.Password);
            if (checkPassword)
            {
                var result = new ResultLogin();
                var user = new UserDto();
                if (resultAuth.RoleId == 1)
                {
                    var findCustomer = _dbContext.AuthCustomers.FirstOrDefault(c => c.CustomerId == resultAuth.UserId);
                    user.UserId = findCustomer.CustomerId;
                    user.FirstName = findCustomer.FirstName;
                    user.LastName = findCustomer.LastName;
                    user.PhoneNumber = findCustomer.PhoneNumber;
                    user.CitizenIdentity = findCustomer.CitizenIdentity;
                    user.DateOfBirth = findCustomer.DateOfBirth;
                    result.User = user;
                    result.Token = Createtokens(user, resultAuth.RoleId);
                }
                else
                {
                    var findCustomer = _dbContext.AuthReceptionists.FirstOrDefault(c => c.ReceptionistId == resultAuth.UserId);
                    user.UserId = findCustomer.ReceptionistId;
                    user.FirstName = findCustomer.FirstName;
                    user.LastName = findCustomer.LastName;
                    user.PhoneNumber = findCustomer.PhoneNumber;
                    user.CitizenIdentity = findCustomer.CitizenIdentity;
                    user.DateOfBirth = findCustomer.DateOfBirth;
                    result.User = user;

                    result.Token = Createtokens(user, resultAuth.RoleId);
                }
                return result;
            }
            else
            {
                throw new UserExceptions("Sai mật khẩu");
            }
        }

        public List<string> GetFunctionManager()
        {
            var func = new List<string>();

            var findFunc = from p in _dbContext.AuthRolesPermissions
                           join pn in _dbContext.AuthPermissions on p.PermissonKey equals pn.PermissonKey
                           where p.RoleId == 3
                           select new
                           {
                               permission = p.PermissonKey,
                               permissionName = pn.PermissionName
                           };
            foreach (var item in findFunc)
            {
                func.Add(item.permissionName);
            }
            return func;
        }

        public List<string> GetFunctionReceptionist()
        {
            var func = new List<string>();

            var findFunc = from p in _dbContext.AuthRolesPermissions
                           join pn in _dbContext.AuthPermissions on p.PermissonKey equals pn.PermissonKey
                           where p.RoleId == 2
                           select new
                           {
                               permission = p.PermissonKey,
                               permissionName = pn.PermissionName
                           };
            foreach (var item in findFunc)
            {
                func.Add(item.permissionName);
            }
            return func;
        }

        public List<string> GetFunctionCustomer()
        {
            var func = new List<string>();

            var findFunc = from p in _dbContext.AuthRolesPermissions
                           join pn in _dbContext.AuthPermissions on p.PermissonKey equals pn.PermissonKey
                           where p.RoleId == 1
                           select new
                           {
                               permission = p.PermissonKey,
                               permissionName = pn.PermissionName
                           };
            foreach (var item in findFunc)
            {
                func.Add(item.permissionName);
            }
            return func;
        }
    }
}
