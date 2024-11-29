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

        [Range(1, int.MaxValue, ErrorMessage = "Giá phòng/giờ phải lớn hơn 0")]
        public int PricePerHour { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Giá phòng/giờ phải lớn hơn 0")]
        public int PricePerNight { get; set; }
    }
}
