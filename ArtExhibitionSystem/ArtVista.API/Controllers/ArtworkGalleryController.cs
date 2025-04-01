using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ArtVista.Application.Interfaces;
using System.Threading.Tasks;

namespace ArtVista.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtworkGalleryController : ControllerBase
    {
        private readonly IArtworkGalleryService _artworkGalleryService;

        public ArtworkGalleryController(IArtworkGalleryService artworkGalleryService)
        {
            _artworkGalleryService = artworkGalleryService;
        }

        [HttpPost("add")]
        [Authorize(Roles = "Artist")]
        public async Task<IActionResult> AddArtworkToGallery(int artworkId, int galleryId)
        {
            try
            {
                var result = await _artworkGalleryService.AddArtworkToGalleryAsync(artworkId, galleryId);
                if (!result) return BadRequest("Artwork already exists in this gallery.");
                return Ok(new { success = true, message = "Artwork added to gallery successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpDelete("remove")]
        [Authorize(Roles = "Artist")]
        public async Task<IActionResult> RemoveArtworkFromGallery(int artworkId, int galleryId)
        {
            try
            {
                var result = await _artworkGalleryService.RemoveArtworkFromGalleryAsync(artworkId, galleryId);
                if (!result) return BadRequest("Artwork not found in this gallery.");
                return Ok(new { success = true, message = "Artwork removed from gallery successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("gallery/{galleryId}")]
        public async Task<IActionResult> GetArtworksInGallery(int galleryId)
        {
            try
            {
                var artworks = await _artworkGalleryService.GetArtworksInGalleryAsync(galleryId);
                if (artworks == null || artworks.Count == 0)
                    return NotFound(new { success = false, message = "No artworks found in this gallery." });

                return Ok(artworks);
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
