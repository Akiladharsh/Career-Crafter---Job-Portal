using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerCrafter.Migrations
{
    /// <inheritdoc />
    public partial class AddJobSeekerModelUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "JobSeekers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserType",
                table: "JobSeekers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "JobSeekers");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "JobSeekers");
        }
    }
}
