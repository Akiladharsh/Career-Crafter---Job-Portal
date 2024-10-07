using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerCrafter.Migrations
{
    /// <inheritdoc />
    public partial class JobSeekerChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstituteName",
                table: "JobSeekers");

            migrationBuilder.DropColumn(
                name: "PassedOutYear",
                table: "JobSeekers");

            migrationBuilder.DropColumn(
                name: "Resume",
                table: "JobSeekers");

            migrationBuilder.AddColumn<string>(
                name: "InstituteName",
                table: "JobSeekerQualification",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PassedOutYear",
                table: "JobSeekerQualification",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstituteName",
                table: "JobSeekerQualification");

            migrationBuilder.DropColumn(
                name: "PassedOutYear",
                table: "JobSeekerQualification");

            migrationBuilder.AddColumn<string>(
                name: "InstituteName",
                table: "JobSeekers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PassedOutYear",
                table: "JobSeekers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Resume",
                table: "JobSeekers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
