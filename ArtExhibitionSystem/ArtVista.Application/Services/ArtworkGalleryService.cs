using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtVista.Application.Interfaces;
using ArtVista.Domain.Entities;
using ArtVista.Domain.Interfaces;

namespace ArtVista.Application.Services
{
    public class ArtworkGalleryService : IArtworkGalleryService
    {
        private readonly IArtworkGalleryRepository _artworkGalleryRepository;
        private readonly IArtworkRepository _artworkRepository;
        private readonly IGalleryRepository _galleryRepository;

        public ArtworkGalleryService(
            IArtworkGalleryRepository artworkGalleryRepository,
            IArtworkRepository artworkRepository,
            IGalleryRepository galleryRepository)
        {
            _artworkGalleryRepository = artworkGalleryRepository;
            _artworkRepository = artworkRepository;
            _galleryRepository = galleryRepository;
        }

        public async Task<bool> AddArtworkToGalleryAsync(int artworkId, int galleryId)
        {
            // Check if artwork exists
            var artwork = await _artworkRepository.GetArtworkByIdAsync(artworkId);
            if (artwork == null)
                throw new Exception("Artwork not found");

            // Check if gallery exists
            var gallery = await _galleryRepository.GetGalleryByIdAsync(galleryId);
            if (gallery == null)
                throw new Exception("Gallery not found");

            // Add artwork to gallery
            await _artworkGalleryRepository.AddArtworkToGalleryAsync(artworkId, galleryId);
            return true;
        }

        public async Task<bool> RemoveArtworkFromGalleryAsync(int artworkId, int galleryId)
        {
            await _artworkGalleryRepository.RemoveArtworkFromGalleryAsync(artworkId, galleryId);
            return true;
        }

        public async Task<List<Artwork>> GetArtworksInGalleryAsync(int galleryId)
        {
            return await _artworkGalleryRepository.GetArtworksInGalleryAsync(galleryId);
        }
    }

}
