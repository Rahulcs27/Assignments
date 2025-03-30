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
        public DbSet<Artwork> Artworks { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<FavoriteArtwork> FavoriteArtworks { get; set; }
        public DbSet<ArtworkGallery> ArtworkGalleries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FavoriteArtwork>()
                .HasKey(fa => new { fa.UserId, fa.ArtworkID });

            modelBuilder.Entity<ArtworkGallery>()
                .HasKey(ag => new { ag.ArtworkID, ag.GalleryID });

            modelBuilder.Entity<Artwork>()
                .HasOne(a => a.Artist)
                .WithMany(artist => artist.Artworks)
                .HasForeignKey(a => a.ArtistID)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<Gallery>()
                .HasOne(g => g.Artist)
                .WithMany(a => a.Galleries)
                .HasForeignKey(g => g.ArtistID)
                .OnDelete(DeleteBehavior.Cascade);  

            modelBuilder.Entity<ArtworkGallery>()
                .HasOne(ag => ag.Artwork)
                .WithMany(a => a.ArtworkGalleries)
                .HasForeignKey(ag => ag.ArtworkID)
                .OnDelete(DeleteBehavior.NoAction);  

            modelBuilder.Entity<ArtworkGallery>()
                .HasOne(ag => ag.Gallery)
                .WithMany(g => g.ArtworkGalleries)
                .HasForeignKey(ag => ag.GalleryID)
                .OnDelete(DeleteBehavior.NoAction);  
        }

    }
}
