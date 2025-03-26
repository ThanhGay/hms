using HMS.Hol.ApplicationService.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HMS.Hol.ApplicationService.BillManager.Abstracts
{
    public interface IPaymentService
    {
        string GenerateVnpayUrl(VnPayRequest request, string vnp_Url, string vnp_HashSecret, string vnp_TmnCode, string vnp_Returnurl, string ipAddress);

        Task<Dictionary<string, object>> GenerateZaloPayPaymentAsync(VnPayRequest request, string zalo_app_id, string zalo_key1, string zalo_paymentUrl, string zalo_app_user);
    }
}
