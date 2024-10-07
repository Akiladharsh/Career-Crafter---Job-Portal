using System.Threading.Tasks;
using CareerCrafter.Models;
using CareerCrafterClassLib.Model;

namespace CareerCrafterClassLib.Repository
{
    public interface IJobSeekerRepository
    {
        Task<JobSeeker> GetJobSeekerByEmailAndUserType(string email, string userType);
    }
}
