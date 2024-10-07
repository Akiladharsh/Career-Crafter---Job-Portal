using CareerCrafterClassLib.DTO;
using CareerCrafterClassLib.Interface;
using CareerCrafterClassLib.Model;
using JWT.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCrafterClassLib.Repository
{
    public class EmployerService : IEmployerService
    {
        private readonly IEmployerRepository _repository;
        private readonly TokenGeneration _tokenGeneration;

        public EmployerService(IEmployerRepository repository, TokenGeneration tokenGeneration)
        {
            _repository = repository;
            _tokenGeneration = tokenGeneration;
        }

        public async Task<EmployerLoginResponseDto> LoginEmployerByType(string email, string password, string userType)
        {
            var employer = await _repository.GetEmployerByEmailAndUserType(email, userType);
            if (employer != null && employer.Password == password)
            {
                // Pass employer.EmployerId to GenerateJWT
                var token = _tokenGeneration.GenerateJWT(employer.EmployerId.ToString(), employer.UserType, "Department", "AccessLevel", employer.EmployerId);

                return new EmployerLoginResponseDto
                {
                    Token = token,
                    EmployerId = employer.EmployerId,
                    UserType = employer.UserType
                };
            }
            return null;
        }

        public async Task<EmployerDto> RegisterEmployerAsync(EmployerDto employerDto)
        {
            var employer = new Employer
            {
                ContactPersonName = employerDto.ContactPersonName,
                CompanyName = employerDto.CompanyName,
                Location = employerDto.Location,
                Email = employerDto.Email,
                PhoneNo = employerDto.PhoneNo,
                Password = employerDto.Password,
                UserType = employerDto.UserType
            };

            var createdEmployer = await _repository.AddEmployerAsync(employer);

            return new EmployerDto
            {
                EmployerId = createdEmployer.EmployerId,
                ContactPersonName = createdEmployer.ContactPersonName,
                CompanyName = createdEmployer.CompanyName,
                Location = createdEmployer.Location,
                Email = createdEmployer.Email,
                PhoneNo = createdEmployer.PhoneNo,
                Password = createdEmployer.Password,
                UserType = createdEmployer.UserType
            };
        }
        public async Task<bool> UpdateEmployerDetailsAsync(int employerId, EmployerDto employerDto)
        {
            return await _repository.UpdateEmployerDetailsAsync(employerId, employerDto);
        }

    }
}
