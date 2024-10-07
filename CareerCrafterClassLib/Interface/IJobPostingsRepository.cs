using CareerCrafterClassLib.DTO;
using CareerCrafterClassLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCrafterClassLib.Interface
{
    public interface IJobPostingsRepository
    {
        Task<IEnumerable<JobPostings>> GetAllJobPostingsAsync();
        Task<JobPostings> GetJobPostingByIdAsync(int id);
        Task AddJobPostingAsync(JobPostings jobPosting);
        //Task UpdateJobPostingAsync(JobPostings jobPosting);
        //Task DeleteJobPostingAsync(int id);

        IEnumerable<JobPostingFetchDto> GetRecentJobPostings();

        IEnumerable<JobPostingDto> GetJobPostingsByRole(string role);

        Task<IEnumerable<JobPostings>> GetJobPostingsByEmployerIdAsync(int employerId);

        Task<bool> UpdateJobPostingAsync(JobPostings jobPosting);

        Task<bool> DeleteJobPostingAsync(int jobPostingId);

        Task<int> GetJobPostingsCountByEmployerIdAsync(int employerId);
    }
}
