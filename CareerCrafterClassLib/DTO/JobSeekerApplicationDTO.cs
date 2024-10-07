using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCrafterClassLib.DTO
{
    public class JobSeekerApplicationDTO
    {
        public int JobSeeker_Id { get; set; }  // Add this property
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public int JobPostingId { get; set; } // New property for JobPostingId
    }
}
