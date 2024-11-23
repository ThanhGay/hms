using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Auth.ApplicationService.UserModule.Abstracts
{
    public interface ITokentBlacklistService
    {
        void AddToBlacklist(string token);
        bool IsTokenBlacklisted(string token);
    }
}
