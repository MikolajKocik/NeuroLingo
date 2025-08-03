using System.ComponentModel.DataAnnotations;

namespace NeuroLingo.Features.Auth.ViewModels;

public sealed class LoginUserViewModel
{
    [EmailAddress]
    public required string Email { get; set; } 

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public required string Password { get; set; } 
}
