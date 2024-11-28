
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

    }
}
