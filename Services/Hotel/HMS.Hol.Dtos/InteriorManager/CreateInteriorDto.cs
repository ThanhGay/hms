using System.ComponentModel.DataAnnotations;

namespace HMS.Hol.Dtos.InteriorManager
{
    public class CreateInteriorDto
    {
        public required string Name { get; set; }

        [Range(1, float.MaxValue, ErrorMessage = "Giá đồ dùng trong phòng phải lớn hơn 0")]
        public decimal Price { get; set; }
    }
}
