using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Auth.Dtos.Customer
{
    public class FavouriteRoomDto
    {
        public int FavouriteId { get; set; }
        public int HotelId { get; set; }
        public int RoomId { get; set; }
    }
}
