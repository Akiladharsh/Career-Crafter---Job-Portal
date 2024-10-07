using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareerCrafterClassLib.DTO;

namespace CareerCrafterClassLib.Interface
{
    public interface IApplicationStatusService
    {
        Task<bool> UpdateStatusByJobPostingIdAsync(int jobPostingId, string newStatus);

        Task<bool> UpdateStatusAsync(int jobSeekerId, int jobPostingId, int employerId, string newStatus); // <-- Add this method

        Task<List<JobSeekerApplicationDTO>> GetJobSeekersWithRoleAsync(int employerId);
        Task<int> CountApplicationsByEmployerIdAsync(int employerId);// This should match the method in your service
    }
}
