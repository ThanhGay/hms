using HMS.Auth.ApplicationService.Common;
using HMS.Auth.Domain;
using HMS.Auth.Dtos.Receptionist;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Auth.ApplicationService.UserModule.Abstracts
{
    public interface IReceptionistService
    {
        AuthReceptionist CreateReceptionist([FromBody] AddReceptionistDto input);
        AuthReceptionist UpdateInfReceptionist(UpdateReceptionistDto input);
        void DeleteReceptionist(int receptionistId);
        AuthReceptionist GetReceptionistById([FromQuery] int id);
        PageResultDto<AuthReceptionist> GetAllReceptionist([FromQuery] FilterDto input);
    }
}
