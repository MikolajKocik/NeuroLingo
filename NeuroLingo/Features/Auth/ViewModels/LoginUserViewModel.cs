using System.ComponentModel.DataAnnotations;

namespace NeuroLingo.Features.Auth.ViewModels
{
    public sealed class LoginUserViewModel
    {
        [EmailAddress]
        public required string Email { get; set; } = string.Empty;
        public required string Password { get; set; } = string.Empty;
    }
}
