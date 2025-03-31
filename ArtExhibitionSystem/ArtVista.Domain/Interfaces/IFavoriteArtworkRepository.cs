using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtVista.Domain.Entities;

namespace ArtVista.Domain.Interfaces
{
    public interface IFavoriteArtworkRepository
    {
        Task<IEnumerable<FavoriteArtwork>> GetFavoritesByUserIdAsync(string userId);
        Task<bool> IsFavoriteAsync(string userId, int artworkId);
        Task AddFavoriteAsync(FavoriteArtwork favoriteArtwork);
        Task RemoveFavoriteAsync(string userId, int artworkId);
        Task SaveChangesAsync();
    }

}
