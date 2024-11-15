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
    [Table(nameof(HolDiscount), Schema = DbSchema.Hotel)]
    public class HolDiscount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DiscountID { get; set; }
        public float Percent {  get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpDate { get; set; }
    }
}
