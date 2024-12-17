using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Shared.Constant.Permission
{
    public class PermissionKeys
    {
        public const string AddRoom = "create_room_in_hotel";
        public const string UpdateRoom = "change_information_of_room";
        public const string InfomationRoom = "View_room";
        public const string ChangePriceRoom = "change_price_of_room";
        public const string AddReceptionist = "add_receptionist";
        public const string UpdateInfReceptionist = "update_information_receptionist";
        public const string DeleteReceptionist = "delete_receptionist";
        public const string AddCustomer = "add_customer";
        public const string UpdateInfCustomer = "update_information_customer";
        public const string DeleteCustomer = "delete_customer";
        public const string GetReceptionistById = "get_receptionist_by_id";
        public const string GetAllReceptionist = "get_all_receptionist";
        public const string GetCustomerById = "get_customer_by_id";
        public const string GetAllCustomer = "get_all_customer";
        public const string GetAllFunctionCustomer = "get_all_function_customer";
        public const string GetAllFunctionReceptionist = "get_all_function_receptionist";
        public const string GetAllFunctionManager = "get_all_function_manager";
        public const string GetVoucherById = "get_voucher_by_id";
        public const string GetAllVoucher = "get_all_voucher";
        public const string CreateVoucher = "create_voucher";
        public const string UpdateVoucher = "update_voucher";
        public const string DeleteVoucher = "delete_voucher";
        public const string SetVoucherToCustomer = "set_voucher_to_Customer";
        public const string GetAllVoucherCustomer = "get_all_voucher_customer";
        public const string CreateBooking = "create_booking";
        public const string CreatePreBooking = "create_pre_booking";
        public const string CreateCharge = "create_charge";
        public const string CreateBookingRoom = "create_booking_room";
        public const string CreateBookingCharge = "create_booking_charge";
        public const string CheckIn = "check_in";
        public const string CheckOut = "check_out";
        public const string UpdateBooking = "update_booking";
        public const string DeleteBookingById = "delete_booking_by_id";
        public const string GetBookingById = "get_booking_by_id";
        public const string GetAllBooking = "get_all_booking";
        public const string GetExpectedTotalByBillId = "get_expected_total_by_billId";
        public const string GetTotalAmountByBillId = "get_total_amount_by_billId";
        public const string GetAllHotel = "get_all_hotel";
        public const string CreateHotel = "create_hotel";
        public const string GetHotelById = "get_hotel_by_id";
        public const string UpdateHotel = "update_hotel";
        public const string DeleteHotelById = "delete_hotel_by_id";
        public const string CreatePaymentUrl = "create_payment_url";
        public const string ReturnVnPayUrl = "return_vnpay_url";
        public const string GetAllRoomInHotel = "get_all_room_in_hotel";
        public const string GetRoomById = "get_room_by_id";
        public const string GetAllTimeByRoomId = "get_at_time_by_room_id";
        public const string GetAtRangeTimeByRoomId = "get_at_range_time_by_room_id";
        public const string CreateRoomInHotel = "create_room_in_hotel";
        public const string UpdateRoomByIdInHotel = "update_room_by_id_in_hotel";
        public const string DeleteRoomById = "delete_room_by_id";
        public const string GetAllRoomType = "get_all_room_type";
        public const string GetRoomTypeById = "get_room_type_by_id";
        public const string CreateRoomType = "create_room_type";
        public const string UpdateRoomType = "update_room_type";
        public const string DeleteRoomTypeById = "delete_room_type_by_id";
        public const string SetPriceInHoliday = "set_price_in_holiday";
        public const string UpdatePriceHoliday = "update_price_in_holiday";
        public const string DeletePriceHoliday = "delete_price_in_holiday";
    }
}
