namespace HMS.Hol.Dtos.HotelManager
{
    public class UpdateHotelDto
    {
        public int HotelId { get; set; }
        public required string HotelName { get; set; }
        public required string HotelAddress { get; set; }
        public required string Hotline { get; set; }

    }
}
