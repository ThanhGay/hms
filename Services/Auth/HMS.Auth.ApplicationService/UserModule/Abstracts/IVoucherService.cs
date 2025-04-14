using HMS.Auth.ApplicationService.Common;
using HMS.Auth.Domain;
using HMS.Auth.Dtos.Voucher;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Auth.ApplicationService.UserModule.Abstracts
{
    public interface IVoucherService
    {
        AuthVoucher CreateVoucher([FromQuery] CreateVoucherDto input);
        void DeleteVoucher([FromQuery] int voucherId);
        PageResultDto<AuthVoucher> GetAllVoucher([FromQuery] FilterDto input);
        AuthVoucher GetVoucherById(int voucherId);
        void SetVoucherToCustomer(int voucherId, int customerId);
        AuthVoucher UpdateVoucher([FromBody] UpdateVoucherDto input);
    }
}
