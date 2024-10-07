using CareerCrafterClassLib.Controllers;
using CareerCrafterClassLib.Data;
using CareerCrafterClassLib.Model;
using Castle.Core.Configuration;
using JWT.Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using System.Threading.Tasks;
using CareerCrafterClassLib.DTO;
using CareerCrafterClassLib.Interface;

namespace CareerCrafterClassLib.Tests
{
   
    

    [TestFixture]
    public class EmployerControllerTests
    {
        private Mock<IEmployerRepository> _mockRepo;
        private EmployerController _employerController;

        [SetUp]
        public void Setup()
        {
            // Mocking the repository
            _mockRepo = new Mock<IEmployerRepository>();
            // Creating the controller instance
            _employerController = new EmployerController(_mockRepo.Object);
        }

        [Test]
        public async Task GetEmployerById_ValidId_ReturnsOkResult()
        {
            // Arrange
            var employerId = 4;
            var employerDto = new EmployerDto
            {
                ContactPersonName = "akil",
                CompanyName = "abc tech",
                Location = "chennai",
                Email = "akil@gmail.com",
                PhoneNo = "1322354"
            };

            _mockRepo.Setup(repo => repo.GetEmployerByIdAsync(employerId))
                     .ReturnsAsync(employerDto);

            // Act
            var result = await _employerController.GetEmployerById(employerId);

            // Assert
            Assert.That(result.Result, Is.TypeOf<OkObjectResult>());

        }

        [Test]
        public async Task GetEmployerById_InvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            var employerId = 1;

            _mockRepo.Setup(repo => repo.GetEmployerByIdAsync(employerId))
                     .ReturnsAsync((EmployerDto)null); // Return null to simulate not found

            // Act
            var result = await _employerController.GetEmployerById(employerId);

            // Assert
            Assert.That(result.Result, Is.TypeOf<NotFoundResult>());
        }
    }

}

