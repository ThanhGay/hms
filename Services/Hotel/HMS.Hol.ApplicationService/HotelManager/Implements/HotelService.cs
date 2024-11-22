using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    throw new ArgumentException("HotelName, HotelAddress, and Hotline are required.");
                }

                // Check if the hotel already exists (example: check by name and address)
                var existingHotel = _dbContext.Hotels
                    .FirstOrDefault(h => h.HotelName == input.HotelName && h.HotelAddress == input.HotelAddress);

                if (existingHotel != null)
                {
                    throw new InvalidOperationException("A hotel with the same name and address already exists.");
                }

                // Create new hotel entity
                var newHotel = new HolHotel
                {
                    HotelName = input.HotelName,
                    HotelAddress = input.HotelAddress,
                    Hotline = input.Hotline,
                };

                _dbContext.Hotels.Add(newHotel);
                _dbContext.SaveChanges();

                _logger.LogInformation("Hotel created successfully: {HotelName}", input.HotelName);
                return newHotel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the hotel: {HotelName}", input.HotelName);

                throw;
            }
        }

        public void DeleteHotel(int HotelId)
        {
            throw new NotImplementedException();
        }

        public PageResultDto<HotelInformationDto> GetAllHotel([FromQuery] FilterDto input)
        {
            var result = new PageResultDto<HotelInformationDto>();
            var hotelsQuery = from h in _dbContext.Hotels
                              select new HotelInformationDto
                              {
                                  HotelId = h.HotelId,
                                  HotelName = h.HotelName,
                                  HotelAddress = h.HotelAddress,
                                  Hotline = h.Hotline,
                             
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




        public HotelInformationDto GetById(int HotelId)
        {
            throw new NotImplementedException();
        }

        public void UpdateHotel(UpdateHotelDto input)
        {
            throw new NotImplementedException();
        }


        HolHotel IHotelService.UpdateHotel(UpdateHotelDto input)
        {
            throw new NotImplementedException();
        }
    }
}
