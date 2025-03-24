using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Neosoft_LeaveManagement.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, NotNull, RegularExpression("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&-+=()])(?=\\S+$).{4,10}$")]
        public string Password { get; set; }

        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; }
        public IFormFile? ProfilePicture { get; set; }

    }
}
