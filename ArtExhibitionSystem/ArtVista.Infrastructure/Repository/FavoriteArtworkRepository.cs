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
    public class FavoriteArtworkRepository : IFavoriteArtworkRepository
    {
        private readonly ArtVistaDbContext _context;

        public FavoriteArtworkRepository(ArtVistaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FavoriteArtwork>> GetFavoritesByUserIdAsync(string userId)
        {
            return await _context.FavoriteArtworks
                .Where(fa => fa.UserId == userId)
                .Include(fa => fa.Artwork)
                .ToListAsync();
        }

        public async Task<bool> IsFavoriteAsync(string userId, int artworkId)
        {
            return await _context.FavoriteArtworks
                .AnyAsync(fa => fa.UserId == userId && fa.ArtworkID == artworkId);
        }

        public async Task AddFavoriteAsync(FavoriteArtwork favoriteArtwork)
        {
            var existingArtwork = await _context.Artworks.FindAsync(favoriteArtwork.ArtworkID);

            if (existingArtwork == null)
            {
                throw new Exception("Artwork not found.");
            }

            favoriteArtwork.Artwork = existingArtwork; 
            await _context.FavoriteArtworks.AddAsync(favoriteArtwork);
            await _context.SaveChangesAsync();
        }


        public async Task RemoveFavoriteAsync(string userId, int artworkId)
        {
            var favoriteArtwork = await _context.FavoriteArtworks
                .FirstOrDefaultAsync(fa => fa.UserId == userId && fa.ArtworkID == artworkId);

            if (favoriteArtwork != null)
            {
                _context.FavoriteArtworks.Remove(favoriteArtwork);
                await _context.SaveChangesAsync();
            }
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}
