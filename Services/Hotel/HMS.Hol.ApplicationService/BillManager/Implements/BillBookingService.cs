using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using HMS.Hol.ApplicationService.BillManager.Abstracts;
using HMS.Hol.ApplicationService.Common;
using HMS.Hol.ApplicationService.RoomManager.Implements;
using HMS.Hol.Domain;
using HMS.Hol.Dtos.BookingManager;
using HMS.Hol.Dtos.RoomManager;
using HMS.Hol.Infrastructures;
using HMS.Shared.Constant.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HMS.Hol.ApplicationService.BillManager.Implements
{
    public class BillBookingService : HotelServiceBase, IBillBookingService
    {

        public BillBookingService(ILogger<BillBookingService> logger, HotelDbContext dbContext)
            : base(logger, dbContext)
        {
        }

        public BookingDto CreateBooking(CreateBookingDto input)
        {
            var exists = _dbContext.BillBookings
                .FirstOrDefault(s => s.BookingDate == input.BookingDate);

            if (exists != null)
            {
                throw new HotelExceptions("Booking này đã tồn tại!");
            }

            // Kiểm tra ngày check-out hợp lệ
            if (input.ExpectedCheckOut <= input.ExpectedCheckIn)
            {
                throw new HotelExceptions("Ngày Check Out không hợp lệ!");
            }

            var newBooking = new HolBillBooking
            {
                BookingDate = input.BookingDate,
                CheckIn = input.CheckIn,
                ExpectedCheckIn = input.ExpectedCheckIn,
                ExpectedCheckOut = input.ExpectedCheckOut,
                Status = input.Status,
                CustomerID = input.CustomerID,
                ReceptionistID = input.ReceptionistID,
            };

            _dbContext.BillBookings.Add(newBooking);
            _dbContext.SaveChanges();

            if (input.RoomIds != null && input.RoomIds.Any())
            {
                foreach (var roomId in input.RoomIds)
                {
                    var roomExists = _dbContext.Rooms.Any(r => r.RoomID == roomId);
                    if (!roomExists)
                    {
                        throw new HotelExceptions($"Room với ID {roomId} không tồn tại.");
                    }

                    var bookingRoom = new HolBillBooking_Room
                    {
                        BillID = newBooking.BillID,
                        RoomID = roomId,
                        status = newBooking.Status
                    };
                    _dbContext.BillBooking_Rooms.Add(bookingRoom);
                }
                _dbContext.SaveChanges();
            }

            return new BookingDto
            {
                BillID = newBooking.BillID,
                ExpectedCheckIn = newBooking.ExpectedCheckIn,
                ExpectedCheckOut = newBooking.ExpectedCheckOut,
                DiscountID = newBooking.DiscountID,
                CheckIn = newBooking.CheckIn,
                CheckOut = newBooking.CheckOut,
                Prepayment = newBooking.Prepayment,
                BookingDate = newBooking.BookingDate,
                Status = newBooking.Status,
                CustomerID = newBooking.CustomerID,
                ReceptionistID = newBooking.ReceptionistID,
            };
        }



        public BookingDto CreatePreBooking(CreatePreBookingDto input)
        {
         
            var exists = _dbContext.BillBookings
            .FirstOrDefault(s => s.BookingDate == input.BookingDate );

            if (exists != null)
            {
                throw new HotelExceptions("Booking này đã tồn tại!");
            }
            if (input.ExpectedCheckOut <= input.ExpectedCheckIn)
            {
                throw new HotelExceptions("Ngày Check Out không hợp lệ!");
            }

            var newBooking = new HolBillBooking
            {
                BookingDate = input.BookingDate,
                ExpectedCheckIn = input.ExpectedCheckIn,
                ExpectedCheckOut = input.ExpectedCheckOut,
                DiscountID = input.DiscountID,
                Status = input.Status,
                CustomerID = input.CustomerID,
                ReceptionistID = input.ReceptionistID,
            };
                _dbContext.BillBookings.Add(newBooking);
                _dbContext.SaveChanges();

            var booking = new BookingDto
            {
                BillID = newBooking.BillID,
                ExpectedCheckIn = newBooking.ExpectedCheckIn,
                ExpectedCheckOut = newBooking.ExpectedCheckOut,
                DiscountID = newBooking.DiscountID,
                CheckIn = newBooking.CheckIn,
                CheckOut = newBooking.CheckOut,
                Prepayment = newBooking.Prepayment,
                BookingDate = newBooking.BookingDate,
                Status = newBooking.Status,
                CustomerID = newBooking.CustomerID,
                ReceptionistID = newBooking.ReceptionistID,
            };

            return booking;
        }

        public void CreateBooking_Room(int roomId, int bookingId)
        {
            var bookingExists = _dbContext.BillBookings
        .FirstOrDefault(b => b.BillID == bookingId);
            if (bookingExists == null)
            {
                throw new HotelExceptions($"Booking với ID {bookingId} không tồn tại.");
            }

            var roomExists = _dbContext.Rooms.Any(b => b.RoomID == roomId);
            if (!roomExists)
            {
                throw new HotelExceptions($"Room với ID {roomId} không tồn tại.");
            }

            var booking_room = new HolBillBooking_Room
            {
                BillID = bookingId,
                RoomID = roomId,
                status = bookingExists.Status
            };
            _dbContext.BillBooking_Rooms.Add(booking_room);
            _dbContext.SaveChanges();

        }

        public ChargeDto CreateCharge(CreateChargeDto input)
        {

            var exists = _dbContext.Charges
                .FirstOrDefault(s => s.Descreption == input.Descreption);

            if (exists != null)
            {
                throw new HotelExceptions("Charge này đã tồn tại!");
            }

            var charge = new HolCharge
            {
                Descreption = input.Descreption,
                Price = input.Price,
            };

            _dbContext.Charges.Add(charge);
            _dbContext.SaveChanges();

            return new ChargeDto
            {
                ChargeId = charge.Id,
                Descreption = charge.Descreption,
                Price = charge.Price,
            };
        }

        public void CreateBooking_Charge(int chargeId, int bookingId)
        {
            var bookingExists = _dbContext.BillBookings.Any(b => b.BillID == bookingId);
            if (!bookingExists)
            {
                throw new HotelExceptions($"Booking với ID {bookingId} không tồn tại.");
            }

            var chargeExists = _dbContext.Charges.Any(b => b.Id == chargeId);
            if (!chargeExists)
            {
                throw new HotelExceptions($"Charge với ID {chargeId} không tồn tại.");
            }

            var booking_charge = new HolBillBooking_Charge
            {
                BillID = bookingId,
                ChargeID = chargeId,
            };
                _dbContext.BillBooking_Charges.Add(booking_charge);
                _dbContext.SaveChanges();

        }

        public void CheckIn(CheckInDto checkIn)
        {
            var booking = _dbContext.BillBookings
                .FirstOrDefault(b => b.BillID == checkIn.BillID);

            if (booking == null)
            {
                throw new HotelExceptions("Booking không tồn tại!");
            }

            booking.CheckIn = checkIn.CheckIn;
            booking.Status = checkIn.Status;

            _dbContext.SaveChanges();
        }

        public void CheckOut(CheckOutDto checkOut) 
        {
            var booking = _dbContext.BillBookings
               .FirstOrDefault(b => b.BillID == checkOut.BillId);

            if (booking == null)
            {
                throw new HotelExceptions("Booking không tồn tại!");
            }

            booking.CheckOut = checkOut.CheckOut;
            booking.Status = checkOut.Status;

            _dbContext.SaveChanges();
        }



        public PriceDto GetPriceRoom(int id)
        {
            var defaultP = (from room in _dbContext.Rooms
                           join roomType in _dbContext.RoomTypes
                               on room.RoomTypeId equals roomType.RoomTypeID
                           join defaultPrice in _dbContext.DefaultPrices on roomType.RoomTypeID equals defaultPrice.RoomTypeID
                           where room.RoomID == id
                           select new
                           {
                               PriceNight = defaultPrice.PricePerNight,
                               PriceHour = defaultPrice.PricePerHour
                           }).FirstOrDefault();
            var sub = (from room in _dbContext.Rooms
                      join roomType in _dbContext.RoomTypes
                             on room.RoomTypeId equals roomType.RoomTypeID
                      join subPrice in _dbContext.SubPrices on roomType.RoomTypeID equals subPrice.RoomTypeID
                      where room.RoomID == id
                      select new
                      {
                          PriceNight = subPrice.PricePerNight,
                          PriceHour = subPrice.PricePerHours,
                          DateStart = subPrice.DayStart,
                          DateEnd = subPrice.DayEnd,
                      }).FirstOrDefault();
            return new PriceDto
            {
                PricePerHourDefault = defaultP?.PriceHour ?? 0, 
                PricePerNightDefault = defaultP?.PriceNight ?? 0,
                PricePerHourSub = sub?.PriceHour ?? 0,
                PricePerNightSub = sub?.PriceNight ?? 0,
                DateStart = sub?.DateStart ?? DateTime.Now, 
                DateEnd = sub?.DateEnd ?? DateTime.Now
            };
        }

        public decimal GetTotalChargeByBillId(int billId)
        {
            var totalCharge = (from bookingCharge in _dbContext.BillBooking_Charges
                               join charge in _dbContext.Charges
                               on bookingCharge.ChargeID equals charge.Id
                               where bookingCharge.BillID == billId
                               select charge.Price).Sum();

            return totalCharge;
        }

        public decimal GetTotalAmountByRoom(DateTime checkIn, DateTime checkOut, int roomId)
        {
            
            var priceRoom = GetPriceRoom(roomId);
            if (checkIn <= priceRoom.DateStart && checkOut <= priceRoom.DateEnd)
            {
                TimeSpan overlap = checkOut - checkIn;
                int days = overlap.Days;
                int hours = overlap.Hours;
                decimal totalAmount = days * priceRoom.PricePerNightSub + hours * priceRoom.PricePerNightSub;
                return totalAmount;
            }
            else if (checkOut < priceRoom.DateStart || checkIn > priceRoom.DateEnd)
            {
                TimeSpan overlap = checkOut - checkIn;
                int days = overlap.Days;
                int hours = overlap.Hours;
                decimal totalAmount = days * priceRoom.PricePerNightDefault + hours * priceRoom.PricePerNightDefault;
                return totalAmount;
            }
            else if (checkIn < priceRoom.DateStart && checkOut > priceRoom.DateStart && checkOut < priceRoom.DateEnd)
            {
                TimeSpan overlap = priceRoom.DateStart - checkIn;
                TimeSpan overlapSub = checkOut - priceRoom.DateStart;
                int days = overlap.Days;
                int hours = overlap.Hours;
                int daysSub = overlapSub.Days;
                int hoursSub = overlapSub.Hours;
                decimal totalAmount = days * priceRoom.PricePerNightDefault + hours * priceRoom.PricePerNightDefault
                    + daysSub * priceRoom.PricePerNightSub + hoursSub * priceRoom.PricePerHourSub;
                return totalAmount;
            }

            else if (checkIn > priceRoom.DateStart && checkIn < priceRoom.DateEnd && checkOut > priceRoom.DateEnd)
            {
                TimeSpan overlap = checkOut - priceRoom.DateEnd;
                TimeSpan overlapSub = priceRoom.DateEnd - checkIn;
                int days = overlap.Days;
                int hours = overlap.Hours;
                int daysSub = overlapSub.Days;
                int hoursSub = overlapSub.Hours;
                decimal totalAmount = days * priceRoom.PricePerNightDefault + hours * priceRoom.PricePerNightDefault
                    + daysSub * priceRoom.PricePerNightSub + hoursSub * priceRoom.PricePerHourSub;
                return totalAmount;
            }
            else
            {
                TimeSpan overlap1 = checkOut - priceRoom.DateEnd;
                TimeSpan overlap2 = priceRoom.DateStart - checkIn;
                TimeSpan overlapSub = priceRoom.DateEnd - priceRoom.DateStart;
                int days1 = overlap1.Days;
                int hours1 = overlap1.Hours;
                int days2 = overlap2.Days;
                int hours2 = overlap2.Hours;
                int daysSub = overlapSub.Days;
                int hoursSub = overlapSub.Hours;
                decimal totalAmount = (days1 + days2) * priceRoom.PricePerNightDefault + (hours1 + hours2) * priceRoom.PricePerNightDefault
                    + daysSub * priceRoom.PricePerNightSub + hoursSub * priceRoom.PricePerHourSub;
                return totalAmount;
            }
        }

        public decimal GetExpectedTotalByBillId(int billId)
        {
            var bill = _dbContext.BillBookings.FirstOrDefault(b => b.BillID == billId);
            if (bill == null)
            {
                throw new HotelExceptions("Booking không tồn tại!");
            }
            var roomList = (from booking in _dbContext.BillBookings
                            join bookingRoom in _dbContext.BillBooking_Rooms
                                on booking.BillID equals bookingRoom.BillID
                            join room in _dbContext.Rooms
                                on bookingRoom.RoomID equals room.RoomID
                            where booking.BillID == billId
                            select new
                            {
                                RoomId = room.RoomID,
                                CheckIn = booking.ExpectedCheckIn,
                                CheckOut = booking.ExpectedCheckOut
                            }).ToList();

            if (!roomList.Any())
            {
                throw new Exception($"Không tìm thấy phòng nào thuộc bookingId: {billId}");
            }

            decimal totalAmount = 0;
            foreach (var room in roomList)
            {
                totalAmount += GetTotalAmountByRoom(room.CheckIn, room.CheckOut, room.RoomId);
            }

            decimal prepayment = totalAmount /10;
            
            bill.Prepayment = prepayment;
            _dbContext.SaveChanges();
            return totalAmount;

        }

        public decimal GetTotalLateByBillId(int billId)
        {
            var roomList = (from booking in _dbContext.BillBookings
                            join bookingRoom in _dbContext.BillBooking_Rooms
                                on booking.BillID equals bookingRoom.BillID
                            join room in _dbContext.Rooms
                                on bookingRoom.RoomID equals room.RoomID
                            where booking.BillID == billId
                            select new
                            {
                                RoomId = room.RoomID,
                                CheckIn = booking.ExpectedCheckOut,
                                CheckOut = booking.CheckOut ?? DateTime.Now
        }).ToList();

            if (!roomList.Any())
            {
                throw new Exception($"Không tìm thấy phòng nào thuộc bookingId: {billId}");
            }

            // Tính tổng tiền cho tất cả các phòng
            decimal totalAmount = 0;
            foreach (var room in roomList)
            {
                totalAmount += GetTotalAmountByRoom(room.CheckIn, room.CheckOut, room.RoomId);
            }

            return totalAmount;

        }

        public decimal GetTotalAmountByBillId(int billId)
        {
            var charge = GetTotalChargeByBillId(billId);
            var expectedTotal = GetExpectedTotalByBillId(billId);
            var bill = _dbContext.BillBookings.FirstOrDefault(s => s.BillID == billId);

            decimal checkOutLate = 0;
            if (bill.CheckOut > bill.ExpectedCheckOut)
            {
                checkOutLate = GetTotalLateByBillId(billId);
            }

            decimal totalAmount = charge + expectedTotal + checkOutLate;
            return totalAmount;
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
            findBooking.CheckIn = input.CheckIn;
            findBooking.CheckOut = input.CheckOut;
            findBooking.ExpectedCheckOut = input.ExpectedCheckOut;
            findBooking.ExpectedCheckIn = input.ExpectedCheckIn;
            findBooking.ReceptionistID = input.ReceptionistID;
            findBooking.Status = input.Status;
            findBooking.DiscountID = input.DiscountID;
            findBooking.CustomerID = input.CustomerID;
            findBooking.BookingDate = input.BookingDate;
            findBooking.Prepayment =    input.Prepayment;
            
            _dbContext.SaveChanges();
        }

        public BookingDto GetIdBooking(int id)
        {
            var findBooking = FindBooking(id);
            return new BookingDto
            {
                BillID = findBooking.BillID,
                ExpectedCheckIn = findBooking.ExpectedCheckIn,
                ExpectedCheckOut = findBooking.ExpectedCheckOut,
                CheckIn = findBooking.CheckIn,
                CheckOut = findBooking.CheckOut,
                Prepayment = findBooking.Prepayment,
                DiscountID = findBooking.DiscountID,
                CustomerID = findBooking.CustomerID,
                ReceptionistID = findBooking.ReceptionistID,
                BookingDate = findBooking.BookingDate,
                Status = findBooking.Status
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
                })
                .ToList();

            return result;
        }



    }


}
