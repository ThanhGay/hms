using HMS.Shared.Constant.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Hol.Domain
{
    [Table(nameof(HolRoomType_RoomDetail), Schema = DbSchema.Hotel)]
    public class HolRoomType_RoomDetail
    {
        public int RoomDetailID  { get; set; }  
        public int RoomTypeID { get; set; }
    }
}
