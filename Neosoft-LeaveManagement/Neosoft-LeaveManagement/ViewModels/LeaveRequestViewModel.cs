using Neosoft_LeaveManagement.Constants;
using System.ComponentModel.DataAnnotations;

namespace Neosoft_LeaveManagement.ViewModels
{
    public class LeaveRequestViewModel
    {
        public int Id { get; set; } 

        public int UserId { get; set; }
        public string? EmployeeName { get; set; }

        [Required]
        [EnumDataType(typeof(LeaveType))]
        public LeaveType LeaveType { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        [StringLength(500)]
        public string Reason { get; set; }

        public LeaveStatus Status { get; set; } = LeaveStatus.Pending;

        public DateTime AppliedDate { get; set; } = DateTime.Now;
    }
}
