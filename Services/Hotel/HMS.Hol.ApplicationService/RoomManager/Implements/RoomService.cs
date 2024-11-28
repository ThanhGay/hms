using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Hol.ApplicationService.Common;
using HMS.Hol.ApplicationService.RoomManager.Abstracts;
using HMS.Hol.Domain;
using HMS.Hol.Dtos.RoomManager;
using HMS.Hol.Infrastructures;
using HMS.Shared.Constant.Common;
using Microsoft.Extensions.Logging;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HMS.Hol.ApplicationService.RoomManager.Implements
{
    public class RoomService : HotelServiceBase, IRoomService
    {
        public RoomService(ILogger<RoomService> logger, HotelDbContext dbContext)
            : base(logger, dbContext) { }

        public PageResultDto<RoomDetailDto> GetAllRoom(int hotelId)
        {
            var result = new PageResultDto<RoomDetailDto>();

            var foundRoomQuery =
                from r in _dbContext.Rooms
                join t in _dbContext.RoomTypes on r.RoomTypeId equals t.RoomTypeID
                join p in _dbContext.DefaultPrices on t.RoomTypeID equals p.RoomTypeID
                where r.HotelId == hotelId
                select new RoomDetailDto
                {
                    RoomId = r.RoomID,
                    Description = t.Description,
                    RoomTypeName = t.RoomTypeName,
                    Floor = r.Floor,
                    HotelId = r.HotelId,
                    PricePerHour = p.PricePerHour,
                    PricePerNight = p.PricePerNight,
                    RoomName = r.RoomName,
                    RoomTypeId = r.RoomTypeId,
                };

            var totalRoom = foundRoomQuery.Count();

            result.TotalItem = totalRoom;
            result.Items = foundRoomQuery;

            return result;
        }

        /// <summary>
        /// Return information of room (price in current)
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public RoomDetailDto GetById(int roomId)
        {
            var existRoom = _dbContext.Rooms.FirstOrDefault(r => r.RoomID == roomId);

            if (existRoom == null)
            {
                throw new Exception($"Không tìm thấy phòng");
            }
            else
            {
                var priceHoliDay = _dbContext.SubPrices.Any(r =>
                    r.RoomTypeID == existRoom.RoomTypeId
                && (
                DateOnly.FromDateTime(r.DayStart) <= DateOnly.FromDateTime(DateTime.Now)
                        && DateOnly.FromDateTime(DateTime.Now) <= DateOnly.FromDateTime(r.DayEnd)
                    )
                );
                if (priceHoliDay)
                {
                    var foundRoomQuery =
                        from r in _dbContext.Rooms
                        join t in _dbContext.RoomTypes on r.RoomTypeId equals t.RoomTypeID
                        join sp in _dbContext.SubPrices on t.RoomTypeID equals sp.RoomTypeID
                        where r.RoomID == roomId
                        select new RoomDetailDto
                        {
                            RoomId = r.RoomID,
                            Description = t.Description,
                            RoomTypeName = t.RoomTypeName,
                            Floor = r.Floor,
                            HotelId = r.HotelId,
                            PricePerHour = sp.PricePerHours,
                            PricePerNight = sp.PricePerNight,
                            RoomName = r.RoomName,
                            RoomTypeId = r.RoomTypeId,
                        };

                    return foundRoomQuery.ToList()[0];
                }
                else
                {
                    var foundRoomQuery =
                        from r in _dbContext.Rooms
                        join t in _dbContext.RoomTypes on r.RoomTypeId equals t.RoomTypeID
                        join p in _dbContext.DefaultPrices on t.RoomTypeID equals p.RoomTypeID
                        where r.RoomID == roomId
                        select new RoomDetailDto
                        {
                            RoomId = r.RoomID,
                            Description = t.Description,
                            RoomTypeName = t.RoomTypeName,
                            Floor = r.Floor,
                            HotelId = r.HotelId,
                            PricePerHour = p.PricePerHour,
                            PricePerNight = p.PricePerNight,
                            RoomName = r.RoomName,
                            RoomTypeId = r.RoomTypeId,
                        };

                    return foundRoomQuery.ToList()[0];
                }
            }
        }

        public RoomDetailDto GetById(int roomId, DateOnly date)
        {
            var existRoom = _dbContext.Rooms.FirstOrDefault(r => r.RoomID == roomId);

            if (existRoom == null)
            {
                throw new Exception($"Không tìm thấy phòng");
            }
            else
            {
                var priceHoliDay = _dbContext.SubPrices.Any(r =>
                    r.RoomTypeID == existRoom.RoomTypeId
                    && (
                        DateOnly.FromDateTime(r.DayStart) <= date
                        && date <= DateOnly.FromDateTime(r.DayEnd)
                    )
                );
                if (priceHoliDay)
                {
                    var foundRoomQuery =
                        from r in _dbContext.Rooms
                        join t in _dbContext.RoomTypes on r.RoomTypeId equals t.RoomTypeID
                        join sp in _dbContext.SubPrices on t.RoomTypeID equals sp.RoomTypeID
                        where r.RoomID == roomId
                        select new RoomDetailDto
                        {
                            RoomId = r.RoomID,
                            Description = t.Description,
                            RoomTypeName = t.RoomTypeName,
                            Floor = r.Floor,
                            HotelId = r.HotelId,
                            PricePerHour = sp.PricePerHours,
                            PricePerNight = sp.PricePerNight,
                            RoomName = r.RoomName,
                            RoomTypeId = r.RoomTypeId,
                        };

                    return foundRoomQuery.ToList()[0];
                }
                else
                {
                    var foundRoomQuery =
                        from r in _dbContext.Rooms
                        join t in _dbContext.RoomTypes on r.RoomTypeId equals t.RoomTypeID
                        join p in _dbContext.DefaultPrices on t.RoomTypeID equals p.RoomTypeID
                        where r.RoomID == roomId
                        select new RoomDetailDto
                        {
                            RoomId = r.RoomID,
                            Description = t.Description,
                            RoomTypeName = t.RoomTypeName,
                            Floor = r.Floor,
                            HotelId = r.HotelId,
                            PricePerHour = p.PricePerHour,
                            PricePerNight = p.PricePerNight,
                            RoomName = r.RoomName,
                            RoomTypeId = r.RoomTypeId,
                        };

                    return foundRoomQuery.ToList()[0];
                }
            }
        }

        public HolRoom CreateRoom(CreateRoomDto input, int HotelId)
        {
            var existHotel = _dbContext.Hotels.Any(h => h.HotelId == HotelId);
            var existRoomType = _dbContext.RoomTypes.Any(type =>
                type.RoomTypeID == input.RoomTypeId
            );
            var existRoom = _dbContext.Rooms.Any(r =>
                r.RoomName == input.RoomName && r.Floor == input.Floor && r.HotelId == HotelId
            );

            if (!existHotel)
            {
                throw new Exception(
                    $"Chưa tồn tại khách sạc có Id {HotelId}. Vui lòng kiểm tra lại"
                );
            }
            else if (!existRoomType)
            {
                throw new Exception(
                    $"Không có thể loại phòng với Id {input.RoomTypeId}. Vui lòng kiểm tra lại."
                );
            }
            else if (existRoom)
            {
                throw new Exception(
                    $"Đã có phòng {input.RoomName} ở tầng {input.Floor} trong khách sạn {HotelId}."
                );
            }
            else
            {
                var newRoom = new HolRoom
                {
                    HotelId = HotelId,
                    Floor = input.Floor,
                    RoomName = input.RoomName,
                    RoomTypeId = input.RoomTypeId,
                };

                _dbContext.Rooms.Add(newRoom);
                _dbContext.SaveChanges();

                return newRoom;
            }
        }

        public HolRoom UpdateRoom(UpdateRoomDto input, int HotelId)
        {
            var existRoom = _dbContext.Rooms.FirstOrDefault(r =>
                r.RoomID == input.RoomID && r.Floor == input.Floor && r.HotelId == HotelId
            );
            var existAnotherRoom = _dbContext.Rooms.Any(r =>
                r.RoomID != input.RoomID && r.Floor == input.Floor && r.HotelId == HotelId
            );
            var existHotel = _dbContext.Hotels.Any(h => h.HotelId == HotelId);
            var existRoomType = _dbContext.RoomTypes.Any(type =>
                type.RoomTypeID == input.RoomTypeId
            );

            if (!existHotel)
            {
                throw new Exception(
                    $"Chưa tồn tại khách sạc có Id {HotelId}. Vui lòng kiểm tra lại"
                );
            }
            else if (!existRoomType)
            {
                throw new Exception(
                    $"Không có thể loại phòng với Id {input.RoomTypeId}. Vui lòng kiểm tra lại."
                );
            }
            if (existAnotherRoom)
            {
                throw new Exception(
                    $"Đã có phòng {input.RoomName} ở tầng {input.Floor} trong khách sạn {HotelId}."
                );
            }
            else if (existRoom == null)
            {
                throw new Exception(
                    $"Không tồn tại phòng {input.RoomName} ở tầng {input.Floor} trong khách sạn {HotelId}."
                );
            }
            else
            {
                existRoom.RoomName = input.RoomName;
                existRoom.Floor = input.Floor;
                existRoom.RoomTypeId = input.RoomTypeId;

                _dbContext.Rooms.Update(existRoom);
                _dbContext.SaveChanges();

                return existRoom;
            }
        }

        public void DeleteRoom(int roomId)
        {
            var existRoom = _dbContext.Rooms.FirstOrDefault(r => r.RoomID == roomId);

            if (existRoom == null)
            {
                throw new Exception($"Không có phòng có Id {roomId}.");
            }
            else
            {
                _dbContext.Rooms.Remove(existRoom);
                _dbContext.SaveChanges();
            }
        }
    }
}
