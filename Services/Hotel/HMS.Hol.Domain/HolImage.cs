using HMS.Shared.Constant.Database;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
