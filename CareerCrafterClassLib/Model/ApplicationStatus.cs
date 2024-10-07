using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCrafterClassLib.Model
{
    public class ApplicationStatus
    {
        public int ApplicationStatusId { get; set; } // Primary Key
        public int JobSeeker_Id { get; set; } // Foreign Key to JobSeeker
        public int JobPostingId { get; set; } // Foreign Key to Job
        public int EmployerId { get; set; } // Foreign Key to Employer
        public string? CompanyName { get; set; }
        public string? Role { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; } = "Pending"; // E.g., "Pending", "Accepted", "Rejected", etc.

        //public JobPostings jobPostings { get; set; }
        public string? UploadedResume { get; set; }
    }

}