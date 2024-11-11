using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Auth.Dtos
{
    public class UserExceptions : Exception
    {
        public UserExceptions(String message) : base(message)
        {
        }

    }
}
