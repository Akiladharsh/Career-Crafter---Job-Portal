namespace CareerCrafter.Models
{
    public class JobSeeker
    {
        public int JobSeeker_Id { get; set; }
        public string? Name { get; set; }
        public string? PhoneNo { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; } // Added Password property
        public DateOnly? DOB { get; set; }
        public string? TempAddress { get; set; }
        public string? PermanentAddress { get; set; }
        public string? Gender { get; set; }
       
        //public string Resume { get; set; }
        public int? TotalExperienceInYears { get; set; }

        public string? UserType { get; set; }

        // Navigation properties
        public ICollection<JobSeekerSkills> JobSeekerSkills { get; set; }
        public ICollection<JobSeekerCertifications> JobSeekerCertifications { get; set; }
        public ICollection<JobSeekerLanguages> JobSeekerLanguages { get; set; }
        public ICollection<JobSeekerProjects> JobSeekerProjects { get; set; }
        public ICollection<JobSeekerExperience> JobSeekerExperience { get; set; }
        public ICollection<JobSeekerQualification> JobSeekerQualification { get; set; }
    }

    public class JobSeekerSkills
    {
        public int SkillId { get; set; }
        public int JobSeeker_Id { get; set; }
        public string? Skills { get; set; }

        // Navigation property
        public JobSeeker JobSeeker { get; set; }
    }

    public class JobSeekerCertifications
    {
        public int CertificationId { get; set; }
        public int JobSeeker_Id { get; set; }
        public string? Certifications { get; set; }

        // Navigation property
        public JobSeeker JobSeeker { get; set; }
    }

    public class JobSeekerLanguages
    {
        public int LanguageId { get; set; }
        public int JobSeeker_Id { get; set; }
        public string? Languages { get; set; }

        // Navigation property
        public JobSeeker JobSeeker { get; set; }
    }

    public class JobSeekerProjects
    {
        public int ProjectId { get; set; }
        public int JobSeeker_Id { get; set; }
        public string? Projects { get; set; }

        // Navigation property
        public JobSeeker JobSeeker { get; set; }
    }

    public class JobSeekerExperience
    {
        public int PreviousCompanyId { get; set; }
        public int JobSeeker_Id { get; set; }
        public string? PreviousCompanyName { get; set; }
        public int? FromYear { get; set; }
        public int? ToYear { get; set; }

        // Navigation property
        public JobSeeker JobSeeker { get; set; }
    }

    public class JobSeekerQualification
    {
        public int QualificationId { get; set; }
        public int JobSeeker_Id { get; set; }
        public string? Qualification { get; set; }
        public string? InstituteName { get; set; }
        public int? PassedOutYear { get; set; }

        // Navigation property
        public JobSeeker JobSeeker { get; set; }
    }
}
