using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Auth.Dtos.Customer
{
    public class AddCustomerDto
    {
        [
            Required(AllowEmptyStrings = false, ErrorMessage = "Vui lòng nhập email"),
            EmailAddress(ErrorMessage = "Email không đúng định dạng")
        ]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public string PassWord { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public string LastName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Số điện thoại phải bắt đầu bằng 0 và chứa đúng 10 chữ số.")]
        public string PhoneNumber { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        [RegularExpression(@"^\d{12}$", ErrorMessage = "Chỉ được nhập số và phải chứa đúng 12 chữ số.")]
        public string CitizenIdentity { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
