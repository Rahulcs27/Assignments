using Neosoft_LeaveManagement.Constants;
using Neosoft_LeaveManagement.Interfaces;
using Neosoft_LeaveManagement.Models;

namespace Neosoft_LeaveManagement.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Register(User user, UserRole role)
        {
            if (role == UserRole.Admin || role == UserRole.Manager)
            {
                throw new Exception("Only an Admin can add Managers.");
            }

            var existingUser = await _userRepository.GetUserByEmailAsync(user.Email);
            if (existingUser != null)
                throw new Exception("User already exists.");

            await _userRepository.AddUserAsync(user);
            await _userRepository.SaveChangesAsync();
            return user;
        }


        public async Task<User> Login(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null || user.Password != password)
                throw new Exception("Invalid credentials.");

            return user;
        }
        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _userRepository.GetUserByIdAsync(userId);
        }
    }
}
