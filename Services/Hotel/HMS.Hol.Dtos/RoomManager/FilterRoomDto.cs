using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Hol.Dtos.RoomManager
{
    public class FilterRoomDto
    {
        public int hotelId { get; set; }
        public bool? isHighLow { get; set; }
        public bool? isLowHigh { get; set; }
        public string? Search { get; set; }
        public bool? isDoubleRoom { get; set; }
        public bool? isSingleRoom { get; set; }

    }
}
