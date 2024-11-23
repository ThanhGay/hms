using HMS.Hol.ApplicationService.RoomManager.Abstracts;
using HMS.Hol.Dtos.RoomTypeManager;
using HMS.Shared.Constant.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMS.WebAPI.Controllers.Type
{
    [Route("api/type")]
    [ApiController]
    public class RoomTypeController : ControllerBase
    {
        private readonly IRoomTypeService _roomTypeService;

        public RoomTypeController(IRoomTypeService roomTypeService)
        {
            _roomTypeService = roomTypeService;
        }

        /// <summary>
        /// Lấy danh sách thể loại phòng
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("all")]
        public IActionResult All(FilterDto input)
        {
            try
            {
                return Ok(_roomTypeService.GetAll(input));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Lấy thông tin mặc định của phòng
        /// </summary>
        /// <param name="roomTypeId"></param>
        /// <returns></returns>
        [HttpGet("get/{roomTypeId}")]
        public IActionResult Get(int roomTypeId)
        {
            try
            {
                return Ok(_roomTypeService.GetById(roomTypeId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Tạo thể loại phòng mới
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public IActionResult CreateRoomType(CreateRoomTypeDto input)
        {
            try
            {
                return Ok(_roomTypeService.CreateRoomType(input));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Cập nhật các thông tin cần thiết cho thể loại phòng
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public IActionResult UpdateInformation(UpdateRoomTypeDto input)
        {
            try
            {
                return Ok(_roomTypeService.UpdateRoomType(input));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Xóa thể loại phòng
        /// </summary>
        /// <param name="roomTypeId"></param>
        /// <returns></returns>
        [HttpDelete("delete/{roomTypeId}")]
        public IActionResult Delete(int roomTypeId)
        {
            try
            {
                _roomTypeService.DeleteRoomType(roomTypeId);
                return Ok("Xóa thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
