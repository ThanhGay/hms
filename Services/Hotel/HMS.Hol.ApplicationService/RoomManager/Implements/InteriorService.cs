using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Hol.ApplicationService.Common;
using HMS.Hol.ApplicationService.RoomManager.Abstracts;
using HMS.Hol.Domain;
using HMS.Hol.Dtos.InteriorManager;
using HMS.Hol.Infrastructures;
using Microsoft.Extensions.Logging;

namespace HMS.Hol.ApplicationService.RoomManager.Implements
{
    public class InteriorService : HotelServiceBase, IInteriorService
    {
        public InteriorService(ILogger<InteriorService> logger, HotelDbContext dbContext)
            : base(logger, dbContext) { }

        public void AddInterior(CreateInteriorDto input)
        {
            var existInteriorName = _dbContext.RoomDetails.Any(item => item.Name == input.Name);

            if (existInteriorName)
            {
                _logger.LogError($"Đã tồn tại vật dụng có tên {input.Name}");
                throw new Exception($"Đã tồn tại vật dụng có tên {input.Name}");
            }
            else
            {
                var newInterior = new HolRoomDetail { Name = input.Name, Price = input.Price };

                _dbContext.RoomDetails.Add(newInterior);
                _dbContext.SaveChanges();
            }
        }

        public void UpdateInterior(UpdateInteriorDto input)
        {
            var existInterior = _dbContext.RoomDetails.FirstOrDefault(item =>
                item.RoomDetailID == input.RoomDetailId
            );
            var existInteriorName = _dbContext.RoomDetails.Any(item => item.Name == input.Name);

            if (existInteriorName)
            {
                _logger.LogError($"Đã tồn tại vật dụng có tên {input.Name}");
                throw new Exception($"Đã tồn tại vật dụng có tên {input.Name}");
            }
            else if (existInterior == null)
            {
                _logger.LogError($"Không tìm thấy vật dụng phòng có Id \"{input.RoomDetailId}\".");
                throw new Exception(
                    $"Không tìm thấy vật dụng phòng có Id \"{input.RoomDetailId}\"."
                );
            }
            else
            {
                existInterior.Name = input.Name;
                existInterior.Price = input.Price;

                _dbContext.RoomDetails.Update(existInterior);
                _dbContext.SaveChanges();
            }
        }

        public void DeleteInterior(int roomDetailId)
        {
            var existInterior = _dbContext.RoomDetails.FirstOrDefault(item =>
                item.RoomDetailID == roomDetailId
            );

            if (existInterior == null)
            {
                _logger.LogError($"Không tìm thấy vật dụng phòng có Id \"{roomDetailId}\".");
                throw new Exception($"Không tìm thấy vật dụng phòng có Id \"{roomDetailId}\".");
            }
            else
            {
                var roomHaveInterior = _dbContext
                    .RoomType_RoomDetails.Where(d => d.RoomDetailID == existInterior.RoomDetailID)
                    .Select(d => new HolRoomType_RoomDetail
                    {
                        RoomDetailID = d.RoomDetailID,
                        RoomTypeID = d.RoomTypeID,
                    });

                _dbContext.RoomType_RoomDetails.RemoveRange(roomHaveInterior);
                _dbContext.SaveChanges();
                _dbContext.RoomDetails.Remove(existInterior);
                _dbContext.SaveChanges();
            }
        }

        public List<string> AddInteriorIntoRoomType(List<string> itemNames, int roomTypeId)
        {
            var added = new List<string>();
            foreach (var item in itemNames)
            {
                var existInteriorName = _dbContext.RoomDetails.FirstOrDefault(i =>
                    i.Name.ToLower() == item.ToLower()
                );
                if (existInteriorName == null)
                {
                    continue;
                }
                else
                {
                    _dbContext.RoomType_RoomDetails.Add(
                        new HolRoomType_RoomDetail
                        {
                            RoomDetailID = existInteriorName.RoomDetailID,
                            RoomTypeID = roomTypeId,
                        }
                    );
                    _dbContext.SaveChanges();
                    added.Add(item);
                }
            }
            return added;
        }

        public void RemoveInteriorFromType(List<int> itemIds, int roomTypeId)
        {
            foreach (var item in itemIds)
            {
                var existItem = _dbContext.RoomType_RoomDetails.FirstOrDefault(e =>
                    e.RoomDetailID == item && e.RoomTypeID == roomTypeId
                );

                if (existItem == null)
                {
                    continue;
                }
                else
                {
                    _dbContext.RoomType_RoomDetails.Remove(existItem);
                    _dbContext.SaveChanges();
                }
            }
        }
    }
}
