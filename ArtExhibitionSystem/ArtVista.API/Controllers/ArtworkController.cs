using System.Security.Claims;
using ArtVista.Application.Interfaces;
using ArtVista.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArtVista.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtworkController : ControllerBase
    {
        private readonly IArtworkService _artworkService;

        public ArtworkController(IArtworkService artworkService)
        {
            _artworkService = artworkService;
        }
        [Authorize(Roles = "Artist")]
        [HttpPost]
        public async Task<IActionResult> AddArtwork([FromBody] Artwork artwork)
        {
            await _artworkService.AddArtworkAsync(artwork);
            return Ok(new { message = "Artwork added successfully" });
        }
        [Authorize(Roles = "Artist")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArtwork(int id, [FromBody] Artwork artwork)
        {
            var userId = User.FindFirst(ClaimTypes.Sid)?.Value;

            if (userId == null)
                return Unauthorized("User authentication failed.");

            artwork.ArtworkID = id;
            var result = await _artworkService.UpdateArtworkAsync(artwork, userId);

            return Ok(new { message = "Artwork updated successfully" });
        }
        [Authorize(Roles = "Artist")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtwork(int id)
        {
            var userId = User.FindFirst(ClaimTypes.Sid)?.Value;

            if (userId == null)
                return Unauthorized("User authentication failed.");

            var result = await _artworkService.RemoveArtworkAsync(id, userId);

            return result ? Ok("Artwork deleted successfully") : Forbid("You can only delete your own artworks.");
        }
        [HttpGet]
        public async Task<IActionResult> GetAllArtworks()
        {
            var artworks = await _artworkService.GetAllArtworksAsync();
            return Ok(artworks);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetArtworkById(int id)
        {
            var artwork = await _artworkService.GetArtworkByIdAsync(id);
            return artwork != null ? Ok(artwork) : NotFound("Artwork not found");
        }

        [HttpGet("search/{keyword}")]
        public async Task<IActionResult> SearchArtworks(string keyword)
        {
            var artworks = await _artworkService.SearchArtworksAsync(keyword);
            return Ok(artworks);
        }
    }
}
