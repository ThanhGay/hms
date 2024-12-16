using HMS.Auth.ApplicationService.Common;
using HMS.Auth.ApplicationService.UserModule.Abstracts;
using HMS.Auth.Domain;
using HMS.Auth.Dtos;
using HMS.Auth.Infrastructures;
using HMS.Shared.ApplicationService.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HMS.Auth.ApplicationService.UserModule.Implements
{
    public class InformationService :  AuthServiceBase,IInformationService
    {
        public InformationService(ILogger<UserService> logger, AuthDbContext dbContext) : base(logger, dbContext)
        {
        }
        public AuthCustomer GetCustomerById([FromQuery] int id)
        {
            var findCustomer = _dbContext.AuthCustomers.FirstOrDefault(r => r.CustomerId == id)
                ?? throw new UserExceptions("Không tồn tại customer");
            var checkDelete = _dbContext.AuthUsers.FirstOrDefault(u => u.UserId == id);

            if (checkDelete.IsDeleted)
            {
                throw new UserExceptions("Người dùng đã bị xóa");
            }

            return findCustomer;
        }
    }
}
