using HMS.Auth.ApplicationService.Common;
using HMS.Auth.ApplicationService.UserModule.Abstracts;
using HMS.Auth.Domain;
using HMS.Auth.Dtos;
using HMS.Auth.Dtos.Receptionist;
using HMS.Auth.Infrastructures;
using HMS.Shared.Constant.Permission;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Auth.ApplicationService.UserModule.Implements
{
    public class ReceptionistService : AuthServiceBase, IReceptionistService
    {
        public ReceptionistService(ILogger<ReceptionistService> logger, AuthDbContext dbContext) : base(logger, dbContext) 
        {
        }

        public AuthReceptionist CreateReceptionist([FromQuery] string email, string password, AddReceptionistDto input)
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
                RoleId = 2
            };
            _dbContext.AuthUsers.Add(user);
            _dbContext.SaveChanges();

            var receptionist = new AuthReceptionist
            {
                ReceptionistId = user.UserId,
                FirstName = input.FirstName,
                LastName = input.LastName,
                CitizenIdentity = input.CitizenIdentity,
                PhoneNumber = input.PhoneNumber,
                DateOfBirth = input.DateOfBirth,
            };
            _dbContext.AuthReceptionists.Add(receptionist);
            _dbContext.SaveChanges();
            return receptionist;
        }
        public AuthReceptionist UpdateInfReceptionist(int receptionistId, AddReceptionistDto input)
        {
            var findReceptionist = _dbContext.AuthReceptionists.FirstOrDefault(r => r.ReceptionistId == receptionistId)
                ?? throw new UserExceptions("Không tồn tại receptioníst");
            if (findReceptionist != null)
            {
                findReceptionist.FirstName = input.FirstName;
                findReceptionist.LastName = input.LastName;
                findReceptionist.CitizenIdentity = input.CitizenIdentity;
                findReceptionist.PhoneNumber = input.PhoneNumber;
                findReceptionist.DateOfBirth = input.DateOfBirth;
            }
            _dbContext.AuthReceptionists.Update(findReceptionist);
            _dbContext.SaveChanges();
            return findReceptionist;
        }
        public void DeleteReceptionist(int receptionistId)
        {
            var findReceptionist = _dbContext.AuthUsers.FirstOrDefault(r => r.UserId == receptionistId)
            ?? throw new UserExceptions("Không tồn tại receptioníst");
            findReceptionist.IsDeleted = true;
            _dbContext.AuthUsers.Update(findReceptionist);
            _dbContext.SaveChanges();
        }
        public AuthReceptionist GetReceptionistById([FromQuery] int id)
        {
            var findReceptionist = _dbContext.AuthReceptionists.FirstOrDefault(r => r.ReceptionistId == id)
                ?? throw new UserExceptions("Không tồn tại receptionist");
            var checkDelete = _dbContext.AuthUsers.FirstOrDefault(u => u.UserId == id);

            if (checkDelete.IsDeleted)
            {
                throw new UserExceptions("Người dùng này đã bị xóa");
            }
            return findReceptionist;
        }
        public PageResultDto<AuthReceptionist> GetAllReceptionist([FromQuery] FilterDto input)
        {
            var result = new PageResultDto<AuthReceptionist>();
            var checkDelete = from au in _dbContext.AuthUsers
                              join ar in _dbContext.AuthReceptionists on au.UserId equals ar.ReceptionistId
                              where au.IsDeleted == false
                              select new AuthReceptionist
                              {
                                  ReceptionistId = ar.ReceptionistId,
                                  FirstName = ar.FirstName,
                                  LastName = ar.LastName,
                                  PhoneNumber = ar.PhoneNumber,
                                  CitizenIdentity = ar.CitizenIdentity,
                                  DateOfBirth = ar.DateOfBirth
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
