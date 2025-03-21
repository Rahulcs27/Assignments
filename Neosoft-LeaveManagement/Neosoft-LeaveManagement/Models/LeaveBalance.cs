using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Neosoft_LeaveManagement.Models
{
    public class LeaveBalance
    {
        [Key]
        public int BalanceId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public int TotalLeaveDays { get; set; } = 20;
        public int RemainingLeaveDays { get; set; } = 20;
        public DateTime LastUpdated { get; set; } = DateTime.Now;

        
        public virtual User Employee { get; set; }
    }
}
