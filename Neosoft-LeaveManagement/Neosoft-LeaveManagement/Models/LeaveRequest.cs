using Neosoft_LeaveManagement.Constants;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Neosoft_LeaveManagement.Models
{
    public class LeaveRequest
    {
        [Key]
        public int LeaveRequestId { get; set; }

        [Required]
        public int UserId { get; set; }  

        [Required]
        public LeaveType LeaveType { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public LeaveStatus Status { get; set; } = LeaveStatus.Pending;

        [Required]
        [StringLength(500)]
        public string Reason { get; set; }

        public DateTime AppliedDate { get; set; } = DateTime.UtcNow; 

        [ForeignKey("UserId")]
        public User Employee { get; set; }
        public LeaveApproval LeaveApproval { get; set; }

    }
}

