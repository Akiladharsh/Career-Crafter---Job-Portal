using CareerCrafterClassLib.DTO;
using CareerCrafterClassLib.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CareerCrafterClassLib.Interface
{
    public interface IEmployerRepository
    {
        Task<IEnumerable<EmployerDto>> GetAllEmployersAsync();
        Task<EmployerDto> GetEmployerByIdAsync(int employerId);
        Task<EmployerDto> AddEmployerAsync(EmployerDto employerDto);
        Task<EmployerDto> UpdateEmployerAsync(int employerId, EmployerDto employerDto);
        Task<bool> DeleteEmployerAsync(int employerId);

        // Additional APIs
        Task<IEnumerable<JobPostingFetchDto>> GetJobPostingsByEmployerAsync(int employerId);
        Task<bool> DeleteJobPostingAsync(int jobId);
        //Task<IEnumerable<ApplicationDto>> GetApplicationsByEmployerAsync(int employerId);

        Task<Employer> GetEmployerByEmailAndUserType(string email, string userType);

        Task<Employer> AddEmployerAsync(Employer employer);
        Task<bool> UpdateEmployerDetailsAsync(int employerId, EmployerDto employerDto);
    }
}
