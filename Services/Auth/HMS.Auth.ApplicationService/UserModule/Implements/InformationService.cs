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
        public AuthVoucher GetVoucherCustomer(int voucherId, int customerId)
        {
            var AnyCustomer = _dbContext.AuthCustomers.Any(a => a.CustomerId == voucherId);
            if (!AnyCustomer)
            {
                _logger.LogError("Không tìm thấy người dùng");
                throw new UserExceptions("Không tìm thấy người dùng");
            }
            var findVou = _dbContext.AuthCustomerVouchers.Any(v => v.VoucherId == voucherId && v.CustomerId == customerId);
            if (!findVou)
            {
                _logger.LogError("Người dùng không có tại voucher này");
                throw new UserExceptions("Người dùng không có tại voucher này");
            }
            var result = _dbContext.AuthVouchers.FirstOrDefault(v => v.VoucherId == voucherId);
            return result;
        }
        
    }
}
