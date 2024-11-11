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
    [Table(nameof(AuthRole), Schema = DbSchema.Auth)]
    public class AuthRole
    {
        [Key]
        public int RoleId { get; set; }
        public required string RoleName { get; set; }
    }
}
