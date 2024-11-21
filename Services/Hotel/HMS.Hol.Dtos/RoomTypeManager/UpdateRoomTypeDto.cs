using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Hol.Dtos.RoomTypeManager
{
    public class UpdateRoomTypeDto
    {
        private string _roomType,
            _description;

        public int RoomTypeId { get; set; }

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

        [Range(1, int.MaxValue, ErrorMessage = "Giá phòng/giờ phải lớn hơn 0")]
        public int PricePerHour { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Giá phòng/giờ phải lớn hơn 0")]
        public int PricePerNight { get; set; }
    }
}
