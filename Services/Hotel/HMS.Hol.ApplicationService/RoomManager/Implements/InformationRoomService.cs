using HMS.Hol.ApplicationService.Common;
using HMS.Hol.Domain;
using HMS.Hol.Infrastructures;
using HMS.Shared.ApplicationService.Hotel.Room;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Hol.ApplicationService.RoomManager.Implements
{
    public class InformationRoomService : HotelServiceBase, IInformationRoomService
    {
        public InformationRoomService(ILogger<InformationRoomService> logger, HotelDbContext dbContext)
    : base(logger, dbContext) { }

        public bool CheckRoom(int roomId)
        {
            var checkRoom = _dbContext.Rooms.FirstOrDefault(c => c.RoomID == roomId);
            return checkRoom != null;
        }

         public int FindHotelRoom(int roomId)
        {
            var findRoom = _dbContext.Rooms.FirstOrDefault(r => r.RoomID == roomId);
            return findRoom.HotelId ;
        }
    }
}
