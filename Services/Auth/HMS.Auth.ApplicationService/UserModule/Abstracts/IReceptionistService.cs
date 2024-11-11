using HMS.Auth.Domain;
using HMS.Auth.Dtos.Receptionist;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Auth.ApplicationService.UserModule.Abstracts
{
    public interface IReceptionistService
    {
        AuthReceptionist CreateReceptionist([FromQuery] string email, string password, AddReceptionistDto input);
        AuthReceptionist UpdateInfReceptionist(int receptionistId, AddReceptionistDto input);
        void DeleteReceptionist(int receptionistId);
    }
}
