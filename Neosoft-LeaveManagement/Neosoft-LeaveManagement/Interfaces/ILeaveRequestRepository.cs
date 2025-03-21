using Neosoft_LeaveManagement.Models;

namespace Neosoft_LeaveManagement.Interfaces
{
    public interface ILeaveRequestRepository
    {
        Task<IEnumerable<LeaveRequest>> GetAllLeaveRequestsAsync();
        Task<LeaveRequest> GetLeaveRequestByIdAsync(int requestId);
        Task AddLeaveRequestAsync(LeaveRequest leaveRequest);
        Task<bool> UpdateLeaveRequestAsync(LeaveRequest leaveRequest);
        Task<IEnumerable<LeaveRequest>> GetLeaveRequestsByUserIdAsync(int userId);
        Task DeleteLeaveRequestAsync(int requestId);
        Task SaveChangesAsync();
    }
}
