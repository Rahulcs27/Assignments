using Neosoft_LeaveManagement.Models;

namespace Neosoft_LeaveManagement.Interfaces
{
    public interface ILeaveBalanceRepository
    {
        Task<LeaveBalance> GetLeaveBalanceByUserIdAsync(int userId);
        Task UpdateLeaveBalanceAsync(LeaveBalance leaveBalance);
    }
}
