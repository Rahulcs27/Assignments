using ArtVista.Application.Interfaces;
using ArtVista.Domain.Entities;
using ArtVista.Infrastructure.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArtVista.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class GalleryController : ControllerBase
    {
        private readonly IGalleryService _galleryService;
        private readonly IArtistRepository _artistRepository;

        public GalleryController(IGalleryService galleryService, IArtistRepository artistRepository)
        {
            _galleryService = galleryService;
            _artistRepository = artistRepository;
        }

        [Authorize(Roles = "Artist")] 
        [HttpPost("create")]
        public async Task<IActionResult> CreateGallery([FromBody] CreateGalleryDto galleryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var artist = await _artistRepository.GetArtistByUserIdAsync(galleryDto.ArtistId);
            if (artist == null)
            {
                return NotFound(new { message = "Artist not found." });
            }

            var gallery = new Gallery
            {
                Name = galleryDto.Name,
                Description = galleryDto.Description,
                Location = galleryDto.Location,
                ArtistId = galleryDto.ArtistId, 
                Artist = artist
            };

            await _galleryService.AddGalleryAsync(gallery);

            return Ok(new { success = true, message = "Gallery created successfully" });
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAllGalleries()
        {
            var galleries = await _galleryService.GetAllGalleriesAsync();
            return Ok(galleries);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGalleryById(int id)
        {
            var gallery = await _galleryService.GetGalleryByIdAsync(id);
            if (gallery == null) return NotFound("Gallery not found.");
            return Ok(gallery);
        }

        [HttpPut("Update/{id}")]
        [Authorize(Roles = "Artist")]
        public async Task<IActionResult> UpdateGallery(int id, [FromBody] Gallery gallery)
        {
            if (id != gallery.GalleryID) return BadRequest("Gallery ID mismatch.");

            var result = await _galleryService.UpdateGalleryAsync(gallery);
            if (!result) return BadRequest("Gallery update failed.");
            return Ok(new { success = true, message = "Gallery updated successfully." });
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "Artist")]
        public async Task<IActionResult> DeleteGallery(int id)
        {
            var result = await _galleryService.DeleteGalleryAsync(id);
            if (!result) return BadRequest("Gallery deletion failed.");
            return Ok(new { success = true, message = "Gallery deleted successfully." });
        }
    }
}
