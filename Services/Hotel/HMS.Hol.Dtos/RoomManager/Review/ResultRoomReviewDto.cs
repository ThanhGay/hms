using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Hol.Dtos.RoomManager.Review
{
    public class ResultRoomReviewDto
    {
        public int RoomId { get; set; }
        public int? Total { get; set; }
        public double? Value { get; set; }
        public List<DetailStar>? DetailStars { get; set; }
        public List<ViewRoomReviewDto>? DetailReviews { get; set; }
    }

    public class DetailStar
    {
        public int Star { get; set; }
        public int Count { get; set; }
    }
}
