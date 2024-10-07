namespace CareerCrafter.DTO
{
    public class JobSeekerDto
    {
        public string? Name { get; set; }
        public string? PhoneNo { get; set; }
        public string? Email { get; set; }
        public DateOnly? DOB { get; set; }
        public string? TempAddress { get; set; }
        public string? PermanentAddress { get; set; }
        public string? Gender { get; set; }
        
       
        public int? TotalExperienceInYears { get; set; }

       
        public List<JobSeekerSkillsDto> JobSeekerSkills { get; set; }
        public List<JobSeekerCertificationsDto> JobSeekerCertifications { get; set; }
        public List<JobSeekerLanguagesDto> JobSeekerLanguages { get; set; }
        public List<JobSeekerProjectsDto> JobSeekerProjects { get; set; }
        public List<JobSeekerExperienceDto> JobSeekerExperience { get; set; }
        public List<JobSeekerQualificationDto> JobSeekerQualification { get; set; }
    }
}

