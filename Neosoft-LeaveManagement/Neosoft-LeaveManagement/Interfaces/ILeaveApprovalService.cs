using Neosoft_LeaveManagement.Models;
using Neosoft_LeaveManagement.ViewModels;

namespace Neosoft_LeaveManagement.Interfaces
{
    public interface ILeaveApprovalService
    {
        Task<IEnumerable<LeaveApprovalViewModel>> GetAllLeaveApprovalsAsync();
        Task<LeaveApprovalViewModel> GetLeaveApprovalByIdAsync(int approvalId);
        Task<bool> ApproveLeaveAsync(int leaveRequestId, int managerId, string comments);
        Task<bool> RejectLeaveAsync(int leaveRequestId, int managerId, string comments);
        Task<IEnumerable<LeaveApprovalViewModel>> GetApprovalsByManagerIdAsync(int managerId);
    }
}
