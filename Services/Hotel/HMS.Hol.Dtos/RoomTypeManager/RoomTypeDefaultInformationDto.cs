namespace HMS.Hol.Dtos.RoomTypeManager
{
    public class RoomTypeDefaultInformationDto
    {
        public int RoomTypeId { get; set; }
        public required string RoomTypeName { get; set; }
        public required string Description { get; set; }
        public decimal PricePerHour { get; set; }
        public decimal PricePerNight { get; set; }
    }
}
