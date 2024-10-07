using CareerCrafterClassLib.Data;
using CareerCrafterClassLib.Model;
using CareerCrafterClassLib.Interface;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CareerCrafterClassLib.DTO.Resume.Models;

namespace CareerCrafterClassLib.Repository
{
    public class ResumeRepository : IResumeRepository
    {
        private readonly AppDbContext _context;

        public ResumeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Resume> AddResumeAsync(Resume resume)
        {
            _context.Resumes.Add(resume);
            await _context.SaveChangesAsync();
            return resume;
        }

        public async Task<Resume> GetResumeByJobSeekerIdAsync(int jobSeekerId)
        {
            return await _context.Resumes.FirstOrDefaultAsync(r => r.JobSeeker_Id == jobSeekerId);
        }

        public async Task<Resume> UpdateResumeAsync(Resume resume)
        {
            _context.Resumes.Update(resume);
            await _context.SaveChangesAsync();
            return resume;
        }
    }
}
