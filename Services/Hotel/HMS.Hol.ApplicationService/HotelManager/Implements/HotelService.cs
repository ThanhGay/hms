using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Hol.ApplicationService.Common;
using HMS.Hol.ApplicationService.HotelManager.Abstracts;
using HMS.Hol.Dtos.HotelManager;
using HMS.Hol.Infrastructures;
using HMS.Shared.Constant.Common;
using Microsoft.Extensions.Logging;

namespace HMS.Hol.ApplicationService.HotelManager.Implements
{
    public class HotelService : HotelServiceBase, IHotelService
    {
        public HotelService(ILogger<HotelService> logger, HotelDbContext dbContext)
            : base(logger, dbContext) { }

        public void CreateHotel(CreateHotelDto input)
        {
            throw new NotImplementedException();
        }

        public void DeleteHotel(int HotelId)
        {
            throw new NotImplementedException();
        }

        public PageResultDto<HotelInformationDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public HotelInformationDto GetById(int HotelId)
        {
            throw new NotImplementedException();
        }

        public void UpdateHotel(UpdateHotelDto input)
        {
            throw new NotImplementedException();
        }
    }
}
