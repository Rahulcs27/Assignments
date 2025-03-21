using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using Neosoft_LeaveManagement.Constants;
using Neosoft_LeaveManagement.Interfaces;
using Neosoft_LeaveManagement.Models;
using Neosoft_LeaveManagement.ViewModels;

namespace Neosoft_LeaveManagement.Controllers
{
    public class LeaveRequestController : Controller
    {
        private readonly ILeaveRequestService _leaveRequestService;
        private readonly DataContext _context;

        public LeaveRequestController(ILeaveRequestService leaveRequestService, DataContext context) 
        {
            _leaveRequestService = leaveRequestService;
            _context = context; 
        }

        [HttpGet]
        public IActionResult Apply()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Apply(LeaveRequestViewModel model)
        {
            ModelState.Remove("EmployeeName");
            if (!ModelState.IsValid)
                return View(model);
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }

            try
            {
                var leaveRequest = new LeaveRequest
                {
                    UserId = userId.Value,
                    LeaveType = model.LeaveType,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    Reason = model.Reason,
                    Status = model.Status,
                    AppliedDate = model.AppliedDate
                };

                await _leaveRequestService.ApplyLeaveAsync(leaveRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        // Display All Leave Requests for the Logged-in User
        public async Task<IActionResult> List()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }

            var leaveRequests = await _leaveRequestService.GetLeaveRequestsByUserIdAsync(userId.Value);
            var leaveRequestViewModels = leaveRequests.Select(lr => new LeaveRequestViewModel
            {
                Id = lr.Id,
                UserId = lr.UserId,
                LeaveType = lr.LeaveType,
                StartDate = lr.StartDate,
                EndDate = lr.EndDate,
                Reason = lr.Reason,
                Status = lr.Status,
                AppliedDate = lr.AppliedDate
            }).ToList();

            return View(leaveRequestViewModels);
        }
    }
}
