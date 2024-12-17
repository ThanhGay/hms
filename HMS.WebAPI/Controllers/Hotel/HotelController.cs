using HMS.Hol.ApplicationService.HotelManager.Abstracts;
using HMS.Hol.ApplicationService.RoomManager.Abstracts;
using HMS.Hol.ApplicationService.RoomManager.Implements;
using HMS.Hol.Dtos.HotelManager;
using HMS.Hol.Dtos.RoomManager;
using HMS.Shared.Constant.Common;
using HMS.Shared.Constant.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.WebAPI.Controllers.Hotel
{
    [Route("api/hotel")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;
        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }
        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] { PermissionKeys.GetAllHotel})]
        [HttpGet("all")]
        public IActionResult GetAllHotel([FromQuery] FilterDto input)
        {
            try
            {
                return Ok(_hotelService.GetAllHotel(input));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] { PermissionKeys.CreateHotel })]
        [HttpPost("create")]
        public IActionResult CreateHotel(CreateHotelDto input)
        {
            try
            {
                return Ok(_hotelService.CreateHotel(input));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] { PermissionKeys.GetHotelById })]
        [HttpGet("get/{hotelId}")]
        public IActionResult GetById(int hotelId)
        {
            try
            {
                return Ok(_hotelService.GetById(hotelId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] { PermissionKeys.UpdateHotel })]
        [HttpPut("update")]
        public IActionResult UpdateHotel(UpdateHotelDto input)
        {
            try
            {
                return Ok(_hotelService.UpdateHotel(input));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] { PermissionKeys.DeleteHotelById })]
        [HttpDelete("delete/{hotelId}")]
        public IActionResult DeleteHotel(int hotelId)
        {
            try
            {
                _hotelService.DeleteHotel(hotelId);
                return Ok("Xóa hotel thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
