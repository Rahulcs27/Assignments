using Microsoft.EntityFrameworkCore;
using Neosoft_LeaveManagement.Constants;
using Neosoft_LeaveManagement.Interfaces;
using Neosoft_LeaveManagement.Models;
using Neosoft_LeaveManagement.ViewModels;

namespace Neosoft_LeaveManagement.Services
{
    public class LeaveApprovalService : ILeaveApprovalService
    {
        private readonly ILeaveApprovalRepository _leaveApprovalRepository;
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly DataContext _context;

        public LeaveApprovalService(ILeaveApprovalRepository leaveApprovalRepository, ILeaveRequestRepository leaveRequestRepository,DataContext context)
        {
            _leaveApprovalRepository = leaveApprovalRepository;
            _leaveRequestRepository = leaveRequestRepository;
            _context = context;
        }

        public async Task<bool> ApproveLeaveAsync(int leaveRequestId, int managerId, string comments)
        {
            var leaveRequest = await _leaveRequestRepository.GetLeaveRequestByIdAsync(leaveRequestId);
            if (leaveRequest == null) return false;
            leaveRequest.Status = LeaveStatus.Approved;

            var approval = new LeaveApproval
            {
                LeaveRequestId = leaveRequestId,
                ManagerId = managerId,
                ApprovalStatus = ApprovalStatus.Approved,
                ReviewedDate = DateTime.Now,
                Comments = comments
            };

            await _leaveApprovalRepository.AddLeaveApprovalAsync(approval);

     
            if (leaveRequest == null)
            {
                throw new Exception("Leave request not found.");
            }



            leaveRequest.Status = LeaveStatus.Approved;

            await _leaveRequestRepository.UpdateLeaveRequestAsync(leaveRequest);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RejectLeaveAsync(int leaveRequestId, int managerId, string comments)
        {
            var leaveRequest = await _leaveRequestRepository.GetLeaveRequestByIdAsync(leaveRequestId);
            if (leaveRequest == null)
                throw new Exception("Leave request not found.");

            if (leaveRequest != null)
            {
                var approval = new LeaveApproval
                {
                    LeaveRequestId = leaveRequestId,
                    ManagerId = managerId,
                    ApprovalStatus = ApprovalStatus.Rejected,
                    ReviewedDate = DateTime.Now,
                    Comments = comments
                };

                await _leaveApprovalRepository.AddLeaveApprovalAsync(approval);
            }

            leaveRequest.Status = LeaveStatus.Rejected;
            _leaveRequestRepository.UpdateLeaveRequestAsync(leaveRequest);

            return true;
        }

        public async Task<IEnumerable<LeaveApprovalViewModel>> GetAllLeaveApprovalsAsync()
        {
            var approvals = await _leaveApprovalRepository.GetAllLeaveApprovalsAsync();
            return approvals.Select(approval => new LeaveApprovalViewModel
            {
                ApprovalId = approval.ApprovalId,
                LeaveRequestId = approval.LeaveRequestId ?? 0,
                ManagerId = approval.ManagerId,
                ApprovalStatus = approval.ApprovalStatus,
                ReviewedDate = approval.ReviewedDate,
                Comments = approval.Comments
            });
        }

        public async Task<LeaveApprovalViewModel> GetLeaveApprovalByIdAsync(int approvalId)
        {
            var approval = await _leaveApprovalRepository.GetLeaveApprovalByIdAsync(approvalId);
            if (approval == null) return null;

            return new LeaveApprovalViewModel
            {
                ApprovalId = approval.ApprovalId,
                LeaveRequestId = approval.LeaveRequestId ?? 0,
                ManagerId = approval.ManagerId,
                ApprovalStatus = approval.ApprovalStatus,
                ReviewedDate = approval.ReviewedDate,
                Comments = approval.Comments
            };
        }

        public async Task<IEnumerable<LeaveApprovalViewModel>> GetApprovalsByManagerIdAsync(int managerId)
        {
            var approvals = await _leaveApprovalRepository.GetApprovalsByManagerIdAsync(managerId);
            return approvals.Select(approval => new LeaveApprovalViewModel
            {
                ApprovalId = approval.ApprovalId,
                LeaveRequestId = approval.LeaveRequestId ?? 0,
                ManagerId = approval.ManagerId,
                ApprovalStatus = approval.ApprovalStatus,
                ReviewedDate = approval.ReviewedDate,
                Comments = approval.Comments
            });
        }
    }
}
