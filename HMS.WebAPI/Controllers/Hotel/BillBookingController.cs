
using HMS.Hol.ApplicationService.BillManager.Abstracts;
using HMS.Hol.ApplicationService.Common;
using HMS.Hol.Dtos.BookingManager;
using HMS.Shared.Constant.Common;
using HMS.Shared.Constant.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;

namespace HMS.WebAPI.Controllers.Hotel
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillBookingController : ControllerBase
    {
        private readonly IBillBookingService _billBookingService;
        public BillBookingController(IBillBookingService billBookingService)
        {
            _billBookingService = billBookingService;
        }
        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new Object[] { PermissionKeys.CreateBooking })]
        [HttpPost("create-booking")]
        public IActionResult CreateBooking([FromBody] CreateBookingDto input)
        {
            var result = _billBookingService.CreateBooking(input);
            return Ok(new { message = "Thêm thành công", data = result });
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new Object[] { PermissionKeys.CreatePreBooking })]
        [HttpPost("create-pre-booking")]
        public IActionResult CreatePreBooking([FromBody] CreatePreBookingDto input)
        {
            var result = _billBookingService.CreatePreBooking(input);
            return Ok(new { message = "Thêm thành công", data = result });
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new Object[] { PermissionKeys.CreateCharge })]
        [HttpPost("create-charge")]
        public IActionResult CreateCharge([FromBody] CreateChargeDto input)
        {
            var result = _billBookingService.CreateCharge(input);
            return Ok(new { message = "Thêm thành công", data = result });
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new Object[] { PermissionKeys.CreateBookingRoom })]
        [HttpPost("create-booking_room")]
        public IActionResult CreateBooking_Room(int roomIds, int bookingId)
        {
            _billBookingService.CreateBooking_Room(roomIds, bookingId); ;
            return Ok(new { message = "Thêm thành công", });
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new Object[] { PermissionKeys.CreateBookingCharge })]
        [HttpPost("create-booking_charge")]
        public IActionResult CreateBooking_Charge(int chargeIds, int bookingId)
        {
            _billBookingService.CreateBooking_Room(chargeIds, bookingId); ;
            return Ok(new { message = "Thêm thành công", });
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new Object[] { PermissionKeys.CheckIn })]
        [HttpPut("check-in")]
        public IActionResult CheckIn(CheckInDto checkIn)
        {
            _billBookingService.CheckIn(checkIn);
            return Ok(new { message = "Check in thành công" });
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new Object[] { PermissionKeys.CheckOut })]
        [HttpPut("check-out")]
        public IActionResult CheckOut(CheckOutDto checkOut)
        {
            _billBookingService.CheckOut(checkOut);
            return Ok(new { message = "Check out thành công" });
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new Object[] { PermissionKeys.UpdateBooking })]
        [HttpPut("update-booking")]
        public IActionResult UpdateBooking([FromBody] BookingDto input)
        {
            _billBookingService.UpdateBooking(input);
            return Ok(new { message = "Cập nhật thành công" });
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new Object[] { PermissionKeys.DeleteBookingById })]
        [HttpDelete("delete-booking/{id}")]
        public IActionResult DeleteBooking(int id)
        {
            _billBookingService.DeleteBooking(id);
            return Ok(new { message = "Xóa thành công" });
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new Object[] { PermissionKeys.GetBookingById })]
        [HttpGet("get-booking/{id}")]
        public IActionResult GetIdBooking(int id)
        {

            return Ok(_billBookingService.GetIdBooking(id));
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new Object[] { PermissionKeys.GetAllBooking })]
        [HttpGet("get-all-booking")]
        public IActionResult GetAllBooking([FromQuery] FilterDto input)
        {

            return Ok(_billBookingService.GetAllBooking(input));

        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new Object[] { PermissionKeys.GetExpectedTotalByBillId })]
        [HttpGet("get-expected-total-by-billId/{billId}")]
        public IActionResult GetExpectedTotalByBillId(int billId)
        {
            var result = _billBookingService.GetExpectedTotalByBillId(billId);
            return Ok(new
            {
                message = $"Tiền dự đoán: {result}đ"
            });

        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new Object[] { PermissionKeys.GetTotalAmountByBillId })]
        [HttpGet("get-total-amount-by-billId/{billId}")]
        public IActionResult GetTotalAmountByBillId(int billId)
        {
            var result = _billBookingService.GetTotalAmountByBillId(billId);
            return Ok(new
            {
                message = $"Tổng tiền thanh toán: {result}đ"
            });

        }


    }
}
