using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Auth.Domain
{
    public class AuthCustomer
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [Length(10, 11), Phone]
        public string? PhoneNumber { get; set; }
    }
}
