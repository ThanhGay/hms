using HMS.Hol.Dtos.HotelManager;
using HMS.Shared.Constant.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Hol.ApplicationService.HotelManager.Abstracts
{
    public interface IHotelService
    {
        public void CreateHotel(CreateHotelDto input);
        public void UpdateHotel(UpdateHotelDto input);
        public void DeleteHotel(int HotelId);
        public PageResultDto<HotelInformationDto> GetAll();
        public HotelInformationDto GetById(int HotelId);
    }
}
