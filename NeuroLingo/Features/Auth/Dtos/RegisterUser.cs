using System.ComponentModel.DataAnnotations;

namespace NeuroLingo.Features.Auth.Dtos;

public sealed record RegisterUserDto
{
    public required string Email { get; init; } = string.Empty;
    public required string Password { get; init; } = string.Empty;

    [Compare(nameof(Password), ErrorMessage = "Confirm Password must match Password.")]
    public required string ConfirmPassword { get; init; } = string.Empty;

    public required string UserName { get; init; } = string.Empty;
}