using System.ComponentModel.DataAnnotations;

namespace NeuroLingo.Features.Auth.ViewModels;

public sealed class RegisterUserViewModel
{
    [EmailAddress]
    [Display(Name = "Email address")]
    public required string Email { get; set; } = string.Empty;

    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$",
        ErrorMessage = "Password must be at least 8 characters long, contain uppercase, lowercase and a digit.")]
    public required string Password { get; set; } = string.Empty;

    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
    public required string ConfirmPassword { get; set; } = string.Empty;
}
