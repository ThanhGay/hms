using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Hol.Dtos.BookingManager
{
    public class CreatePreBookingDto
    {
        public DateTime BookingDate { get; set; } = DateTime.Now;
        public DateTime ExpectedCheckIn { get; set; }
        public DateTime ExpectedCheckOut { get; set; }
        public string Status { get; set; } = "PreBooking";
        public int? DiscountID { get; set; }
        public int CustomerID { get; set; }
        public int? ReceptionistID { get; set; }
    }
}
