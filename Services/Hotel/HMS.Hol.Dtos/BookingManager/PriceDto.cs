using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Hol.Dtos.BookingManager
{
    public class PriceDto
    {
        public decimal PricePerNightDefault { get; set; }
        public decimal PricePerHourDefault { get; set; }
        public decimal PricePerHourSub { get; set; }
        public decimal PricePerNightSub { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
    }
}
