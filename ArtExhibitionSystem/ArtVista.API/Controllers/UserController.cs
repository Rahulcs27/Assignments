using ArtVista.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArtVista.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly IArtistService _artistService;
        UserController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        //[HttpGet("GetArtistById")]
        //public async Task<IActionResult> GetArtistByIdAsync(string userId)
        //{
        //    var foundArtist = await _artistService.GetArtistByUserIdAsync(userId);
        //    return Ok(foundArtist);
        //}

    }
}
