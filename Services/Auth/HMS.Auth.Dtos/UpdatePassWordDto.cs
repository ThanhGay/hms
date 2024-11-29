using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Auth.Dtos
{
    public class UpdatePassWordDto
    {
        public string Email { get; set; }
        public string Otp { get; set; }
        public string Password { get; set; }
    }
}
