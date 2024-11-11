using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Auth.Dtos
{
    public class ResultLogin
    {
        public UserDto? User { get; set; }
        public string? Token { get; set; }
    }
}
