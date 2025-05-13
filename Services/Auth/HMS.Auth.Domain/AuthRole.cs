using HMS.Shared.Constant.Database;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
