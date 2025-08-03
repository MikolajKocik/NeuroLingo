using System.ComponentModel.DataAnnotations;

namespace NeuroLingo.Features.Auth.Dtos;

public sealed record RegisterUserDto
{
    public required string Email { get; init; } = string.Empty;

    public required string Password { get; init; } = string.Empty;
}