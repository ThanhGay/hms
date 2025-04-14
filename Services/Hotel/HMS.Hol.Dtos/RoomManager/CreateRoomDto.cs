using System.ComponentModel.DataAnnotations;

namespace HMS.Hol.Dtos.RoomManager
{
    public class CreateRoomDto
    {
        public string RoomName { get; set; }
        public int RoomTypeId { get; set; }
        [Range(1, 100, ErrorMessage = "Số tầng phải từ 1 - 100")]
        public int Floor { get; set; }
    }
}
