using HMS.Shared.Constant.Database;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS.Hol.Domain
{
    [Table(nameof(HolRoom), Schema = DbSchema.Hotel)]
    public class HolRoom
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoomID { get; set; }
        [MaxLength(250)]
        public string RoomName { get; set; }
        public int HotelId { get; set; }
        public int RoomTypeId { get; set; }
        public int Floor { get; set; }
    }
}
