using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Auth.Dtos
{
    public class SendNotificationDto
    {
        public int CustomerId { get; set; }
        public string Title { get; set; }

        public string Body { get; set; }
    }
}
