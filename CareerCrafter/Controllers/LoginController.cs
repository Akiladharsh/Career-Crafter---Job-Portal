using CareerCrafterClassLib.Data;
using CareerCrafterClassLib.DTO;
using CareerCrafterClassLib.Interface;
using CareerCrafterClassLib.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using log4net;
using System;
using CareerCrafter.Models;

namespace CareerCrafterClassLib.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IJobSeekerService _jobSeekerService;
        private readonly IEmployerService _employerService;
        private readonly AppDbContext _context;
        private static readonly ILog Log = LogManager.GetLogger(typeof(AuthController)); // Log4net instance

        public AuthController(IEmployerService employerService, IJobSeekerService jobSeekerService, AppDbContext context)
        {
            _employerService = employerService;
            _jobSeekerService = jobSeekerService;
            _context = context;
        }

        [HttpPost("jobseeker/login")]
        public async Task<IActionResult> JobSeekerLogin([FromBody] JobSeekerLoggingDto jobSeeker)
        {
            try
            {
                if (jobSeeker == null || string.IsNullOrEmpty(jobSeeker.Email) || string.IsNullOrEmpty(jobSeeker.Password))
                {
                    Log.Warn("JobSeeker login attempt with invalid credentials.");
                    return BadRequest("Email and password must be provided.");
                }

                var loginResult = await _jobSeekerService.LoginJobSeekerByType(jobSeeker.Email, jobSeeker.Password, jobSeeker.UserType);

                if (loginResult != null)
                {
                    Log.Info($"JobSeeker {jobSeeker.Email} logged in successfully.");
                    return Ok(new
                    {
                        Token = loginResult.Token,
                        JobSeekerId = loginResult.JobSeekerId,
                        UserType = loginResult.UserType
                    });
                }

                Log.Warn($"Failed login attempt for JobSeeker: {jobSeeker.Email}.");
                return Unauthorized("Invalid email or password for JobSeeker");
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred during JobSeeker login.", ex);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("employer/login")]
        public async Task<IActionResult> EmployerLogin([FromBody] EmployerLoginDto employerLogin)
        {
            try
            {
                if (employerLogin == null || string.IsNullOrEmpty(employerLogin.Email) || string.IsNullOrEmpty(employerLogin.Password))
                {
                    Log.Warn("Employer login attempt with invalid credentials.");
                    return BadRequest("Email and password must be provided.");
                }

                var loginResult = await _employerService.LoginEmployerByType(employerLogin.Email, employerLogin.Password, employerLogin.UserType);

                if (loginResult != null)
                {
                    Log.Info($"Employer {employerLogin.Email} logged in successfully.");
                    return Ok(new
                    {
                        Token = loginResult.Token,
                        EmployerId = loginResult.EmployerId,
                        UserType = loginResult.UserType
                    });
                }

                Log.Warn($"Failed login attempt for Employer: {employerLogin.Email}.");
                return Unauthorized("Invalid credentials for Employer.");
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred during Employer login.", ex);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] JobSeekerLoggingDto dto)
        {
            try
            {
                // Validate the incoming request (e.g., check if email is already registered)
                var existingJobSeeker = await _context.JobSeekers
                    .FirstOrDefaultAsync(js => js.Email == dto.Email);

                if (existingJobSeeker != null)
                {
                    Log.Warn($"Registration attempt with existing email: {dto.Email}.");
                    return BadRequest(new { message = "Email already exists" });
                }

                // Create a new JobSeeker entity
                var jobSeeker = new JobSeeker
                {
                    Email = dto.Email,
                    Password = dto.Password, // Use password hashing in production
                    UserType = dto.UserType,
                    Name = "Name", // Default values for other properties
                    PhoneNo = "000-000-0000", // Default phone number
                    DOB = DateOnly.FromDateTime(DateTime.Now), // Default to today’s date
                    TempAddress = "Temp Address",
                    PermanentAddress = "Permanent Address",
                    Gender = "Gender",
                    TotalExperienceInYears = 0 // Default experience
                };

                // Save to the database
                _context.Add(jobSeeker);
                await _context.SaveChangesAsync();

                Log.Info($"JobSeeker {dto.Email} registered successfully.");
                return Ok(new { message = "Registration successful" });
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred during registration.", ex);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
