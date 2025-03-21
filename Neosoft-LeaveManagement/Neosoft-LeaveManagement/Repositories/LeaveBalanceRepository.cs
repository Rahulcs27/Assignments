using Microsoft.EntityFrameworkCore;
using Neosoft_LeaveManagement.Interfaces;
using Neosoft_LeaveManagement.Models;

public class LeaveBalanceRepository : ILeaveBalanceRepository
{
    private readonly DataContext _context;

    public LeaveBalanceRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<LeaveBalance> GetLeaveBalanceByUserIdAsync(int userId)
    {
        return await _context.LeaveBalances
            .FirstOrDefaultAsync(lb => lb.UserId == userId);
    }

    public async Task UpdateLeaveBalanceAsync(LeaveBalance leaveBalance)
    {
        _context.LeaveBalances.Update(leaveBalance);
        await _context.SaveChangesAsync();
    }
}
