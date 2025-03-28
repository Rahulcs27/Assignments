using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtVista.Application.Interfaces;
using ArtVista.Domain.Entities;

namespace ArtVista.Application.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IArtistRepository _artistRepository;

        public ArtistService(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public async Task<Artist?> GetArtistByUserIdAsync(string userId)
        {
            return await _artistRepository.GetArtistByUserIdAsync(userId);
        }

        public async Task AddArtistAsync(Artist artist)
        {
            await _artistRepository.AddArtistAsync(artist);
        }

        public async Task<IEnumerable<Artist>> GetAllArtistsAsync()
        {
            return await _artistRepository.GetAllArtistsAsync();
        }
    }
}
