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
    public class GalleryService : IGalleryService
    {
        private readonly IGalleryRepository _galleryRepository;

        public GalleryService(IGalleryRepository galleryRepository)
        {
            _galleryRepository = galleryRepository;
        }

        public async Task<IEnumerable<Gallery>> GetAllGalleriesAsync()
        {
            return await _galleryRepository.GetAllGalleriesAsync();
        }

        public async Task<Gallery> GetGalleryByIdAsync(int galleryId)
        {
            return await _galleryRepository.GetGalleryByIdAsync(galleryId);
        }

        public async Task<bool> AddGalleryAsync(Gallery gallery)
        {
            await _galleryRepository.AddGalleryAsync(gallery);
            return true;
        }

        public async Task<bool> UpdateGalleryAsync(Gallery gallery)
        {
            await _galleryRepository.UpdateGalleryAsync(gallery);
            return true;
        }

        public async Task<bool> DeleteGalleryAsync(int galleryId)
        {
            await _galleryRepository.DeleteGalleryAsync(galleryId);
            return true;
        }
    }
}
