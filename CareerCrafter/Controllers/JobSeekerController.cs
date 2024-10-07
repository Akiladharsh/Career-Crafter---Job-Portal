using CareerCrafter.Models;
using CareerCrafterClassLib.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CareerCrafter.DTO;
using CareerCrafterClassLib.Exceptions;
using CareerCrafterClassLib.Interface;
using Microsoft.EntityFrameworkCore;
using CareerCrafterClassLib.DTO;
using CareerCrafterClassLib.Model;


namespace CareerCrafter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   //[Authorize]
    public class JobSeekerController : ControllerBase
    {
        private readonly AppDbContext _context;
        public JobSeekerController(AppDbContext context)
        {
            _context = context;
        }

        // GET api/jobseeker
        [HttpGet]
        public IEnumerable<JobSeeker> Get()
        {
            return _context.JobSeekers.ToList();
        }

        // GET api/jobseeker/2
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var specificJobSeeker = _context.JobSeekers
                .Include(js => js.JobSeekerSkills)
                .Include(js => js.JobSeekerCertifications)
                .Include(js => js.JobSeekerLanguages)
                .Include(js => js.JobSeekerProjects)
                .Include(js => js.JobSeekerExperience)
                .Include(js => js.JobSeekerQualification)
                .SingleOrDefault(js => js.JobSeeker_Id == id);

            if (specificJobSeeker != null)
            {
                var jobSeekerDto = new JobSeekerDto
                {
                    Name = specificJobSeeker.Name,
                    PhoneNo = specificJobSeeker.PhoneNo,
                    Email = specificJobSeeker.Email,
                    DOB = specificJobSeeker.DOB,
                    TempAddress = specificJobSeeker.TempAddress,
                    PermanentAddress = specificJobSeeker.PermanentAddress,
                    Gender = specificJobSeeker.Gender,
                    TotalExperienceInYears = specificJobSeeker.TotalExperienceInYears,
                    JobSeekerSkills = specificJobSeeker.JobSeekerSkills.Select(js => new JobSeekerSkillsDto
                    {
                        Skills = js.Skills// Map properties from JobSeekerSkills entity to JobSeekerSkillsDto
                    }).ToList(),
                    JobSeekerCertifications = specificJobSeeker.JobSeekerCertifications.Select(js => new JobSeekerCertificationsDto
                    {
                        Certifications = js.Certifications// Map properties from JobSeekerCertifications entity to JobSeekerCertificationsDto
                    }).ToList(),
                    JobSeekerLanguages = specificJobSeeker.JobSeekerLanguages.Select(js => new JobSeekerLanguagesDto
                    {
                        Languages = js.Languages// Map properties from JobSeekerLanguages entity to JobSeekerLanguagesDto
                    }).ToList(),
                    JobSeekerProjects = specificJobSeeker.JobSeekerProjects.Select(js => new JobSeekerProjectsDto
                    {
                        Projects = js.Projects// Map properties from JobSeekerProjects entity to JobSeekerProjectsDto
                    }).ToList(),
                    JobSeekerExperience = specificJobSeeker.JobSeekerExperience.Select(js => new JobSeekerExperienceDto
                    {
                        PreviousCompanyName = js.PreviousCompanyName,
                        FromYear = (int)js.FromYear,
                        ToYear = (int)js.ToYear// Map properties from JobSeekerExperience entity to JobSeekerExperienceDto
                    }).ToList(),
                    JobSeekerQualification = specificJobSeeker.JobSeekerQualification.Select(js => new JobSeekerQualificationDto
                    {
                        Qualification = js.Qualification,
                        InstituteName = js.InstituteName,
                        PassedOutYear = (int)js.PassedOutYear// Map properties from JobSeekerQualification entity to JobSeekerQualificationDto
                    }).ToList()
                };

                return Ok(jobSeekerDto);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobSeeker(int id, [FromBody] JobSeekerDto jobSeekerDto)
        {
            if (jobSeekerDto == null)
            {
                return BadRequest("Invalid job seeker data.");
            }

            var existingJobSeeker = await _context.JobSeekers
                .Include(js => js.JobSeekerSkills)
                .Include(js => js.JobSeekerCertifications)
                .Include(js => js.JobSeekerLanguages)
                .Include(js => js.JobSeekerProjects)
                .Include(js => js.JobSeekerExperience)
                .Include(js => js.JobSeekerQualification)
                .FirstOrDefaultAsync(js => js.JobSeeker_Id == id);

            if (existingJobSeeker == null)
            {
                return NotFound("Job seeker not found.");
            }

            // Update fields
            existingJobSeeker.Name = jobSeekerDto.Name;
            existingJobSeeker.PhoneNo = jobSeekerDto.PhoneNo;
            existingJobSeeker.Email = jobSeekerDto.Email;
            existingJobSeeker.DOB = jobSeekerDto.DOB; // Direct assignment
            existingJobSeeker.TempAddress = jobSeekerDto.TempAddress;
            existingJobSeeker.PermanentAddress = jobSeekerDto.PermanentAddress;
            existingJobSeeker.Gender = jobSeekerDto.Gender;
            existingJobSeeker.TotalExperienceInYears = jobSeekerDto.TotalExperienceInYears;

            // Update collections directly
            existingJobSeeker.JobSeekerSkills.Clear();
            foreach (var skillDto in jobSeekerDto.JobSeekerSkills)
            {
                existingJobSeeker.JobSeekerSkills.Add(new JobSeekerSkills
                {
                    Skills = skillDto.Skills
                });
            }

            existingJobSeeker.JobSeekerCertifications.Clear();
            foreach (var certDto in jobSeekerDto.JobSeekerCertifications)
            {
                existingJobSeeker.JobSeekerCertifications.Add(new JobSeekerCertifications
                {
                    Certifications = certDto.Certifications
                });
            }

            existingJobSeeker.JobSeekerLanguages.Clear();
            foreach (var langDto in jobSeekerDto.JobSeekerLanguages)
            {
                existingJobSeeker.JobSeekerLanguages.Add(new JobSeekerLanguages
                {
                    Languages = langDto.Languages
                });
            }

            existingJobSeeker.JobSeekerProjects.Clear();
            foreach (var projDto in jobSeekerDto.JobSeekerProjects)
            {
                existingJobSeeker.JobSeekerProjects.Add(new JobSeekerProjects
                {
                    Projects = projDto.Projects
                });
            }

            existingJobSeeker.JobSeekerExperience.Clear();
            foreach (var expDto in jobSeekerDto.JobSeekerExperience)
            {
                existingJobSeeker.JobSeekerExperience.Add(new JobSeekerExperience
                {
                    PreviousCompanyName = expDto.PreviousCompanyName,
                    FromYear = expDto.FromYear,
                    ToYear = expDto.ToYear
                });
            }

            existingJobSeeker.JobSeekerQualification.Clear();
            foreach (var qualDto in jobSeekerDto.JobSeekerQualification)
            {
                existingJobSeeker.JobSeekerQualification.Add(new JobSeekerQualification
                {
                    Qualification = qualDto.Qualification,
                    InstituteName = qualDto.InstituteName,
                    PassedOutYear = qualDto.PassedOutYear
                });
            }

            // Save changes to the database
            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "Job seeker updated successfully." });
            }
            catch (DbUpdateException ex)
            {
                return BadRequest($"Database update error: {ex.InnerException?.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
           // return Ok(new { message = "Job seeker updated successfully." });

        }
       [HttpPost]
            public async Task<IActionResult> AddApplicationStatus([FromBody] ApplicationStatus applicationStatus)
            {
                // Validate the input
                if (applicationStatus == null || applicationStatus.JobSeeker_Id == 0 || applicationStatus.JobPostingId == 0 || applicationStatus.EmployerId == 0)
                {
                    return BadRequest("Invalid application status data.");
                }

                // Check if the job seeker has already applied for this job
                var existingApplication = await _context.ApplicationStatus
                    .FirstOrDefaultAsync(app => app.JobSeeker_Id == applicationStatus.JobSeeker_Id
                                                && app.JobPostingId == applicationStatus.JobPostingId);

                if (existingApplication != null)
                {
                    // If an application already exists, return a conflict response
                    return Conflict(new { message = "You have already applied for this job." });
                }

                // Set default status if not provided
                applicationStatus.Status = applicationStatus.Status ?? "Pending";

                // Add the application status to the database
                await _context.ApplicationStatus.AddAsync(applicationStatus);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Application status added successfully" });
            }

        [HttpDelete("{id}/jobSeeker/{jobSeekerId}")]
        public async Task<IActionResult> DeleteApplicationStatus(int id, int jobSeekerId)
        {
            // Find the application status by ID and JobSeekerId
            var applicationStatus = await _context.ApplicationStatus
                .FirstOrDefaultAsync(a => a.ApplicationStatusId == id && a.JobSeeker_Id == jobSeekerId);

            if (applicationStatus == null)
            {
                // Return 404 Not Found if the application doesn't exist or doesn't belong to the specified job seeker
                return NotFound(new { message = "Application not found or does not belong to the specified job seeker." });
            }

            // Remove the application from the database
            _context.ApplicationStatus.Remove(applicationStatus);
            await _context.SaveChangesAsync(); // Save the changes

            return Ok(new { message = "Application deleted successfully." });
        }

        [HttpGet("applications")]
        public IActionResult GetAllApplications([FromQuery] int jobSeekerId)
        {
            try
            {
                // Fetch all application statuses from the database
                var applications = _context.ApplicationStatus
                    //.Include(a => a.JobPostings)  // Include related data if necessary (e.g., Job entity)
                    .Where(a => a.JobSeeker_Id == jobSeekerId)
                    .ToList();

                // Check if the result is null or empty
                if (applications == null || applications.Count == 0)
                {
                    return NotFound(new { message = "No applications found" });
                }

                // Return the list of applications with a 200 OK response
                return Ok(applications);
            }
            catch (Exception ex)
            {
                // Log the exception (optional) and return a 500 error with a custom message
                return StatusCode(500, new { message = "An error occurred while retrieving the applications", error = ex.Message });
            }
        }
        // DELETE api/jobseeker/3
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var jobSeekerToBeDeleted = _context.JobSeekers.SingleOrDefault(js => js.JobSeeker_Id == id);
            if (jobSeekerToBeDeleted == null)
            {
                return NotFound();
            }

            _context.JobSeekers.Remove(jobSeekerToBeDeleted);
            _context.SaveChanges();

            return Ok();
        }
        [HttpGet("GetEmployerById/{jobSeekerId}")]
        public async Task<ActionResult<JobSeekerDetailsDto>> GetJobSeekerDetails(int jobSeekerId)
        {
            var jobSeeker = await _context.JobSeekers
                .Include(js => js.JobSeekerSkills)
                .Include(js => js.JobSeekerCertifications)
                .Include(js => js.JobSeekerLanguages)
                .Include(js => js.JobSeekerProjects)
                .Include(js => js.JobSeekerExperience)
                .Include(js => js.JobSeekerQualification)
                .FirstOrDefaultAsync(js => js.JobSeeker_Id == jobSeekerId);

            if (jobSeeker == null)
            {
                return NotFound();
            }

            var jobSeekerDetails = new JobSeekerDetailsDto
            {
                Name = jobSeeker.Name,
                PhoneNo = jobSeeker.PhoneNo,
                Email = jobSeeker.Email,
                DOB = jobSeeker.DOB,
                TempAddress = jobSeeker.TempAddress,
                PermanentAddress = jobSeeker.PermanentAddress,
                Gender = jobSeeker.Gender,
                TotalExperienceInYears = jobSeeker.TotalExperienceInYears,
                Skills = jobSeeker.JobSeekerSkills.Select(skill => skill.Skills).ToList(),
                Certifications = jobSeeker.JobSeekerCertifications.Select(cert => cert.Certifications).ToList(),
                Languages = jobSeeker.JobSeekerLanguages.Select(lang => lang.Languages).ToList(),
                Projects = jobSeeker.JobSeekerProjects.Select(project => project.Projects).ToList(),
                Experience = jobSeeker.JobSeekerExperience.Select(exp => new ExperienceDto
                {
                    PreviousCompanyName = exp.PreviousCompanyName,
                    FromYear = exp.FromYear,
                    ToYear = exp.ToYear
                }).ToList(),
                Qualifications = jobSeeker.JobSeekerQualification.Select(qual => new QualificationDto
                {
                    Qualification = qual.Qualification,
                    InstituteName = qual.InstituteName,
                    PassedOutYear = qual.PassedOutYear
                }).ToList(),
                //Applications = await _context.ApplicationStatus
                //    .Where(app => app.JobSeekerId == jobSeekerId)
                //    .Select(app => new ApplicationStatusDto
                //    {
                //        CompanyName = app.CompanyName,
                //        Role = app.Role,
                //        Description = app.Description,
                //        Status = app.Status
                //    }).ToListAsync(),
                Resumes = await _context.Resumes
                    .Where(res => res.JobSeeker_Id == jobSeekerId)
                    .Select(res => new ResumeDto
                    {
                        FilePath = res.FilePath
                    }).ToListAsync()
            };

            return Ok(jobSeekerDetails);
        }


    }
}
        


    

