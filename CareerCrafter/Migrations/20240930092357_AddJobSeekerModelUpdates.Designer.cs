﻿// <auto-generated />
using System;
using CareerCrafterClassLib.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CareerCrafter.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240930092357_AddJobSeekerModelUpdates")]
    partial class AddJobSeekerModelUpdates
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CareerCrafter.Models.JobSeeker", b =>
                {
                    b.Property<int>("JobSeeker_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("JobSeeker_Id"));

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InstituteName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("PassedOutYear")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PermanentAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Resume")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TempAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TotalExperienceInYears")
                        .HasColumnType("int");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("JobSeeker_Id");

                    b.ToTable("JobSeekers");
                });

            modelBuilder.Entity("CareerCrafter.Models.JobSeekerCertifications", b =>
                {
                    b.Property<int>("CertificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CertificationId"));

                    b.Property<string>("Certifications")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("JobSeeker_Id")
                        .HasColumnType("int");

                    b.HasKey("CertificationId");

                    b.HasIndex("JobSeeker_Id");

                    b.ToTable("JobSeekerCertifications");
                });

            modelBuilder.Entity("CareerCrafter.Models.JobSeekerExperience", b =>
                {
                    b.Property<int>("PreviousCompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PreviousCompanyId"));

                    b.Property<int>("FromYear")
                        .HasColumnType("int");

                    b.Property<int>("JobSeeker_Id")
                        .HasColumnType("int");

                    b.Property<string>("PreviousCompanyName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("ToYear")
                        .HasColumnType("int");

                    b.HasKey("PreviousCompanyId");

                    b.HasIndex("JobSeeker_Id");

                    b.ToTable("JobSeekerExperience");
                });

            modelBuilder.Entity("CareerCrafter.Models.JobSeekerLanguages", b =>
                {
                    b.Property<int>("LanguageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LanguageId"));

                    b.Property<int>("JobSeeker_Id")
                        .HasColumnType("int");

                    b.Property<string>("Languages")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("LanguageId");

                    b.HasIndex("JobSeeker_Id");

                    b.ToTable("JobSeekerLanguages");
                });

            modelBuilder.Entity("CareerCrafter.Models.JobSeekerProjects", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProjectId"));

                    b.Property<int>("JobSeeker_Id")
                        .HasColumnType("int");

                    b.Property<string>("Projects")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ProjectId");

                    b.HasIndex("JobSeeker_Id");

                    b.ToTable("JobSeekerProjects");
                });

            modelBuilder.Entity("CareerCrafter.Models.JobSeekerQualification", b =>
                {
                    b.Property<int>("QualificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("QualificationId"));

                    b.Property<int>("JobSeeker_Id")
                        .HasColumnType("int");

                    b.Property<string>("Qualification")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("QualificationId");

                    b.HasIndex("JobSeeker_Id");

                    b.ToTable("JobSeekerQualification");
                });

            modelBuilder.Entity("CareerCrafter.Models.JobSeekerSkills", b =>
                {
                    b.Property<int>("SkillId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SkillId"));

                    b.Property<int>("JobSeeker_Id")
                        .HasColumnType("int");

                    b.Property<string>("Skills")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("SkillId");

                    b.HasIndex("JobSeeker_Id");

                    b.ToTable("JobSeekerSkills");
                });

            modelBuilder.Entity("CareerCrafterClassLib.DTO.Resume.Models.Resume", b =>
                {
                    b.Property<int>("ResumeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ResumeId"));

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UploadDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ResumeId");

                    b.ToTable("Resumes");
                });

            modelBuilder.Entity("CareerCrafterClassLib.Model.Employer", b =>
                {
                    b.Property<int>("EmployerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployerId"));

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ContactPersonName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PhoneNo")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("EmployerId");

                    b.ToTable("Employers");
                });

            modelBuilder.Entity("CareerCrafterClassLib.Model.JobPostings", b =>
                {
                    b.Property<int>("JobPostingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("JobPostingId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("EmployerId")
                        .HasColumnType("int");

                    b.Property<string>("OrganisationName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("JobPostingId");

                    b.HasIndex("EmployerId");

                    b.ToTable("JobPostings");
                });

            modelBuilder.Entity("CareerCrafterClassLib.Model.Login", b =>
                {
                    b.Property<int>("LoginId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LoginId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("LoginId");

                    b.ToTable("Login");
                });

            modelBuilder.Entity("CareerCrafter.Models.JobSeekerCertifications", b =>
                {
                    b.HasOne("CareerCrafter.Models.JobSeeker", "JobSeeker")
                        .WithMany("JobSeekerCertifications")
                        .HasForeignKey("JobSeeker_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("JobSeeker");
                });

            modelBuilder.Entity("CareerCrafter.Models.JobSeekerExperience", b =>
                {
                    b.HasOne("CareerCrafter.Models.JobSeeker", "JobSeeker")
                        .WithMany("JobSeekerExperience")
                        .HasForeignKey("JobSeeker_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("JobSeeker");
                });

            modelBuilder.Entity("CareerCrafter.Models.JobSeekerLanguages", b =>
                {
                    b.HasOne("CareerCrafter.Models.JobSeeker", "JobSeeker")
                        .WithMany("JobSeekerLanguages")
                        .HasForeignKey("JobSeeker_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("JobSeeker");
                });

            modelBuilder.Entity("CareerCrafter.Models.JobSeekerProjects", b =>
                {
                    b.HasOne("CareerCrafter.Models.JobSeeker", "JobSeeker")
                        .WithMany("JobSeekerProjects")
                        .HasForeignKey("JobSeeker_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("JobSeeker");
                });

            modelBuilder.Entity("CareerCrafter.Models.JobSeekerQualification", b =>
                {
                    b.HasOne("CareerCrafter.Models.JobSeeker", "JobSeeker")
                        .WithMany("JobSeekerQualification")
                        .HasForeignKey("JobSeeker_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("JobSeeker");
                });

            modelBuilder.Entity("CareerCrafter.Models.JobSeekerSkills", b =>
                {
                    b.HasOne("CareerCrafter.Models.JobSeeker", "JobSeeker")
                        .WithMany("JobSeekerSkills")
                        .HasForeignKey("JobSeeker_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("JobSeeker");
                });

            modelBuilder.Entity("CareerCrafterClassLib.Model.JobPostings", b =>
                {
                    b.HasOne("CareerCrafterClassLib.Model.Employer", "Employer")
                        .WithMany("JobPostings")
                        .HasForeignKey("EmployerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employer");
                });

            modelBuilder.Entity("CareerCrafter.Models.JobSeeker", b =>
                {
                    b.Navigation("JobSeekerCertifications");

                    b.Navigation("JobSeekerExperience");

                    b.Navigation("JobSeekerLanguages");

                    b.Navigation("JobSeekerProjects");

                    b.Navigation("JobSeekerQualification");

                    b.Navigation("JobSeekerSkills");
                });

            modelBuilder.Entity("CareerCrafterClassLib.Model.Employer", b =>
                {
                    b.Navigation("JobPostings");
                });
#pragma warning restore 612, 618
        }
    }
}
