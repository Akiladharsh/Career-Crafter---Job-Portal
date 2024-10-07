using CareerCrafterClassLib.DTO.Resume.Models;
using System.Threading.Tasks;

namespace CareerCrafterClassLib.Interface
{
    public interface IResumeRepository
    {
        Task<Resume> AddResumeAsync(Resume resume);
        Task<Resume> GetResumeByJobSeekerIdAsync(int jobSeekerId);
        Task<Resume> UpdateResumeAsync(Resume resume);
    }
}
