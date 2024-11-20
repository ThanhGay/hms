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
    [Table(nameof(HolCharge), Schema = DbSchema.Hotel)]
    public class HolCharge
    {
        [Key]
        public int ChargeId { get; set; }
        public int Price { get; set; }
        public string Descreption { get; set; }
    }
}
