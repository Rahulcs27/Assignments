using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtVista.Application.DTOs;
using ArtVista.Domain.Entities;

namespace ArtVista.Application.Interfaces
{
    public interface IArtistService
    {
        Task<Artist?> GetArtistByUserIdAsync(string userId);
        Task AddArtistAsync(Artist artist);
        Task<IEnumerable<Artist>> GetAllArtistsAsync();
    }
}
