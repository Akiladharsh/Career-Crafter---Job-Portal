//using System.Threading.Tasks;
//using CareerCrafter.Models;
//using CareerCrafterClassLib.Data;
//using CareerCrafterClassLib.Model;
//using Microsoft.EntityFrameworkCore;

//namespace CareerCrafterClassLib.Repository
//{
//    public class LoginRepository : ILoginRepository
//    {
//        private readonly AppDbContext _context;

//        public LoginRepository(AppDbContext context)
//        {
//            _context = context;
//        }

//        // Register a new user
//        public async Task<bool> RegisterUser(Login user)
//        {
//            _context.Login.Add(user);
//            return await _context.SaveChangesAsync() > 0;
//        }

//        // Retrieve a user by email
//        public async Task<Login> GetUserByEmail(string email)
//        {
//            return await _context.Login
//                .FirstOrDefaultAsync(u => u.Email == email);
//        }

//        // Retrieve a user by email and user type
//        //public async Task<Login> GetUserByEmailAndUserType(string email, string userType)
//        //{
//        //    return await _context.Login
//        //        .FirstOrDefaultAsync(u => u.Email == email && u.UserType == userType);
//        //}
//        //public async Task<Login> GetUserByEmailAndUserType(string email, string userType)
//        //{
//        //    return await _context.Login
//        //        .FirstOrDefaultAsync(u => u.Email == email && u.UserType == userType);
//        //}
//        public async Task<JobSeeker> GetUserByEmailAndUserType(string email, string userType)
//        {
//            return await _context.JobSeekers
//                .FirstOrDefaultAsync(u => u.Email == email && u.UserType == userType);
//        }

//    }
//}
