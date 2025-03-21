using Microsoft.EntityFrameworkCore;
using Neosoft_LeaveManagement.Interfaces;
using Neosoft_LeaveManagement.Models;

namespace Neosoft_LeaveManagement.Repositories
{
    public class LeaveApprovalRepository : ILeaveApprovalRepository
    {
        private readonly DataContext _context;

        public LeaveApprovalRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LeaveApproval>> GetAllLeaveApprovalsAsync()
        {
            return await _context.LeaveApprovals.Include(l => l.LeaveRequest).Include(m => m.Manager).ToListAsync();
        }

        public async Task<LeaveApproval> GetLeaveApprovalByIdAsync(int approvalId)
        {
            return await _context.LeaveApprovals.FindAsync(approvalId);
        }

        public async Task AddLeaveApprovalAsync(LeaveApproval approval)
        {
            await _context.LeaveApprovals.AddAsync(approval);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<LeaveApproval>> GetApprovalsByManagerIdAsync(int managerId)
        {
            return await _context.LeaveApprovals
                .Where(a => a.ManagerId == managerId)
                .Include(l => l.LeaveRequest)
                .ToListAsync();
        }
    }
}
