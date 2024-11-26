using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Auth.Dtos.Customer
{
    public class VoucherCustomerDto
    {
        public int VoucherId { get; set; }
        public float Percent { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly ExpDate { get; set; }
        
        public bool checkUse {  get; set; }
    }
}
