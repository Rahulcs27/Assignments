using Microsoft.EntityFrameworkCore;
using Neosoft_LeaveManagement.Interfaces;
using Neosoft_LeaveManagement.Models;

namespace Neosoft_LeaveManagement.Repositories
{
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        private readonly DataContext _context;

        public LeaveRequestRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LeaveRequest>> GetAllLeaveRequestsAsync()
        {
            return await _context.LeaveRequests.Include(lr => lr.Employee).ToListAsync();
        }

        public async Task<LeaveRequest> GetLeaveRequestByIdAsync(int requestId)
        {
            return await _context.LeaveRequests.Include(lr => lr.Employee)
                                               .FirstOrDefaultAsync(lr => lr.LeaveRequestId == requestId);
        }

        public async Task AddLeaveRequestAsync(LeaveRequest leaveRequest)
        {
            _context.LeaveRequests.Add(leaveRequest);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> UpdateLeaveRequestAsync(LeaveRequest leaveRequest)
        {
            _context.LeaveRequests.Update(leaveRequest);
            return await _context.SaveChangesAsync() > 0; 
        }

        public async Task<IEnumerable<LeaveRequest>> GetLeaveRequestsByUserIdAsync(int userId)
        {
            return await _context.LeaveRequests.Where(lr => lr.UserId == userId).ToListAsync();
        }

        public async Task DeleteLeaveRequestAsync(int requestId)
        {
            var leaveRequest = await _context.LeaveRequests.FindAsync(requestId);
            if (leaveRequest != null)
            {
                _context.LeaveRequests.Remove(leaveRequest);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();

        }
    }
}
