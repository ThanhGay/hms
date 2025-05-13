using HMS.Shared.Constant.Database;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS.Hol.Domain
{
    [Table(nameof(HolBillBooking_Room), Schema = DbSchema.Hotel)]
    public class HolBillBooking_Room
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int BillID { get; set; }
        public int RoomID { get; set; }
        public string Status { get; set; }
        public decimal PricePerNight { get; set; }
        public decimal PricePerHour { get; set; }
        public string RoomTypeDescription { get; set; }
        public string RoomTypeName { get; set; }
    }
}
