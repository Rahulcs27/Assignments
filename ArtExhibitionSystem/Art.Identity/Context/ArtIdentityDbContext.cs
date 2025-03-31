using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ArtVista.Identity.Model;


using ArtVista.Identity.Configuration;

namespace ArtVista.Identity.Context
{
    public class ArtIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public ArtIdentityDbContext(DbContextOptions<ArtIdentityDbContext> options) : base(options) { }
        public ArtIdentityDbContext() { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
            builder.Entity<ApplicationUser>().ToTable("AspNetUsers");


        }
    }
}
