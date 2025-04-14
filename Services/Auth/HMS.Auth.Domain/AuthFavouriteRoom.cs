using HMS.Shared.Constant.Database;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS.Auth.Domain
{
    [Table(nameof(AuthFavouriteRoom), Schema = DbSchema.Auth)]
    public class AuthFavouriteRoom
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FavouriteId { get; set; }
        public int CustomerId { get; set; }
        public int RoomId { get; set; }

    }
}
