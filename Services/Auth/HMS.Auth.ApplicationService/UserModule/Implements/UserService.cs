using BCrypt.Net;
using HMS.Auth.ApplicationService.Common;
using HMS.Auth.ApplicationService.UserModule.Abstracts;
using HMS.Auth.Domain;
using HMS.Auth.Dtos;
using HMS.Auth.Infrastructures;
using HMS.Shared.ApplicationService.Notification;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HMS.Auth.ApplicationService.UserModule.Implements
{
    public class UserService : AuthServiceBase, IUserService
    {
        private readonly IConfiguration _configuration;
        private static List<string> blackList = new List<string>();
        private static Dictionary<string, (string Otp, DateTime Expiry)> otpStore = new Dictionary<string, (string, DateTime)>();
        private readonly INotificationService _notificationService;
        public UserService(ILogger<UserService> logger, AuthDbContext dbContext,IConfiguration configuration, INotificationService notificationService) : base(logger, dbContext) 
        {
            _configuration = configuration;
            _notificationService = notificationService;
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
                expires: DateTime.Now.AddMinutes(_configuration.GetValue<int>("JwtSettings:ExpiryMinutes")),
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

        public void AddToBlacklist(string token)

        {
            if (!blackList.Contains(token))
            {
                blackList.Add(token);
            }

        }

        public bool IsTokenBlacklisted(string token)
        {
            return blackList.Contains(token);
        }

        public async Task ForgotPassword([FromQuery] string email)
        {
            var findEmail = _dbContext.AuthUsers.Any( u => u.Email == email );
            if (!findEmail) { throw new UserExceptions("Tài khoản chưa đăng kí"); }
            Random random = new Random();
            string randomNumber = random.Next(0, 1000000).ToString("D6");
            otpStore[email] = (randomNumber, DateTime.Now.AddMinutes(5));
            await _notificationService.SendEmail(email, "OTP của bạn để lấy lại mật khẩu", randomNumber ); 
        }

        public void ResetPassword(string email, string otp, string password)
        {
            if (otpStore.ContainsKey(email))
            {
                var (storedOtp, expiry) = otpStore[email];
                if(DateTime.Now > expiry)
                {
                    otpStore.Remove(email);
                    throw new UserExceptions("Đã hết hạn Otp");
                }
                if(storedOtp == otp)
                {
                    var findUser = _dbContext.AuthUsers.FirstOrDefault(u => u.Email == email);
                    findUser.Password = BCrypt.Net.BCrypt.HashPassword(password);

                    otpStore.Remove(email);

                    _dbContext.AuthUsers.Update(findUser);
                    _dbContext.SaveChanges();
                }
            }   
        }
    }
}
