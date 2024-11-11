using HMS.Auth.ApplicationService.Common;
using HMS.Auth.ApplicationService.UserModule.Abstracts;
using HMS.Auth.Domain;
using HMS.Auth.Dtos;
using HMS.Auth.Infrastructures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HMS.Auth.Dtos.Customer;

namespace HMS.Auth.ApplicationService.UserModule.Implements
{
    public class CustomerService : AuthServiceBase, ICustomerService
    {
        public CustomerService(ILogger<ReceptionistService> logger, AuthDbContext dbContext) : base(logger, dbContext) 
        {
        }

        public AuthCustomer CreateCustomer([FromQuery] string email, string password, AddCustomer input)
        {
            var findEmail = _dbContext.AuthUsers.Any(u => u.Email == email);
            if (findEmail)
            {
                throw new UserExceptions("Đã tồn tại email");
            }
            var user = new AuthUser
            {
                Email = email,
                Password = BCrypt.Net.BCrypt.HashPassword(password),
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
            return customer;
        }
        public AuthCustomer UpdateInfCustomer(int customerId, AddCustomer input)
        {
            var findCustomer = _dbContext.AuthCustomers.FirstOrDefault(r => r.CustomerId == customerId)
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

    }
}
