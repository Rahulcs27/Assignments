using System.ComponentModel.DataAnnotations.Schema;
using ArtVista.Domain.Entities;
using Microsoft.AspNetCore.Identity;
namespace ArtVista.Identity.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }

        //public string? ArtistID { get; set; }

    }
}
