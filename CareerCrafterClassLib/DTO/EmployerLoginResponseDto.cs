using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCrafterClassLib.DTO
{
    public class EmployerLoginResponseDto
    {
        public string Token { get; set; }
        public int EmployerId { get; set; }
        public string UserType { get; set; }
    }
}

