using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Hol.Dtos.RoomManager
{
    public class UpdateRoomDto
    {
        public int RoomID { get; set; }
        public string RoomName { get; set; }
        public int RoomTypeId { get; set; }
        [Range(1, 100, ErrorMessage = "Số tầng phải từ 1 - 100")]
        public int Floor { get; set; }
    }
}
