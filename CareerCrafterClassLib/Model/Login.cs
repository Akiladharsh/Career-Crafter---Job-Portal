using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCrafterClassLib.Model
{
    public class Login
    {
        public int LoginId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
    }
}