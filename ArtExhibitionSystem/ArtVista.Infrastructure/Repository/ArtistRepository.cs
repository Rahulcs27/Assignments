using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtVista.Application.Interfaces;
using ArtVista.Domain.Entities;
using ArtVista.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ArtVista.Infrastructure.Repository
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly ArtVistaDbContext _context;

        public ArtistRepository(ArtVistaDbContext context)
        {
            _context = context;
        }

        public async Task<Artist?> GetArtistByUserIdAsync(string userId)
        {
            return await _context.Artists.FirstOrDefaultAsync(a => a.ArtistID == userId);
        }

        public async Task AddArtistAsync(Artist artist)
        {
            await _context.Artists.AddAsync(artist);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Artist>> GetAllArtistsAsync()
        {
            return await _context.Artists.ToListAsync();
        }
    }
}
