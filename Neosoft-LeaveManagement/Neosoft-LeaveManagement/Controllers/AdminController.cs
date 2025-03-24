using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Neosoft_LeaveManagement.Constants;
using Neosoft_LeaveManagement.Filters;
using Neosoft_LeaveManagement.Interfaces;

namespace Neosoft_LeaveManagement.Controllers
{
    [ServiceFilter(typeof(RequireLoginFilter))]
    public class AdminController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly DataContext _context;
        public AdminController(IUserRepository userRepository,DataContext context)
        {
            _userRepository = userRepository;
            _context = context;
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
        public IActionResult AllLeaves(string statusFilter, string searchUser)
        {
            var leaves = _context.LeaveApprovals
                .Include(l => l.LeaveRequest)
                .Include(l => l.Manager)
                .Select(l => new
                {
                    LeaveId = l.LeaveRequestId,
                    EmployeeName = l.LeaveRequest.Employee.Name,
                    Status = l.ApprovalStatus == ApprovalStatus.Approved ? "Approved" : "Pending",
                    ManagerName = l.Manager.Name
                })
                .ToList();

            if (!string.IsNullOrEmpty(statusFilter))
            {
                leaves = leaves.Where(l => l.Status == statusFilter).ToList();
            }

            //Search Filter
            if (!string.IsNullOrEmpty(searchUser))
            {
                leaves = leaves.Where(l => l.EmployeeName.Contains(searchUser, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return View(leaves);
        }

    }
}
