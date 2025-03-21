﻿using HMS.Hol.Dtos.RoomManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Hol.Dtos.BookingManager
{
    public class BookingDto
    {
        public int BillID { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime ExpectedCheckIn { get; set; }
        public DateTime ExpectedCheckOut { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public decimal? Prepayment { get; set; } = 0;
        public string Status { get; set; }
        public int? DiscountID { get; set; }
        public int? CustomerID { get; set; }
        public int? ReceptionistID { get; set; }
        public List<RoomBookingDto> Rooms { get; set; }
        public List<ChargeDto> Charges { get; set; }
    }
}
