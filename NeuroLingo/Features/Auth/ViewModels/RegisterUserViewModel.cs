using System.ComponentModel.DataAnnotations;

namespace NeuroLingo.Features.Auth.ViewModels;

public sealed class RegisterUserViewModel
{
    [Required]
    [EmailAddress]
    [Display(Name = "Email address")]
    public string Email { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$",
        ErrorMessage = "Password must be at least 8 characters long, contain uppercase, lowercase and a digit.")]
    public string Password { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
