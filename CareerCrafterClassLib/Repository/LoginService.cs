//using System.Threading.Tasks;
//using CareerCrafterClassLib.Model;
//using CareerCrafterClassLib.Repository;
//using JWT.Logic;

//namespace CareerCrafterClassLib.Service
//{
//    public class LoginService : ILoginService
//    {
//        private readonly ILoginRepository _repository;
//        private readonly TokenGeneration _tokenGeneration;

//        public LoginService(ILoginRepository repository, TokenGeneration tokenGeneration)
//        {
//            _repository = repository;
//            _tokenGeneration = tokenGeneration;
//        }

//        public async Task<bool> RegisterUser(Login user)
//        {
//            return await _repository.RegisterUser(user);
//        }

//        public async Task<Login> GetUserByEmail(string email) // This method implementation should be included
//        {
//            return await _repository.GetUserByEmail(email);
//        }

//        //public async Task<string> LoginUserByType(string email, string password, string userType)
//        //{
//        //    var user = await _repository.GetUserByEmailAndUserType(email, userType);
//        //    if (user != null && user.Password == password)
//        //    {
//        //        // Generate JWT Token
//        //        return _tokenGeneration.GenerateJWT(user.LoginId.ToString(), user.UserType, "Department", "AccessLevel");
//        //    }
//        //    return null;
//        //}

//        public async Task<bool> EmailExists(string email)
//        {
//            var user = await _repository.GetUserByEmail(email);
//            return user != null; // Return true if user exists, false otherwise
//        }
//        //public async Task<string> LoginUserByType(string email, string password, string userType)
//        //{
//        //    // Retrieve the user based on the email and user type (JobSeeker in this case)
//        //    var user = await _repository.GetUserByEmailAndUserType(email, userType);

//        //    // Check if user exists and if password matches
//        //    if (user != null && user.Password == password)
//        //    {
//        //        // Generate JWT Token only if the user type is JobSeeker
//        //        if (userType == "JobSeeker")
//        //        {
//        //            return _tokenGeneration.GenerateJWT(user.LoginId.ToString(), user.UserType, "Department", "AccessLevel");
//        //        }
//        //    }
//        //    return null;
//        //}
//        // Login a user by email, password, and userType (JobSeeker in this case)
//        public async Task<string> LoginJobSeekerByType(string email, string password, string userType)
//        {
//            // Retrieve the JobSeeker based on the email and userType
//            var jobSeeker = await _repository.GetUserByEmailAndUserType(email, userType);

//            // Check if the JobSeeker exists and if the password matches
//            if (jobSeeker != null && jobSeeker.Password == password)
//            {
//                // Generate JWT token only if the userType is "JobSeeker"
//                if (userType == "JobSeeker")
//                {
//                    return _tokenGeneration.GenerateJWT(jobSeeker.Id.ToString(), jobSeeker.UserType, "Department", "AccessLevel");
//                }
//            }
//            return null;
//        }

//    }
//}
