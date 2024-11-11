using HMS.Auth.ApplicationService.Common;
using HMS.Auth.ApplicationService.UserModule.Abstracts;
using HMS.Auth.Domain;
using HMS.Auth.Dtos;
using HMS.Auth.Dtos.Receptionist;
using HMS.Auth.Infrastructures;
using HMS.Shared.Constant.Permission;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Auth.ApplicationService.UserModule.Implements
{
    public class ReceptionistService : AuthServiceBase, IReceptionistService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public ReceptionistService(ILogger<ReceptionistService> logger, AuthDbContext dbContext, IHttpContextAccessor contextAccessor) : base(logger, dbContext) 
        {
            _contextAccessor = contextAccessor;
        }

        public AuthReceptionist CreateReceptionist([FromQuery] string email, string password, AddReceptionistDto input)
        {
            int userId = CommonUntil.GetCurrentUserId(_contextAccessor);
            var permisstionKey = from u in _dbContext.AuthUsers
                                 join up in _dbContext.AuthRolesPermissions on u.RoleId equals up.RoleId
                                 where u.UserId == userId
                                 select new
                                 {
                                     roleId = u.RoleId,
                                     permisstion = up.PermissonKey,
                                 };
            int checkPermisstion = 0;
            foreach (var item in permisstionKey)
            {
                Console.WriteLine($"{item.permisstion}, {PermissionKeys.AddReceptionist}");
                if (item.permisstion == PermissionKeys.AddReceptionist)
                {
                    checkPermisstion++;
                }
            }
            if (checkPermisstion == 0)
            {
                throw new UserExceptions("Bạn Không được phép create receptionist");
            }
            var findEmail = _dbContext.AuthUsers.Any(u => u.Email == email);
            if (findEmail)
            {
                throw new UserExceptions("Đã tồn tại email");
            }
            var user = new AuthUser
            {
                Email = email,
                Password = BCrypt.Net.BCrypt.HashPassword(password),
                RoleId = 2
            };
            _dbContext.AuthUsers.Add(user);
            _dbContext.SaveChanges();

            var receptionist = new AuthReceptionist
            {
                ReceptionistId = user.UserId,
                FirstName = input.FirstName,
                LastName = input.LastName,
                CitizenIdentity = input.CitizenIdentity,
                PhoneNumber = input.PhoneNumber,
                DateOfBirth = input.DateOfBirth,
            };
            _dbContext.AuthReceptionists.Add(receptionist);
            _dbContext.SaveChanges();
            return receptionist;
        }
        public AuthReceptionist UpdateInfReceptionist(int receptionistId, AddReceptionistDto input)
        {
            int userId = CommonUntil.GetCurrentUserId(_contextAccessor);
            var permisstionKey = from u in _dbContext.AuthUsers
                                 join up in _dbContext.AuthRolesPermissions on u.RoleId equals up.RoleId
                                 where u.UserId == userId
                                 select new
                                 {
                                     roleId = u.RoleId,
                                     permisstion = up.PermissonKey,
                                 };
            int checkPermisstion = 0;
            foreach (var item in permisstionKey)
            {
                Console.WriteLine($"{item.permisstion}, {PermissionKeys.UpdateInfReceptionist}");
                if (item.permisstion == PermissionKeys.UpdateInfReceptionist)
                {
                    checkPermisstion++;
                }
            }
            if (checkPermisstion == 0)
            {
                throw new UserExceptions("Bạn Không được phép update receptionist");
            }
            var findReceptionist = _dbContext.AuthReceptionists.FirstOrDefault(r => r.ReceptionistId == receptionistId)
                ?? throw new UserExceptions("Không tồn tại receptioníst");
            if (findReceptionist != null)
            {
                findReceptionist.FirstName = input.FirstName;
                findReceptionist.LastName = input.LastName;
                findReceptionist.CitizenIdentity = input.CitizenIdentity;
                findReceptionist.PhoneNumber = input.PhoneNumber;
                findReceptionist.DateOfBirth = input.DateOfBirth;
            }
            _dbContext.AuthReceptionists.Update(findReceptionist);
            _dbContext.SaveChanges();
            return findReceptionist;
        }
        public void DeleteReceptionist(int receptionistId)
        {
            int userId = CommonUntil.GetCurrentUserId(_contextAccessor);
            var permisstionKey = from u in _dbContext.AuthUsers
                                 join up in _dbContext.AuthRolesPermissions on u.RoleId equals up.RoleId
                                 where u.UserId == userId
                                 select new
                                 {
                                     roleId = u.RoleId,
                                     permisstion = up.PermissonKey,
                                 };
            int checkPermisstion = 0;
            foreach (var item in permisstionKey)
            {
                Console.WriteLine($"{item.permisstion}, {PermissionKeys.DeleteReceptionist}");
                if (item.permisstion == PermissionKeys.DeleteReceptionist)
                {
                    checkPermisstion++;
                }
            }
            if (checkPermisstion == 0)
            {
                throw new UserExceptions("Bạn Không được phép delete receptionist");
            }
            var findReceptionist = _dbContext.AuthUsers.FirstOrDefault(r => r.UserId == receptionistId)
            ?? throw new UserExceptions("Không tồn tại receptioníst");
            findReceptionist.IsDeleted = true;
            _dbContext.AuthUsers.Update(findReceptionist);
            _dbContext.SaveChanges();
        }
    }
}
