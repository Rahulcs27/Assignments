using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ArtVista.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using ArtVista.Identity.Model;

namespace ArtVista.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteArtworkController : ControllerBase
    {
        private readonly IFavoriteArtworkService _favoriteArtworkService;
        private readonly UserManager<ApplicationUser> _userManager;

        public FavoriteArtworkController(IFavoriteArtworkService favoriteArtworkService, UserManager<ApplicationUser> userManager)
        {
            _favoriteArtworkService = favoriteArtworkService;
            _userManager = userManager;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserFavorites(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId); // ✅ Validate user here
            if (user == null) return NotFound("User not found");

            var favorites = await _favoriteArtworkService.GetUserFavoritesAsync(userId);
            return Ok(favorites);
        }
        [HttpPost("ToggleFavorite")]
        public async Task<IActionResult> ToggleFavorite(int artworkId)
        {
            var userEmail = User.Identity.Name; // This is returning the email
            if (string.IsNullOrEmpty(userEmail))
                return Unauthorized();

            // Fetch the actual user ID (GUID) from IdentityDbContext
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
                return Unauthorized();

            string userId = user.Id; // Now we get the actual GUID

            var result = await _favoriteArtworkService.ToggleFavoriteAsync(userId, artworkId);
            return Ok(new { success = result });
        }

    }
}
