
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
    [Route("api/bill-booking")]
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
            try
            {
                var result = _billBookingService.CreateBooking(input);
                return Ok(new { message = "Thêm thành công", data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new Object[] { PermissionKeys.CreatePreBooking })]
        [HttpPost("create-pre-booking")]
        public IActionResult CreatePreBooking([FromBody] CreatePreBookingDto input)
        {

            try
            {
                var result = _billBookingService.CreatePreBooking(input);
                return Ok(new { message = "Thêm thành công", data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new Object[] { PermissionKeys.CreateCharge })]
        [HttpPost("create-charge")]
        public IActionResult CreateCharge([FromBody] CreateChargeDto input)
        {

            try
            {
                var result = _billBookingService.CreateCharge(input);
                return Ok(new { message = "Thêm thành công", data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new Object[] { PermissionKeys.CreateBookingRoom })]
        [HttpPost("create-booking-room")]
        public IActionResult CreateBooking_Room(int roomIds, int bookingId)
        {

            try
            {
                _billBookingService.CreateBooking_Room(roomIds, bookingId); ;
                return Ok(new { message = "Thêm thành công", });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new Object[] { PermissionKeys.CreateBookingCharge })]
        [HttpPost("create-booking-charge")]
        public IActionResult CreateBooking_Charge(int chargeIds, int bookingId)
        {

            try
            {
                _billBookingService.CreateBooking_Room(chargeIds, bookingId); ;
                return Ok(new { message = "Thêm thành công", });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new Object[] { PermissionKeys.CheckIn })]
        [HttpPut("check-in")]
        public IActionResult CheckIn(CheckInDto checkIn)
        {

            try
            {
                _billBookingService.CheckIn(checkIn);
                return Ok(new { message = "Check in thành công" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new Object[] { PermissionKeys.CheckOut })]
        [HttpPut("check-out")]
        public IActionResult CheckOut(CheckOutDto checkOut)
        {

            try
            {
                _billBookingService.CheckOut(checkOut);
                return Ok(new { message = "Check out thành công" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new Object[] { PermissionKeys.UpdateBooking })]
        [HttpPut("update-booking")]
        public IActionResult UpdateBooking([FromBody] BookingDto input)
        {

            try
            {
                _billBookingService.UpdateBooking(input);
                return Ok(new { message = "Cập nhật thành công" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new Object[] { PermissionKeys.DeleteBookingById })]
        [HttpDelete("delete-booking/{id}")]
        public IActionResult DeleteBooking(int id)
        {

            try
            {
                _billBookingService.DeleteBooking(id);
                return Ok(new { message = "Xóa thành công" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new Object[] { PermissionKeys.GetBookingById })]
        [HttpGet("get-booking/{id}")]
        public IActionResult GetIdBooking(int id)
        {

            try
            {
                return Ok(_billBookingService.GetIdBooking(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new Object[] { PermissionKeys.GetAllBooking })]
        [HttpGet("get-all-booking")]
        public IActionResult GetAllBooking([FromQuery] FilterDto input)
        {

            try
            {
                return Ok(_billBookingService.GetAllBooking(input));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new Object[] { PermissionKeys.GetExpectedTotalByBillId })]
        [HttpGet("get-expected-total-by-billId/{billId}")]
        public IActionResult GetExpectedTotalByBillId(int billId)
        {


            try
            {
                var result = _billBookingService.GetExpectedTotalByBillId(billId);
                return Ok(new
                {
                    message = $"Tiền dự đoán: {result}đ"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new Object[] { PermissionKeys.GetTotalAmountByBillId })]
        [HttpGet("get-total-amount-by-billId/{billId}")]
        public IActionResult GetTotalAmountByBillId(int billId)
        {

            try
            {
                var result = _billBookingService.GetTotalAmountByBillId(billId);
                return Ok(new
                {
                    message = $"Tổng tiền thanh toán: {result}đ"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new Object[] { PermissionKeys.UpdateCharge })]
        [HttpPut("update-charge")]
        public IActionResult UpdateCharge([FromBody] ChargeDto input)
        {

            try
            {
                _billBookingService.UpdateCharge(input);
                return Ok(new { message = "Cập nhật thành công" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new Object[] { PermissionKeys.DeleteCharge })]
        [HttpDelete("delete-charge/{id}")]
        public IActionResult DeleteCharge(int id)
        {

            try
            {
                _billBookingService.DeleteCharge(id);
                return Ok(new { message = "Xóa thành công" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new Object[] { PermissionKeys.GetChargeById })]
        [HttpGet("get-charge/{id}")]
        public IActionResult GetChargeById(int id)
        {

            try
            {
                return Ok(_billBookingService.GetChargeById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new Object[] { PermissionKeys.GetChargeByIdBooking })]
        [HttpGet("get-all-charge-by-booking/{id}")]
        public IActionResult GetChargeByIdBooking(int id)
        {
            try
            {
                return Ok(_billBookingService.GetChargeByIdBooking(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new Object[] { PermissionKeys.CancelBooking })]
        [HttpPut("cancel-booking")]
        public IActionResult CancelBooking(int bookingId)
        {

            try
            {
                _billBookingService.CancelBooking(bookingId);
                return Ok(new { message = "Cập nhật thành công" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
