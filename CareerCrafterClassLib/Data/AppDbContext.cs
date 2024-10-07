using CareerCrafter.Models;
using CareerCrafterClassLib.DTO.Resume.Models;
using CareerCrafterClassLib.Model;
using Microsoft.EntityFrameworkCore;

namespace CareerCrafterClassLib.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
      
        public DbSet<Employer> Employers { get; set; }
        public DbSet<JobPostings> JobPostings { get; set; }
        public DbSet<Login> Login { get; set; }
        public DbSet<JobSeeker> JobSeekers { get; set; }
        public DbSet<JobSeekerSkills> JobSeekerSkills { get; set; }
        public DbSet<JobSeekerCertifications> JobSeekerCertifications { get; set; }
        public DbSet<JobSeekerLanguages> JobSeekerLanguages { get; set; }
        public DbSet<JobSeekerProjects> JobSeekerProjects { get; set; }
        public DbSet<JobSeekerExperience> JobSeekerExperience { get; set; }
        public DbSet<JobSeekerQualification> JobSeekerQualification { get; set; }
        public DbSet<Resume> Resumes { get; set; }
        public DbSet<ApplicationStatus> ApplicationStatus { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // JobSeeker entity configuration
            modelBuilder.Entity<JobSeeker>()
                .HasKey(js => js.JobSeeker_Id);
            modelBuilder.Entity<JobSeeker>()
                .Property(js => js.JobSeeker_Id)
                .UseIdentityColumn();

            modelBuilder.Entity<JobSeeker>()
                .Property(js => js.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<JobSeeker>()
                .Property(js => js.Email)
                //.IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<JobSeeker>()
                .HasMany(js => js.JobSeekerSkills)
                .WithOne(s => s.JobSeeker)
                .HasForeignKey(s => s.JobSeeker_Id);

            modelBuilder.Entity<JobSeeker>()
                .HasMany(js => js.JobSeekerCertifications)
                .WithOne(c => c.JobSeeker)
                .HasForeignKey(c => c.JobSeeker_Id);

            modelBuilder.Entity<JobSeeker>()
                .HasMany(js => js.JobSeekerLanguages)
                .WithOne(l => l.JobSeeker)
                .HasForeignKey(l => l.JobSeeker_Id);

            modelBuilder.Entity<JobSeeker>()
                .HasMany(js => js.JobSeekerProjects)
                .WithOne(p => p.JobSeeker)
                .HasForeignKey(p => p.JobSeeker_Id);

            modelBuilder.Entity<JobSeeker>()
                .HasMany(js => js.JobSeekerExperience)
                .WithOne(e => e.JobSeeker)
                .HasForeignKey(e => e.JobSeeker_Id);

            modelBuilder.Entity<JobSeeker>()
                .HasMany(js => js.JobSeekerQualification)
                .WithOne(q => q.JobSeeker)
                .HasForeignKey(q => q.JobSeeker_Id);

            // JobSeekerSkills
            modelBuilder.Entity<JobSeekerSkills>()
                .HasKey(s => s.SkillId);
            modelBuilder.Entity<JobSeekerSkills>()
                .Property(s => s.SkillId)
                .UseIdentityColumn();

            modelBuilder.Entity<JobSeekerSkills>()
                .Property(s => s.Skills)
                .IsRequired()
                .HasMaxLength(100);

            // JobSeekerCertifications
            modelBuilder.Entity<JobSeekerCertifications>()
                .HasKey(c => c.CertificationId);
            modelBuilder.Entity<JobSeekerCertifications>()
                .Property(c => c.CertificationId)
                .UseIdentityColumn();

            modelBuilder.Entity<JobSeekerCertifications>()
                .Property(c => c.Certifications)
                .IsRequired()
                .HasMaxLength(100);

            // JobSeekerLanguages
            modelBuilder.Entity<JobSeekerLanguages>()
                .HasKey(l => l.LanguageId);
            modelBuilder.Entity<JobSeekerLanguages>()
                .Property(l => l.LanguageId)
                .UseIdentityColumn();

            modelBuilder.Entity<JobSeekerLanguages>()
                .Property(l => l.Languages)
                .IsRequired()
                .HasMaxLength(100);

            // JobSeekerProjects
            modelBuilder.Entity<JobSeekerProjects>()
                .HasKey(p => p.ProjectId);
            modelBuilder.Entity<JobSeekerProjects>()
                .Property(p => p.ProjectId)
                .UseIdentityColumn();

            modelBuilder.Entity<JobSeekerProjects>()
                .Property(p => p.Projects)
                .IsRequired()
                .HasMaxLength(100);

            // JobSeekerExperience
            modelBuilder.Entity<JobSeekerExperience>()
                .HasKey(e => e.PreviousCompanyId);
            modelBuilder.Entity<JobSeekerExperience>()
                .Property(e => e.PreviousCompanyId)
                .UseIdentityColumn();

            modelBuilder.Entity<JobSeekerExperience>()
                .Property(e => e.PreviousCompanyName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<JobSeekerExperience>()
                .Property(e => e.FromYear)
                .IsRequired();

            modelBuilder.Entity<JobSeekerExperience>()
                .Property(e => e.ToYear)
                .IsRequired();

            // JobSeekerQualification
            modelBuilder.Entity<JobSeekerQualification>()
                .HasKey(q => q.QualificationId);
            modelBuilder.Entity<JobSeekerQualification>()
                .Property(q => q.QualificationId)
                .UseIdentityColumn();

            modelBuilder.Entity<JobSeekerQualification>()
                .Property(q => q.Qualification)
                .IsRequired()
                .HasMaxLength(100);

            // Employer entity configuration
            modelBuilder.Entity<Employer>(entity =>
            {
                entity.HasKey(e => e.EmployerId);

                entity.Property(e => e.EmployerId)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ContactPersonName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Location)
                    .HasMaxLength(100);

                entity.Property(e => e.Email)
                    .HasMaxLength(100);

                entity.Property(e => e.PhoneNo)
                    .HasMaxLength(15);

                entity.HasMany(e => e.JobPostings)
                    .WithOne(jp => jp.Employer)
                    .HasForeignKey(jp => jp.EmployerId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // JobPostings entity configuration
            modelBuilder.Entity<JobPostings>(entity =>
            {
                entity.HasKey(jp => jp.JobPostingId);

                entity.Property(jp => jp.JobPostingId)
                    .ValueGeneratedOnAdd();

                entity.Property(jp => jp.OrganisationName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(jp => jp.Description)
                    .HasMaxLength(1000);

                entity.Property(jp => jp.Role)
                    .HasMaxLength(100);
            });

            // Login entity configuration
            modelBuilder.Entity<Login>(entity =>
            {
                entity.HasKey(l => l.LoginId);

                entity.Property(l => l.LoginId)
                    .ValueGeneratedOnAdd();

                entity.Property(l => l.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(l => l.Password)
                    .IsRequired();

                entity.Property(l => l.UserType)
                    .IsRequired()
                    .HasMaxLength(50); // Assuming max length for UserType is 50
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=DESKTOP-S44ARGN\\SQLEXPRESS;Initial Catalog=CareerCrafter;Integrated Security=True;TrustServerCertificate=true",
                    b => b.MigrationsAssembly("CareerCrafter"));
            }
        }
    }
}