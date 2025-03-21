using Neosoft_LeaveManagement.Constants;
using System.ComponentModel.DataAnnotations;

namespace Neosoft_LeaveManagement.ViewModels
{
    public class LeaveApprovalViewModel
    {
        public int ApprovalId { get; set; }
        public int LeaveRequestId { get; set; }
        public int ManagerId { get; set; }

        [Required]
        public ApprovalStatus ApprovalStatus { get; set; }

        public DateTime ReviewedDate { get; set; }

        [StringLength(500)]
        public string Comments { get; set; }
    }
}
