using HMS.Auth.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Auth.ApplicationService.UserModule.Abstracts
{
    public interface IUserService
    {
        List<string> GetFunctionCustomer();
        List<string> GetFunctionManager();
        List<string> GetFunctionReceptionist();
        ResultLogin Login([FromQuery] LoginDto input);
    }
}
