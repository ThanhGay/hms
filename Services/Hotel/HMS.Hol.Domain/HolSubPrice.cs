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
    [Table(nameof(HolSubPrice), Schema = DbSchema.Hotel)]
    public class HolSubPrice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
        public int SubPriceID { get; set; }
        public int PricePerHours { get; set; }
        public int PricePerNight { get; set; }
        public DateTime DayStart  { get; set; }
        public DateTime DayEnd { get; set; }
        public int RoomTypeID { get; set; }

    }
}
