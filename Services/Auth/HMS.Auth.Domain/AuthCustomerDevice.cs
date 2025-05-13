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
    [Table(nameof(AuthCustomerDevice), Schema = DbSchema.Auth)]

    public class AuthCustomerDevice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int CustomerId { get; set; }

        [MaxLength(255)]
        public string DeviceToken { get; set; }

    }
}
