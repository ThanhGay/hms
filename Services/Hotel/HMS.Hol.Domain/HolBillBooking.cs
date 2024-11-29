using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Shared.Constant.Database;

namespace HMS.Hol.Domain
{
    [Table(nameof(HolBillBooking), Schema = DbSchema.Hotel)]
    public class HolBillBooking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BillID { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime ExpectedCheckIn { get; set; }
        public DateTime ExpectedCheckOut { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public decimal? Prepayment { get; set; }
        public string Status { get; set; }
        public int? DiscountID { get; set; }
        public int? CustomerID { get; set; }
        public int? ReceptionistID { get; set; }
    }

}