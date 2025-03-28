using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using ArtVista.Identity.Model;
using ArtVista.Identity.Configuration;
using ArtVista.Domain.Entities;

namespace ArtVista.Infrastructure.Context
{
    public class ArtVistaDbContext : DbContext
    {
        public ArtVistaDbContext(DbContextOptions<ArtVistaDbContext> options) : base(options) { }
        public DbSet<Artist> Artists { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Artist>()
            //    .HasOne<ApplicationUser>()  // No navigation property in ApplicationUser
            //    .WithOne()
            //    .HasForeignKey<Artist>(a => a.ArtistID)
            //    .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete
        }
    }
}
