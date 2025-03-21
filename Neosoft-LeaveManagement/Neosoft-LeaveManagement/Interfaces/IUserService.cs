using Neosoft_LeaveManagement.Constants;
using Neosoft_LeaveManagement.Models;

namespace Neosoft_LeaveManagement.Interfaces
{
    public interface IUserService
    {
        Task<User> Register(User user, UserRole role);
        Task<User> Login(string email, string password);
        Task<User> GetUserByIdAsync(int userId);
    }
}
