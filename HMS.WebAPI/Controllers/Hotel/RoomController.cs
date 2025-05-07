using HMS.Hol.ApplicationService.RoomManager.Abstracts;
using HMS.Hol.Dtos.RoomManager;
using HMS.Hol.Dtos.RoomManager.Review;
using HMS.Hol.Dtos.Upload;
using HMS.Shared.Constant.Permission;
using Microsoft.AspNetCore.Authorization;
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

        /// <summary>
        /// danh sách phòng của khách sạn
        /// </summary>
        /// <param name="hotelId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// thông tin phòng (giá cúa ngày hiện tại)
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Thông tin phòng (giá của ngày cần tìm)
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        [Authorize]
        [TypeFilter(
            typeof(AuthorizationFilter),
            Arguments = new object[] { PermissionKeys.GetAllTimeByRoomId }
        )]
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

        /// <summary>
        /// Thông tin phòng (giá trong khoảng thời gian)
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        [Authorize]
        [TypeFilter(
            typeof(AuthorizationFilter),
            Arguments = new object[] { PermissionKeys.GetAtRangeTimeByRoomId }
        )]
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

        /// <summary>
        /// Thông tin đánh giá của phòng
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        [HttpGet("vote/view/{roomId}")]
        public IActionResult GetReview(int roomId)
        {
            try
            {
                return Ok(_roomService.GetAllReviewByRoomId(roomId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Tạo phòng cho khách sạn
        /// </summary>
        /// <param name="input"></param>
        /// <param name="hotelId"></param>
        /// <returns></returns>
        [Authorize]
        [TypeFilter(
            typeof(AuthorizationFilter),
            Arguments = new object[] { PermissionKeys.CreateRoomInHotel }
        )]
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

        /// <summary>
        /// Chỉnh sửa thông tin phòng
        /// </summary>
        /// <param name="input"></param>
        /// <param name="hotelId"></param>
        /// <returns></returns>
        [Authorize]
        [TypeFilter(
            typeof(AuthorizationFilter),
            Arguments = new object[] { PermissionKeys.UpdateRoomByIdInHotel }
        )]
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

        /// <summary>
        /// Xóa phòng (xóa hẳn)
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        [Authorize]
        [TypeFilter(
            typeof(AuthorizationFilter),
            Arguments = new object[] { PermissionKeys.DeleteRoomById }
        )]
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

        /// <summary>
        /// Thêm ảnh cho phòng
        /// </summary>
        /// <param name="image"></param>
        /// <param name="roomId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Lấy tất cả ảnh mô tả của phòng
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        [HttpGet("get-all-image-by-roomid")]
        public IActionResult GetAllImageByRoomId(int roomId)
        {
            try
            {
                return Ok(_roomService.GetAllImageByRoomId(roomId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Đánh giá phòng (bình luận, đánh giá sao)
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("vote/create")]
        [Authorize]
        public IActionResult CreateReviewRoom(CreateReviewRoomDto dto)
        {
            try
            {
                _roomService.ReviewRoom(dto);
                return Ok("Đánh giá thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Cập nhật đánh giá phòng
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("vote/update")]
        [Authorize]
        public IActionResult UpdateReviewRoom(UpdateReviewRoomDto dto)
        {
            try
            {
                _roomService.UpdateReviewRoom(dto);
                return Ok("Cập nhật đánh giá thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Xóa đánh giá (xóa mềm)
        /// </summary>
        /// <param name="reviewId"></param>
        /// <returns></returns>
        [HttpDelete("vote/delete/{reviewId}")]
        [Authorize]
        public IActionResult DeleteReviewRoom(int reviewId)
        {
            try
            {
                _roomService.DeleteReviewRoom(reviewId);
                return Ok("Xóa đánh giá thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
