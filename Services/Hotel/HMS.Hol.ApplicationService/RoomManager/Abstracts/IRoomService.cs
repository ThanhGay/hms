using HMS.Hol.Domain;
using HMS.Hol.Dtos.RoomManager;
using HMS.Shared.Constant.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Hol.ApplicationService.RoomManager.Abstracts
{
    public interface IRoomService
    {
        public void CreateRoom(CreateRoomDto input);
        public void UpdateRoom();
        public void DeleteRoom(int roomId);
        public PageResultDto<HolRoom> GetAllRoom(int hotelId);
        public HolRoom GetById (int roomId);
    }
}
