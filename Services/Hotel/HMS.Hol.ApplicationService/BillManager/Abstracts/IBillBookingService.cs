using HMS.Hol.ApplicationService.Common;
using HMS.Hol.Dtos.BookingManager;
using HMS.Shared.Constant.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Hol.ApplicationService.BillManager.Abstracts
{
    public interface IBillBookingService
    {
        BookingDto CreateBooking(CreateBookingDto input);
        void DeleteBooking(int id);
        PageResultDto<BookingDto> GetAllBooking(FilterDto input);
        BookingDto GetIdBooking(int id);
        void UpdateBooking(BookingDto input);
    }
}
