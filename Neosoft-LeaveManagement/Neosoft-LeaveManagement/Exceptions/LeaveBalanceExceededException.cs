using System;

namespace Neosoft_LeaveManagement.Exceptions
{
    public class LeaveBalanceExceededException : Exception
    {
        public LeaveBalanceExceededException() : base("You have insufficient leave balance.")
        {
        }
    }
}
