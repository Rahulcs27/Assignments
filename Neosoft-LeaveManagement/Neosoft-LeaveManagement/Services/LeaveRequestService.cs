﻿using Microsoft.EntityFrameworkCore;
using Neosoft_LeaveManagement.Constants;
using Neosoft_LeaveManagement.Interfaces;
using Neosoft_LeaveManagement.Models;
using Neosoft_LeaveManagement.ViewModels;

namespace Neosoft_LeaveManagement.Services
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly DataContext _context;

        public LeaveRequestService(ILeaveRequestRepository leaveRequestRepository, DataContext context)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _context = context;
        }

        public async Task<IEnumerable<LeaveRequest>> GetAllLeaveRequestsAsync()
        {
            return await _leaveRequestRepository.GetAllLeaveRequestsAsync();
        }

        public async Task<LeaveRequest> GetLeaveRequestByIdAsync(int requestId)
        {
            return await _leaveRequestRepository.GetLeaveRequestByIdAsync(requestId);
        }

        public async Task AddLeaveRequestAsync(LeaveRequest leaveRequest)
        {
            await _leaveRequestRepository.AddLeaveRequestAsync(leaveRequest);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<LeaveRequestViewModel>> GetLeaveRequestsByUserIdAsync(int userId)
        {
            var leaveRequests = await _leaveRequestRepository.GetLeaveRequestsByUserIdAsync(userId);
            return leaveRequests.Select(lr => new LeaveRequestViewModel
            {
                Id = lr.LeaveRequestId,
                UserId = lr.UserId,
                LeaveType = lr.LeaveType,
                StartDate = lr.StartDate,
                EndDate = lr.EndDate,
                Reason = lr.Reason,
                Status = lr.Status,
                AppliedDate = lr.AppliedDate
            });
        }

        public async Task DeleteLeaveRequestAsync(int requestId)
        {
            await _leaveRequestRepository.DeleteLeaveRequestAsync(requestId);
        }

        public async Task ApplyLeaveAsync(LeaveRequest leaveRequest)
        {
            Console.WriteLine($"Applying Leave: {leaveRequest.UserId}, {leaveRequest.StartDate}");

            await _leaveRequestRepository.AddLeaveRequestAsync(leaveRequest);
            await _leaveRequestRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<LeaveRequestViewModel>> GetPendingLeaveRequestsAsync()
        {
            return await _context.LeaveRequests
            .Where(lr => lr.Status == LeaveStatus.Pending)
            .Include(lr => lr.Employee)  
            .Select(lr => new LeaveRequestViewModel
            {
                Id = lr.LeaveRequestId,
                UserId = lr.UserId,
                EmployeeName = lr.Employee.Name,  
                LeaveType = lr.LeaveType,
                StartDate = lr.StartDate,
                EndDate = lr.EndDate,
                Reason = lr.Reason
            })
            .ToListAsync();
        }
    }
}
