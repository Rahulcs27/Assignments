using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtVista.Domain.Entities;
using ArtVista.Domain.Interfaces;
using ArtVista.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ArtVista.Infrastructure.Repository
{
    public class ArtworkRepository : IArtworkRepository
    {
        private readonly ArtVistaDbContext _context;

        public ArtworkRepository(ArtVistaDbContext context)
        {
            _context = context;
        }

        public async Task<Artwork?> GetArtworkByIdAsync(int artworkId)
        {
            return await _context.Artworks
                .Include(a => a.Artist)
                .FirstOrDefaultAsync(a => a.ArtworkID == artworkId);
        }

        public async Task AddArtworkAsync(Artwork artwork)
        {
            var existingArtist = await _context.Artists.FindAsync(artwork.ArtistID);

            if (existingArtist == null)
            {
                throw new Exception("Artist not found.");
            }

            // Ensure that the ArtworkID is not explicitly set if it's an identity column
            artwork.ArtworkID = 0; 

            await _context.Artworks.AddAsync(artwork);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Artwork>> GetAllArtworksAsync()
        {
            return await _context.Artworks.Include(a => a.Artist).ToListAsync();
        }

        public async Task<bool> UpdateArtworkAsync(Artwork artwork ,string userId)
        {
            var existingArtwork = await _context.Artworks.FindAsync(artwork.ArtworkID);
            if (existingArtwork == null) return false;

            if (existingArtwork.ArtistID != userId)
                throw new UnauthorizedAccessException("You can only update your own artworks.");

            existingArtwork.Title = artwork.Title;
            existingArtwork.Description = artwork.Description;
            existingArtwork.ImageURL = artwork.ImageURL;

            _context.Artworks.Update(existingArtwork);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveArtworkAsync(int artworkId,string userId)
        {
            var artwork = await _context.Artworks.FindAsync(artworkId);
            if (artwork == null) return false;

            if (artwork.ArtistID != userId)
                throw new UnauthorizedAccessException("You can only delete your own artworks.");

            _context.Artworks.Remove(artwork);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Artwork>> SearchArtworksAsync(string keyword)
        {
            return await _context.Artworks
                .Where(a => a.Title.Contains(keyword) || a.Description.Contains(keyword))
                .ToListAsync();
        }
    }
}
