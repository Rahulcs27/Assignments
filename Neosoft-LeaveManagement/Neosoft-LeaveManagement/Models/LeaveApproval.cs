using Neosoft_LeaveManagement.Constants;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Neosoft_LeaveManagement.Models
{
    public class LeaveApproval
    {
        [Key]
        public int ApprovalId { get; set; }

        [Required]
        public int? LeaveRequestId { get; set; }

        [Required]
        public int ManagerId { get; set; }

        [Required]
        public ApprovalStatus ApprovalStatus { get; set; }

        public DateTime ReviewedDate { get; set; } = DateTime.Now;

        [StringLength(500)]
        public string Comments { get; set; }

        [ForeignKey("LeaveRequestId")]
        public LeaveRequest LeaveRequest { get; set; }
        [ForeignKey("ManagerId")]
        public User Manager { get; set; }
    }
}
