namespace NeuroLingo.Features.Auth.Dtos;

public sealed record LoginUserDto
{
    public required string Email { get; set; } = string.Empty;
    public required string Password { get; set; } = string.Empty;
}