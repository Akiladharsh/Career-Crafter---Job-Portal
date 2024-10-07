using System.Threading.Tasks;
using CareerCrafter.Models;
using CareerCrafterClassLib.Data;
using CareerCrafterClassLib.Model;
using Microsoft.EntityFrameworkCore;

namespace CareerCrafterClassLib.Repository
{
    public class JobSeekerRepository : IJobSeekerRepository
    {
        private readonly AppDbContext _context;

        public JobSeekerRepository(AppDbContext context)
        {
            _context = context;
        }

        // Retrieve a JobSeeker by email and user type (for login verification)
        public async Task<JobSeeker> GetJobSeekerByEmailAndUserType(string email, string userType)
        {
            // Ensure you're querying the JobSeeker table
            return await _context.JobSeekers
                .FirstOrDefaultAsync(u => u.Email == email && u.UserType == userType);
        }
    }
}
