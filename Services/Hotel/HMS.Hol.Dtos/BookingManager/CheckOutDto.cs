using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Hol.Dtos.BookingManager
{
    public class CheckOutDto
    {
        public int BillId { get; set; }
        public DateTime CheckOut { get; set; }
        public string Status { get; set; } = "WaitingPayment";
        public List<int> ChargeIds { get; set; }

    }
}
