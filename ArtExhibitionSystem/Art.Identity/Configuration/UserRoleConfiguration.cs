using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArtVista.Identity.Configuration
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {   // Admin User 
                    RoleId = "2", 
                    UserId = "41776062-6086-1fbf-b923-2879a6680b9a" 
                },
                new IdentityUserRole<string>
                {
                    // Regular User 
                    RoleId = "1",
                    UserId = "41776062-6086-1fcf-b923-2879a6680b9a" 
                },
                new IdentityUserRole<string>
                {
                    // Artist User
                    RoleId = "3",
                    UserId = "41776062-6086-1fdf-b923-2879a6680b9a"
                }

            );
        }
    }
}
