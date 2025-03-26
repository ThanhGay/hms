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
using HMS.Shared.ApplicationService.Auth;
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
        private IInformationService _informationService;

        public BillBookingService(ILogger<BillBookingService> logger, HotelDbContext dbContext, IInformationService informationService)
            : base(logger, dbContext)
        {
            _informationService = informationService;
        }

        public BookingDto CreateBooking(CreateBookingDto input)
        {
            var existsCustomer = _informationService.GetCustomerById(input.CustomerID);
            if (existsCustomer == null)
            {
                _logger.LogError("Customer này đã không tồn tại!");
                throw new HotelExceptions("Customer này đã không tồn tại!");
            }

            var existsReceptionist = _informationService.GetReceptionistById(input.ReceptionistID);
            if (existsReceptionist == null)
            {
                _logger.LogError("Receptionist này đã không tồn tại!");
                throw new HotelExceptions("Receptionist này đã không tồn tại!");
            }

            var exists = _dbContext.BillBookings
                .FirstOrDefault(s => s.BookingDate == input.BookingDate);

            if (exists != null)
            {
                _logger.LogError("Booking này đã tồn tại!");
                throw new HotelExceptions("Booking này đã tồn tại!");
            }

            // Kiểm tra ngày check-out hợp lệ
            if (input.ExpectedCheckOut <= input.ExpectedCheckIn)
            {
                _logger.LogError("Ngày Check Out không hợp lệ!");
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
            if (input.RoomIds != null && input.RoomIds.Any())
            {
                foreach (var roomId in input.RoomIds)
                {
                    var roomExists = _dbContext.Rooms.Any(r => r.RoomID == roomId);
                    if (!roomExists)
                    {
                        _logger.LogError($"Room với ID {roomId} không tồn tại.");
                        throw new HotelExceptions($"Room với ID {roomId} không tồn tại.");
                    }
                }
            }
            _dbContext.BillBookings.Add(newBooking);
            _dbContext.SaveChanges();

            if (input.RoomIds != null && input.RoomIds.Any())
            {
                foreach (var roomId in input.RoomIds)
                {
                    var roomExists = _dbContext.Rooms.Any(r => r.RoomID == roomId);
                    if (!roomExists)
                    {
                        _logger.LogError($"Room với ID {roomId} không tồn tại.");
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

            var findBooking = FindBooking(newBooking.BillID);
            decimal totalAmount = GetExpectedTotalByBillId(newBooking.BillID);
            decimal prepayment = totalAmount / 10;
            findBooking.Prepayment = prepayment;
            _dbContext.SaveChanges();

            return new BookingDto
            {
                BillID = newBooking.BillID,
                ExpectedCheckIn = newBooking.ExpectedCheckIn,
                ExpectedCheckOut = newBooking.ExpectedCheckOut,
                DiscountID = newBooking.DiscountID,
                CheckIn = newBooking.CheckIn,
                CheckOut = newBooking.CheckOut,
                Prepayment = prepayment,
                BookingDate = newBooking.BookingDate,
                Status = newBooking.Status,
                CustomerID = newBooking.CustomerID,
                ReceptionistID = newBooking.ReceptionistID,
                Rooms = _dbContext.BillBooking_Rooms
                    .Where(br => br.BillID == newBooking.BillID)
                    .Join(_dbContext.Rooms,
                          br => br.RoomID,
                          r => r.RoomID,
                          (br, r) => new RoomBookingDto
                          {
                              RoomID = r.RoomID,
                              RoomName = r.RoomName,
                              Floor = r.Floor,
                              RoomTypeId = r.RoomTypeId,
                              HotelId = r.HotelId,
                          })
                    .ToList()
            };
        }



        public BookingDto CreatePreBooking(CreatePreBookingDto input)
        {
            var existsCustomer = _informationService.GetCustomerById(input.CustomerID);
            if (existsCustomer == null)
            {
                _logger.LogError("Customer này đã không tồn tại!");
                throw new HotelExceptions("Customer này đã không tồn tại!");
            }

            var check = _informationService.CheckVoucher(input.DiscountID, input.CustomerID);
            if(check == 1)
            {
                throw new HotelExceptions("Voucher đã được sử dụng");
            }
            var exists = _dbContext.BillBookings
            .FirstOrDefault(s => s.BookingDate == input.BookingDate);

            if (exists != null)
            {
                _logger.LogError("Booking này đã tồn tại!");
                throw new HotelExceptions("Booking này đã tồn tại!");
            }
            if (input.ExpectedCheckOut <= input.ExpectedCheckIn)
            {
                _logger.LogError("Ngày Check Out không hợp lệ!");
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
            };
            if (input.RoomIds != null && input.RoomIds.Any())
            {
                foreach (var roomId in input.RoomIds)
                {
                    var roomExists = _dbContext.Rooms.Any(r => r.RoomID == roomId);
                    if (!roomExists)
                    {
                        _logger.LogError($"Room với ID {roomId} không tồn tại.");
                        throw new HotelExceptions($"Room với ID {roomId} không tồn tại.");
                    }
                }
            }
            _dbContext.BillBookings.Add(newBooking);
            _dbContext.SaveChanges();

            var bookingDateOnly = DateOnly.FromDateTime(newBooking.BookingDate);
            if (newBooking.DiscountID != null)
            {
                _informationService.UseVoucher(newBooking.DiscountID, bookingDateOnly);
            }


            if (input.RoomIds != null && input.RoomIds.Any())
            {
                foreach (var roomId in input.RoomIds)
                {
                    var roomExists = _dbContext.Rooms.Any(r => r.RoomID == roomId);
                    if (!roomExists)
                    {
                        _logger.LogError($"Room với ID {roomId} không tồn tại.");
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

            var findBooking = FindBooking(newBooking.BillID);
            decimal totalAmount = GetExpectedTotalByBillId(newBooking.BillID);
            decimal prepayment = totalAmount / 10;
            findBooking.Prepayment = prepayment;
            _dbContext.SaveChanges();

            var booking = new BookingDto
            {
                BillID = newBooking.BillID,
                ExpectedCheckIn = newBooking.ExpectedCheckIn,
                ExpectedCheckOut = newBooking.ExpectedCheckOut,
                DiscountID = newBooking.DiscountID,
                CheckIn = newBooking.CheckIn,
                CheckOut = newBooking.CheckOut,
                Prepayment = prepayment,
                BookingDate = newBooking.BookingDate,
                Status = newBooking.Status,
                CustomerID = newBooking.CustomerID,
                ReceptionistID = newBooking.ReceptionistID,
                Rooms = _dbContext.BillBooking_Rooms
                    .Where(br => br.BillID == newBooking.BillID)
                    .Join(_dbContext.Rooms,
                          br => br.RoomID,
                          r => r.RoomID,
                          (br, r) => new RoomBookingDto
                          {
                              RoomID = r.RoomID,
                              RoomName = r.RoomName,
                              Floor = r.Floor,
                              RoomTypeId = r.RoomTypeId,
                              HotelId = r.HotelId,
                          })
                    .ToList()
            };

            return booking;
        }

        public void CancelBooking(int bookingId)
        {
            var bookingExists = _dbContext.BillBookings
        .FirstOrDefault(b => b.BillID == bookingId);
            if (bookingExists == null)
            {
                _logger.LogError($"Booking với ID {bookingId} không tồn tại.");
                throw new HotelExceptions($"Booking với ID {bookingId} không tồn tại.");
            }

            bookingExists.Status = "Cancelled";
            _dbContext.SaveChanges();
        }


        public void CreateBooking_Room(int roomId, int bookingId)
        {
            var bookingExists = _dbContext.BillBookings
        .FirstOrDefault(b => b.BillID == bookingId);
            if (bookingExists == null)
            {
                _logger.LogError($"Booking với ID {bookingId} không tồn tại.");
                throw new HotelExceptions($"Booking với ID {bookingId} không tồn tại.");
            }

            var roomExists = _dbContext.Rooms.Any(b => b.RoomID == roomId);
            if (!roomExists)
            {
                _logger.LogError($"Room với ID {roomId} không tồn tại.");
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
                _logger.LogError("Charge này đã tồn tại!");
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
                _logger.LogError($"Booking với ID {bookingId} không tồn tại.");
                throw new HotelExceptions($"Booking với ID {bookingId} không tồn tại.");
            }

            var chargeExists = _dbContext.Charges.Any(b => b.Id == chargeId);
            if (!chargeExists)
            {
                _logger.LogError($"Charge với ID {chargeId} không tồn tại.");
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
                _logger.LogError("Booking không tồn tại!");
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
                _logger.LogError("Booking không tồn tại!");
                throw new HotelExceptions("Booking không tồn tại!");
            }
            booking.CheckOut = checkOut.CheckOut;
            booking.Status = checkOut.Status;
            _dbContext.SaveChanges();

            if (checkOut.ChargeIds != null && checkOut.ChargeIds.Any())
            {
                foreach (var chargeId in checkOut.ChargeIds)
                {
                    var chargeExists = _dbContext.Charges.Any(r => r.Id == chargeId);
                    if (!chargeExists)
                    {
                        _logger.LogError($"Room với ID {chargeId} không tồn tại.");
                        throw new HotelExceptions($"Room với ID {chargeId} không tồn tại.");
                    }

                    var bookingCharge = new HolBillBooking_Charge
                    {
                        BillID = booking.BillID,
                        ChargeID = chargeId,
                    };
                    _dbContext.BillBooking_Charges.Add(bookingCharge);
                }
                _dbContext.SaveChanges();
            }
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

        // Tính tổng các phí ngoài
        public decimal GetTotalChargeByBillId(int billId)
        {
            var checkCharge = _dbContext.BillBooking_Charges.FirstOrDefault(b => b.BillID == billId);
           
            if (checkCharge == null)
            {
                return 0;
            }
            var totalCharge = (from bookingCharge in _dbContext.BillBooking_Charges
                               join charge in _dbContext.Charges
                               on bookingCharge.ChargeID equals charge.Id
                               where bookingCharge.BillID == billId
                               select new
                               {
                                   money = charge.Price
                               }).ToList();
            decimal total = 0;
            foreach (var item in totalCharge)
            {
                total += item.money;  
            }
            Console.WriteLine($"TOtal charge: {total}");
            return total;
        }


        public decimal GetTotalAmountByRoom(DateTime checkIn, DateTime checkOut, int roomId)
        {

            var priceRoom = GetPriceRoom(roomId);
            // 
            if (checkIn >= priceRoom.DateStart && checkOut <= priceRoom.DateEnd)
            {
                TimeSpan overlap = checkOut - checkIn;
                int days = overlap.Days;
                int hours = overlap.Hours;
                decimal totalAmount = days * priceRoom.PricePerNightSub + hours * priceRoom.PricePerHourSub;
                return totalAmount;
            }
            else if (checkOut < priceRoom.DateStart || checkIn > priceRoom.DateEnd)
            {
                TimeSpan overlap = checkOut - checkIn;
                int days = overlap.Days;
                int hours = overlap.Hours;
                decimal totalAmount = days * priceRoom.PricePerNightDefault + hours * priceRoom.PricePerHourDefault;
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
                decimal totalAmount = days * priceRoom.PricePerNightDefault + hours * priceRoom.PricePerHourDefault
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
                decimal totalAmount = days * priceRoom.PricePerNightDefault + hours * priceRoom.PricePerHourDefault
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
                decimal totalAmount = (days1 + days2) * priceRoom.PricePerNightDefault + (hours1 + hours2) * priceRoom.PricePerHourDefault
                    + daysSub * priceRoom.PricePerNightSub + hoursSub * priceRoom.PricePerHourSub;
                return totalAmount;
            }
        }

        //Tính tiền trước
        public decimal GetExpectedTotalByBillId(int billId)
        {
            var bill = _dbContext.BillBookings.FirstOrDefault(b => b.BillID == billId);
            if (bill == null)
            {
                _logger.LogError("Booking không tồn tại!");
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
                _logger.LogError($"Không tìm thấy phòng nào thuộc bookingId: {billId}");
                throw new Exception($"Không tìm thấy phòng nào thuộc bookingId: {billId}");
            }

            decimal totalAmount = 0;
            foreach (var room in roomList)
            {
                totalAmount += GetTotalAmountByRoom(room.CheckIn, room.CheckOut, room.RoomId);
                Console.WriteLine($"Total: {totalAmount}");

            }
            if (bill.DiscountID != null)
            {
                decimal voucher = Convert.ToDecimal(_informationService.GetVoucherCustomer(bill.DiscountID));
                totalAmount = totalAmount - ((voucher/100) * totalAmount);
                
                return totalAmount;
            }
            return totalAmount;

        }

        // Tính tiền nếu trả phòng muộn
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
                _logger.LogError($"Không tìm thấy phòng nào thuộc bookingId: {billId}");
                throw new Exception($"Không tìm thấy phòng nào thuộc bookingId: {billId}");
            }

            // Tính tổng tiền cho tất cả các phòng
            decimal totalAmount = 0;
            foreach (var room in roomList)
            {
                totalAmount += GetTotalAmountByRoom(room.CheckIn, room.CheckOut, room.RoomId);
                Console.WriteLine($"Total {totalAmount}");
            }

            return totalAmount;

        }

        //Tính tiền tổng bill
        public decimal GetTotalAmountByBillId(int billId)
        {
            var charge = GetTotalChargeByBillId(billId);
            var expectedTotal = GetExpectedTotalByBillId(billId);
            var bill = _dbContext.BillBookings.FirstOrDefault(s => s.BillID == billId);

            decimal checkOutLate = 0;
            
            Console.WriteLine($"checkout: {bill.CheckOut}");
            Console.WriteLine($"expert check out:{bill.ExpectedCheckOut}");

            if (bill.CheckOut > bill.ExpectedCheckOut)
            {
                checkOutLate = GetTotalLateByBillId(billId);
            }
            Console.WriteLine(expectedTotal);
            Console.WriteLine(checkOutLate);
            decimal totalAmount = charge + expectedTotal + checkOutLate;
            return totalAmount;
        }

        public HolBillBooking FindBooking(int id)
        {
            var booking = _dbContext.BillBookings.FirstOrDefault(p => p.BillID == id);
            if (booking == null)
            {
                _logger.LogError("Không tìm thấy với ID đã cung cấp.");
                throw new HotelExceptions("Không tìm thấy với ID đã cung cấp.");
            }
            return booking;
        }

        public void DeleteBooking(int id)
        {
            var findBooking = FindBooking(id);
            var relatedBookingRooms = _dbContext.BillBooking_Rooms
                                        .Where(br => br.BillID == id)
                                        .ToList();
            if (relatedBookingRooms.Any())
            {
                _dbContext.BillBooking_Rooms.RemoveRange(relatedBookingRooms);
            }

            var relatedBookingCharges = _dbContext.BillBooking_Charges
                .Where(br => br.BillID == id).ToList();
            if (relatedBookingCharges.Any())
            {
                _dbContext.BillBooking_Charges.RemoveRange(relatedBookingCharges);
            }
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
            findBooking.Prepayment = input.Prepayment;

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
                Rooms = _dbContext.BillBooking_Rooms
                    .Where(br => br.BillID == findBooking.BillID)
                    .Join(_dbContext.Rooms,
                          br => br.RoomID,
                          r => r.RoomID,
                          (br, r) => new RoomBookingDto
                          {
                              RoomID = r.RoomID,
                              RoomName = r.RoomName,
                              Floor = r.Floor,
                              RoomTypeId = r.RoomTypeId,
                              HotelId = r.HotelId,
                          })
                    .ToList(),
                Charges = _dbContext.BillBooking_Charges
                    .Where(br => br.BillID == findBooking.BillID)
                    .Join(_dbContext.Charges,
                          br => br.ChargeID,
                          r => r.Id,
                          (br, r) => new ChargeDto
                          {
                              ChargeId = r.Id,
                              Descreption = r.Descreption,
                              Price = r.Price,
                          })
                    .ToList(),
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
                    BillID = s.BillID,
                    BookingDate = s.BookingDate,
                    CheckIn = s.CheckIn,
                    CheckOut = s.CheckOut,
                    CustomerID = s.CustomerID,
                    DiscountID = s.DiscountID,
                    ExpectedCheckIn = s.ExpectedCheckIn,
                    ExpectedCheckOut = s.ExpectedCheckOut,
                    Prepayment = s.Prepayment,
                    ReceptionistID = s.ReceptionistID,
                    Rooms = _dbContext.BillBooking_Rooms
                    .Where(br => br.BillID == s.BillID)
                    .Join(_dbContext.Rooms,
                          br => br.RoomID,
                          r => r.RoomID,
                          (br, r) => new RoomBookingDto
                          {
                              RoomID = r.RoomID,
                              RoomName = r.RoomName,
                              Floor = r.Floor,
                              RoomTypeId = r.RoomTypeId,
                              HotelId = r.HotelId,
                          })
                    .ToList(),
                    Status = s.Status
                })
                .ToList();

            return result;
        }

        public HolCharge FindCharge(int id)
        {
            var charge = _dbContext.Charges.FirstOrDefault(p => p.Id == id);
            if (charge == null)
            {
                _logger.LogError("Không tìm thấy với ID đã cung cấp.");
                throw new HotelExceptions("Không tìm thấy với ID đã cung cấp.");
            }
            return charge;
        }

        public void UpdateCharge(ChargeDto input)
        {
            var findCharge = FindCharge(input.ChargeId);
            findCharge.Descreption = input.Descreption;
            findCharge.Price = input.Price;

            _dbContext.SaveChanges();
        }

        public void DeleteCharge(int id)
        {
            var findCharge = FindCharge(id);

            var relatedBookingCharges = _dbContext.BillBooking_Charges
                .Where(br => br.ChargeID == id).ToList();
            if (relatedBookingCharges.Any())
            {
                _dbContext.BillBooking_Charges.RemoveRange(relatedBookingCharges);
            }
            _dbContext.Charges.Remove(findCharge);
            _dbContext.SaveChanges();
        }

        public ChargeDto GetChargeById(int id)
        {
            var findCharge = FindCharge(id);
            return new ChargeDto
            {
                ChargeId = findCharge.Id,
                Descreption = findCharge.Descreption,
                Price = findCharge.Price,
            };
        }

        public List<ChargeDto> GetChargeByIdBooking(int bookingId)
        {
            var isBookingExists = _dbContext.BillBookings.Any(b => b.BillID == bookingId);
            if (!isBookingExists)
            {
                _logger.LogError($"Booking với ID {bookingId} không tồn tại.");
                throw new HotelExceptions($"Booking với ID {bookingId} không tồn tại.");
            }

            var charges = (from bc in _dbContext.BillBooking_Charges
                           join c in _dbContext.Charges
                           on bc.ChargeID equals c.Id
                           where bc.BillID == bookingId
                           select new ChargeDto
                           {
                               ChargeId = c.Id,
                               Price = c.Price,
                               Descreption = c.Descreption
                           }).ToList();

            return charges;
        }
    }

}
