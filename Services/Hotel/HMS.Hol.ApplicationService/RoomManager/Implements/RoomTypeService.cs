using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Hol.ApplicationService.Common;
using HMS.Hol.ApplicationService.RoomManager.Abstracts;
using HMS.Hol.Domain;
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

        public PageResultDto<RoomTypeInformationDto> GetAll(FilterDto input)
        {
            var result = new PageResultDto<RoomTypeInformationDto>();

            var query =
                from t in _dbContext.RoomTypes
                join p in _dbContext.DefaultPrices on t.RoomTypeID equals p.RoomTypeID
                select new RoomTypeInformationDto
                {
                    RoomTypeId = t.RoomTypeID,
                    Description = t.Description,
                    RoomTypeName = t.RoomTypeName,
                    PricePerHour = p.PricePerHour,
                    PricePerNight = p.PricePerNight,
                };

            if (!string.IsNullOrEmpty(input.Keyword))
            {
                query = query.Where(type =>
                    type.Description.ToLower().Contains(input.Keyword.ToLower())
                );
            }

            var totalItems = query.Count();

            query = query.Skip(input.SkipCount()).Take(input.PageSize);

            result.TotalItem = totalItems;
            result.Items = query.ToList();

            return result;
        }

        public RoomTypeInformationDto GetById(int roomTypeId)
        {
            var existRoomType = _dbContext.RoomTypes.FirstOrDefault(type =>
                type.RoomTypeID == roomTypeId
            );
            if (existRoomType != null)
            {
                var typeDefaultPrice = _dbContext.DefaultPrices.FirstOrDefault(p =>
                    p.RoomTypeID == roomTypeId
                );

                var returnData = new RoomTypeInformationDto
                {
                    RoomTypeId = roomTypeId,
                    Description = existRoomType.Description,
                    RoomTypeName = existRoomType.RoomTypeName,
                    PricePerNight = typeDefaultPrice.PricePerNight,
                    PricePerHour = typeDefaultPrice.PricePerHour,
                };

                return returnData;
            }
            else
            {
                throw new Exception($"Không tìm thấy thể loại phòng có Id \"{roomTypeId}\".");
            }
        }

        public RoomTypeInformationDto CreateRoomType(CreateRoomTypeDto input)
        {
            var existType = _dbContext.RoomTypes.Any(type =>
                type.RoomTypeName == input.RoomTypeName
            );
            if (existType)
            {
                throw new Exception($"Đã tồn tại thể loại có tên \"{input.RoomTypeName}\".");
            }
            else
            {
                var newType = new HolRoomType
                {
                    RoomTypeName = input.RoomTypeName,
                    Description = input.Description,
                };

                _dbContext.RoomTypes.Add(newType);
                _dbContext.SaveChanges();

                var newTypeDefault = new HolDefaultPrice
                {
                    RoomTypeID = newType.RoomTypeID,
                    PricePerHour = input.PricePerHour,
                    PricePerNight = input.PricePerNight,
                };

                _dbContext.DefaultPrices.Add(newTypeDefault);
                _dbContext.SaveChanges();

                var returnData = new RoomTypeInformationDto
                {
                    RoomTypeId = newType.RoomTypeID,
                    RoomTypeName = newType.RoomTypeName,
                    Description = newType.Description,
                    PricePerHour = newTypeDefault.PricePerHour,
                    PricePerNight = newTypeDefault.PricePerNight,
                };

                return returnData;
            }
        }

        public RoomTypeInformationDto UpdateRoomType(UpdateRoomTypeDto input)
        {
            var existTypeId = _dbContext.RoomTypes.Any(type => type.RoomTypeID == input.RoomTypeId);
            var existTypeName = _dbContext.RoomTypes.Any(type =>
                type.RoomTypeID != input.RoomTypeId && type.RoomTypeName == input.RoomTypeName
            );

            if (existTypeName)
            {
                throw new Exception($"Đã có thể loại phòng có tên \"{input.RoomTypeName}\".");
            }
            else if (!existTypeId)
            {
                throw new Exception($"Không tìm thấy thể loại phòng có Id \"{input.RoomTypeId}\".");
            }
            else
            {
                var existType = _dbContext.RoomTypes.FirstOrDefault(type =>
                    type.RoomTypeID == input.RoomTypeId
                );
                var existTypePrice = _dbContext.DefaultPrices.FirstOrDefault(p =>
                    p.RoomTypeID == input.RoomTypeId
                );

                existType.RoomTypeName = input.RoomTypeName;
                existType.Description = input.Description;

                existTypePrice.PricePerHour = input.PricePerHour;
                existTypePrice.PricePerNight = input.PricePerNight;

                _dbContext.RoomTypes.Update(existType);
                _dbContext.DefaultPrices.Update(existTypePrice);
                _dbContext.SaveChanges();

                var returnData = new RoomTypeInformationDto
                {
                    RoomTypeId = existType.RoomTypeID,
                    RoomTypeName = existType.RoomTypeName,
                    Description = existType.Description,
                    PricePerHour = existTypePrice.PricePerHour,
                    PricePerNight = existTypePrice.PricePerNight,
                };

                return returnData;
            }
        }

        public void DeleteRoomType(int roomTypeId)
        {
            var existType = _dbContext.RoomTypes.FirstOrDefault(type =>
                type.RoomTypeID == roomTypeId
            );

            if (existType != null)
            {
                var existRoomTypePrice = _dbContext.DefaultPrices.FirstOrDefault(p =>
                    p.RoomTypeID == roomTypeId
                );

                _dbContext.DefaultPrices.Remove(existRoomTypePrice);
                _dbContext.RoomTypes.Remove(existType);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new Exception($"Không tìm thấy thể loại phòng có Id: \"{roomTypeId}\".");
            }
        }
    }
}
