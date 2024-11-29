
using HMS.Hol.ApplicationService.BillManager.Abstracts;
using HMS.Hol.ApplicationService.Common;
using HMS.Hol.Dtos.BookingManager;
using HMS.Shared.Constant.Common;
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

        [HttpPost("create-booking")]
        public IActionResult CreateBooking([FromBody] CreateBookingDto input)
        {
            var result = _billBookingService.CreateBooking(input);
            return Ok(new { message = "Thêm thành công", data = result });
        }

        [HttpPost("create-pre-booking")]
        public IActionResult CreatePreBooking([FromBody] CreatePreBookingDto input)
        {
            var result = _billBookingService.CreatePreBooking(input);
            return Ok(new { message = "Thêm thành công", data = result });
        }

        [HttpPost("create-charge")]
        public IActionResult CreateCharge([FromBody] CreateChargeDto input)
        {
            var result = _billBookingService.CreateCharge(input);
            return Ok(new { message = "Thêm thành công", data = result });
        }

        [HttpPost("create-booking_room")]
        public IActionResult CreateBooking_Room(int roomIds, int bookingId)
        {
            _billBookingService.CreateBooking_Room(roomIds, bookingId); ;
            return Ok(new { message = "Thêm thành công", });
        }

        [HttpPost("create-booking_charge")]
        public IActionResult CreateBooking_Charge(int chargeIds, int bookingId)
        {
            _billBookingService.CreateBooking_Room(chargeIds, bookingId); ;
            return Ok(new { message = "Thêm thành công", });
        }

        [HttpPut("check-in")]
        public IActionResult CheckIn(CheckInDto checkIn)
        {
            _billBookingService.CheckIn(checkIn);
            return Ok(new { message = "Check in thành công" });
        }

        [HttpPut("check-out")]
        public IActionResult CheckOut(CheckOutDto checkOut)
        {
            _billBookingService.CheckOut(checkOut);
            return Ok(new { message = "Check out thành công" });
        }



        [HttpPut("update-booking")]
        public IActionResult UpdateBooking([FromBody] BookingDto input)
        {
            _billBookingService.UpdateBooking(input);
            return Ok(new { message = "Cập nhật thành công" });
        }

        [HttpDelete("delete-booking/{id}")]
        public IActionResult DeleteBooking(int id)
        {
            _billBookingService.DeleteBooking(id);
            return Ok(new { message = "Xóa thành công" });
        }

        [HttpGet("get-booking/{id}")]
        public IActionResult GetIdBooking(int id)
        {

            return Ok(_billBookingService.GetIdBooking(id));
        }

        [HttpGet("get-all-booking")]
        public IActionResult GetAllBooking([FromQuery] FilterDto input)
        {

            return Ok(_billBookingService.GetAllBooking(input));

        }

        [HttpGet("get-expected-total-by-billId/{billId}")]
        public IActionResult GetExpectedTotalByBillId(int billId)
        {
            var result = _billBookingService.GetExpectedTotalByBillId(billId);
            return Ok(new
            {
                message = $"Tiền dự đoán: {result}đ"
            });

        }

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
