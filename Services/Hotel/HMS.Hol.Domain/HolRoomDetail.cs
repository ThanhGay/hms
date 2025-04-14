using HMS.Shared.Constant.Database;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS.Hol.Domain
{
    [Table(nameof(HolRoomDetail), Schema = DbSchema.Hotel)]
    public class HolRoomDetail
    {
        [Key]
        public int RoomDetailID { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
    }
}
