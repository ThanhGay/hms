using HMS.Auth.ApplicationService.Common;
using HMS.Auth.ApplicationService.UserModule.Abstracts;
using HMS.Auth.Domain;
using HMS.Auth.Dtos;
using HMS.Auth.Dtos.Customer;
using HMS.Auth.Dtos.Voucher;
using HMS.Auth.Infrastructures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Auth.ApplicationService.UserModule.Implements
{
    public class VoucherService : AuthServiceBase, IVoucherService
    {
        public VoucherService(ILogger<VoucherService> logger, AuthDbContext authDbContext ) : base( logger, authDbContext ) { }

        public AuthVoucher CreateVoucher([FromQuery] CreateVoucherDto input)
        {
            if(input.Percent > 100 || input.Percent <= 0)
            {
                throw new UserExceptions("Giảm giá phải lớn hơn 0% và bé hơn 100%");
            }
            var newVoucher = new AuthVoucher
            {
                Percent = input.Percent,
                StartDate = input.StartDate,
                ExpDate = input.ExpDate,
            };
            _dbContext.AuthVouchers.Add( newVoucher );
            _dbContext.SaveChanges();
            return newVoucher;
        }

        public AuthVoucher UpdateVoucher([FromQuery] UpdateVoucherDto input, int voucherId)
        {
            var findVoucher = _dbContext.AuthVouchers.FirstOrDefault(x => x.VoucherId == voucherId)
                ??  throw new UserExceptions("Không tồn tại voucher");
            
            findVoucher.Percent = input.Percent;
            findVoucher.StartDate = input.StartDate;
            findVoucher.ExpDate = input.ExpDate;
            _dbContext.AuthVouchers.Update( findVoucher );
            _dbContext.SaveChanges();
            return findVoucher;
        }

        public void DeleteVoucher([FromQuery] int voucherId)
        {
            var findVoucher = _dbContext.AuthVouchers.FirstOrDefault(x => x.VoucherId == voucherId)
                ?? throw new UserExceptions("Không tồn tại voucher");

            var findUser = from v in _dbContext.AuthCustomerVouchers
                           where v.VoucherId == voucherId
                           select v;
            _dbContext.AuthCustomerVouchers.RemoveRange(findUser.ToList());
            _dbContext.SaveChanges();
            _dbContext.AuthVouchers.Remove(findVoucher);
            _dbContext.SaveChanges();
        }

        public AuthVoucher GetVoucherById(int voucherId)
        {
            var findVoucher = _dbContext.AuthVouchers.FirstOrDefault(x => x.VoucherId == voucherId)
                ?? throw new UserExceptions("Không tồn tại voucher");
            return findVoucher;

        }

        public void SetVoucherToCustomer(int voucherId, int customerId)
        {
            var result = new AuthCustomerVoucher
            {
                CustomerId = customerId,
                VoucherId = voucherId
            };
            _dbContext.AuthCustomerVouchers.Add(result);
            _dbContext.SaveChanges();
        }

        public PageResultDto<AuthVoucher> GetAllVoucher([FromQuery] FilterDto input)
        {
            var result = new PageResultDto<AuthVoucher>();
            var query = _dbContext.AuthVouchers.Where(e =>
            string.IsNullOrEmpty(input.KeyWord)
            || e.VoucherId.ToString().ToLower().Contains(input.KeyWord.ToLower()));
            result.TotalItem = query.Count();
            query = query.OrderBy(e => e.VoucherId)
                         .Skip(input.Skip())
                         .Take(input.PageSize);
            result.Items = query.ToList();
            return result;
        }


    }
}
