using HMS.Hol.Dtos.RoomTypeManager;
using HMS.Shared.Constant.Common;

namespace HMS.Hol.ApplicationService.RoomManager.Abstracts
{
    public interface IRoomTypeService
    {
        public RoomTypeDefaultInformationDto CreateRoomType(CreateRoomTypeDto input);
        public RoomTypeDefaultInformationDto UpdateRoomType(UpdateRoomTypeDto input);
        public void DeleteRoomType(int roomTypeId);
        public PageResultDto<RoomTypeDefaultInformationDto> GetAll(FilterDto input);
        public RoomTypeDefaultInformationDto GetById(int roomTypeId);

        public void SetPriceInHoliday(SetPriceInHolidayDto input);
        public void UpdatePriceInHoliday(UpdatePriceInHoliday input);
        public void DeletePriceInHoliday(int subPriceId);
    }
}
