using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerCrafter.Migrations
{
    /// <inheritdoc />
    public partial class SecondCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobSeekers",
                columns: table => new
                {
                    JobSeeker_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TempAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PermanentAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstituteName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassedOutYear = table.Column<int>(type: "int", nullable: false),
                    Resume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalExperienceInYears = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSeekers", x => x.JobSeeker_Id);
                });

            migrationBuilder.CreateTable(
                name: "JobSeekerCertifications",
                columns: table => new
                {
                    CertificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobSeeker_Id = table.Column<int>(type: "int", nullable: false),
                    Certifications = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSeekerCertifications", x => x.CertificationId);
                    table.ForeignKey(
                        name: "FK_JobSeekerCertifications_JobSeekers_JobSeeker_Id",
                        column: x => x.JobSeeker_Id,
                        principalTable: "JobSeekers",
                        principalColumn: "JobSeeker_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobSeekerExperience",
                columns: table => new
                {
                    PreviousCompanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobSeeker_Id = table.Column<int>(type: "int", nullable: false),
                    PreviousCompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FromYear = table.Column<int>(type: "int", nullable: false),
                    ToYear = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSeekerExperience", x => x.PreviousCompanyId);
                    table.ForeignKey(
                        name: "FK_JobSeekerExperience_JobSeekers_JobSeeker_Id",
                        column: x => x.JobSeeker_Id,
                        principalTable: "JobSeekers",
                        principalColumn: "JobSeeker_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobSeekerLanguages",
                columns: table => new
                {
                    LanguageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobSeeker_Id = table.Column<int>(type: "int", nullable: false),
                    Languages = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSeekerLanguages", x => x.LanguageId);
                    table.ForeignKey(
                        name: "FK_JobSeekerLanguages_JobSeekers_JobSeeker_Id",
                        column: x => x.JobSeeker_Id,
                        principalTable: "JobSeekers",
                        principalColumn: "JobSeeker_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobSeekerProjects",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobSeeker_Id = table.Column<int>(type: "int", nullable: false),
                    Projects = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSeekerProjects", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_JobSeekerProjects_JobSeekers_JobSeeker_Id",
                        column: x => x.JobSeeker_Id,
                        principalTable: "JobSeekers",
                        principalColumn: "JobSeeker_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobSeekerQualification",
                columns: table => new
                {
                    QualificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobSeeker_Id = table.Column<int>(type: "int", nullable: false),
                    Qualification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSeekerQualification", x => x.QualificationId);
                    table.ForeignKey(
                        name: "FK_JobSeekerQualification_JobSeekers_JobSeeker_Id",
                        column: x => x.JobSeeker_Id,
                        principalTable: "JobSeekers",
                        principalColumn: "JobSeeker_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobSeekerSkills",
                columns: table => new
                {
                    SkillId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobSeeker_Id = table.Column<int>(type: "int", nullable: false),
                    Skills = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSeekerSkills", x => x.SkillId);
                    table.ForeignKey(
                        name: "FK_JobSeekerSkills_JobSeekers_JobSeeker_Id",
                        column: x => x.JobSeeker_Id,
                        principalTable: "JobSeekers",
                        principalColumn: "JobSeeker_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekerCertifications_JobSeeker_Id",
                table: "JobSeekerCertifications",
                column: "JobSeeker_Id");

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekerExperience_JobSeeker_Id",
                table: "JobSeekerExperience",
                column: "JobSeeker_Id");

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekerLanguages_JobSeeker_Id",
                table: "JobSeekerLanguages",
                column: "JobSeeker_Id");

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekerProjects_JobSeeker_Id",
                table: "JobSeekerProjects",
                column: "JobSeeker_Id");

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekerQualification_JobSeeker_Id",
                table: "JobSeekerQualification",
                column: "JobSeeker_Id");

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekerSkills_JobSeeker_Id",
                table: "JobSeekerSkills",
                column: "JobSeeker_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobSeekerCertifications");

            migrationBuilder.DropTable(
                name: "JobSeekerExperience");

            migrationBuilder.DropTable(
                name: "JobSeekerLanguages");

            migrationBuilder.DropTable(
                name: "JobSeekerProjects");

            migrationBuilder.DropTable(
                name: "JobSeekerQualification");

            migrationBuilder.DropTable(
                name: "JobSeekerSkills");

            migrationBuilder.DropTable(
                name: "JobSeekers");
        }
    }
}
