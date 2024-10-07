using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerCrafter.Migrations
{
    /// <inheritdoc />
    public partial class Loginmigrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Login");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Login",
                newName: "Email");

            migrationBuilder.AddColumn<string>(
                name: "UserType",
                table: "Login",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserType",
                table: "Login");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Login",
                newName: "Username");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Login",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");
        }
    }
}
