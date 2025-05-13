namespace HMS.Hol.Dtos.BookingManager
{
    public class CreatePreBookingDto
    {
        public DateTime BookingDate { get; set; } = DateTime.Now;
        public DateTime ExpectedCheckIn { get; set; }
        public DateTime ExpectedCheckOut { get; set; }
        public string Status { get; set; } = "PreBooking";
        public int? DiscountID { get; set; }
        public int CustomerID { get; set; }
        public List<int> RoomIds { get; set; }
    }
}
