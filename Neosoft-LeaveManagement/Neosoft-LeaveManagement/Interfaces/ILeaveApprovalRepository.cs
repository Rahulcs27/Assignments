using Neosoft_LeaveManagement.Models;

namespace Neosoft_LeaveManagement.Interfaces
{
    public interface ILeaveApprovalRepository
    {
        Task<IEnumerable<LeaveApproval>> GetAllLeaveApprovalsAsync();
        Task<LeaveApproval> GetLeaveApprovalByIdAsync(int approvalId);
        Task AddLeaveApprovalAsync(LeaveApproval approval);
        Task<IEnumerable<LeaveApproval>> GetApprovalsByManagerIdAsync(int managerId);
    }
}
