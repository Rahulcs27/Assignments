using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Neosoft_LeaveManagement.Constants;
using Neosoft_LeaveManagement.Interfaces;
using Neosoft_LeaveManagement.Services;

namespace Neosoft_LeaveManagement.Controllers
{
    public class LeaveApprovalController : Controller
    {
        private readonly ILeaveApprovalService _leaveApprovalService;
        private readonly ILeaveRequestService _leaveRequestService;
        private readonly DataContext _context;

        public LeaveApprovalController(ILeaveApprovalService leaveApprovalService ,ILeaveRequestService leaveRequestService,DataContext context)
        {
            _leaveApprovalService = leaveApprovalService;
            _leaveRequestService = leaveRequestService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var pendingLeaveRequests = await _leaveRequestService.GetPendingLeaveRequestsAsync();
            return View(pendingLeaveRequests);
        }

        public async Task<IActionResult> Approve(int leaveRequestId, string comments)
        {
            var manager = await _context.Users.FirstOrDefaultAsync(u => u.Role == UserRole.Manager);
            if (manager == null)
                return BadRequest("No manager exists. Please assign a manager.");

            await _leaveApprovalService.ApproveLeaveAsync(leaveRequestId, manager.UserId, comments);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Reject(int leaveRequestId, string comments)
        {
            var managerId = HttpContext.Session.GetInt32("UserId");
            if (managerId == null)
            {
                return RedirectToAction("Login", "User");
            }

            var manager = await _context.Users.FirstOrDefaultAsync(u => u.UserId == managerId.Value);
            if (manager == null || manager.Role != UserRole.Manager)
            {
                return Unauthorized();
            }

            await _leaveApprovalService.RejectLeaveAsync(leaveRequestId, managerId.Value, comments);
            return RedirectToAction("Index");
        }
    }
}
