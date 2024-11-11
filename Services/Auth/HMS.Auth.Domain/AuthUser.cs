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
    [Table(nameof(AuthUser), Schema =DbSchema.Auth)]
    public class AuthUser
    {
        [Key]
        public int UserId { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
        public required string Password { get; set; }
        public int RoleId { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
