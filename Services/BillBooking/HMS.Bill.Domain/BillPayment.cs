using HMS.Shared.Constant.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Bill.Domain
{
    [Table(nameof(BillPayment), Schema = DbSchema.BillBooking)]
    public class BillPayment
    {
        [Key]
        public int BillID { get; set; } 
        public int TotalPrice { get; set; }
    }
}
