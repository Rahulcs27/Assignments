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
    public class ArtworkService : IArtworkService
    {
        private readonly IArtworkRepository _artworkRepository;

        public ArtworkService(IArtworkRepository artworkRepository)
        {
            _artworkRepository = artworkRepository;
        }

        public async Task<Artwork?> GetArtworkByIdAsync(int artworkId)
        {
            return await _artworkRepository.GetArtworkByIdAsync(artworkId);
        }

        public async Task AddArtworkAsync(Artwork artwork)
        {

            await _artworkRepository.AddArtworkAsync(artwork);
        }

        public async Task<IEnumerable<Artwork>> GetAllArtworksAsync()
        {
            return await _artworkRepository.GetAllArtworksAsync();
        }

        public async Task<bool> UpdateArtworkAsync(Artwork artwork,string userId)
        {
            return await _artworkRepository.UpdateArtworkAsync(artwork,userId);
        }

        public async Task<bool> RemoveArtworkAsync(int artworkId ,string userId)
        {
            return await _artworkRepository.RemoveArtworkAsync(artworkId,userId);
        }

        public async Task<List<Artwork>> SearchArtworksAsync(string keyword)
        {
            return await _artworkRepository.SearchArtworksAsync(keyword);
        }
    }
}
