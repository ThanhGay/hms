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
        public int Floor {  get; set; }
    }
}
