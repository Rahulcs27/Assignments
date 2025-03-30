using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtVista.Domain.Entities;

namespace ArtVista.Domain.Interfaces
{
    public interface IArtworkRepository
    {
        Task<Artwork?> GetArtworkByIdAsync(int artworkId);
        Task AddArtworkAsync(Artwork artwork);
        Task<IEnumerable<Artwork>> GetAllArtworksAsync();
        Task<bool> UpdateArtworkAsync(Artwork artwork,string userId);
        Task<bool> RemoveArtworkAsync(int artworkId,string userId);
        Task<List<Artwork>> SearchArtworksAsync(string keyword);
    }
}
