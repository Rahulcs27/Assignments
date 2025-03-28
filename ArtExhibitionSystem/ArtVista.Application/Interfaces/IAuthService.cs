using ArtVista.Application.DTOs;
using System.Threading.Tasks;

namespace ArtVista.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(AuthRequest request);
        Task<string> Register(RegistrationRequest request, ArtistDTO ato);
    }
}
