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
    public class ArtworkGalleryRepository : IArtworkGalleryRepository
    {
        private readonly ArtVistaDbContext _context;

        public ArtworkGalleryRepository(ArtVistaDbContext context)
        {
            _context = context;
        }

        public async Task AddArtworkToGalleryAsync(int artworkId, int galleryId)
        {
            var artworkGallery = new ArtworkGallery { ArtworkID = artworkId, GalleryID = galleryId };
            _context.ArtworkGalleries.Add(artworkGallery);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveArtworkFromGalleryAsync(int artworkId, int galleryId)
        {
            var artworkGallery = await _context.ArtworkGalleries
                .FirstOrDefaultAsync(ag => ag.ArtworkID == artworkId && ag.GalleryID == galleryId);

            if (artworkGallery != null)
            {
                _context.ArtworkGalleries.Remove(artworkGallery);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Artwork>> GetArtworksInGalleryAsync(int galleryId)
        {
            return await _context.ArtworkGalleries
                .Where(ag => ag.GalleryID == galleryId)
                .Select(ag => ag.Artwork)
                .ToListAsync();
        }
    }

}
