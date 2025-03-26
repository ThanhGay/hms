using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Hol.Dtos.InteriorManager
{
    public class CreateInteriorDto
    {
        public required string Name { get; set; }

        [Range(1, float.MaxValue, ErrorMessage = "Giá đồ dùng trong phòng phải lớn hơn 0")]
        public decimal Price { get; set; }
    }
}
