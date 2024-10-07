using CareerCrafterClassLib.DTO;
using CareerCrafterClassLib.Interface;
using CareerCrafterClassLib.Model;
using Microsoft.EntityFrameworkCore;
using CareerCrafterClassLib.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class EmployerRepository : IEmployerRepository
{
    private readonly AppDbContext _context;

    public EmployerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<EmployerDto>> GetAllEmployersAsync()
    {
        return await _context.Employers.Select(e => new EmployerDto
        {
            ContactPersonName = e.ContactPersonName,
            CompanyName = e.CompanyName,
            Location = e.Location,
            Email = e.Email,
            PhoneNo = e.PhoneNo
        }).ToListAsync();
    }

    public async Task<EmployerDto> GetEmployerByIdAsync(int employerId)
    {
        var employer = await _context.Employers.FindAsync(employerId);
        if (employer == null) return null;

        return new EmployerDto
        {
            ContactPersonName = employer.ContactPersonName,
            CompanyName = employer.CompanyName,
            Location = employer.Location,
            Email = employer.Email,
            PhoneNo = employer.PhoneNo
        };
    }

    public async Task<EmployerDto> AddEmployerAsync(EmployerDto employerDto)
    {
        var employer = new Employer
        {
            ContactPersonName = employerDto.ContactPersonName,
            CompanyName = employerDto.CompanyName,
            Location = employerDto.Location,
            Email = employerDto.Email,
            PhoneNo = employerDto.PhoneNo
        };

        _context.Employers.Add(employer);
        await _context.SaveChangesAsync();

        return employerDto;
    }

    public async Task<EmployerDto> UpdateEmployerAsync(int employerId, EmployerDto employerDto)
    {
        var employer = await _context.Employers.FindAsync(employerId);
        if (employer == null) return null;

        employer.ContactPersonName = employerDto.ContactPersonName;
        employer.CompanyName = employerDto.CompanyName;
        employer.Location = employerDto.Location;
        employer.Email = employerDto.Email;
        employer.PhoneNo = employerDto.PhoneNo;

        _context.Employers.Update(employer);
        await _context.SaveChangesAsync();

        return employerDto;
    }

    public async Task<bool> DeleteEmployerAsync(int employerId)
    {
        var employer = await _context.Employers.FindAsync(employerId);
        if (employer == null) return false;

        _context.Employers.Remove(employer);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<JobPostingFetchDto>> GetJobPostingsByEmployerAsync(int employerId)
    {
        return await _context.JobPostings
            .Where(j => j.EmployerId == employerId)
            .Select(j => new JobPostingFetchDto
            {
                OrganisationName = j.OrganisationName,
                Role = j.Role,
                Description = j.Description
            }).ToListAsync();
    }

    public async Task<bool> DeleteJobPostingAsync(int jobId)
    {
        var jobPosting = await _context.JobPostings.FindAsync(jobId);
        if (jobPosting == null) return false;

        _context.JobPostings.Remove(jobPosting);
        await _context.SaveChangesAsync();

        return true;
    }

    //public async Task<IEnumerable<ApplicationDto>> GetApplicationsByEmployerAsync(int employerId)
    //{
    //    return await _context.Applications
    //        .Where(a => a.EmployerId == employerId)
    //        .Select(a => new ApplicationDto
    //        {
    //            ApplicantName = a.JobSeeker.Name,
    //            ApplicationStatus = a.Status,
    //            JobRole = a.JobPosting.Role
    //        }).ToListAsync();
    //}
    public async Task<Employer> GetEmployerByEmailAndUserType(string email, string userType)
    {
        return await _context.Employers
            .FirstOrDefaultAsync(e => e.Email == email && e.UserType == userType);
    }
    public async Task<Employer> AddEmployerAsync(Employer employer)
    {
        _context.Employers.Add(employer);
        await _context.SaveChangesAsync();
        return employer;
    }
    public async Task<bool> UpdateEmployerDetailsAsync(int employerId, EmployerDto employerDto)
    {
        var employer = await _context.Employers.FindAsync(employerId);
        if (employer == null) return false;

        if (!string.IsNullOrEmpty(employerDto.ContactPersonName))
            employer.ContactPersonName = employerDto.ContactPersonName;

        if (!string.IsNullOrEmpty(employerDto.CompanyName))
            employer.CompanyName = employerDto.CompanyName;

        if (!string.IsNullOrEmpty(employerDto.Location))
            employer.Location = employerDto.Location;

        if (!string.IsNullOrEmpty(employerDto.PhoneNo))
            employer.PhoneNo = employerDto.PhoneNo;

        _context.Employers.Update(employer);
        await _context.SaveChangesAsync();
        return true;
    }
}

