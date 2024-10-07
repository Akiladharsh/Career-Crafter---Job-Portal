using CareerCrafterClassLib.Interface;
using CareerCrafterClassLib.DTO;
using CareerCrafterClassLib.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class JobPostingsController : ControllerBase
{
    private readonly IJobPostingsRepository _jobPostingsRepository;

    public JobPostingsController(IJobPostingsRepository jobPostingsRepository)
    {
        _jobPostingsRepository = jobPostingsRepository;
    }

    // Get all job postings
    [HttpGet]
    public async Task<IActionResult> GetAllJobPostings()
    {
        var jobPostings = await _jobPostingsRepository.GetAllJobPostingsAsync();
        var jobPostingDtos = jobPostings.Select(jp => new JobPostingDto
        {
            JobPostingId = jp.JobPostingId,
            EmployerId = jp.EmployerId,
            OrganisationName = jp.OrganisationName,
            Description = jp.Description,
            Role = jp.Role,
            Employer = new EmployerDto // Assuming you have this filled or leave it null if not needed
            {
                EmployerId = jp.Employer.EmployerId,
                ContactPersonName = jp.Employer.ContactPersonName // Replace with appropriate properties
            }
        }).ToList();

        return Ok(jobPostingDtos);
    }

    // Get job posting by ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetJobPostingById(int id)
    {
        var jobPosting = await _jobPostingsRepository.GetJobPostingByIdAsync(id);
        if (jobPosting == null)
        {
            return NotFound();
        }

        var jobPostingDto = new JobPostingDto
        {
            JobPostingId = jobPosting.JobPostingId,
            EmployerId = jobPosting.EmployerId,
            OrganisationName = jobPosting.OrganisationName,
            Description = jobPosting.Description,
            Role = jobPosting.Role,
            Employer = new EmployerDto // Assuming you have this filled or leave it null if not needed
            {
                EmployerId = jobPosting.Employer.EmployerId,
                ContactPersonName = jobPosting.Employer.ContactPersonName // Replace with appropriate properties
            }
        };
            
        return Ok(jobPostingDto);
    }

    // Create a new job posting
    [HttpPost]
    public async Task<IActionResult> CreateJobPosting([FromBody] JobPostingCreateDto jobPostingCreateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Extract EmployerId from JWT token
        var employerIdClaim = User.Claims.FirstOrDefault(c => c.Type == "EmployerId"); // Adjust the claim type if necessary
        if (employerIdClaim == null)
        {
            return Unauthorized("Employer not found in token");
        }

        // Parse the EmployerId to int
        if (!int.TryParse(employerIdClaim.Value, out int employerId))
        {
            return BadRequest("Invalid EmployerId from token");
        }

        var jobPosting = new JobPostings
        {
            EmployerId = employerId, // Set EmployerId from token
            OrganisationName = jobPostingCreateDto.OrganisationName,
            Description = jobPostingCreateDto.Description,
            Role = jobPostingCreateDto.Role
        };

        await _jobPostingsRepository.AddJobPostingAsync(jobPosting);

        // Return the created job posting with a location header
        var createdJobPostingDto = new JobPostingDto
        {
            JobPostingId = jobPosting.JobPostingId,
            EmployerId = jobPosting.EmployerId,
            OrganisationName = jobPosting.OrganisationName,
            Description = jobPosting.Description,
            Role = jobPosting.Role
        };

        return CreatedAtAction(nameof(GetJobPostingById), new { id = createdJobPostingDto.JobPostingId }, createdJobPostingDto);
    }

    //// Delete job posting
    //[HttpDelete("{id}")]
    //public async Task<IActionResult> DeleteJobPosting(int id)
    //{
    //    var jobPosting = await _jobPostingsRepository.GetJobPostingByIdAsync(id);
    //    if (jobPosting == null)
    //    {
    //        return NotFound();
    //    }

    //    await _jobPostingsRepository.DeleteJobPostingAsync(id);
    //    return NoContent();
    //}

    // Get recent job postings
    [HttpGet("recent")]
    public ActionResult<IEnumerable<JobPostingDto>> GetRecentJobPostings()
    {
        var jobPostings = _jobPostingsRepository.GetRecentJobPostings();

        if (!jobPostings.Any())
        {
            return NotFound(new { message = "No job postings found" });
        }

        return Ok(jobPostings);
    }

    // Get job postings by role
    [HttpGet("search")]
    public ActionResult<IEnumerable<JobPostingDto>> GetJobPostingsByRole([FromQuery] string role)
    {
        if (string.IsNullOrWhiteSpace(role))
        {
            return BadRequest(new { message = "Role must be provided" });
        }

        var jobPostings = _jobPostingsRepository.GetJobPostingsByRole(role);

        if (!jobPostings.Any())
        {
            return NotFound(new { message = $"No job postings found for role: {role}" });
        }

        return Ok(jobPostings);
    }
    // Get job postings by employer ID - JWT
    [HttpGet("employer")]
    public async Task<IActionResult> GetJobPostingsByEmployer()
    {
        // Extract EmployerId from JWT token
        var employerIdClaim = User.Claims.FirstOrDefault(c => c.Type == "EmployerId");
        if (employerIdClaim == null)
        {
            return Unauthorized("Employer not found in token");
        }

        // Parse the EmployerId to int
        if (!int.TryParse(employerIdClaim.Value, out int employerId))
        {
            return BadRequest("Invalid EmployerId from token");
        }

        var jobPostings = await _jobPostingsRepository.GetJobPostingsByEmployerIdAsync(employerId);

        if (jobPostings == null || !jobPostings.Any())
        {
            return NotFound(new { message = "No job postings found for this employer" });
        }

        var jobPostingDtos = jobPostings.Select(jp => new JobPostingDto
        {
            JobPostingId = jp.JobPostingId,
            EmployerId = jp.EmployerId,
            OrganisationName = jp.OrganisationName,
            Description = jp.Description,
            Role = jp.Role,
            Employer = new EmployerDto
            {
                EmployerId = jp.Employer.EmployerId,
                ContactPersonName = jp.Employer.ContactPersonName // Replace with appropriate properties
            }
        }).ToList();

        return Ok(jobPostingDtos);
    }

    //Update Job Posting - JWT
    [HttpPut("{jobPostingId}")]
    public async Task<IActionResult> UpdateJobPosting(int jobPostingId, [FromBody] UpdateJobPostingDto updateJobPostingDto)
    {
        // Extract EmployerId from JWT token
        var employerIdClaim = User.Claims.FirstOrDefault(c => c.Type == "EmployerId");
        if (employerIdClaim == null)
        {
            return Unauthorized("Employer not found in token");
        }

        if (!int.TryParse(employerIdClaim.Value, out int employerId))
        {
            return BadRequest("Invalid EmployerId from token");
        }

        // Get the job posting by ID
        var jobPosting = await _jobPostingsRepository.GetJobPostingByIdAsync(jobPostingId);
        if (jobPosting == null)
        {
            return NotFound(new { message = "Job posting not found" });
        }

        // Ensure the employer is authorized to update this job posting
        if (jobPosting.EmployerId != employerId)
        {
            return Forbid("You are not authorized to update this job posting");
        }

        // Update the job posting fields
        jobPosting.Description = updateJobPostingDto.Description;
        jobPosting.Role = updateJobPostingDto.Role;
        jobPosting.OrganisationName = updateJobPostingDto.OrganisationName;

        // Update in repository
        var result = await _jobPostingsRepository.UpdateJobPostingAsync(jobPosting);
        if (!result)
        {
            return StatusCode(500, "An error occurred while updating the job posting");
        }

        return Ok(new { message = "Job posting updated successfully" });
    }

    //Delete Job Posting - jwt
    [HttpDelete("{jobPostingId}")]
    public async Task<IActionResult> DeleteJobPosting(int jobPostingId)
    {
        // Extract EmployerId from JWT token
        var employerIdClaim = User.Claims.FirstOrDefault(c => c.Type == "EmployerId");
        if (employerIdClaim == null)
        {
            return Unauthorized("Employer not found in token");
        }

        if (!int.TryParse(employerIdClaim.Value, out int employerId))
        {
            return BadRequest("Invalid EmployerId from token");
        }

        // Get the job posting by ID
        var jobPosting = await _jobPostingsRepository.GetJobPostingByIdAsync(jobPostingId);
        if (jobPosting == null)
        {
            return NotFound(new { message = "Job posting not found" });
        }

        // Ensure the employer is authorized to delete this job posting
        if (jobPosting.EmployerId != employerId)
        {
            return Forbid("You are not authorized to delete this job posting");
        }

        // Delete the job posting
        var result = await _jobPostingsRepository.DeleteJobPostingAsync(jobPostingId);
        if (!result)
        {
            return StatusCode(500, "An error occurred while deleting the job posting");
        }

        return Ok(new { message = "Job posting deleted successfully" });
    }
    // Get the number of job postings by employer ID (JWT based)
    [HttpGet("employer/{employerId}/count")]
    public async Task<IActionResult> GetJobPostingsCountByEmployer(int employerId)
    {
        // Ensure the employer is authorized
        var employerIdClaim = User.Claims.FirstOrDefault(c => c.Type == "EmployerId");
        if (employerIdClaim == null || !int.TryParse(employerIdClaim.Value, out int tokenEmployerId))
        {
            return Unauthorized("Employer not found in token");
        }

        // Ensure the employer is requesting their own postings count
        if (tokenEmployerId != employerId)
        {
            return Forbid("You are not authorized to view this data.");
        }

        var count = await _jobPostingsRepository.GetJobPostingsCountByEmployerIdAsync(employerId);

        return Ok(new { employerId = employerId, jobPostingCount = count });
    }
}
