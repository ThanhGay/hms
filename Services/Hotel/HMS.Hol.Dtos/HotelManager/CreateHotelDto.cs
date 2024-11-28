using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Hol.Dtos.HotelManager
{
    public class CreateHotelDto
    {
        [Required(ErrorMessage = "Tên khách sạn là bắt buộc.")]
        public string HotelName { get; set; }

        [Required(ErrorMessage = "Địa chỉ khách sạn là bắt buộc.")]
        public string HotelAddress { get; set; }

        [Required(ErrorMessage = "Số điện thoại là bắt buộc.")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string Hotline { get; set; }
    }
}
