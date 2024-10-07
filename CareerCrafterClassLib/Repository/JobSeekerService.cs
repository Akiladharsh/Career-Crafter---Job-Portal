//using System.Threading.Tasks;
//using CareerCrafterClassLib.Interface;
//using CareerCrafterClassLib.Model;
//using CareerCrafterClassLib.Repository;
//using JWT.Logic;

//namespace CareerCrafterClassLib.Service
//{
//    public class JobSeekerService : IJobSeekerService
//    {
//        private readonly IJobSeekerRepository _repository;
//        private readonly TokenGeneration _tokenGeneration;

//        public JobSeekerService(IJobSeekerRepository repository, TokenGeneration tokenGeneration)
//        {
//            _repository = repository;
//            _tokenGeneration = tokenGeneration;
//        }

//        // Login a JobSeeker by email, password, and userType
//        //public async Task<string> LoginJobSeekerByType(string email, string password, string userType)
//        //{
//        //    // Retrieve the JobSeeker based on the email and userType
//        //    var jobSeeker = await _repository.GetJobSeekerByEmailAndUserType(email, userType);

//        //    // Check if the JobSeeker exists and if the password matches
//        //    if (jobSeeker != null && jobSeeker.Password == password)
//        //    {
//        //        // Generate JWT token only if the userType is "JobSeeker"
//        //        if (userType == "JobSeeker")
//        //        {
//        //            return _tokenGeneration.GenerateJWT(jobSeeker.JobSeeker_Id.ToString(), jobSeeker.UserType, "Department", "AccessLevel");
//        //        }
//        //    }
//        //    return null;
//        //}
//        public async Task<JobSeekerLoginResponseDto> LoginJobSeekerByType(string email, string password, string userType)
//        {
//            // Retrieve the JobSeeker based on the email and userType
//            var jobSeeker = await _repository.GetJobSeekerByEmailAndUserType(email, userType);

//            // Check if the JobSeeker exists and if the password matches
//            if (jobSeeker != null && jobSeeker.Password == password)
//            {
//                // Generate JWT token
//                var token = _tokenGeneration.GenerateJWT(jobSeeker.JobSeeker_Id.ToString(), jobSeeker.UserType, "Department", "AccessLevel");

//                return new JobSeekerLoginResponseDto
//                {
//                    Token = token,
//                    JobSeekerId = jobSeeker.JobSeeker_Id, // Assuming JobSeeker_Id is an int
//                    UserType = jobSeeker.UserType // Add UserType to the response
//                };
//            }
//            return null; // Return null if credentials are invalid
//        }


//    }
//}
using CareerCrafterClassLib.Interface;
using CareerCrafterClassLib.Repository;
using CareerCrafterClassLib.DTO ;
using JWT.Logic;

public class JobSeekerService : IJobSeekerService
{
    private readonly IJobSeekerRepository _repository;
    private readonly TokenGeneration _tokenGeneration;

    public JobSeekerService(IJobSeekerRepository repository, TokenGeneration tokenGeneration)
    {
        _repository = repository;
        _tokenGeneration = tokenGeneration;
    }

    public async Task<JobSeekerLoginResponseDto> LoginJobSeekerByType(string email, string password, string userType)
    {
        var jobSeeker = await _repository.GetJobSeekerByEmailAndUserType(email, userType);
        if (jobSeeker != null && jobSeeker.Password == password)
        {
            var token = _tokenGeneration.GenerateJWT(jobSeeker.JobSeeker_Id.ToString(), jobSeeker.UserType, "Department", "AccessLevel",0);

            return new JobSeekerLoginResponseDto
            {
                Token = token,
                JobSeekerId = jobSeeker.JobSeeker_Id,
                UserType = jobSeeker.UserType
            };
        }
        return null;
    }
}
