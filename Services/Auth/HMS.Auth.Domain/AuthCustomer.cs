using HMS.Shared.Constant.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Auth.Domain
{
    [Table(nameof(AuthCustomer), Schema = DbSchema.Auth)]

    public class AuthCustomer
    {
        [Key]
        public int CustomerId { get; set; }
        //public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [Length(10, 11), Phone]
        public string? PhoneNumber { get; set; }
        public string? CitizenIdentity { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
