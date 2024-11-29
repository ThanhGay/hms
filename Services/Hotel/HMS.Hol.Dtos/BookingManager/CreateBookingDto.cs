using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Hol.Dtos.BookingManager
{
    public class CreateBookingDto
    {
        public DateTime BookingDate { get; set; } = DateTime.Now;
        public DateTime ExpectedCheckIn { get; set; } = DateTime.Now;
        public DateTime ExpectedCheckOut { get; set; }
        public DateTime CheckIn { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Staying";
        public int CustomerID { get; set; }
        public int ReceptionistID { get; set; }
    }
}
