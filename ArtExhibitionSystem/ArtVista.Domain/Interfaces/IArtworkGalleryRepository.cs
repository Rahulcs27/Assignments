using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtVista.Domain.Entities;

namespace ArtVista.Domain.Interfaces
{
    public interface IArtworkGalleryRepository
    {
        Task AddArtworkToGalleryAsync(int artworkId, int galleryId);
        Task RemoveArtworkFromGalleryAsync(int artworkId, int galleryId);
        Task<List<Artwork>> GetArtworksInGalleryAsync(int galleryId);
    }
}
