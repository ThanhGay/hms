using HMS.Shared.Constant.Database;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS.Hol.Domain
{
    [Table(nameof(HolCharge), Schema = DbSchema.Hotel)]
    public class HolCharge
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string? Descreption { get; set; }
    }
}
