using CareerCrafterClassLib.Data;
using CareerCrafterClassLib.Interface;
using CareerCrafterClassLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CareerCrafterClassLib.DTO;
using CareerCrafter.Models;


namespace CareerCrafterClassLib.Repository
{
    public class ApplicationStatusRepository : IApplicationStatusRepository
    {
        private readonly AppDbContext _context;

        public ApplicationStatusRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ApplicationStatus> GetByJobPostingIdAsync(int jobPostingId)
        {
            return await _context.ApplicationStatus
                .FirstOrDefaultAsync(a => a.JobPostingId == jobPostingId);
        }

        public async Task UpdateStatusAsync(ApplicationStatus applicationStatus, string newStatus)
        {
            applicationStatus.Status = newStatus;
            _context.ApplicationStatus.Update(applicationStatus);
            await _context.SaveChangesAsync();
        }

        //public async Task<List<JobSeekerApplicationDTO>> GetJobSeekersWithRoleAsync(int employerId)
        //{
        //    var query = from application in _context.ApplicationStatus
        //                join jobSeeker in _context.JobSeekers on application.JobSeekerId equals jobSeeker.JobSeeker_Id
        //                where application.EmployerId == employerId  // Filter by EmployerId
        //                select new JobSeekerApplicationDTO
        //                {
        //                    Name = jobSeeker.Name,
        //                    PhoneNo = jobSeeker.PhoneNo,
        //                    Email = jobSeeker.Email,
        //                    Role = application.Role
        //                };

        //    return await query.ToListAsync();
        //}
        //public async Task<List<JobSeekerApplicationDTO>> GetJobSeekersWithRoleAsync(int employerId)
        //{
        //    var query = from application in _context.ApplicationStatus
        //                join jobSeeker in _context.JobSeekers on application.JobSeekerId equals jobSeeker.JobSeeker_Id
        //                where application.EmployerId == employerId  // Filter by EmployerId
        //                select new JobSeekerApplicationDTO
        //                {
        //                    JobSeeker_Id = jobSeeker.JobSeeker_Id, // Include JobSeekerId
        //                    Name = jobSeeker.Name,
        //                    PhoneNo = jobSeeker.PhoneNo,
        //                    Email = jobSeeker.Email,
        //                    Role = application.Role
        //                };

        //    return await query.ToListAsync();
        //}

        public async Task<List<JobSeekerApplicationDTO>> GetJobSeekersWithRoleAsync(int employerId)
        {
            var query = from application in _context.ApplicationStatus
                        join jobSeeker in _context.JobSeekers on application.JobSeeker_Id equals jobSeeker.JobSeeker_Id
                        where application.EmployerId == employerId  // Filter by EmployerId
                        select new JobSeekerApplicationDTO
                        {
                            JobSeeker_Id = jobSeeker.JobSeeker_Id, // Include JobSeekerId
                            Name = jobSeeker.Name,
                            PhoneNo = jobSeeker.PhoneNo,
                            Email = jobSeeker.Email,
                            Role = application.Role,
                            JobPostingId = application.JobPostingId // Include JobPostingId from ApplicationStatus
                        };

            return await query.ToListAsync();
        }


        public async Task<ApplicationStatus> GetByDetailsAsync(int jobSeekerId, int jobPostingId, int employerId)
        {
            return await _context.ApplicationStatus
                .FirstOrDefaultAsync(a => a.JobSeeker_Id == jobSeekerId && a.JobPostingId == jobPostingId && a.EmployerId == employerId);
        }

        public async Task<int> CountApplicationsByEmployerIdAsync(int employerId)
        {
            return await _context.ApplicationStatus
                                 .Where(a => a.EmployerId == employerId)
                                 .CountAsync();
        }

    }
}
