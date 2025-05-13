using HMS.Shared.Constant.Database;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS.Hol.Domain
{
    [Table(nameof(HolBillBooking_Charge), Schema = DbSchema.Hotel)]
    public class HolBillBooking_Charge
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Booking_ChargeID { get; set; }
        public int BillID { get; set; }
        public int ChargeID { get; set; }
    }
}
