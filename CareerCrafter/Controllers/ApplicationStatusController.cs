using CareerCrafterClassLib.Data;
using CareerCrafterClassLib.DTO;
using CareerCrafterClassLib.Interface;
using CareerCrafterClassLib.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CareerCrafter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ApplicationStatusController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IApplicationStatusService _applicationStatusService;

        public ApplicationStatusController(AppDbContext context, IApplicationStatusService applicationStatusService)
        {
            _context = context;
            _applicationStatusService = applicationStatusService;
        }

        // GET: api/ApplicationStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationStatus>>> GetApplicationStatuses()
        {
            var applicationStatuses = await _context.ApplicationStatus.ToListAsync();
            return Ok(applicationStatuses);
        }

        // GET: api/ApplicationStatus/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationStatus>> GetApplicationStatus(int id)
        {
            var applicationStatus = await _context.ApplicationStatus.FindAsync(id);

            if (applicationStatus == null)
            {
                return NotFound();
            }

            return Ok(applicationStatus);
        }

        // POST: api/ApplicationStatus
        [HttpPost]
        public async Task<ActionResult<ApplicationStatus>> PostApplicationStatus(ApplicationStatus applicationStatus)
        {
            // Validate the model
            if (applicationStatus == null)
            {
                return BadRequest("Application status cannot be null.");
            }

            // Add the new application status to the context
            _context.ApplicationStatus.Add(applicationStatus);

            try
            {
                // Save changes to the database
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Handle database update exceptions (e.g., constraint violations)
                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException?.Message);
            }

            // Return the created application status with a 201 Created response
            return CreatedAtAction(nameof(GetApplicationStatus), new { id = applicationStatus.ApplicationStatusId }, applicationStatus);
        }

        // PATCH: api/ApplicationStatus/UpdateStatus
        [HttpPatch("UpdateStatus")]
        public async Task<IActionResult> UpdateStatus(int jobSeekerId, int jobPostingId, int employerId, [FromBody] string newStatus)
        {
            if (string.IsNullOrEmpty(newStatus))
            {
                return BadRequest("Status cannot be null or empty.");
            }

            var isUpdated = await _applicationStatusService.UpdateStatusAsync(jobSeekerId, jobPostingId, employerId, newStatus);

            if (!isUpdated)
            {
                return NotFound($"No application status found for JobSeekerId: {jobSeekerId}, JobPostingId: {jobPostingId}, EmployerId: {employerId}");
            }

            return Ok($"Status updated to '{newStatus}' for JobSeekerId: {jobSeekerId}, JobPostingId: {jobPostingId}, EmployerId: {employerId}");
        }


        //// GET: api/ApplicationStatus/JobSeekersWithRole
        //[HttpGet("JobSeekersWithRole")]
        //public async Task<ActionResult<List<JobSeekerApplicationDTO>>> GetJobSeekersWithRole()
        //{
        //    // Extract EmployerId from the JWT token
        //    var employerIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "EmployerId");

        //    if (employerIdClaim == null)
        //    {
        //        return Unauthorized("EmployerId is missing in the token.");
        //    }

        //    // Convert EmployerId to integer
        //    int employerId = int.Parse(employerIdClaim.Value);

        //    // Call the service and pass the EmployerId to filter the applications
        //    var jobSeekersWithRole = await _applicationStatusService.GetJobSeekersWithRoleAsync(employerId);

        //    return Ok(jobSeekersWithRole);
        //}

        [HttpGet("JobSeekersWithRole")]
        public async Task<ActionResult<List<JobSeekerApplicationDTO>>> GetJobSeekersWithRole()
        {
            // Extract EmployerId from the JWT token
            var employerIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "EmployerId");

            if (employerIdClaim == null)
            {
                return Unauthorized("EmployerId is missing in the token.");
            }

            // Convert EmployerId to integer
            int employerId = int.Parse(employerIdClaim.Value);

            // Call the service and pass the EmployerId to filter the applications
            var jobSeekersWithRole = await _applicationStatusService.GetJobSeekersWithRoleAsync(employerId);

            return Ok(jobSeekersWithRole);
        }

        [HttpGet("CountApplications/{employerId}")]
        public async Task<ActionResult<int>> GetCountOfApplications(int employerId)
        {
            var applicationCount = await _applicationStatusService.CountApplicationsByEmployerIdAsync(employerId);

            if (applicationCount == 0)
            {
                return NotFound($"No applications found for EmployerId: {employerId}");
            }

            return Ok(applicationCount);
        }

    }
}
