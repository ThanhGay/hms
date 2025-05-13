using HMS.Hol.ApplicationService.Common;
using HMS.Hol.ApplicationService.RoomManager.Abstracts;
using HMS.Hol.Domain;
using HMS.Hol.Dtos.RoomManager;
using HMS.Hol.Dtos.RoomManager.Review;
using HMS.Hol.Dtos.Upload;
using HMS.Hol.Infrastructures;
using HMS.Shared.ApplicationService.Auth;
using HMS.Shared.Constant.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HMS.Hol.ApplicationService.RoomManager.Implements
{
    public class RoomService : HotelServiceBase, IRoomService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IInformationService _informationService;

        public RoomService(
            ILogger<RoomService> logger,
            HotelDbContext dbContext,
            IHttpContextAccessor httpContextAccessor,
            IInformationService informationService
        )
            : base(logger, dbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _informationService = informationService;
        }

        /// <summary>
        /// Danh sách phòng theo khách sạn
        /// </summary>
        /// <param name="hotelId"></param>
        /// <returns></returns>
        public PageResultDto<RoomDetailDto> GetAllRoom(FilterRoomDto dto)
        {
            var result = new PageResultDto<RoomDetailDto>();

            // Câu truy vấn gốc
            var foundRoomQuery =
                from r in _dbContext.Rooms
                join t in _dbContext.RoomTypes on r.RoomTypeId equals t.RoomTypeID
                join p in _dbContext.DefaultPrices on t.RoomTypeID equals p.RoomTypeID
                where r.HotelId == dto.hotelId
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

            // Lọc theo từ khóa tìm kiếm (RoomName hoặc Description)
            if (!string.IsNullOrWhiteSpace(dto.Search))
            {
                string keyword = dto.Search.ToLower();
                foundRoomQuery = foundRoomQuery.Where(x =>
                    x.RoomName.ToLower().Contains(keyword) ||
                    x.Description.ToLower().Contains(keyword)
                );
            }

            // Lọc theo loại phòng (phòng đơn/phòng đôi)
            if (dto.isSingleRoom && !dto.isDoubleRoom)
            {
                foundRoomQuery = foundRoomQuery.Where(x => x.RoomTypeName.ToLower().Contains("đơn"));
            }
            else if (dto.isDoubleRoom && !dto.isSingleRoom)
            {
                foundRoomQuery = foundRoomQuery.Where(x => x.RoomTypeName.ToLower().Contains("đôi"));
            }
            // nếu cả hai được bật thì không cần lọc gì thêm

            // Sắp xếp theo giá
            if (dto.isLowHigh && !dto.isHighLow)
            {
                foundRoomQuery = foundRoomQuery.OrderBy(x => x.PricePerNight);
            }
            else if (dto.isHighLow && !dto.isLowHigh)
            {
                foundRoomQuery = foundRoomQuery.OrderByDescending(x => x.PricePerNight);
            }
            // nếu cả hai đều false thì giữ nguyên

            // Tổng số phòng sau lọc
            result.TotalItem = foundRoomQuery.Count();

            // Thực thi truy vấn
            result.Items = foundRoomQuery.ToList();

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
                    var reviews = GetAllReviewByRoomId(roomId);

                    result.RoomImages = imgs;
                    result.Reviews = reviews;

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
                    var reviews = GetAllReviewByRoomId(roomId);

                    result.RoomImages = imgs;
                    result.Reviews = reviews;

                    return result;
                }
            }
        }

        /// <summary>
        /// Return information of room (price in date parameter)
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
                    var reviews = GetAllReviewByRoomId(roomId);

                    result.RoomImages = imgs;
                    result.Reviews = reviews;

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
                    var reviews = GetAllReviewByRoomId(roomId);

                    result.RoomImages = imgs;
                    result.Reviews = reviews;

                    return result;
                }
            }
        }

        /// <summary>
        /// Return information of room (price in date range)
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
                    var reviews = GetAllReviewByRoomId(roomId);

                    result.RoomImages = imgs;
                    result.Reviews = reviews;

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
                    var reviews = GetAllReviewByRoomId(roomId);

                    result.RoomImages = imgs;
                    result.Reviews = reviews;

                    return result;
                }
            }
        }

        /// <summary>
        /// Create room
        /// </summary>
        /// <param name="input"></param>
        /// <param name="HotelId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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

        /// <summary>
        /// Update basic information of room
        /// </summary>
        /// <param name="input"></param>
        /// <param name="HotelId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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

        /// <summary>
        /// Delete room from database
        /// </summary>
        /// <param name="roomId"></param>
        /// <exception cref="Exception"></exception>
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

        /// <summary>
        /// Add image for roomId
        /// </summary>
        /// <param name="image"></param>
        /// <param name="roomId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
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

        /// <summary>
        /// Return all image with roomId
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        public List<ImageDto> GetAllImageByRoomId(int roomId)
        {
            var result = _dbContext
                .Images.Where(i => i.RoomId == roomId)
                .Select(i => new ImageDto
                {
                    Name = i.Name,
                    Description = i.Description,
                    ImageURL = i.URL,
                })
                .ToList();

            return result;
        }

        /// <summary>
        /// Đánh giá phòng
        /// </summary>
        /// <param name="dto"></param>
        /// <exception cref="Exception"></exception>
        public void ReviewRoom(CreateReviewRoomDto dto)
        {
            var currentUserId = CommonUtils.GetCurrentUserId(_httpContextAccessor);

            var lastReview = _dbContext
                .RoomReviews.Where(r => r.UserId == currentUserId && r.RoomId == dto.RoomId)
                .OrderByDescending(r => r.CreatedAt)
                .FirstOrDefault();

            if (
                lastReview != null
                && (DateTime.Now - lastReview.CreatedAt) < TimeSpan.FromSeconds(30)
            )
            {
                throw new Exception("Don't spam");
            }

            var newReview = new HolRoomReview
            {
                RoomId = dto.RoomId,
                Comment = dto.Comment,
                CreatedAt = DateTime.Now,
                Star = dto.Star,
                UserId = currentUserId,
            };

            _dbContext.RoomReviews.Add(newReview);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Cập nhật đánh giá phòng
        /// </summary>
        /// <param name="dto"></param>
        /// <exception cref="Exception"></exception>
        public void UpdateReviewRoom(UpdateReviewRoomDto dto)
        {
            var exist = _dbContext.RoomReviews.Any(r => r.Id == dto.ReviewId);
            if (exist)
            {
                var rv = _dbContext.RoomReviews.FirstOrDefault(r => r.Id == dto.ReviewId);
                if (!rv!.IsDeleted)
                {
                    rv.Star = dto.Star;
                    rv.Comment = dto.Commemt;
                }
                else
                {
                    throw new Exception("Đánh giá này đã bị xóa");
                }
                _dbContext.SaveChanges();
            }
            else
            {
                throw new Exception("Không tìm thấy đánh giá này");
            }
        }

        /// <summary>
        /// Xóa mềm bản đánh giá phòng
        /// </summary>
        /// <param name="reviewId"></param>
        /// <exception cref="Exception"></exception>
        public void DeleteReviewRoom(int reviewId)
        {
            var existRv = _dbContext.RoomReviews.Any(rv => rv.Id == reviewId && !rv.IsDeleted);
            if (existRv)
            {
                var currentUserId = CommonUtils.GetCurrentUserId(_httpContextAccessor);
                var _rv = _dbContext.RoomReviews.FirstOrDefault(rv => rv.Id == reviewId);
                if (_rv != null)
                {
                    _rv.IsDeleted = true;
                    _rv.DeletedBy = currentUserId;
                    _rv.DeletedAt = DateTime.Now;
                }
                _dbContext.SaveChanges();
            }
            else
            {
                throw new Exception($"Đánh giá không tồn tại hoặc đã bị xóa");
            }
        }

        /// <summary>
        /// Trả ra thông tin đánh giá phòng
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ResultRoomReviewDto GetAllReviewByRoomId(int roomId)
        {
            var existRoom = _dbContext.Rooms.Any(r => r.RoomID == roomId);
            var existRoomReview = _dbContext.RoomReviews.Any(r => r.RoomId == roomId);

            if (existRoom)
            {
                if (!existRoomReview)
                {
                    var listDetail = new List<DetailStar>();
                    for (int i = 1; i < 6; i++)
                    {
                        var item = GetDetailStar(i, roomId);
                        listDetail.Add(item);
                    }

                    var result = new ResultRoomReviewDto
                    {
                        RoomId = roomId,
                        Total = 0,
                        Value = 0,
                        DetailStars = listDetail,
                        DetailReviews = [],
                    };

                    return result;
                }
                else
                {
                    // lấy ra 15 đánh giá mới nhất
                    var query = _dbContext
                        .RoomReviews.Where(rv => rv.RoomId == roomId && !rv.IsDeleted)
                        .Select(rv => new ViewRoomReviewDto
                        {
                            RoomId = rv.RoomId,
                            Commemt = rv.Comment,
                            Star = rv.Star,
                            Create = rv.CreatedAt,
                            UserId = rv.UserId,
                            Name = _informationService.GetCustomerById(rv.UserId).LastName,
                        })
                        .OrderByDescending(rv => rv.Create)
                        .Take(15);

                    var totalCount = query.Count();

                    var totalStarValue = 0.0;
                    var listDetail = new List<DetailStar>();
                    for (int i = 1; i < 6; i++)
                    {
                        var item = GetDetailStar(i, roomId);
                        totalStarValue += item.Star * item.Count;
                        listDetail.Add(item);
                    }

                    var result = new ResultRoomReviewDto
                    {
                        RoomId = roomId,
                        Total = totalCount,
                        Value = totalStarValue / totalCount,
                        DetailStars = listDetail,
                        DetailReviews = query.ToList(),
                    };

                    return result;
                }
            }
            else
            {
                throw new Exception($"Không tồn tại phòng với id: {roomId}");
            }
        }

        private DetailStar GetDetailStar(int star, int roomId)
        {
            var count = _dbContext.RoomReviews.Count(rv =>
                rv.RoomId == roomId && rv.Star == star && !rv.IsDeleted
            );

            return new DetailStar { Star = star, Count = count };
        }
    }
}
