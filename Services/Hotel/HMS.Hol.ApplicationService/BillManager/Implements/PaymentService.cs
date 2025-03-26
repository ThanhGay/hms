using HMS.Hol.ApplicationService.BillManager.Abstracts;
using HMS.Hol.ApplicationService.Common;
using HMS.Hol.Infrastructures;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZaloPay.Helper.Crypto;
using ZaloPay.Helper;

namespace HMS.Hol.ApplicationService.BillManager.Implements
{
    public class PaymentService : HotelServiceBase, IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly Utils _utils;

        public PaymentService(ILogger<PaymentService> logger, HotelDbContext dbContext, IConfiguration configuration, Utils utils)
            : base(logger, dbContext)
        {
            _configuration = configuration;
            _utils = utils;
        }

        // Public method for VnPay
        public string GenerateVnpayUrl(VnPayRequest request, string vnp_Url, string vnp_HashSecret, string vnp_TmnCode, string vnp_Returnurl, string ipAddress)
        {
            VnPayLibrary vnpay = new VnPayLibrary();
            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", ((int)(request.Amount * 100)).ToString());
            vnpay.AddRequestData("vnp_OrderInfo", request.OrderDesc);
            vnpay.AddRequestData("vnp_OrderType", request.OrderType);
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", request.OrderId.ToString());
            vnpay.AddRequestData("vnp_ExpireDate", DateTime.Now.AddMinutes(15).ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", ipAddress);
            vnpay.AddRequestData("vnp_Locale", "vn");

            return vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
        }

        // Public method for ZaloPay
        public async Task<Dictionary<string, object>> GenerateZaloPayPaymentAsync(VnPayRequest request, string zalo_app_id, string zalo_key1, string zalo_paymentUrl, string zalo_app_user)
        {
            var embed_data = new { };
            var items = new[] { new { } };
            var param = new Dictionary<string, string>
        {
            { "app_id", zalo_app_id },
            { "app_user", zalo_app_user },
            { "app_time", ZaloUtils.GetTimeStamp().ToString() },
            { "amount", request.Amount.ToString() },
            { "app_trans_id", DateTime.Now.ToString("yyMMdd") + "_" + request.OrderId },
            { "embed_data", JsonConvert.SerializeObject(embed_data) },
            { "item", JsonConvert.SerializeObject(items) },
            { "description", request.OrderDesc },
            { "bank_code", request.OrderType ?? "zalopayapp" }
        };

            var data = $"{zalo_app_id}|{param["app_trans_id"]}|{param["app_user"]}|{param["amount"]}|{param["app_time"]}|{param["embed_data"]}|{param["item"]}";
            param.Add("mac", HmacHelper.Compute(ZaloPayHMAC.HMACSHA256, zalo_key1, data));

            return await HttpHelper.PostFormAsync(zalo_paymentUrl, param);
        }
    }




}

