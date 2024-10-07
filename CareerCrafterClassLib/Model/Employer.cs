using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCrafterClassLib.Model
{
    public class Employer
    {
        public int EmployerId { get; set; }
        public string? ContactPersonName { get; set; }
        public string? CompanyName { get; set; }
        public string? Location { get; set; }
        public string? Email { get; set; }
        public string? PhoneNo { get; set; }

        // Add these properties (they should map to the new columns in the database)
        public string? Password { get; set; }
        public string? UserType { get; set; }

        public ICollection<JobPostings> JobPostings { get; set; } = new List<JobPostings>();
    }
}