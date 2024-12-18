using HMS.Hol.Dtos.Upload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Hol.Dtos.RoomManager
{
    // return room with default price
    public class RoomFullDetailDto
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public int Floor { get; set; }
        public required string RoomTypeName { get; set; }
        public required string Description { get; set; }
        public decimal PricePerHour { get; set; }
        public decimal PricePerNight { get; set; }
        public decimal PricePerHolidayNight { get; set; }
        public decimal PricePerHolidayHour { get; set; }
        public int RoomTypeId { get; set; }
        public List<ImageDto>? RoomImages { get; set; }
        public int HotelId { get; set; }
    }
}
