﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Hol.Dtos.RoomManager
{
    // return room with default price
    public class RoomDetailDto
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public int Floor { get; set; }
        public required string RoomTypeName { get; set; }
        public required string Description { get; set; }
        public int PricePerHour { get; set; }
        public int PricePerNight { get; set; }
        public int RoomTypeId { get; set; }
        public int HotelId { get; set; }
    }
}