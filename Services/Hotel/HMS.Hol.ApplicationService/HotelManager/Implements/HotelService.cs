using HMS.Hol.ApplicationService.Common;
using HMS.Hol.ApplicationService.HotelManager.Abstracts;
using HMS.Hol.Domain;
using HMS.Hol.Dtos.HotelManager;
using HMS.Hol.Infrastructures;
using HMS.Shared.Constant.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HMS.Hol.ApplicationService.HotelManager.Implements
{
    public class HotelService : HotelServiceBase, IHotelService
    {
        public HotelService(ILogger<HotelService> logger, HotelDbContext dbContext)
            : base(logger, dbContext) { }

        public HolHotel CreateHotel(CreateHotelDto input)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(input.HotelName) ||
                    string.IsNullOrWhiteSpace(input.HotelAddress) ||
                    string.IsNullOrWhiteSpace(input.Hotline))
                {
                    throw new ArgumentException("HotelName, HotelAddress, and Hotline bắt buộc phải có.");
                }

                // Check if the hotel already exists (example: check by name and address)
                var existingHotel = _dbContext.Hotels
                    .FirstOrDefault(h => h.HotelName == input.HotelName && h.HotelAddress == input.HotelAddress);

                if (existingHotel != null)
                {
                    throw new InvalidOperationException("Đã tồn tại một khách sạn có cùng tên và địa chỉ");
                }

                var newHotel = new HolHotel
                {
                    HotelName = input.HotelName,
                    HotelAddress = input.HotelAddress,
                    Hotline = input.Hotline,
                };

                _dbContext.Hotels.Add(newHotel);
                _dbContext.SaveChanges();

                _logger.LogInformation("Khách sạn được tạo thành công: {HotelName}", input.HotelName);
                return newHotel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Đã xảy ra lỗi khi tạo khách sạn:{HotelName}", input.HotelName);

                throw;
            }
        }

        public PageResultDto<HotelInformationDto> GetAllHotel([FromQuery] FilterDto input)
        {
            var result = new PageResultDto<HotelInformationDto>();
            var hotelsQuery = from h in _dbContext.Hotels
                              join r in _dbContext.Rooms on h.HotelId equals r.HotelId into hotelRooms
                              select new HotelInformationDto
                              {
                                  HotelId = h.HotelId,
                                  HotelName = h.HotelName,
                                  HotelAddress = h.HotelAddress,
                                  Hotline = h.Hotline,
                                  TotalRoom = hotelRooms.Count() 
                              };

            var query = hotelsQuery.Where(e =>
                          string.IsNullOrEmpty(input.Keyword)
                          || e.HotelName.ToLower().Contains(input.Keyword.ToLower()));
            result.TotalItem = query.Count();
            query = query.OrderBy(e => e.HotelName)
                         .Skip(input.SkipCount())
                         .Take(input.PageSize);
            result.Items = query.ToList();

            return result;
        }

        public HotelInformationDto GetById(int hotelId)
        {
            try
            {
                var hotel = _dbContext.Hotels.FirstOrDefault(h => h.HotelId == hotelId);

                if (hotel == null)
                {
                    throw new KeyNotFoundException($"Không tìm thấy khách sạn với ID {hotelId}.");
                }

                var totalRooms = _dbContext.Rooms.Count(r => r.HotelId == hotelId);

                var hotelDto = new HotelInformationDto
                {
                    HotelId = hotel.HotelId,
                    HotelName = hotel.HotelName,
                    HotelAddress = hotel.HotelAddress,
                    Hotline = hotel.Hotline,
                    TotalRoom = totalRooms
                };

                return hotelDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy thông tin khách sạn với ID={HotelId}", hotelId);
                throw;
            }
        }


        public HolHotel UpdateHotel(UpdateHotelDto input)
        {
            try
            {
                var existingHotel = _dbContext.Hotels.FirstOrDefault(h => h.HotelId == input.HotelId);
                if (existingHotel == null)
                {
                    throw new KeyNotFoundException($"Không tìm thấy khách sạn có ID {input.HotelId}.");
                }

                var duplicateHotel = _dbContext.Hotels.Any(h =>
                    h.HotelId != input.HotelId &&
                    h.HotelName == input.HotelName &&
                    h.HotelAddress == input.HotelAddress);

                if (duplicateHotel)
                {
                    throw new InvalidOperationException(
                        $"Đã tồn tại khách sạn có tên '{input.HotelName}' tại địa chỉ '{input.HotelAddress}'.");
                }

                existingHotel.HotelName = input.HotelName;
                existingHotel.HotelAddress = input.HotelAddress;
                existingHotel.Hotline = input.Hotline;

                _dbContext.Hotels.Update(existingHotel);
                _dbContext.SaveChanges();

                _logger.LogInformation("Cập nhật thông tin khách sạn thành công: {HotelId}", input.HotelId);
                return existingHotel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi cập nhật thông tin khách sạn: {HotelId}", input.HotelId);
                throw;
            }
        }
        public void DeleteHotel(int hotelId)
        {
            var existHotel = _dbContext.Hotels.FirstOrDefault(r => r.HotelId == hotelId);

            if (existHotel == null)
            {
                throw new Exception($"Không có phòng có Id {hotelId}.");
            }
            else
            {
                _dbContext.Hotels.Remove(existHotel);
                _dbContext.SaveChanges();
            }
        }



    }
}
