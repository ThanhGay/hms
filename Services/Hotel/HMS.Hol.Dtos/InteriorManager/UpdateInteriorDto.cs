using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Hol.Dtos.InteriorManager
{
    public class UpdateInteriorDto
    {
        public int RoomDetailId { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
    }
}
