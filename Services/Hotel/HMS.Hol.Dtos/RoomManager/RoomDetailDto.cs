using HMS.Hol.Dtos.RoomManager.Review;
using HMS.Hol.Dtos.Upload;

namespace HMS.Hol.Dtos.RoomManager
{
    /// <summary>
    /// return room with default price
    /// </summary>
    public class RoomDetailDto
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public int Floor { get; set; }
        public required string RoomTypeName { get; set; }
        public required string Description { get; set; }
        public decimal PricePerHour { get; set; }
        public decimal PricePerNight { get; set; }
        public int RoomTypeId { get; set; }
        public int HotelId { get; set; }
        public List<ImageDto>? RoomImages { get; set; }
        public ResultRoomReviewDto? Reviews { get; set; }
    }
}
