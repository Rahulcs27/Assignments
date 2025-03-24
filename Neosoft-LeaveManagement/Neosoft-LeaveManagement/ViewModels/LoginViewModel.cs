using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Neosoft_LeaveManagement.ViewModels
{
    public class LoginViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, NotNull, RegularExpression("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&-+=()])(?=\\S+$).{4,10}$")]
        public string Password { get; set; }
    }
}
