using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Hol.Dtos.BookingManager
{
    public class CreateChargeDto
    {
        public decimal Price { get; set; }
        public string? Descreption { get; set; }
    }
}
