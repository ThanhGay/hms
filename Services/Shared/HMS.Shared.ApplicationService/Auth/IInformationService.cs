using HMS.Auth.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Shared.ApplicationService.Auth
{
    public interface IInformationService
    {
        int CheckVoucher(int? voucherId, int customerId);
        AuthCustomer GetCustomerById(int id);
        AuthReceptionist GetReceptionistById(int receptionistId);
        float GetVoucherCustomer(int? voucherId);
        void UseVoucher(int? voucherId, DateOnly useAt);
    }
}
