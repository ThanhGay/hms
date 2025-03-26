using HMS.Hol.ApplicationService.RoomManager.Abstracts;
using HMS.Hol.Dtos.RoomTypeManager;
using HMS.Shared.Constant.Common;
using HMS.Shared.Constant.Permission;
using Microsoft.AspNetCore.Authorization;
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
        /// 
        //[Authorize]
        //[TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] { PermissionKeys.GetAllRoomType })]
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
        /// 
        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] { PermissionKeys.GetRoomTypeById })]
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
        
        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] { PermissionKeys.CreateRoomType })]
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
        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] { PermissionKeys.UpdateRoomType })]
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
        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] { PermissionKeys.DeleteRoomTypeById })]
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

        /// <summary>
        /// Định giá cho các loại phòng trong khoảng thời gian
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] { PermissionKeys.SetPriceInHoliday })]
        [HttpPost("set-price-holiday")]
        public IActionResult SetPriceInHoliday(SetPriceInHolidayDto input)
        {
            try
            {
                _roomTypeService.SetPriceInHoliday(input);
                return Ok("Đặt giá cho ngày lễ thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Cập nhật giá của các ngày quy định là ngày lễ
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] { PermissionKeys.UpdatePriceHoliday })]
        [HttpPut("update-price-holiday")]
        public IActionResult UpdatePriceHoliday(UpdatePriceInHoliday input)
        {
            try
            {
                _roomTypeService.UpdatePriceInHoliday(input);
                return Ok("Cập nhật giá cho ngày lễ thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Xóa giá ngày lễ
        /// </summary>
        /// <param name="subPriceId"></param>
        /// <returns></returns>

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] { PermissionKeys.DeletePriceHoliday })]
        [HttpDelete("delete-price-holiday/{subPriceId}")]
        public IActionResult DeletePriceHoliday(int subPriceId)
        {
            try
            {
                _roomTypeService.DeletePriceInHoliday(subPriceId);
                return Ok("Xóa giá ngày lễ thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
