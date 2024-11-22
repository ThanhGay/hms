using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Hol.Dtos.Room
{
    public class AddRoom
    {
        public int RoomID { get; set; }
        public string RoomName { get; set; }
        public int HotelId { get; set; }
        public int RoomTypeId { get; set; }
        public int Floor { get; set; }
    }
}
