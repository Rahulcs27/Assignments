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
    public class FavoriteArtworkService : IFavoriteArtworkService
    {
        private readonly IFavoriteArtworkRepository _favoriteArtworkRepository;
        private readonly IArtworkRepository _artworkRepository;

        public FavoriteArtworkService(IFavoriteArtworkRepository favoriteArtworkRepository, IArtworkRepository artworkRepository)
        {
            _favoriteArtworkRepository = favoriteArtworkRepository;
            _artworkRepository = artworkRepository;
        }
        public async Task<IEnumerable<FavoriteArtwork>> GetUserFavoritesAsync(string userId)
        {
            return await _favoriteArtworkRepository.GetFavoritesByUserIdAsync(userId);
        }

        public async Task<bool> ToggleFavoriteAsync(string userId, int artworkId)
        {
            // Check if the artwork exists before adding it to favorites
            var existingArtwork = await _artworkRepository.GetArtworkByIdAsync(artworkId);
            if (existingArtwork == null)
            {
                throw new Exception("Artwork not found.");
            }

            // Check if the artwork is already favorited
            var isFavorite = await _favoriteArtworkRepository.IsFavoriteAsync(userId, artworkId);

            if (isFavorite)
            {
                await _favoriteArtworkRepository.RemoveFavoriteAsync(userId, artworkId);
                return false; // Artwork removed from favorites
            }
            else
            {
                var favoriteArtwork = new FavoriteArtwork
                {
                    UserId = userId,
                    ArtworkID = artworkId,
                    Artwork = existingArtwork // Explicitly attach Artwork
                };

                await _favoriteArtworkRepository.AddFavoriteAsync(favoriteArtwork);
                return true; // Artwork added to favorites
            }
        }

    }

}
