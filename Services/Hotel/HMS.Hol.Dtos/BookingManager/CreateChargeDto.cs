namespace HMS.Hol.Dtos.BookingManager
{
    public class CreateChargeDto
    {
        public decimal Price { get; set; }
        public string Descreption { get; set; }
        public int BookingId { get; set; }
    }
}
