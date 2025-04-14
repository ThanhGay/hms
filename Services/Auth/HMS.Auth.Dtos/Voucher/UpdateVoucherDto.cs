namespace HMS.Auth.Dtos.Voucher
{
    public class UpdateVoucherDto
    {
        public int VoucherId { get; set; }
        public float Percent { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly ExpDate { get; set; }
    }
}
