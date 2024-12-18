using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Hol.Domain;
using HMS.Hol.Dtos.RoomManager;
using HMS.Hol.Dtos.Upload;
using HMS.Shared.Constant.Common;

namespace HMS.Hol.ApplicationService.RoomManager.Abstracts
{
    public interface IRoomService
    {
        public HolRoom CreateRoom(CreateRoomDto input, int HotelId);
        public HolRoom UpdateRoom(UpdateRoomDto input, int HotelId);
        public void DeleteRoom(int roomId);
        public PageResultDto<RoomDetailDto> GetAllRoom(int hotelId);
        public RoomDetailDto GetById(int roomId);
        public RoomDetailDto GetById(int roomId, DateOnly date);
        public RoomFullDetailDto GetById(int roomId, DateOnly start, DateOnly end);
        public Task<ImageDto> AddImgae(UploadImageDto image, int roomId);
    }
}
