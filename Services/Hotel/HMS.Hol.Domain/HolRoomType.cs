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
    [Table(nameof(HolRoomType), Schema = DbSchema.Hotel)]
    public class HolRoomType
    {
        [Key]
        public int RoomTypeID { get; set; }
        public string RoomTypeName { get; set; }
        public string Description { get; set; }

    }
}
