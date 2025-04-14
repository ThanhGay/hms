using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Hol.Dtos.RoomManager.Review
{
    public class ViewRoomReviewDto
    {
        public int RoomId { get; set; }
        public int? UserId { get; set; }
        public int Star { get; set; }
        public string? Commemt { get; set; }
        public DateTime Create { get; set; }
    }
}
