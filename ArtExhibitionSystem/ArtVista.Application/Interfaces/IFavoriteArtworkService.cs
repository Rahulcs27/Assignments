using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtVista.Domain.Entities;

namespace ArtVista.Application.Interfaces
{
    public interface IFavoriteArtworkService
    {
        Task<IEnumerable<FavoriteArtwork>> GetUserFavoritesAsync(string userId);
        Task<bool> ToggleFavoriteAsync(string userId, int artworkId);
    }
}
