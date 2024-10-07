using CareerCrafterClassLib.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCrafterClassLib.Interface
{
    public interface IEmployerService
    {
        Task<EmployerLoginResponseDto> LoginEmployerByType(string email, string password, string userType);

        Task<EmployerDto> RegisterEmployerAsync(EmployerDto employerDto);

        Task<bool> UpdateEmployerDetailsAsync(int employerId, EmployerDto employerDto);
    }
}
