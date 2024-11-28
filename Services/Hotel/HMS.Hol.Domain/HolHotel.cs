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
    [Table(nameof(HolHotel), Schema = DbSchema.Hotel)]
    public class HolHotel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HotelId { get; set; }
        [MaxLength(250)]
        public string HotelName { get; set; }
        [MaxLength(250)]
        public string HotelAddress { get; set; }
        [MaxLength(100)]
        public string Hotline { get; set; }
        
    }
}
