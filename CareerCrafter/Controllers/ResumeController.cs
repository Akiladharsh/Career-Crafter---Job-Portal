using CareerCrafterClassLib.DTO.Resume.Models;
using CareerCrafterClassLib.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CareerCrafterClassLib.Model;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ResumeUploader.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize      ]
    public class ResumesController : ControllerBase
    {
        private readonly IResumeRepository _resumeRepository;
        private readonly string _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");

        public ResumesController(IResumeRepository resumeRepository)
        {
            _resumeRepository = resumeRepository;

            if (!Directory.Exists(_uploadPath))
            {
                Directory.CreateDirectory(_uploadPath);
            }
        }

        [HttpPost("upload/{jobSeekerId}")]
        public async Task<IActionResult> UploadResume(int jobSeekerId, IFormFile file)
        {
            // Check if the file is null or has no content
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            // Define the file path and save the new file
            var filePath = Path.Combine(_uploadPath, file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Create a new Resume object
            var resume = new Resume
            {
                JobSeeker_Id = jobSeekerId,
                FileName = file.FileName,
                FilePath = filePath,
                UploadDate = DateTime.UtcNow
            };

            // Add the new resume to the database
            var createdResume = await _resumeRepository.AddResumeAsync(resume);

            return Ok(new { FilePath = filePath, JobSeekerId = createdResume.JobSeeker_Id });
        }
    }
}
