using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCrafterClassLib.Model
{
    public class JobPostings
    {
        public int JobPostingId { get; set; }
        public int EmployerId { get; set; }
        public string OrganisationName { get; set; }
        public string Description { get; set; }
        public string Role { get; set; }

        public Employer Employer { get; set; } // Navigation Property
    }
}
