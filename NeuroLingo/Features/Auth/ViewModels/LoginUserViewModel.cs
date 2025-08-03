using System.ComponentModel.DataAnnotations;

namespace NeuroLingo.Features.Auth.ViewModels
{
    public sealed class LoginUserViewModel
    {
        [EmailAddress]
        public required string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public required string Password { get; set; } = string.Empty;
    }
}
