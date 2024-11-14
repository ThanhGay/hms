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
    [Table(nameof(HolRoomDetail), Schema = DbSchema.Hotel)]
    public class HolRoomDetail
    {
        [Key]
        public int RoomDetailID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int RoomID { get; set; }
    }
}
