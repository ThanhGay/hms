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
    [Table(nameof(HolDefaultPrice), Schema = DbSchema.Hotel)]
    public class HolDefaultPrice
    {
        [Key]
        public int DefaultPriceID { get; set; }
        public decimal PricePerHour { get; set; }
        public decimal PricePerNight { get; set; }
        public int RoomTypeID { get; set; }
    }
}
