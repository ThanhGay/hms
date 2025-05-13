using HMS.Auth.Domain;

namespace HMS.Shared.ApplicationService.Auth
{
    public interface IInformationService
    {
        int CheckVoucher(int? voucherId, int customerId);
        AuthCustomer GetCustomerById(int? id);
        AuthReceptionist GetReceptionistById(int receptionistId);
        float GetVoucherCustomer(int? voucherId);
        void UseVoucher(int? voucherId, DateOnly useAt);
    }
}
