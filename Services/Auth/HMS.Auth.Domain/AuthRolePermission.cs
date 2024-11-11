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
    [Table(nameof(AuthRolePermission), Schema = DbSchema.Auth)]

    public class AuthRolePermission
    {
        [Key]
        public int Id { get; set; }
        public int RoleId { get; set; }
        [MaxLength(128)]
        public required string PermissonKey { get; set; }
    }
}
