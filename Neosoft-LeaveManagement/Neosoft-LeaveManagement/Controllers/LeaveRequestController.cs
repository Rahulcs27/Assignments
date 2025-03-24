using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using Neosoft_LeaveManagement.Constants;
using Neosoft_LeaveManagement.Exceptions;
using Neosoft_LeaveManagement.Filters;
using Neosoft_LeaveManagement.Interfaces;
using Neosoft_LeaveManagement.Models;
using Neosoft_LeaveManagement.ViewModels;

namespace Neosoft_LeaveManagement.Controllers
{
    [ServiceFilter(typeof(RequireLoginFilter))]
    [ServiceFilter(typeof(ExceptionHandlingAttribute))]
    public class LeaveRequestController : Controller
    {
        private readonly ILeaveRequestService _leaveRequestService;
        private readonly DataContext _context;
        private readonly ILeaveBalanceRepository _leaveBalanceRepository;

        public LeaveRequestController(ILeaveRequestService leaveRequestService,ILeaveBalanceRepository leaveBalanceRepository, DataContext context) 
        {
            _leaveRequestService = leaveRequestService;
            _context = context; 
            _leaveBalanceRepository = leaveBalanceRepository;
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
                TempData["SuccessMessage"] = "Leave request submitted successfully!";

                return RedirectToAction("List");
            }
            catch (LeaveBalanceExceededException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(model);
            }
        }

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

            var leaveBalance = await _leaveBalanceRepository.GetLeaveBalanceByUserIdAsync(userId.Value);
            var leaveBalanceViewModel = new LeaveBalanceViewModel
            {
                TotalLeaveDays = leaveBalance?.TotalLeaveDays ?? 0,
                RemainingLeaveDays = leaveBalance?.RemainingLeaveDays ?? 0
            };

            var viewModel = new LeaveRequestListViewModel
            {
                LeaveRequests = leaveRequestViewModels,
                LeaveBalance = leaveBalanceViewModel
            };

            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var leaveRequest = await _leaveRequestService.GetLeaveRequestByIdAsync(id);
            if (leaveRequest == null)
            {
                return NotFound();
            }

            var model = new LeaveRequestViewModel
            {
                Id = leaveRequest.LeaveRequestId,
                LeaveType = leaveRequest.LeaveType,
                StartDate = leaveRequest.StartDate,
                EndDate = leaveRequest.EndDate,
                Reason = leaveRequest.Reason
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, LeaveRequestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //try
            //{
                var existingRequest = await _leaveRequestService.GetLeaveRequestByIdAsync(id);
                if (existingRequest == null)
                {
                    TempData["ErrorMessage"] = "Leave request not found!";
                    return RedirectToAction("List");
                }

                var leaveBalance = await _leaveBalanceRepository.GetLeaveBalanceByUserIdAsync(existingRequest.UserId);
                if (leaveBalance == null)
                {
                    TempData["ErrorMessage"] = "Leave balance record not found!";
                    return RedirectToAction("List");
                }

                int originalLeaveDays = (existingRequest.EndDate - existingRequest.StartDate).Days + 1;
                int newLeaveDays = (model.EndDate - model.StartDate).Days + 1;

                leaveBalance.RemainingLeaveDays += originalLeaveDays; 
                leaveBalance.RemainingLeaveDays -= newLeaveDays; 

                if (leaveBalance.RemainingLeaveDays < 0)
                {
                    TempData["ErrorMessage"] = "Not enough leave balance!";
                    return View(model);
                }

                existingRequest.LeaveType = model.LeaveType;
                existingRequest.StartDate = model.StartDate;
                existingRequest.EndDate = model.EndDate;
                existingRequest.Reason = model.Reason;

                await _leaveRequestService.UpdateLeaveRequestAsync(id, existingRequest);
                await _leaveBalanceRepository.UpdateLeaveBalanceAsync(leaveBalance);

                TempData["SuccessMessage"] = "Leave request updated successfully!";
                return RedirectToAction("List");
            //}
            //catch (Exception ex)
            //{
            //    TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
            //    return View(model);
            //}
        }
        [HttpPost]
        public async Task<IActionResult> Cancel(int id)
        {
            try
            {
                await _leaveRequestService.CancelLeaveRequestAsync(id);
                TempData["SuccessMessage"] = "Leave request canceled successfully!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction("List");
        }

    }
}
