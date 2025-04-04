using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ArtVista.Application.DTOs;
using ArtVista.Identity.Model;
using ArtVista.Application.Interfaces;
using ArtVista.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ArtVista.Identity.Context;
using ArtVista.Application.Exceptions;
namespace ArtVista.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IArtistService _artistService; 
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ArtIdentityDbContext _identityDbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtSettings _jwtSettings;

        public AuthService(IArtistService artistService1,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
             ArtIdentityDbContext identityDbContext,
            RoleManager<IdentityRole> roleManager,
            IOptions<JwtSettings> jwtSettings)  
        {
            _artistService = artistService1;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _identityDbContext = identityDbContext;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<AuthResponse> Login(AuthRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            var token = await GenerateJwtToken(user);
            var roles = await _userManager.GetRolesAsync(user);

            return new AuthResponse
            {
                Token = token,
                Email = user.Email!,
                Role = roles.FirstOrDefault() ?? "User"
            };
        }
        public async Task<string> Register(RegistrationRequest request, ArtistDTO ato)
        {
            using var transaction = await _identityDbContext.Database.BeginTransactionAsync();
            try
            {
                ValidatePassword(request.Password);

                var user = new ApplicationUser
                {
                    UserName = request.Email,
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    DateOfBirth = request.DateOfBirth
                };

                var result = await _userManager.CreateAsync(user, request.Password);
                if (!result.Succeeded)
                {
                    throw new ValidationException("User registration failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }

                user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    throw new NotFoundException("User retrieval failed after creation.");
                }

                var role = string.IsNullOrEmpty(request.Role) ? "User" : request.Role;
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }

                var roleResult = await _userManager.AddToRoleAsync(user, role);
                if (!roleResult.Succeeded)
                {
                    throw new ValidationException("Role assignment failed: " + string.Join(", ", roleResult.Errors.Select(e => e.Description)));
                }

                if (role == "Artist")
                {
                    RegisterAsArtist(user, ato);
                }

                await transaction.CommitAsync();
                return $"User registered successfully with role: {role}";
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw; 
            }
        }
        private void ValidatePassword(string password)
        {
            if (password.Length < 8)
                throw new ValidationException("Password must be at least 8 characters long.");

            if (!password.Any(char.IsUpper))
                throw new ValidationException("Password must contain at least one uppercase letter.");

            if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
                throw new ValidationException("Password must contain at least one special character.");
        }


        public async Task RegisterAsArtist(ApplicationUser user, ArtistDTO ato)
        {
            var artist = new Artist
            {
                ArtistID = user.Id, 
                Name = user.FirstName,
                BirthDate = user.DateOfBirth,
                Phone = ato.Phone
            };

            await _artistService.AddArtistAsync(artist);
        }

        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email!),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("userId", user.Id),
                new Claim(ClaimTypes.Sid, user.Id)
            };

            claims.AddRange(roleClaims);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
