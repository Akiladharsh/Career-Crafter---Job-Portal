using CareerCrafterClassLib.Data;
using CareerCrafterClassLib.DTO;
using CareerCrafterClassLib.Interface;
using CareerCrafterClassLib.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]

[ApiController]
public class EmployerController : ControllerBase
{
    private readonly IEmployerRepository _employerRepository;
    private readonly IEmployerService _employerService;
    private readonly AppDbContext _context;
    public EmployerController(IEmployerRepository employerRepository, IEmployerService employerService)
    {
        _employerRepository = employerRepository;
        _employerService = employerService;
    }

    // Get all employers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployerDto>>> GetAllEmployers()
    {
        var employers = await _employerRepository.GetAllEmployersAsync();
        return Ok(employers);
    }

    // Get employer by ID
    [HttpGet("{id}")]
    public async Task<ActionResult<EmployerDto>> GetEmployerById(int id)
    {
        var employer = await _employerRepository.GetEmployerByIdAsync(id);
        if (employer == null) return NotFound();
        return Ok(employer);
    }

    // Add new employer
    [HttpPost]
    public async Task<ActionResult<EmployerDto>> AddEmployer([FromBody] EmployerDto employerDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var createdEmployer = await _employerRepository.AddEmployerAsync(employerDto);
        return CreatedAtAction(nameof(GetEmployerById), new { id = createdEmployer }, createdEmployer);
    }

    // Update employer by ID
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployer(int id, [FromBody] EmployerDto employerDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var updatedEmployer = await _employerRepository.UpdateEmployerAsync(id, employerDto);
        if (updatedEmployer == null) return NotFound();

        return Ok(updatedEmployer);
    }

    // Delete employer by ID
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployer(int id)
    {
        var result = await _employerRepository.DeleteEmployerAsync(id);
        if (!result) return NotFound();

        return NoContent();
    }

    // Get job postings for an employer
    [HttpGet("jobpostings/employer/{employerId}")]
    public async Task<ActionResult<IEnumerable<JobPostingFetchDto>>> GetPostedJobs(int employerId)
    {
        var jobPostings = await _employerRepository.GetJobPostingsByEmployerAsync(employerId);
        if (jobPostings == null) return NotFound();
        return Ok(jobPostings);
    }



    // Delete job posting by ID
    [HttpDelete("jobpostings/{jobId}")]
    public async Task<IActionResult> DeleteJobPosting(int jobId)
    {
        var result = await _employerRepository.DeleteJobPostingAsync(jobId);
        if (!result) return NotFound();

        return NoContent();
    }

    //// Get applications by employer
    //[HttpGet("applications/employer/{employerId}")]
    //public async Task<ActionResult<IEnumerable<ApplicationDto>>> GetApplicationsForEmployer(int employerId)
    //{
    //    var applications = await _employerRepository.GetApplicationsByEmployerAsync(employerId);
    //    if (applications == null) return NotFound();
    //    return Ok(applications);
    //}



    [HttpPost("register")]
    public async Task<IActionResult> RegisterEmployer([FromBody] EmployerDto employerDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        employerDto.ContactPersonName ??= "Default Contact Name";  // Use null-coalescing assignment
        employerDto.CompanyName ??= "Default Company Name";        // Use null-coalescing assignment
        employerDto.Location ??= "Default Location";                // Use null-coalescing assignment
        employerDto.PhoneNo ??= "000-000-0000";

        var createdEmployer = await _employerService.RegisterEmployerAsync(employerDto);
        return CreatedAtAction(nameof(RegisterEmployer), new { id = createdEmployer.EmployerId }, createdEmployer);
    }

    [Authorize]

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateEmployerDetails(int id, [FromBody] EmployerDto employerDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _employerService.UpdateEmployerDetailsAsync(id, employerDto);
        if (!result) return NotFound();

        return NoContent();
    }
    
}
