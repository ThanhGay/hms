using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Hol.ApplicationService.Common;
using HMS.Hol.ApplicationService.RoomManager.Abstracts;
using HMS.Hol.Domain;
using HMS.Hol.Dtos.RoomManager;
using HMS.Hol.Infrastructures;
using HMS.Shared.Constant.Common;
using Microsoft.Extensions.Logging;

namespace HMS.Hol.ApplicationService.RoomManager.Implements
{
    public class RoomService : HotelServiceBase, IRoomService
    {
        public RoomService(ILogger<RoomService> logger, HotelDbContext dbContext)
            : base(logger, dbContext) { }

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
