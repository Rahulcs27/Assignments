using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtVista.Domain.Entities;

namespace ArtVista.Domain.Interfaces
{
    public interface IGalleryRepository
    {
        Task<IEnumerable<Gallery>> GetAllGalleriesAsync();
        Task<Gallery> GetGalleryByIdAsync(int galleryId);
        Task AddGalleryAsync(Gallery gallery);
        Task UpdateGalleryAsync(Gallery gallery);
        Task DeleteGalleryAsync(int galleryId);
    }
}
