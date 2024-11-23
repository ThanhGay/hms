using HMS.Shared.Constant.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Hol.Domain
{
    [Table(nameof(HolBillBooking_Room), Schema = DbSchema.Hotel)]
    public class HolBillBooking_Room
    {
        public int BillID { get; set; }
        public int RoomID { get; set; }
    }
}
