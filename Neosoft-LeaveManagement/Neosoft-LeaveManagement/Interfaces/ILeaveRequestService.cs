using Neosoft_LeaveManagement.Models;
using Neosoft_LeaveManagement.ViewModels;

namespace Neosoft_LeaveManagement.Interfaces
{
    public interface ILeaveRequestService
    {
        Task<IEnumerable<LeaveRequest>> GetAllLeaveRequestsAsync();
        Task<LeaveRequest> GetLeaveRequestByIdAsync(int requestId);
        Task AddLeaveRequestAsync(LeaveRequest leaveRequest);
        Task<IEnumerable<LeaveRequestViewModel>> GetLeaveRequestsByUserIdAsync(int userId);
        Task DeleteLeaveRequestAsync(int requestId);
        Task ApplyLeaveAsync(LeaveRequest leaveRequest);
        Task<IEnumerable<LeaveRequestViewModel>> GetPendingLeaveRequestsAsync();
    }
}
