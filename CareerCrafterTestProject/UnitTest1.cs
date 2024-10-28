using CareerCrafterClassLib.DTO;
using CareerCrafterClassLib.Interface;
using CareerCrafter.Controllers;
using Moq;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CareerCrafter.Models;
using CareerCrafterClassLib.Model;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CareerCrafterTests
{
    [TestFixture]
    public class EmployerControllerTests
    {
        private Mock<IEmployerRepository> _employerRepositoryMock;
        private Mock<IEmployerService> _employerServiceMock;
        private EmployerController _controller;

        [SetUp]
        public void Setup()
        {
            _employerRepositoryMock = new Mock<IEmployerRepository>();
            _employerServiceMock = new Mock<IEmployerService>();
            _controller = new EmployerController(_employerRepositoryMock.Object, _employerServiceMock.Object);
        }

        [Test]
        public async Task GetEmployerById_EmployerExists_ReturnsOkResultWithEmployer()
        {
            // Arrange
            int employerId = 1;
            var employer = new EmployerDto { EmployerId = employerId, ContactPersonName = "Test Employer" };
            _employerRepositoryMock.Setup(repo => repo.GetEmployerByIdAsync(employerId))
                .ReturnsAsync(employer);

            // Act
            var result = await _controller.GetEmployerById(employerId);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            Assert.That(okResult.Value, Is.EqualTo(employer));
        }

        [Test]
        public async Task GetEmployerById_EmployerDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            int employerId = 1;
            _employerRepositoryMock.Setup(repo => repo.GetEmployerByIdAsync(employerId))
                .ReturnsAsync((EmployerDto)null);

            // Act
            var result = await _controller.GetEmployerById(employerId);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
        }



        [Test]
        public async Task AddEmployer_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var employerDto = new EmployerDto { EmployerId = 1, ContactPersonName = "Invalid Employer" };
            _controller.ModelState.AddModelError("Name", "Required");

            // Act
            var result = await _controller.AddEmployer(employerDto);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = result.Result as BadRequestObjectResult;

            Assert.That(badRequestResult.StatusCode, Is.EqualTo(400)); // BadRequest status

            // Extract SerializableError
            var serializableError = badRequestResult.Value as SerializableError;
            Assert.That(serializableError, Is.Not.Null); // Ensure serializableError is not null

            // Check that the error contains the expected key and message
            Assert.That(serializableError.ContainsKey("Name"));
            Assert.That(serializableError["Name"], Is.EqualTo(new[] { "Required" }));
        }

        [Test]
        public async Task UpdateEmployer_ValidEmployer_ReturnsOk()
        {
            // Arrange
            var employerDto = new EmployerDto { EmployerId = 1, ContactPersonName = "Updated Employer" };
            _employerRepositoryMock.Setup(repo => repo.UpdateEmployerAsync(1, employerDto))
                .ReturnsAsync(employerDto);

            // Act
            var result = await _controller.UpdateEmployer(1, employerDto);

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult.StatusCode, Is.EqualTo(200)); // OK status
            Assert.That(okResult.Value, Is.EqualTo(employerDto));
        }

        [Test]
        public async Task UpdateEmployer_EmployerNotFound_ReturnsNotFound()
        {
            // Arrange
            var employerDto = new EmployerDto { EmployerId = 1, ContactPersonName = "Non-Existent Employer" };
            _employerRepositoryMock.Setup(repo => repo.UpdateEmployerAsync(1, employerDto))
                .ReturnsAsync((EmployerDto)null); // Return null to simulate not found

            // Act
            var result = await _controller.UpdateEmployer(1, employerDto);

            // Assert
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
            var notFoundResult = result as NotFoundResult;
            Assert.That(notFoundResult.StatusCode, Is.EqualTo(404)); // NotFound status
        }

        [Test]
        public async Task UpdateEmployer_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var employerDto = new EmployerDto { EmployerId = 1, ContactPersonName = "Invalid Employer" };
            _controller.ModelState.AddModelError("Name", "Required");

            // Act
            var result = await _controller.UpdateEmployer(1, employerDto);

            // Assert
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(400)); // BadRequest status

            var serializableError = badRequestResult.Value as SerializableError;
            Assert.That(serializableError.ContainsKey("Name"));
            Assert.That(serializableError["Name"], Is.EqualTo(new[] { "Required" }));
        }

        [Test]
        public async Task DeleteEmployer_ValidId_ReturnsNoContent()
        {
            // Arrange
            _employerRepositoryMock.Setup(repo => repo.DeleteEmployerAsync(1))
                .ReturnsAsync(true); // Simulate successful deletion

            // Act
            var result = await _controller.DeleteEmployer(1);

            // Assert
            Assert.That(result, Is.InstanceOf<NoContentResult>());
            var noContentResult = result as NoContentResult;
            Assert.That(noContentResult.StatusCode, Is.EqualTo(204)); // NoContent status
        }

        [Test]
        public async Task DeleteEmployer_InvalidId_ReturnsNotFound()
        {
            // Arrange
            _employerRepositoryMock.Setup(repo => repo.DeleteEmployerAsync(1))
                .ReturnsAsync(false); // Simulate employer not found

            // Act
            var result = await _controller.DeleteEmployer(1);

            // Assert
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
            var notFoundResult = result as NotFoundResult;
            Assert.That(notFoundResult.StatusCode, Is.EqualTo(404)); // NotFound status
        }

        [Test]
        public async Task UpdateEmployerDetails_ValidDetails_ReturnsNoContent()
        {
            // Arrange
            var employerDto = new EmployerDto { EmployerId = 1, ContactPersonName = "Updated Employer" };
            _employerServiceMock.Setup(service => service.UpdateEmployerDetailsAsync(1, employerDto))
                .ReturnsAsync(true); // Simulate successful update

            // Act
            var result = await _controller.UpdateEmployerDetails(1, employerDto);

            // Assert
            Assert.That(result, Is.InstanceOf<NoContentResult>());
            var noContentResult = result as NoContentResult;
            Assert.That(noContentResult.StatusCode, Is.EqualTo(204)); // NoContent status
        }
        [Test]
        public async Task UpdateEmployerDetails_EmployerNotFound_ReturnsNotFound()
        {
            // Arrange
            var employerDto = new EmployerDto { EmployerId = 1, ContactPersonName = "Non-Existent Employer" };
            _employerServiceMock.Setup(service => service.UpdateEmployerDetailsAsync(1, employerDto))
                .ReturnsAsync(false); // Simulate employer not found

            // Act
            var result = await _controller.UpdateEmployerDetails(1, employerDto);

            // Assert
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
            var notFoundResult = result as NotFoundResult;
            Assert.That(notFoundResult.StatusCode, Is.EqualTo(404)); // NotFound status
        }
        [Test]
        public async Task UpdateEmployerDetails_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var employerDto = new EmployerDto { EmployerId = 1, ContactPersonName = "Invalid Employer" };
            _controller.ModelState.AddModelError("Name", "Required");

            // Act
            var result = await _controller.UpdateEmployerDetails(1, employerDto);

            // Assert
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(400)); // BadRequest status

            var serializableError = badRequestResult.Value as SerializableError;
            Assert.That(serializableError.ContainsKey("Name"));
            Assert.That(serializableError["Name"], Is.EqualTo(new[] { "Required" }));
        }

    }

    [TestFixture]
    public class JobPostingsControllerTests
    {
        private Mock<IJobPostingsRepository> _jobPostingsRepositoryMock;
        private JobPostingsController _controller;

        [SetUp]
        public void Setup()
        {
            _jobPostingsRepositoryMock = new Mock<IJobPostingsRepository>();
            _controller = new JobPostingsController(_jobPostingsRepositoryMock.Object);

            // Set up claims for testing
            var claims = new List<Claim>
            {
                new Claim("EmployerId", "1") // Simulating an EmployerId claim
            };

            var identity = new ClaimsIdentity(claims, "TestAuthType");
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(identity)

                }
            };
        }

        [Test]
        public async Task GetJobPostingById_ValidId_ReturnsOkWithJobPosting()
        {
            // Arrange
            var jobPosting = new JobPostings // Ensure to use JobPostings as per your interface
            {
                JobPostingId = 1,
                EmployerId = 2,
                OrganisationName = "Tech Corp",
                Description = "A great job opportunity",
                Role = "Software Engineer",
                Employer = new CareerCrafterClassLib.Model.Employer // Specify the full namespace for Employer
                {
                    EmployerId = 2,
                    ContactPersonName = "John Doe"
                }
            };

            // Mock the repository to return the job posting
            _jobPostingsRepositoryMock.Setup(repo => repo.GetJobPostingByIdAsync(1))
                .ReturnsAsync(jobPosting);

            // Act
            var result = await _controller.GetJobPostingById(1);

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult.StatusCode, Is.EqualTo(200)); // OK status

            var jobPostingDto = okResult.Value as JobPostingDto;
            Assert.That(jobPostingDto.JobPostingId, Is.EqualTo(1));
            Assert.That(jobPostingDto.EmployerId, Is.EqualTo(2));
            Assert.That(jobPostingDto.OrganisationName, Is.EqualTo("Tech Corp"));
            Assert.That(jobPostingDto.Description, Is.EqualTo("A great job opportunity"));
            Assert.That(jobPostingDto.Role, Is.EqualTo("Software Engineer"));
            Assert.That(jobPostingDto.Employer.EmployerId, Is.EqualTo(2));
            Assert.That(jobPostingDto.Employer.ContactPersonName, Is.EqualTo("John Doe"));
        }

        [Test]
        public async Task GetJobPostingById_InvalidId_ReturnsNotFound()
        {
            // Arrange
            _jobPostingsRepositoryMock.Setup(repo => repo.GetJobPostingByIdAsync(1))
                .ReturnsAsync((JobPostings)null); // Simulate job posting not found

            // Act
            var result = await _controller.GetJobPostingById(1);

            // Assert
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
            var notFoundResult = result as NotFoundResult;
            Assert.That(notFoundResult.StatusCode, Is.EqualTo(404)); // NotFound status
        }

        [Test]
        public async Task GetJobPostingsByEmployer_ValidEmployerId_ReturnsOkWithJobPostings()
        {
            // Arrange
            var jobPostings = new List<JobPostings>
            {
                new JobPostings
                {
                    JobPostingId = 1,
                    EmployerId = 1,
                    OrganisationName = "Tech Corp",
                    Description = "Software Engineer Position",
                    Role = "Software Engineer",
                    Employer = new CareerCrafterClassLib.Model.Employer
                    {
                        EmployerId = 1,
                        ContactPersonName = "Jane Doe"
                    }
                },
                new JobPostings
                {
                    JobPostingId = 2,
                    EmployerId = 1,
                    OrganisationName = "Tech Corp",
                    Description = "Senior Developer Position",
                    Role = "Senior Developer",
                    Employer = new CareerCrafterClassLib.Model.Employer
                    {
                        EmployerId = 1,
                        ContactPersonName = "Jane Doe"
                    }
                }
            };

            _jobPostingsRepositoryMock.Setup(repo => repo.GetJobPostingsByEmployerIdAsync(1))
                .ReturnsAsync(jobPostings);

            // Act
            var result = await _controller.GetJobPostingsByEmployer();

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult.StatusCode, Is.EqualTo(200)); // OK status

            var jobPostingDtos = okResult.Value as List<JobPostingDto>;
            Assert.That(jobPostingDtos.Count, Is.EqualTo(2)); // Ensure we get 2 postings
            Assert.That(jobPostingDtos[0].JobPostingId, Is.EqualTo(1));
            Assert.That(jobPostingDtos[1].JobPostingId, Is.EqualTo(2));
        }

        [Test]
        public async Task GetJobPostingsByEmployer_EmployerIdNotFound_ReturnsUnauthorized()
        {
            // Arrange
            var identity = new ClaimsIdentity(); // No claims
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(identity)
                }
            };

            // Act
            var result = await _controller.GetJobPostingsByEmployer();

            // Assert
            Assert.That(result, Is.InstanceOf<UnauthorizedObjectResult>());
            var unauthorizedResult = result as UnauthorizedObjectResult;
            Assert.That(unauthorizedResult.StatusCode, Is.EqualTo(401)); // Unauthorized status
            Assert.That(unauthorizedResult.Value, Is.EqualTo("Employer not found in token"));
        }

        [Test]
        public async Task GetJobPostingsByEmployer_InvalidEmployerId_ReturnsBadRequest()
        {
            // Arrange
            var claims = new List<Claim>
            {
                new Claim("EmployerId", "Invalid") // Invalid EmployerId
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(identity)
                }
            };

            // Act
            var result = await _controller.GetJobPostingsByEmployer();

            // Assert
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(400)); // BadRequest status
            Assert.That(badRequestResult.Value, Is.EqualTo("Invalid EmployerId from token"));
        }


    }


}





