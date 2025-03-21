﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Hol.Dtos.HotelManager
{
    public class HotelInformationDto
    {
        public int HotelId { get; set; }
        public required string HotelName { get; set; }
        public required string HotelAddress { get; set; }
        public required string Hotline { get; set; }
        public int TotalRoom { get; set; }
    }
}
