using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Hol.Dtos.InteriorManager;

namespace HMS.Hol.ApplicationService.RoomManager.Abstracts
{
    public interface IInteriorService
    {
        public void AddInterior(CreateInteriorDto input);
        public void UpdateInterior(UpdateInteriorDto input);
        public void DeleteInterior(int roomDetailId);
        public List<string> AddInteriorIntoRoomType(List<string> itemNames, int roomTypeId);
        public void RemoveInteriorFromType(List<int> itemIds, int roomTypeId);
    }
}
