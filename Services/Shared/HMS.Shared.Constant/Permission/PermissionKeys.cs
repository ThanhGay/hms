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
        public const string DeleteRoom = "Delete_room_in_hotel";
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
    }
}
