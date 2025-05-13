namespace HMS.Hol.Dtos.BookingManager
{
    public class RoomBookingDto
    {
        public int RoomID { get; set; }
        public string RoomName { get; set; }
        public int HotelId { get; set; }
        public int RoomTypeId { get; set; }
        public int Floor { get; set; }
        public decimal PricePerNight { get; set; }
        public decimal PricePerHour { get; set; }
        public string RoomTypeDescription { get; set; }
        public string RoomTypeName { get; set; }
    }
}
