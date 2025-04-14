using HMS.Hol.Dtos.BookingManager;
using HMS.Shared.Constant.Common;

namespace HMS.Hol.ApplicationService.BillManager.Abstracts
{
    public interface IBillBookingService
    {
        BookingDto CreateBooking(CreateBookingDto input);
        BookingDto CreatePreBooking(CreatePreBookingDto input);
        ChargeDto CreateCharge(CreateChargeDto input);
        void CreateBooking_Room(int roomIds, int bookingId);
        void CreateBooking_Charge(int chargeId, int bookingId);
        void CheckIn(CheckInDto checkIn);
        void DeleteBooking(int id);
        PageResultDto<BookingDto> GetAllBooking(FilterDto input);
        PageResultDto<BookingDto> GetBookingByCustomerId(FilterDto input, int? customerId);
        BookingDto GetIdBooking(int id);
        void UpdateBooking(BookingDto input);
        void CheckOut(CheckOutDto checkOut);
        decimal GetExpectedTotalByBillId(int billId);
        decimal GetTotalAmountByBillId(int billId);
        void UpdateCharge(ChargeDto input);
        void DeleteCharge(int id);
        ChargeDto GetChargeById(int id);
        List<ChargeDto> GetChargeByIdBooking(int bookingId);
        void CancelBooking(int bookingId);
    }
}
