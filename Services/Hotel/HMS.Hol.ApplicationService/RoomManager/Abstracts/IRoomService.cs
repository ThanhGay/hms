using HMS.Hol.Domain;
using HMS.Hol.Dtos.RoomManager;
using HMS.Hol.Dtos.RoomManager.Review;
using HMS.Hol.Dtos.Upload;
using HMS.Shared.Constant.Common;

namespace HMS.Hol.ApplicationService.RoomManager.Abstracts
{
    public interface IRoomService
    {
        public HolRoom CreateRoom(CreateRoomDto input, int HotelId);
        public HolRoom UpdateRoom(UpdateRoomDto input, int HotelId);
        public void DeleteRoom(int roomId);
        public PageResultDto<RoomDetailDto> GetAllRoom(FilterRoomDto dto);
        public RoomDetailDto GetById(int roomId);
        public RoomDetailDto GetById(int roomId, DateOnly date);
        public RoomFullDetailDto GetById(int roomId, DateOnly start, DateOnly end);
        public Task<ImageDto> AddImgae(UploadImageDto image, int roomId);
        public List<ImageDto> GetAllImageByRoomId(int roomId);

        #region review
        public void ReviewRoom(CreateReviewRoomDto input);
        public void UpdateReviewRoom(UpdateReviewRoomDto input);
        public void DeleteReviewRoom(int reviewId);        
        public ResultRoomReviewDto GetAllReviewByRoomId(int roomId);

        #endregion
    }
}
