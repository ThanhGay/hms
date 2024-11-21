using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Hol.ApplicationService.Common;
using HMS.Hol.ApplicationService.RoomManager.Abstracts;
using HMS.Hol.Dtos.RoomTypeManager;
using HMS.Hol.Infrastructures;
using HMS.Shared.Constant.Common;
using Microsoft.Extensions.Logging;

namespace HMS.Hol.ApplicationService.RoomManager.Implements
{
    public class RoomTypeService : HotelServiceBase, IRoomTypeService
    {
        public RoomTypeService(ILogger<RoomTypeService> logger, HotelDbContext dbContext)
            : base(logger, dbContext) { }

        public void CreateRoomType(CreateRoomTypeDto input)
        {
            throw new NotImplementedException();
        }

        public void DeleteRoomType(int roomTypeId)
        {
            throw new NotImplementedException();
        }

        public PageResultDto<RoomTypeInformationDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public RoomTypeInformationDto GetById(int roomTypeId)
        {
            throw new NotImplementedException();
        }

        public void UpdateRoomType(UpdateRoomTypeDto input)
        {
            throw new NotImplementedException();
        }
    }
}
