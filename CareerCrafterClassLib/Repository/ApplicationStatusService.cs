using CareerCrafterClassLib.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareerCrafterClassLib.DTO;

namespace CareerCrafterClassLib.Repository
{
    public class ApplicationStatusService : IApplicationStatusService
    {
        private readonly IApplicationStatusRepository _applicationStatusRepository;

        public ApplicationStatusService(IApplicationStatusRepository applicationStatusRepository)
        {
            _applicationStatusRepository = applicationStatusRepository;
        }

        public async Task<bool> UpdateStatusByJobPostingIdAsync(int jobPostingId, string newStatus)
        {
            var applicationStatus = await _applicationStatusRepository.GetByJobPostingIdAsync(jobPostingId);

            if (applicationStatus == null)
            {
                return false; // Not found
            }

            // Update the status using the repository
            await _applicationStatusRepository.UpdateStatusAsync(applicationStatus, newStatus);
            return true;
        }
        //public async Task<List<JobSeekerApplicationDTO>> GetJobSeekersWithRoleAsync()
        //{
        //    return await _applicationStatusRepository.GetJobSeekersWithRoleAsync();
        //}

        public async Task<List<JobSeekerApplicationDTO>> GetJobSeekersWithRoleAsync(int employerId)
        {
            // Fetch applications filtered by EmployerId
            var applications = await _applicationStatusRepository.GetJobSeekersWithRoleAsync(employerId);

            return applications;
        }
        public async Task<bool> UpdateStatusAsync(int jobSeekerId, int jobPostingId, int employerId, string newStatus)
        {
            var applicationStatus = await _applicationStatusRepository.GetByDetailsAsync(jobSeekerId, jobPostingId, employerId);

            if (applicationStatus == null)
            {
                return false; // Not found
            }

            // Update the status using the repository
            await _applicationStatusRepository.UpdateStatusAsync(applicationStatus, newStatus);
            return true;
        }

        public async Task<int> CountApplicationsByEmployerIdAsync(int employerId)
        {
            return await _applicationStatusRepository.CountApplicationsByEmployerIdAsync(employerId);
        }
    }
}
