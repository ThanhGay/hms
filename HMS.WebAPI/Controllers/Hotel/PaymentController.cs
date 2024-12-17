using HMS.Hol.ApplicationService.BillManager.Abstracts;
using HMS.Hol.ApplicationService.Common;
using HMS.Shared.Constant.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.WebAPI.Controllers.Hotel
{
    [ApiController]
    [Route("api/payment")]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly IConfiguration _configuration;
        private readonly Utils _utils;

        public PaymentController(IPaymentService paymentService, IConfiguration configuration, Utils utils)
        {
            _paymentService = paymentService;
            _configuration = configuration;
            _utils = utils;
        }

        /// <summary>
        /// Tạo URL thanh toán cho VNPAY hoặc ZaloPay
        /// </summary>
        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] { PermissionKeys.CreatePaymentUrl })]
        [HttpPost("create-payment-url")]
        public async Task<IActionResult> CreatePaymentUrl([FromBody] VnPayRequest request)
        {
            try
            {
                // Lấy cấu hình
                var ipAddress = _utils.GetIpAddress();

                string vnp_Url = _configuration["Vnpay:PaymentUrl"];
                string vnp_HashSecret = _configuration["Vnpay:HashSecret"];
                string vnp_TmnCode = _configuration["Vnpay:TmnCode"];
                string vnp_Returnurl = _configuration["Vnpay:ReturnUrl"];

                string zalo_app_id = _configuration["Zalopay:Appid"];
                string zalo_key1 = _configuration["Zalopay:Key1"];
                string zalo_paymentUrl = _configuration["Zalopay:PaymentUrl"];
                string zalo_app_user = _configuration["Zalopay:AppUser"];

                // Xử lý theo loại thanh toán
                if (request.OrderType == "vnpay")
                {
                    var paymentUrl = _paymentService.GenerateVnpayUrl(request, vnp_Url, vnp_HashSecret, vnp_TmnCode, vnp_Returnurl, ipAddress);
                    return Ok(new { PaymentUrl = paymentUrl });
                }
                else if (request.OrderType == "zalopayapp")
                {
                    var result = await _paymentService.GenerateZaloPayPaymentAsync(request, zalo_app_id, zalo_key1, zalo_paymentUrl, zalo_app_user);
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Invalid payment type.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Xử lý URL trả về từ VNPAY
        /// </summary>
        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] { PermissionKeys.ReturnVnPayUrl })]
        [HttpGet("return-vnpay-url")]
        public IActionResult ReturnVnpayUrl([FromQuery] VnPayResponse response)
        {
            try
            {
                string vnp_HashSecret = _configuration["Vnpay:HashSecret"];
                var result = new VnPayResponse
                {
                    vnp_Amount = response.vnp_Amount,
                    vnp_BankCode = response.vnp_BankCode,
                    vnp_ResponseCode = response.vnp_ResponseCode ?? "",
                    vnp_BankTranNo = response.vnp_BankTranNo,
                    vnp_CardType = response.vnp_CardType,
                    vnp_OrderInfo = response.vnp_OrderInfo,
                    vnp_PayDate = response.vnp_PayDate,
                    vnp_SecureHash = response.vnp_SecureHash,
                    vnp_TmnCode = response.vnp_TmnCode,
                    vnp_TransactionNo = response.vnp_TransactionNo,
                    vnp_TransactionStatus = response.vnp_TransactionStatus,
                    vnp_TxnRef = response.vnp_TxnRef,
                };

                if (response.vnp_ResponseCode == "00")
                {
                    return Ok(new { message = "Thanh toán thành công", data = result });
                }
                else
                {
                    return Ok(new { message = "Thanh toán thất bại", data = result });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
