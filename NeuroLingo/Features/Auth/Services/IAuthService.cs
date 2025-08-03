using NeuroLingo.Features.Auth.Dtos;
using NeuroLingo.Features.Auth.Models;

namespace NeuroLingo.Features.Auth.Services
{
    public interface IAuthService
    {
        Task<User> RegisterUserAsync(RegisterUserDto dto);
        Task<User> LoginUserAsync(LoginUserDto dto);
        Task Logout();
    }
}
