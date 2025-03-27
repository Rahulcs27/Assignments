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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Artist> Artists { get; set; }
    }
}
