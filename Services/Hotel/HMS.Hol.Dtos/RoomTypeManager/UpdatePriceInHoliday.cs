using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Hol.Dtos.RoomTypeManager
{
    public class UpdatePriceInHoliday
    {
        public int SubPriceId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [Range(1, float.MaxValue, ErrorMessage = "Giá phòng/giờ phải lớn hơn 0")]
        public decimal PricePerHour { get; set; }

        [Range(1, float.MaxValue, ErrorMessage = "Giá phòng/giờ phải lớn hơn 0")]
        public decimal PricePerNight { get; set; }
    }
}
