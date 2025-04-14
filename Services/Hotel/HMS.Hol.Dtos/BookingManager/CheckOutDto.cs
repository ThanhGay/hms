namespace HMS.Hol.Dtos.BookingManager
{
    public class CheckOutDto
    {
        public int BillId { get; set; }
        public DateTime CheckOut { get; set; }
        public string Status { get; set; } = "WaitingPayment";
        public List<int> ChargeIds { get; set; }

    }
}
