using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Auth.Dtos.Voucher
{
    public class CreateVoucherDto
    {
        public float Percent { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly ExpDate { get; set; }
    }
}
