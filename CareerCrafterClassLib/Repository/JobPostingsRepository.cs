using CareerCrafterClassLib.Data;
using CareerCrafterClassLib.DTO;
using CareerCrafterClassLib.Interface;
using CareerCrafterClassLib.Model;
using Microsoft.EntityFrameworkCore;

public class JobPostingsRepository : IJobPostingsRepository
{
    private readonly AppDbContext _context;

    public JobPostingsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<JobPostings>> GetAllJobPostingsAsync()
    {
        return await _context.JobPostings.Include(jp => jp.Employer).ToListAsync();
    }

    public async Task<JobPostings> GetJobPostingByIdAsync(int id)
    {
        return await _context.JobPostings.Include(jp => jp.Employer)
                                         .FirstOrDefaultAsync(jp => jp.JobPostingId == id);
    }

    public async Task AddJobPostingAsync(JobPostings jobPosting)
    {
        await _context.JobPostings.AddAsync(jobPosting);
        await _context.SaveChangesAsync();
    }

    //public async Task UpdateJobPostingAsync(JobPostings jobPosting)
    //{
    //    _context.JobPostings.Update(jobPosting);
    //    await _context.SaveChangesAsync();
    //}

    //public async Task DeleteJobPostingAsync(int id)
    //{
    //    var jobPosting = await _context.JobPostings.FindAsync(id);
    //    if (jobPosting != null)
    //    {
    //        _context.JobPostings.Remove(jobPosting);
    //        await _context.SaveChangesAsync();
    //    }
    //}

    public IEnumerable<JobPostingFetchDto> GetRecentJobPostings()
    {
        var jobPostings = _context.JobPostings
                                  .OrderByDescending(j => j.JobPostingId)
                                  .Select(j => new JobPostingFetchDto
                                  {
                                      JobPostingId = j.JobPostingId,
                                      OrganisationName = j.OrganisationName,
                                      Description = j.Description,
                                      EmployerId = j.EmployerId,
                                      Role = j.Role
                                  })
                                  .ToList();

        return jobPostings;
    }

    public IEnumerable<JobPostingDto> GetJobPostingsByRole(string role)
    {
        var jobPostings = _context.JobPostings
                                  .Where(j => j.Role.ToLower() == role.ToLower())
                                  .Select(j => new JobPostingDto
                                  {
                                      JobPostingId = j.JobPostingId,
                                      EmployerId = j.EmployerId,
                                      OrganisationName = j.OrganisationName,
                                      Description = j.Description,
                                      Role = j.Role
                                  })
                                  .ToList();

        return jobPostings;
    }
    public async Task<IEnumerable<JobPostings>> GetJobPostingsByEmployerIdAsync(int employerId)
    {
        return await _context.JobPostings
            .Where(jp => jp.EmployerId == employerId)
            .Include(jp => jp.Employer) // Include employer details if needed
            .ToListAsync();
    }

    public async Task<bool> UpdateJobPostingAsync(JobPostings jobPosting)
    {
        _context.JobPostings.Update(jobPosting);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteJobPostingAsync(int jobPostingId)
    {
        var jobPosting = await _context.JobPostings.FindAsync(jobPostingId);
        if (jobPosting != null)
        {
            _context.JobPostings.Remove(jobPosting);
            return await _context.SaveChangesAsync() > 0;
        }
        return false;
    }

    public async Task<int> GetJobPostingsCountByEmployerIdAsync(int employerId)
    {
        return await _context.JobPostings
                             .Where(jp => jp.EmployerId == employerId)
                             .CountAsync();
    }

}
