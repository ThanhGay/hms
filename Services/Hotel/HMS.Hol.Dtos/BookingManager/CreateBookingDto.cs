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
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public decimal? Prepayment { get; set; }
        public string Status { get; set; }
        public int? DiscountID { get; set; }
        public int? CustomerID { get; set; }
        public int? ReceptionistID { get; set; }
    }
}
