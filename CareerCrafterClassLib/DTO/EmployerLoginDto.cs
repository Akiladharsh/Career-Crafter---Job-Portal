﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCrafterClassLib.DTO
{
    public class EmployerLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; } // Default value for employers
    }
}

