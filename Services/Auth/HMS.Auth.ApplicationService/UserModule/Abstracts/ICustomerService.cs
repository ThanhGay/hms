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
        AuthCustomer CreateCustomer([FromQuery] string email, string password, AddCustomerDto input);
        void DeleteCustomer(int customerId);
        PageResultDto<AuthCustomer> GetAllCustomer([FromQuery] FilterDto input);
        PageResultDto<VoucherCustomerDto> GetAllVoucherByCustomer([FromQuery] FilterDto input, int customerId);
        AuthCustomer GetCustomerById([FromQuery] int id);
        AuthCustomer UpdateInfCustomer(int customerId, UpdateCustomerDto input);
    }
}
