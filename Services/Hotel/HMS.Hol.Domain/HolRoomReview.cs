using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HMS.Shared.Constant.Database;
using Microsoft.EntityFrameworkCore;

namespace HMS.Hol.Domain
{
    [Table(nameof(HolRoomReview), Schema = DbSchema.Hotel)]
    [Index(
        nameof(Id),
        nameof(Star),
        nameof(IsDeleted),
        IsUnique = false,
        Name = $"IX_{nameof(HolRoomReview)}"
    )]
    public class HolRoomReview
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? UserId { get; set; }
        public int RoomId { get; set; }

        [AllowedValues(
            [1, 2, 3, 4, 5],
            ErrorMessage = "Số sao đánh giá không được bé hơn 1 và lớn hơn 5"
        )]
        public int Star { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int? DeletedBy { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
