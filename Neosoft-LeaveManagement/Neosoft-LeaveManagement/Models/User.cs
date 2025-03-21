using Neosoft_LeaveManagement.Constants;
using System.ComponentModel.DataAnnotations;

namespace Neosoft_LeaveManagement.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public UserRole Role { get; set; }

        public ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();
        public ICollection<LeaveApproval>LeaveApprovals { get; set; } = new List<LeaveApproval>();

    }
}
