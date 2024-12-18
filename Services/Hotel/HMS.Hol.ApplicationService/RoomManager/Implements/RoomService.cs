using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Hol.ApplicationService.Common;
using HMS.Hol.ApplicationService.RoomManager.Abstracts;
using HMS.Hol.Domain;
using HMS.Hol.Dtos.RoomManager;
using HMS.Hol.Dtos.Upload;
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

                    var imgs = _dbContext
                        .Images.Where(i => i.RoomId == roomId)
                        .Select(img => new ImageDto
                        {
                            Description = img.Description,
                            ImageURL = img.URL,
                            Name = img.Name,
                        })
                        .ToList();

                    var result = foundRoomQuery.ToList()[0];
                    result.RoomImages = imgs;

                    return result;
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

                    var imgs = _dbContext
                        .Images.Where(i => i.RoomId == roomId)
                        .Select(img => new ImageDto
                        {
                            Description = img.Description,
                            ImageURL = img.URL,
                            Name = img.Name,
                        })
                        .ToList();

                    var result = foundRoomQuery.ToList()[0];
                    result.RoomImages = imgs;

                    return result;
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

                    var imgs = _dbContext
                        .Images.Where(i => i.RoomId == roomId)
                        .Select(img => new ImageDto
                        {
                            Description = img.Description,
                            ImageURL = img.URL,
                            Name = img.Name,
                        })
                        .ToList();

                    var result = foundRoomQuery.ToList()[0];
                    result.RoomImages = imgs;

                    return result;
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

                    var imgs = _dbContext
                        .Images.Where(i => i.RoomId == roomId)
                        .Select(img => new ImageDto
                        {
                            Description = img.Description,
                            ImageURL = img.URL,
                            Name = img.Name,
                        })
                        .ToList();

                    var result = foundRoomQuery.ToList()[0];
                    result.RoomImages = imgs;

                    return result;
                }
            }
        }

        public RoomFullDetailDto GetById(int roomId, DateOnly start, DateOnly end)
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
                        DateOnly.FromDateTime(r.DayStart) <= start
                        && end <= DateOnly.FromDateTime(r.DayEnd)
                    )
                );
                if (priceHoliDay)
                {
                    var foundRoomQuery =
                        from r in _dbContext.Rooms
                        join t in _dbContext.RoomTypes on r.RoomTypeId equals t.RoomTypeID
                        join sp in _dbContext.SubPrices on t.RoomTypeID equals sp.RoomTypeID
                        join p in _dbContext.DefaultPrices on t.RoomTypeID equals p.RoomTypeID
                        where r.RoomID == roomId
                        select new RoomFullDetailDto
                        {
                            RoomId = r.RoomID,
                            Description = t.Description,
                            RoomTypeName = t.RoomTypeName,
                            Floor = r.Floor,
                            HotelId = r.HotelId,
                            PricePerHour = p.PricePerHour,
                            PricePerNight = p.PricePerNight,
                            PricePerHolidayHour = sp.PricePerHours,
                            PricePerHolidayNight = sp.PricePerNight,
                            RoomName = r.RoomName,
                            RoomTypeId = r.RoomTypeId,
                        };

                    var imgs = _dbContext
                        .Images.Where(i => i.RoomId == roomId)
                        .Select(img => new ImageDto
                        {
                            Description = img.Description,
                            ImageURL = img.URL,
                            Name = img.Name,
                        })
                        .ToList();

                    var result = foundRoomQuery.ToList()[0];
                    result.RoomImages = imgs;

                    return result;
                }
                else
                {
                    var foundRoomQuery =
                        from r in _dbContext.Rooms
                        join t in _dbContext.RoomTypes on r.RoomTypeId equals t.RoomTypeID
                        join p in _dbContext.DefaultPrices on t.RoomTypeID equals p.RoomTypeID
                        where r.RoomID == roomId
                        select new RoomFullDetailDto
                        {
                            RoomId = r.RoomID,
                            Description = t.Description,
                            RoomTypeName = t.RoomTypeName,
                            Floor = r.Floor,
                            HotelId = r.HotelId,
                            PricePerHour = p.PricePerHour,
                            PricePerNight = p.PricePerNight,
                            PricePerHolidayHour = p.PricePerHour,
                            PricePerHolidayNight = p.PricePerNight,
                            RoomName = r.RoomName,
                            RoomTypeId = r.RoomTypeId,
                        };

                    var imgs = _dbContext
                        .Images.Where(i => i.RoomId == roomId)
                        .Select(img => new ImageDto
                        {
                            Description = img.Description,
                            ImageURL = img.URL,
                            Name = img.Name,
                        })
                        .ToList();

                    var result = foundRoomQuery.ToList()[0];
                    result.RoomImages = imgs;

                    return result;
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

        public async Task<ImageDto> AddImgae(UploadImageDto image, int roomId)
        {
            var existRoom = _dbContext.Rooms.Any(r => r.RoomID == roomId);
            if (existRoom)
            {
                string createdImageName = "";

                if (image.ImageFile != null)
                {
                    string[] allowedFileExtentions = [".jpg", ".jpeg", ".png"];

                    var ext = Path.GetExtension(image.ImageFile.FileName);
                    if (!allowedFileExtentions.Contains(ext))
                    {
                        throw new ArgumentException(
                            $"Only {string.Join(",", allowedFileExtentions)} are allowed."
                        );
                    }

                    if (image.ImageFile.Length > 0)
                    {
                        var path = Path.Combine(
                            Directory.GetCurrentDirectory(),
                            "wwwroot",
                            "Images",
                            image.ImageFile.FileName
                        );
                        using (var stream = System.IO.File.Create(path))
                        {
                            await image.ImageFile.CopyToAsync(stream);
                        }
                        ;

                        createdImageName = "/images/" + image.ImageFile.FileName;
                    }

                    var rtnData = new ImageDto
                    {
                        Description = image.Description,
                        ImageURL = createdImageName,
                        Name = image.Name,
                    };

                    var img = new HolImage
                    {
                        Name = image.Name,
                        Description = image.Description,
                        URL = createdImageName,
                        RoomId = roomId,
                    };

                    _dbContext.Images.Add(img);
                    _dbContext.SaveChanges();

                    return rtnData;
                }
                else
                {
                    _logger.LogError($"Không có file nào dược chọn");
                    throw new Exception($"No file selected");
                }
            }
            else
            {
                _logger.LogError("Không tồn tại phòng");
                throw new Exception($"Không tồn tại phòng");
            }
        }
    }
}
