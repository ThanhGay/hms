﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Auth.Dtos
{
    public class UserDto
    {
        public int? UserId { get; set; }
        //public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? CitizenIdentity { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
