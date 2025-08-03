using NeuroLingo.Features.Auth.Models;

namespace NeuroLingo.Features.Auth.Services
{
    public interface IAuthService
    {
        Task<User> RegisterUserAsync(string email, string password, string username);
        Task<User> LoginUserAsync(string email, string password);
    }
}
