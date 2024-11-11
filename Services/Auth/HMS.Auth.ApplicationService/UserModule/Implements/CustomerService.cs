using HMS.Auth.ApplicationService.Common;
using HMS.Auth.ApplicationService.UserModule.Abstracts;
using HMS.Auth.Domain;
using HMS.Auth.Dtos;
using HMS.Auth.Infrastructures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HMS.Auth.Dtos.Customer;
using HMS.Auth.Dtos.Receptionist;
using Microsoft.AspNetCore.Http;
using HMS.Shared.Constant.Permission;

namespace HMS.Auth.ApplicationService.UserModule.Implements
{
    public class CustomerService : AuthServiceBase, ICustomerService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public CustomerService(ILogger<ReceptionistService> logger, AuthDbContext dbContext, IHttpContextAccessor contextAccessor) : base(logger, dbContext) 
        {
            _contextAccessor = contextAccessor;
        }

        public AuthCustomer CreateCustomer([FromQuery] string email, string password, AddCustomer input)
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
                Console.WriteLine($"{item.permisstion}, {PermissionKeys.AddCustomer}");
                if (item.permisstion == PermissionKeys.AddCustomer)
                {
                    checkPermisstion++;
                }
            }
            if (checkPermisstion == 0)
            {
                throw new UserExceptions("Bạn Không được phép create customer");
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
                RoleId = 1
            };
            _dbContext.AuthUsers.Add(user);
            _dbContext.SaveChanges();

            var customer = new AuthCustomer
            {
                CustomerId = user.UserId,
                FirstName = input.FirstName,
                LastName = input.LastName,
                CitizenIdentity = input.CitizenIdentity,
                PhoneNumber = input.PhoneNumber,
                DateOfBirth = input.DateOfBirth,
            };
            _dbContext.AuthCustomers.Add(customer);
            _dbContext.SaveChanges();
            return customer;
        }
        public AuthCustomer UpdateInfCustomer(int customerId, AddCustomer input)
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
                Console.WriteLine($"{item.permisstion}, {PermissionKeys.UpdateInfCustomer}");
                if (item.permisstion == PermissionKeys.UpdateInfCustomer)
                {
                    checkPermisstion++;
                }
            }
            if (checkPermisstion == 0)
            {
                throw new UserExceptions("Bạn Không được phép update customer");
            }
            var findCustomer = _dbContext.AuthCustomers.FirstOrDefault(r => r.CustomerId == customerId)
                ?? throw new UserExceptions("Không tồn tại cutomer");
            if (findCustomer != null)
            {
                findCustomer.FirstName = input.FirstName;
                findCustomer.LastName = input.LastName;
                findCustomer.CitizenIdentity = input.CitizenIdentity;
                findCustomer.PhoneNumber = input.PhoneNumber;
                findCustomer.DateOfBirth = input.DateOfBirth;
            }
            _dbContext.AuthCustomers.Update(findCustomer);
            _dbContext.SaveChanges();
            return findCustomer;
        }
        public void DeleteCustomer(int customerId)
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
                Console.WriteLine($"{item.permisstion}, {PermissionKeys.DeleteCustomer}");
                if (item.permisstion == PermissionKeys.DeleteCustomer)
                {
                    checkPermisstion++;
                }
            }
            if (checkPermisstion == 0)
            {
                throw new UserExceptions("Bạn Không được phép delete customer");
            }
            var findCustomer = _dbContext.AuthUsers.FirstOrDefault(r => r.UserId == customerId)
            ?? throw new UserExceptions("Không tồn tại customer");
            findCustomer.IsDeleted = true;
            _dbContext.AuthUsers.Update(findCustomer);
            _dbContext.SaveChanges();
        }
    }
}
