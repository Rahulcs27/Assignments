using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtVista.Domain.Entities;

namespace ArtVista.Application.Interfaces
{
    public interface IGalleryService
    {
        Task<IEnumerable<Gallery>> GetAllGalleriesAsync();
        Task<Gallery> GetGalleryByIdAsync(int galleryId);
        Task<bool> AddGalleryAsync(Gallery gallery);
        Task<bool> UpdateGalleryAsync(Gallery gallery);
        Task<bool> DeleteGalleryAsync(int galleryId);
    }
}
