using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Auth.Domain
{
    public class AuthRolePermission
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public required string PermissonKey { get; set; }
    }
}
