using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Hol.Dtos.RoomManager
{
    public class CreateRoomDto
    {
        public int HotelID { get; set; }
        public string? RoomName { get; set; }
        public int RoomTypeId { get; set; }
        public int Floor { get; set; }
    }
}
