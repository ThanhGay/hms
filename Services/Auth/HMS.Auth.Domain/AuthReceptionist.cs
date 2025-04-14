using HMS.Shared.Constant.Database;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS.Auth.Domain
{
    [Table(nameof(AuthReceptionist), Schema = DbSchema.Auth)]

    public class AuthReceptionist
    {
        [Key]
        public int ReceptionistId { get; set; }
        //public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [Length(10, 11), Phone]
        public string? PhoneNumber { get; set; }
        public string? CitizenIdentity { get; set; }
        public DateTime? DateOfBirth { get; set; }


    }
}
