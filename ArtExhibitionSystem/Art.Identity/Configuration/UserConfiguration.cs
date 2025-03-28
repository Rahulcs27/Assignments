using ArtVista.Identity.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArtVista.Identity.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();

            builder.HasData(
                new ApplicationUser
                {
                    Id = "41776062-6086-1fbf-b923-2879a6680b9a",
                    Email = "admin@artvista.com",
                    NormalizedEmail = "ADMIN@ARTVISTA.COM",
                    FirstName = "Admin",
                    LastName = "User",
                    UserName = "admin@artvista.com",
                    NormalizedUserName = "ADMIN@ARTVISTA.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Admin@123")
                },
                new ApplicationUser
                {
                    Id = "41776062-6086-1fcf-b923-2879a6680b9a",
                    Email = "rahul@artvista.com",
                    NormalizedEmail = "RAHUL@ARTVISTA.COM",
                    FirstName = "Rahul",
                    LastName = "Suthar",
                    UserName = "Rahul@artvista.com",
                    NormalizedUserName = "RAHUL@ARTVISTA.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Rahul@123")
                },
                new ApplicationUser
                {
                    Id = "41776062-6086-1fdf-b923-2879a6680b9a",
                    Email = "om@artvista.com",
                    NormalizedEmail = "OM@ARTVISTA.COM",
                    FirstName = "Om",
                    LastName = "Auti",
                    UserName = "om@artvista.com",
                    NormalizedUserName = "OM@ARTVISTA.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "OmAuti@123")
                }
            );
        }
    }
}
