using HMS.Shared.Constant.Database;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS.Hol.Domain
{
    [Table(nameof(HolRoomType), Schema = DbSchema.Hotel)]
    public class HolRoomType
    {
        [Key]
        public int RoomTypeID { get; set; }
        public string RoomTypeName { get; set; }
        public string Description { get; set; }

    }
}
