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
    public class GalleryRepository : IGalleryRepository
    {
        private readonly ArtVistaDbContext _context;

        public GalleryRepository(ArtVistaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Gallery>> GetAllGalleriesAsync()
        {
            return await _context.Galleries.Include(g => g.Artist).ToListAsync();
        }

        public async Task<Gallery> GetGalleryByIdAsync(int galleryId)
        {
            return await _context.Galleries
                .Include(g => g.Artist)
                .FirstOrDefaultAsync(g => g.GalleryID == galleryId);
        }

        public async Task AddGalleryAsync(Gallery gallery)
        {
            await _context.Galleries.AddAsync(gallery);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGalleryAsync(Gallery gallery)
        {
            _context.Galleries.Update(gallery);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGalleryAsync(int galleryId)
        {
            var gallery = await _context.Galleries.FindAsync(galleryId);
            if (gallery != null)
            {
                _context.Galleries.Remove(gallery);
                await _context.SaveChangesAsync();
            }
        }
    }
}
