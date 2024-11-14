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
    [Table(nameof(BillBillBooking), Schema = DbSchema.BillBooking)]
    public class BillBillBooking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BillID { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime? CheckIn {  get; set; }
        public DateTime? CheckOut { get; set; }
        public int PrePayment { get; set; }
        public string Status { get; set; }
        public int DiscountID { get; set; }
        public int CustomerID { get; set; }
        public int ReceptionistID { get; set; }
        public int Charge { get; set; }
      
        
    }
}
