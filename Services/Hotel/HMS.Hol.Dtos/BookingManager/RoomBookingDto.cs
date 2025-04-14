namespace HMS.Hol.Dtos.BookingManager
{
    public class RoomBookingDto
    {
        public int RoomID { get; set; }
        public string RoomName { get; set; }
        public int HotelId { get; set; }
        public int RoomTypeId { get; set; }
        public int Floor { get; set; }
    }
}
