using HMS.Auth.ApplicationService.Common;
using HMS.Auth.Domain;
using HMS.Auth.Dtos.Customer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Auth.ApplicationService.UserModule.Abstracts
{
    public interface ICustomerService
    {
        AuthFavouriteRoom AddFavourite(int roomId);
        AuthCustomer CreateCustomer([FromBody] AddCustomerDto input);
        void DeleteCustomer(int customerId);
        PageResultDto<AuthCustomer> GetAllCustomer([FromQuery] FilterDto input);
        PageResultDto<FavouriteRoomDto> GetAllFavourite([FromQuery] FilterDto input);
        PageResultDto<VoucherCustomerDto> GetAllVoucherByCustomer([FromQuery] FilterDto input);
        List<AuthVoucher> GetAllVoucherUse(int customerId);
        AuthCustomer GetCustomerById([FromQuery] int id);
        void RemoveFavourite(int roomId);
        AuthCustomer UpdateInfCustomer(UpdateCustomerDto input);
    }
}
