using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Hol.Dtos.RoomTypeManager
{
    public class CreateRoomTypeDto
    {
        public required string RoomTypeName { get; set; }
        public required string Description { get; set; }
        public int PricePerHour { get; set; }
        public int PricePerNight { get; set; }
    }
}
