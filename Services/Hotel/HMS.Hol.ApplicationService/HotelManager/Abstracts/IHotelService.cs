using HMS.Hol.Domain;
using HMS.Hol.Dtos.HotelManager;
using HMS.Shared.Constant.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Hol.ApplicationService.HotelManager.Abstracts
{
    public interface IHotelService
    {
        public HolHotel CreateHotel(CreateHotelDto input);
        public HolHotel UpdateHotel(UpdateHotelDto input);
        public void DeleteHotel(int HotelId);
        public PageResultDto<HotelInformationDto> GetAllHotel([FromQuery] FilterDto input);
        public HotelInformationDto GetById(int HotelId);
    }
}
