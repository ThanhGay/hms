using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Hol.Dtos.RoomTypeManager
{
    public class CreateRoomTypeDto
    {
        private string _roomType,
            _description;

        [Required(
            AllowEmptyStrings = false,
            ErrorMessage = "Tên thể loại phòng không được để trống"
        )]
        public required string RoomTypeName
        {
            get => _roomType;
            set => _roomType = value.Trim();
        }

        [Required(
            AllowEmptyStrings = false,
            ErrorMessage = "Bạn phải nhập mô tả cho thể loại phòng này."
        )]
        public required string Description
        {
            get => _description;
            set => _description = value.Trim();
        }

        [Range(1, float.MaxValue, ErrorMessage = "Giá phòng/giờ phải lớn hơn 0")]
        public decimal PricePerHour { get; set; }

        [Range(1, float.MaxValue, ErrorMessage = "Giá phòng/giờ phải lớn hơn 0")]
        public decimal PricePerNight { get; set; }
    }
}
