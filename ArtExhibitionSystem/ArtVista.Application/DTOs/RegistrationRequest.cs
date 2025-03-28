using ArtVista.Domain.Entities;

namespace ArtVista.Application.DTOs
{
    public class RegistrationRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string? Role { get; set; }
    }
}
