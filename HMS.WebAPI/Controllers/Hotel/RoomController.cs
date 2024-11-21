using HMS.Hol.ApplicationService.RoomManager.Abstracts;
using HMS.Hol.Dtos.RoomManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMS.WebAPI.Controllers.Hotel
{
    [Route("api/room")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet("all")]
        public IActionResult GetAllRoomInHotel([FromQuery] int hotelId)
        {
            try
            {
                return Ok(_roomService.GetAllRoom(hotelId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get/{roomId}")]
        public IActionResult GetById(int roomId)
        {
            try
            {
                return Ok(_roomService.GetById(roomId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create")]
        public IActionResult CreateRoomInHotel(CreateRoomDto input, [FromQuery] int hotelId)
        {
            try
            {
                return Ok(_roomService.CreateRoom(input, hotelId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update")]
        public IActionResult UpdateRoom(UpdateRoomDto input, [FromQuery] int hotelId)
        {
            try
            {
                return Ok(_roomService.UpdateRoom(input, hotelId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{roomId}")]
        public IActionResult DeleteRoom(int roomId)
        {
            try
            {
                _roomService.DeleteRoom(roomId);
                return Ok("Xóa phòng thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
