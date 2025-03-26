using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Hol.Dtos.BookingManager
{
    public class CheckInDto
    {
        public int BillID { get; set; }
        public DateTime CheckIn { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Staying";
    }
}
