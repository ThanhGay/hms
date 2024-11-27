using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Hol.Dtos.BookingManager
{
    public class HotelExceptions : Exception
    {
        public HotelExceptions(String message) : base(message)
        {
        }

    }
}
