using HMS.Auth.ApplicationService.Common;
using HMS.Auth.ApplicationService.UserModule.Abstracts;
using HMS.Auth.Domain;
using HMS.Auth.Dtos;
using HMS.Auth.Infrastructures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HMS.Auth.Dtos.Customer;
using HMS.Shared.ApplicationService.Hotel.Room;
using HMS.Shared.ApplicationService.Auth;
using Microsoft.AspNetCore.Http;

namespace HMS.Auth.ApplicationService.UserModule.Implements
{
    public class CustomerService : AuthServiceBase, ICustomerService
    {
        private readonly IVoucherService _voucherService;
        private readonly IInformationRoomService _informationRoomService;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        public CustomerService(ILogger<CustomerService> logger, AuthDbContext dbContext, IVoucherService voucherService, IInformationRoomService informationRoomService, IHttpContextAccessor HttpContextAccessor) : base(logger, dbContext)
        {
            _voucherService = voucherService;
            _informationRoomService = informationRoomService;
            _HttpContextAccessor = HttpContextAccessor;
        }

        public AuthCustomer CreateCustomer([FromBody] AddCustomerDto input)
        {
            var findEmail = _dbContext.AuthUsers.Any(u => u.Email == input.Email);
            if (findEmail)
            {
                _logger.LogError("Đã tồn tại email");
                throw new UserExceptions("Đã tồn tại email");
            }
            var user = new AuthUser
            {
                Email = input.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(input.PassWord),
                RoleId = 1
            };
            _dbContext.AuthUsers.Add(user);
            _dbContext.SaveChanges();

            var customer = new AuthCustomer
            {
                CustomerId = user.UserId,
                FirstName = input.FirstName,
                LastName = input.LastName,
                CitizenIdentity = input.CitizenIdentity,
                PhoneNumber = input.PhoneNumber,
                DateOfBirth = input.DateOfBirth,
            };
            _dbContext.AuthCustomers.Add(customer);
            _dbContext.SaveChanges();

            var newVoucher = new AuthVoucher
            {
                Percent = 50,
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                ExpDate = DateOnly.FromDateTime(DateTime.Now).AddDays(30)
            };
            _dbContext.AuthVouchers.Add(newVoucher);
            _dbContext.SaveChanges();


            _voucherService.SetVoucherToCustomer(newVoucher.VoucherId, customer.CustomerId);

            return customer;
        }
        public AuthCustomer UpdateInfCustomer(UpdateCustomerDto input)
        {
            var findCustomer = _dbContext.AuthCustomers.FirstOrDefault(r => r.CustomerId == input.CustomerId)
                ?? throw new UserExceptions("Không tồn tại cutomer");
            if (findCustomer != null)
            {
                findCustomer.FirstName = input.FirstName;
                findCustomer.LastName = input.LastName;
                findCustomer.CitizenIdentity = input.CitizenIdentity;
                findCustomer.PhoneNumber = input.PhoneNumber;
                findCustomer.DateOfBirth = input.DateOfBirth;
            }
            _dbContext.AuthCustomers.Update(findCustomer);
            _dbContext.SaveChanges();
            return findCustomer;
        }
        public void DeleteCustomer(int customerId)
        {
            var findCustomer = _dbContext.AuthUsers.FirstOrDefault(r => r.UserId == customerId)
            ?? throw new UserExceptions("Không tồn tại customer");
            findCustomer.IsDeleted = true;
            _dbContext.AuthUsers.Update(findCustomer);
            _dbContext.SaveChanges();
        }
        public AuthCustomer GetCustomerById([FromQuery] int id)
        {
            var findCustomer = _dbContext.AuthCustomers.FirstOrDefault(r => r.CustomerId == id)
                ?? throw new UserExceptions("Không tồn tại customer");
            var checkDelete = _dbContext.AuthUsers.FirstOrDefault(u => u.UserId == id);

            if (checkDelete.IsDeleted)
            {
                throw new UserExceptions("Người dùng đã bị xóa");
            }

            return findCustomer;
        }
        public PageResultDto<AuthCustomer> GetAllCustomer([FromQuery] FilterDto input)
        {
            var result = new PageResultDto<AuthCustomer>();
            var checkDelete = from au in _dbContext.AuthUsers
                              join ac in _dbContext.AuthCustomers on au.UserId equals ac.CustomerId
                              where au.IsDeleted == false
                              select new AuthCustomer
                              {
                                  CustomerId = ac.CustomerId,
                                  FirstName = ac.FirstName,
                                  LastName = ac.LastName,
                                  PhoneNumber = ac.PhoneNumber,
                                  CitizenIdentity = ac.CitizenIdentity,
                                  DateOfBirth = ac.DateOfBirth
                              };
            var query = checkDelete.Where(e =>
                        string.IsNullOrEmpty(input.KeyWord)
                        || e.LastName.ToLower().Contains(input.KeyWord.ToLower()));
            result.TotalItem = query.Count();
            query = query.OrderBy(e => e.LastName)
                         .Skip(input.Skip())
                         .Take(input.PageSize);
            result.Items = query.ToList();
            return result;
        }

        public PageResultDto<VoucherCustomerDto> GetAllVoucherByCustomer([FromQuery] FilterDto input)
        {
            int userId = CommonUtils.GetCurrentUserId(_HttpContextAccessor);

            var result = new PageResultDto<VoucherCustomerDto>();

            var findVoucher = from v in _dbContext.AuthVouchers
                              join vc in _dbContext.AuthCustomerVouchers on v.VoucherId equals vc.VoucherId
                              where vc.CustomerId == userId
                              select new VoucherCustomerDto
                              {
                                  VoucherId = v.VoucherId,
                                  Percent = v.Percent,
                                  StartDate = v.StartDate,
                                  ExpDate = v.ExpDate,
                                  checkUse = vc.UsedAt == null ? false : true,
                              };

            var query = findVoucher.Where(e =>
            string.IsNullOrEmpty(input.KeyWord)
            || e.VoucherId.ToString().ToLower().Contains(input.KeyWord.ToLower()));
            result.TotalItem = query.Count();
            query = query.OrderBy(e => e.VoucherId)
                         .Skip(input.Skip())
                         .Take(input.PageSize);
            result.Items = query.ToList();
            return result;
        }

        public List<AuthVoucher> GetAllVoucherUse(int customerId)
        {
            var findCustomer = _dbContext.AuthCustomers.Any(x => x.CustomerId == customerId);
            if (!findCustomer) { throw new UserExceptions("Không tồn tại khách hàng"); }
            var findVoucher = from v in _dbContext.AuthVouchers
                              join vc in _dbContext.AuthCustomerVouchers on v.VoucherId equals vc.VoucherId
                              where vc.CustomerId == customerId && vc.UsedAt == null
                              select new AuthVoucher
                              {
                                  VoucherId = v.VoucherId,
                                  Percent = v.Percent,
                                  ExpDate = v.ExpDate,
                                  StartDate = v.StartDate,
                              };
            return findVoucher.ToList();

        }

        public AuthFavouriteRoom AddFavourite(int roomId)
        {
            int userId = CommonUtils.GetCurrentUserId(_HttpContextAccessor);

            var findCustomer = _dbContext.AuthCustomers.FirstOrDefault(c => c.CustomerId == userId)
                ?? throw new UserExceptions("Không tồn tại người dùng");
            
            
            if (_informationRoomService.CheckRoom(roomId))
            {
                AuthFavouriteRoom addFav = new AuthFavouriteRoom { CustomerId = userId, RoomId = roomId };
                _dbContext.AuthFavouriteRooms.Add(addFav);
                _dbContext.SaveChanges();
                return addFav;
            }
            else
            {
                throw new UserExceptions("Không tồn tại phòng");
            }

        }
        public void RemoveFavourite(int roomId)
        {
            int userId = CommonUtils.GetCurrentUserId(_HttpContextAccessor);

            var findCustomer = _dbContext.AuthCustomers.FirstOrDefault(c => c.CustomerId == userId)
                ?? throw new UserExceptions("Không tồn tại người dùng");


            if (_informationRoomService.CheckRoom(roomId))
            {
                var findFav = _dbContext.AuthFavouriteRooms.FirstOrDefault(c => c.CustomerId == userId && c.RoomId == roomId)
                    ?? throw new UserExceptions("Không tồn tại yêu thích");
                _dbContext.AuthFavouriteRooms.Remove(findFav);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new UserExceptions("Không tồn tại phòng");
            }

        }
        public PageResultDto<FavouriteRoomDto> GetAllFavourite([FromQuery] FilterDto input)
        {
            int userId = CommonUtils.GetCurrentUserId(_HttpContextAccessor);

            var result = new PageResultDto<FavouriteRoomDto>();

            var findFavourite = from v in _dbContext.AuthFavouriteRooms
                              where v.CustomerId == userId
                                select new FavouriteRoomDto
                                {
                                  HotelId = _informationRoomService.FindHotelRoom(v.RoomId),
                                  FavouriteId = v.FavouriteId,
                                  RoomId = v.RoomId
                              };


            var query = findFavourite.Where(e =>
            string.IsNullOrEmpty(input.KeyWord)
            || e.FavouriteId.ToString().ToLower().Contains(input.KeyWord.ToLower()));
            result.TotalItem = query.Count();
            query = query.OrderBy(e => e.FavouriteId)
                         .Skip(input.Skip())
                         .Take(input.PageSize);
            result.Items = query.ToList();
            return result;
        }

    }
}
