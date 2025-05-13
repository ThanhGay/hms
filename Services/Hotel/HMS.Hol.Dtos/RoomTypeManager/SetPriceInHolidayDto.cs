using System.ComponentModel.DataAnnotations;

namespace HMS.Hol.Dtos.RoomTypeManager
{
    public class SetPriceInHolidayDto
    {
        public int RoomTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [Range(1, float.MaxValue, ErrorMessage = "Giá phòng/giờ phải lớn hơn 0")]
        public decimal PricePerHour { get; set; }

        [Range(1, float.MaxValue, ErrorMessage = "Giá phòng/giờ phải lớn hơn 0")]
        public decimal PricePerNight { get; set; }
    }
}
