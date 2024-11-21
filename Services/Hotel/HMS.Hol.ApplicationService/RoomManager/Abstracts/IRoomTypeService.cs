using HMS.Hol.Dtos.RoomTypeManager;
using HMS.Shared.Constant.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Hol.ApplicationService.RoomManager.Abstracts
{
    public interface IRoomTypeService
    {
        public void CreateRoomType(CreateRoomTypeDto input);
        public void UpdateRoomType(UpdateRoomTypeDto input);
        public void DeleteRoomType(int roomTypeId);
        public PageResultDto<RoomTypeInformationDto> GetAll();
        public RoomTypeInformationDto GetById (int roomTypeId);
    }
}
