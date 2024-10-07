using System.Threading.Tasks;

using CareerCrafterClassLib.DTO;

namespace CareerCrafterClassLib.Interface
{
    public interface IJobSeekerService
    {
        Task<JobSeekerLoginResponseDto> LoginJobSeekerByType(string email, string password, string userType);
    }

}
