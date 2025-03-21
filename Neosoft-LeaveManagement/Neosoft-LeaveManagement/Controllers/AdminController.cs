using Microsoft.AspNetCore.Mvc;
using Neosoft_LeaveManagement.Constants;
using Neosoft_LeaveManagement.Interfaces;

namespace Neosoft_LeaveManagement.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AdminController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IActionResult> ManageUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> PromoteToManager(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user != null && user.Role == UserRole.Employee)
            {
                user.Role = UserRole.Manager;
                await _userRepository.UpdateUserAsync(user);
            }
            return RedirectToAction("ManageUsers");
        }
    }
}
