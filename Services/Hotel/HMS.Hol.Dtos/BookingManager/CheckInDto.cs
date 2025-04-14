namespace HMS.Hol.Dtos.BookingManager
{
    public class CheckInDto
    {
        public int BillID { get; set; }
        public DateTime CheckIn { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Staying";
    }
}
