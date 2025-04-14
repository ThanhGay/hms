using HMS.Shared.Constant.Database;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS.Hol.Domain
{
    [Table(nameof(HolRoomType_RoomDetail), Schema = DbSchema.Hotel)]
    public class HolRoomType_RoomDetail
    {
        public int RoomDetailID { get; set; }
        public int RoomTypeID { get; set; }
    }
}
