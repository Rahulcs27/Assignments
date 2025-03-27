using ArtVista.Application.DTOs;
using System.Threading.Tasks;

namespace ArtVista.Application.Interfaces
{
    public interface IUserService
    {
        Task<AuthResponse> RegisterUserAsync(RegistrationRequest request);
        Task<AuthResponse> LoginAsync(AuthRequest request);
    }
}
