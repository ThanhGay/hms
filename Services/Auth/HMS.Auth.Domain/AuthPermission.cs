using HMS.Shared.Constant.Database;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS.Auth.Domain
{
    [Table(nameof(AuthPermission), Schema = DbSchema.Auth)]

    public class AuthPermission
    {
        [Key]
        [MaxLength(128)]
        public required string PermissonKey { get; set; }
        [MaxLength(128)]
        public required string PermissionName { get; set; }
    }
}
