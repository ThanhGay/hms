using HMS.Shared.Constant.Database;
using System.ComponentModel.DataAnnotations.Schema;

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
