namespace HMS.Hol.ApplicationService.Common
{
    public class VnPayRequest
    {
        public long OrderId { get; set; }
        public decimal Amount { get; set; }
        public string OrderDesc { get; set; }
        public string OrderType { get; set; } = "other";
    }

}
