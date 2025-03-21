using System.Collections.Generic;

namespace Neosoft_LeaveManagement.ViewModels
{
    public class LeaveRequestListViewModel
    {
        public List<LeaveRequestViewModel> LeaveRequests { get; set; }
        public LeaveBalanceViewModel LeaveBalance { get; set; }
    }
}
