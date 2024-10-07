using CareerCrafterClassLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareerCrafter.DTO;
using CareerCrafterClassLib.DTO;

namespace CareerCrafterClassLib.Interface
{
    public interface IApplicationStatusRepository
    {
        Task<ApplicationStatus> GetByJobPostingIdAsync(int jobPostingId);
        Task UpdateStatusAsync(ApplicationStatus applicationStatus, string newStatus);

        Task<ApplicationStatus> GetByDetailsAsync(int jobSeekerId, int jobPostingId, int employerId); // <-- Add this method

        Task<List<JobSeekerApplicationDTO>> GetJobSeekersWithRoleAsync(int employerId);
        Task<int> CountApplicationsByEmployerIdAsync(int employerId);
    }
}
