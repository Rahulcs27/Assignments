using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtVista.Domain.Entities;

namespace ArtVista.Application.Interfaces
{
    public interface IArtworkGalleryService
    {
        Task<bool> AddArtworkToGalleryAsync(int artworkId, int galleryId);
        Task<bool> RemoveArtworkFromGalleryAsync(int artworkId, int galleryId);
        Task<List<Artwork>> GetArtworksInGalleryAsync(int galleryId);
    }

}
