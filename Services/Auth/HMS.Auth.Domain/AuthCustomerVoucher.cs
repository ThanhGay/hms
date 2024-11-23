using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Shared.Constant.Database;

namespace HMS.Auth.Domain
{
    [Table(nameof(AuthCustomerVoucher), Schema = DbSchema.Auth)]
    public class AuthCustomerVoucher
    {
        public int CustomerId { get; set; }
        public int VoucherId { get; set; }
        public DateOnly? UsedAt { get; set; }
    }
}
