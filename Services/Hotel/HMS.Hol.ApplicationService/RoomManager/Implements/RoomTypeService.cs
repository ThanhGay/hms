using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Hol.ApplicationService.Common;
using HMS.Hol.ApplicationService.RoomManager.Abstracts;
using HMS.Hol.Domain;
using HMS.Hol.Dtos.InteriorManager;
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

        public PageResultDto<RoomTypeDefaultInformationDto> GetAll(FilterDto input)
        {
            var result = new PageResultDto<RoomTypeDefaultInformationDto>();

            var query =
                from t in _dbContext.RoomTypes
                join p in _dbContext.DefaultPrices on t.RoomTypeID equals p.RoomTypeID
                select new RoomTypeDefaultInformationDto
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

        public RoomTypeDefaultInformationDto GetById(int roomTypeId)
        {
            var existRoomType = _dbContext.RoomTypes.FirstOrDefault(type =>
                type.RoomTypeID == roomTypeId
            );
            if (existRoomType != null)
            {
                var typeDefaultPrice = _dbContext.DefaultPrices.FirstOrDefault(p =>
                    p.RoomTypeID == roomTypeId
                );

                var returnData = new RoomTypeDefaultInformationDto
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
                _logger.LogError($"Không tìm thấy thể loại phòng có Id \"{roomTypeId}\".");
                throw new Exception($"Không tìm thấy thể loại phòng có Id \"{roomTypeId}\".");
            }
        }

        public RoomTypeDefaultInformationDto CreateRoomType(CreateRoomTypeDto input)
        {
            var existType = _dbContext.RoomTypes.Any(type =>
                type.RoomTypeName == input.RoomTypeName
            );
            if (existType)
            {
                _logger.LogError($"Đã tồn tại thể loại có tên \"{input.RoomTypeName}\".");
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

                var returnData = new RoomTypeDefaultInformationDto
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

        public RoomTypeDefaultInformationDto UpdateRoomType(UpdateRoomTypeDto input)
        {
            var existTypeId = _dbContext.RoomTypes.Any(type => type.RoomTypeID == input.RoomTypeId);
            var existTypeName = _dbContext.RoomTypes.Any(type =>
                type.RoomTypeID != input.RoomTypeId && type.RoomTypeName == input.RoomTypeName
            );

            if (existTypeName)
            {
                _logger.LogError($"Đã có thể loại phòng có tên \"{input.RoomTypeName}\".");
                throw new Exception($"Đã có thể loại phòng có tên \"{input.RoomTypeName}\".");
            }
            else if (!existTypeId)
            {
                _logger.LogError($"Không tìm thấy thể loại phòng có Id \"{input.RoomTypeId}\".");
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

                var returnData = new RoomTypeDefaultInformationDto
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
                _logger.LogError($"Không tìm thấy thể loại phòng có Id: \"{roomTypeId}\".");
                throw new Exception($"Không tìm thấy thể loại phòng có Id: \"{roomTypeId}\".");
            }
        }

        public void SetPriceInHoliday(SetPriceInHolidayDto input)
        {
            var existTypeId = _dbContext.RoomTypes.Any(type => type.RoomTypeID == input.RoomTypeId);
            var existSubPrice = _dbContext.SubPrices.Any(sp =>
                sp.RoomTypeID == input.RoomTypeId
                && DateOnly.FromDateTime(input.StartDate) == DateOnly.FromDateTime(sp.DayStart)
                && DateOnly.FromDateTime(input.EndDate) == DateOnly.FromDateTime(sp.DayEnd)
            );

            if (existSubPrice)
            {
                _logger.LogError(
                    $"Đã định giá cho thể loại phòng {input.RoomTypeId} từ ngày {DateOnly.FromDateTime(input.StartDate)} đến ngày {DateOnly.FromDateTime(input.EndDate)}."
                );
                throw new Exception(
                    $"Đã định giá cho thể loại phòng {input.RoomTypeId} từ ngày {DateOnly.FromDateTime(input.StartDate)} đến ngày {DateOnly.FromDateTime(input.EndDate)}."
                );
            }
            else if (!existTypeId)
            {
                _logger.LogError($"Không tìm thấy thể loại phòng có Id \"{input.RoomTypeId}\".");
                throw new Exception($"Không tìm thấy thể loại phòng có Id \"{input.RoomTypeId}\".");
            }
            else
            {
                var newPriceInHoliday = new HolSubPrice
                {
                    RoomTypeID = input.RoomTypeId,
                    DayStart = input.StartDate,
                    DayEnd = input.EndDate,
                    PricePerHours = input.PricePerHour,
                    PricePerNight = input.PricePerNight,
                };

                _dbContext.SubPrices.Add(newPriceInHoliday);
                _dbContext.SaveChanges();
            }
        }

        public void UpdatePriceInHoliday(UpdatePriceInHoliday input)
        {
            var existPriceInHolidayId = _dbContext.SubPrices.Any(sp =>
                sp.SubPriceID == input.SubPriceId
            );
            if (existPriceInHolidayId)
            {
                var existHolidayPrice = _dbContext.SubPrices.FirstOrDefault(sp =>
                    sp.SubPriceID == input.SubPriceId
                );
                existHolidayPrice.PricePerHours = input.PricePerHour;
                existHolidayPrice.PricePerNight = input.PricePerNight;
                existHolidayPrice.DayStart = input.StartDate;
                existHolidayPrice.DayEnd = input.EndDate;

                _dbContext.SubPrices.Update(existHolidayPrice);
                _dbContext.SaveChanges();
            }
            else
            {
                _logger.LogError($"Không tìm thấy");
                throw new Exception($"Không tìm thấy");
            }
        }

        public void DeletePriceInHoliday(int subPriceId)
        {
            var existPriceInHoliday = _dbContext.SubPrices.FirstOrDefault(sp =>
                sp.SubPriceID == subPriceId
            );
            if (existPriceInHoliday != null)
            {
                _dbContext.SubPrices.Remove(existPriceInHoliday);
                _dbContext.SaveChanges();
            }
            else
            {
                _logger.LogError($"Không tìm thấy");
                throw new Exception($"Không tìm thấy");
            }
        }
    }
}
