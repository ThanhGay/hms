using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Auth.Dtos
{
    public class checkOtpDto
    {
        public string Email { get; set; }
        public string Otp {  get; set; }
    }
}
