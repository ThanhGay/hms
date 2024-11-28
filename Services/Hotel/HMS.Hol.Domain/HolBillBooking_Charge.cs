using HMS.Shared.Constant.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Hol.Domain
{
    [Table(nameof(HolBillBooking_Charge), Schema = DbSchema.Hotel)]
    public class HolBillBooking_Charge
    {
        public int BillID { get; set; }
        public int ChargeID { get; set; }
    }
}
