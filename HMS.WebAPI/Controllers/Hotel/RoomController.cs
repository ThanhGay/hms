using HMS.Hol.ApplicationService.RoomManager.Abstracts;
using HMS.Hol.Dtos.RoomManager;
using HMS.Hol.Dtos.Upload;
using HMS.Shared.Constant.Permission;
using Microsoft.AspNetCore.Authorization;
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
        //[Authorize]
        //[TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] {PermissionKeys.GetAllRoomInHotel })]
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

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] { PermissionKeys.GetRoomById })]
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

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] { PermissionKeys.GetAllTimeByRoomId })]
        [HttpGet("get-at-time/{roomId}")]
        public IActionResult GetById(int roomId, [FromQuery] DateOnly date)
        {
            try
            {
                return Ok(_roomService.GetById(roomId, date));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] { PermissionKeys.GetAtRangeTimeByRoomId })]
        [HttpGet("get-at-range-time/{roomId}")]
        public IActionResult GetByById(
            int roomId,
            [FromQuery] DateOnly start,
            [FromQuery] DateOnly end
        )
        {
            try
            {
                return Ok(_roomService.GetById(roomId, start, end));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] { PermissionKeys.CreateRoomInHotel })]
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

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] { PermissionKeys.UpdateRoomByIdInHotel })]
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

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] { PermissionKeys.DeleteRoomById })]
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

        [HttpPost("add-image/{roomId}")]
        public async Task<IActionResult> UploadImage(UploadImageDto image, int roomId)
        {
            try
            {
                var result = await _roomService.AddImgae(image, roomId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("get-all-image-by-roomid")]
        public IActionResult GetAllImageByRoomId(int roomId)
        {
            try
            {
                return Ok(_roomService.GetAllImageByRoomId(roomId));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
