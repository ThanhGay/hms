using HMS.Shared.Constant.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Hol.Domain
{
    [Table(nameof(HolImage), Schema = DbSchema.Hotel)]
    public class HolImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
        public int ImageID { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        public string URL { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
        public int RoomId { get; set; }

    }
}
