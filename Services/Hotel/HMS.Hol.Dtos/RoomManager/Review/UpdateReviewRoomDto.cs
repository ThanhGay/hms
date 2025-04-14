using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Hol.Dtos.RoomManager.Review
{
    public class UpdateReviewRoomDto
    {
        public int ReviewId { get; set; }

        [Range(1, 5, ErrorMessage = "Số sao phải 1 - 5")]
        public int Star { get; set; }
        public string? Commemt { get; set; }
    }
}
