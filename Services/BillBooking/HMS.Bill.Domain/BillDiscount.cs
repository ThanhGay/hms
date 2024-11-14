using HMS.Shared.Constant.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Bill.Domain
{
    [Table(nameof(BillDiscount), Schema = DbSchema.BillBooking)]
    public class BillDiscount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DiscountID { get; set; }
        public int Percent {  get; set; }
        public int StartDate { get; set; }
        public DateTime ExpDate { get; set; }
    }
}
