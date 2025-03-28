using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ArtVista.Application.Interfaces;
using ArtVista.Application.DTOs;

namespace ArtVista.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Handles user login and returns a JWT token.
        /// </summary>
        /// <param name="request">User login credentials.</param>
        /// <returns>Authentication response with token.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _authService.Login(request);
            return Ok(response);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegistrationRequest registrationRequest, [FromQuery]ArtistDTO ato)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.Register(registrationRequest, ato);
            return Ok(new { message = result });
        }
    }
}
