using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Hol.ApplicationService.RoomManager.Abstracts;
using HMS.Hol.Domain;
using HMS.Hol.Dtos.RoomManager;
using HMS.Shared.Constant.Common;

namespace HMS.Hol.ApplicationService.RoomManager.Implements
{
    public class RoomService : IRoomService
    {
        public void CreateRoom(CreateRoomDto input)
        {
            throw new NotImplementedException();
        }

        public void DeleteRoom(int roomId)
        {
            throw new NotImplementedException();
        }

        public PageResultDto<HolRoom> GetAllRoom(int hotelId)
        {
            throw new NotImplementedException();
        }

        public HolRoom GetById(int roomId)
        {
            throw new NotImplementedException();
        }

        public void UpdateRoom()
        {
            throw new NotImplementedException();
        }
    }
}
