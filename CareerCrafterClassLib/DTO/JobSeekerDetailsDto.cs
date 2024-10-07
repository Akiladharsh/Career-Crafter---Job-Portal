using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCrafterClassLib.DTO
{
    public class JobSeekerDetailsDto
    {
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public DateOnly? DOB { get; set; }
        public string TempAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string Gender { get; set; }
        public int? TotalExperienceInYears { get; set; }
        public List<string> Skills { get; set; }
        public List<string> Certifications { get; set; }
        public List<string> Languages { get; set; }
        public List<string> Projects { get; set; }
        public List<ExperienceDto> Experience { get; set; }
        public List<QualificationDto> Qualifications { get; set; }
        public List<ApplicationStatusDto> Applications { get; set; }
        public List<ResumeDto> Resumes { get; set; }
    }
}