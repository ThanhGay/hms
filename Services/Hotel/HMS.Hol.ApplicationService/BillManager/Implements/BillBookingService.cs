using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Hol.ApplicationService.BillManager.Abstracts;
using HMS.Hol.ApplicationService.Common;
using HMS.Hol.Domain;
using HMS.Hol.Dtos.BookingManager;
using HMS.Hol.Infrastructures;
using HMS.Shared.Constant.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HMS.Hol.ApplicationService.BillManager.Implements
{
    public class BillBookingService : HotelServiceBase, IBillBookingService
    {

        public BillBookingService(ILogger<BillBookingService> logger,HotelDbContext dbContext)
            : base(logger, dbContext)
        {
        }

        public BookingDto CreateBooking(CreateBookingDto input)
        {
            var exists = _dbContext.BillBookings
                .FirstOrDefault(s => s.BookingDate == input.BookingDate && s.CustomerID == input.CustomerID);

            if (exists != null)
            {
                throw new HotelExceptions("Booking này đã tồn tại!");
            }

            var newBooking = new HolBillBooking
            {
                BookingDate = input.BookingDate,
                CheckIn = input.CheckIn,
                CheckOut = input.CheckOut,
                Prepayment = input.Prepayment,
                DiscountID = input.DiscountID,
                Status = input.Status,
                CustomerID = input.CustomerID,
                ReceptionistID = input.ReceptionistID,
            };

            _dbContext.BillBookings.Add(newBooking);
            _dbContext.SaveChanges();

            return new BookingDto
            {
                BillID = newBooking.BillID,
                BookingDate = newBooking.BookingDate,
                CheckIn = newBooking.CheckIn,
                CheckOut = newBooking.CheckOut,
                Prepayment = newBooking.Prepayment,
                DiscountID = newBooking.DiscountID,
                Status = newBooking.Status,
                CustomerID = newBooking.CustomerID,
                ReceptionistID = newBooking.ReceptionistID,
            };
        }

        public HolBillBooking FindBooking(int id)
        {
            var booking = _dbContext.BillBookings.FirstOrDefault(p => p.BillID == id);
            if (booking == null)
            {
                throw new HotelExceptions("Không tìm thấy với ID đã cung cấp.");
            }
            return booking;
        }

        public void DeleteBooking(int id)
        {
            var findBooking = FindBooking(id);
            _dbContext.BillBookings.Remove(findBooking);
            _dbContext.SaveChanges();
        }

        public void UpdateBooking(BookingDto input)
        {
            var findBooking = FindBooking(input.BillID);
                findBooking.BookingDate = input.BookingDate;
                findBooking.CheckIn = input.CheckIn;
                findBooking.CheckOut = input.CheckOut;
                findBooking.Prepayment = input.Prepayment;
                findBooking.DiscountID = input.DiscountID;
                findBooking.Status = input.Status;
                findBooking.CustomerID = input.CustomerID;
                findBooking.ReceptionistID = input.ReceptionistID;
            _dbContext.SaveChanges();
        }

        public BookingDto GetIdBooking(int id)
        {
            var findBooking = FindBooking(id);
            return new BookingDto
            {
                BillID = findBooking.BillID,
                BookingDate = findBooking.BookingDate,
                CheckIn = findBooking.CheckIn,
                CheckOut = findBooking.CheckOut,
                Prepayment = findBooking.Prepayment,
                DiscountID = findBooking.DiscountID,
                Status = findBooking.Status,
                CustomerID = findBooking.CustomerID,
                ReceptionistID = findBooking.ReceptionistID,
            };
        }

        public PageResultDto<BookingDto> GetAllBooking(FilterDto input)
        {
            var result = new PageResultDto<BookingDto>();

            var query = _dbContext.BillBookings.Where(e =>
                string.IsNullOrEmpty(input.Keyword)
                || e.BookingDate.ToString().ToLower().Contains(input.Keyword.ToLower())
            );

            result.TotalItem = query.Count();

            query = query
                .OrderByDescending(s => s.BookingDate)
                .ThenByDescending(s => s.BillID)
                .Skip(input.SkipCount())
                .Take(input.PageSize);

            result.Items = query
                .Select(s => new BookingDto
                {
                    BillID = s.BillID,
                    BookingDate = s.BookingDate,
                    CheckIn = s.CheckIn,
                    CheckOut = s.CheckOut,
                    Prepayment = s.Prepayment,
                    DiscountID = s.DiscountID,
                    Status = s.Status,
                    CustomerID = s.CustomerID,
                    ReceptionistID = s.ReceptionistID,
                })
                .ToList();

            return result;
        }


    }


}
