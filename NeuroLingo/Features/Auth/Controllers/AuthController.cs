using Microsoft.AspNetCore.Mvc;
using NeuroLingo.Exceptions;
using NeuroLingo.Features.Auth.Dtos;
using NeuroLingo.Features.Auth.Services;
using System.ComponentModel.DataAnnotations;

namespace NeuroLingo.Features.Auth.Controllers;

/// <summary>
/// Provides authentication-related actions, including user registration, login, and logout.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
   
    /// <summary>
    /// Register a new user
    /// </summary>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    isSuccess = false,
                    errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList()
                });
            }

            await _authService.RegisterUserAsync(dto);

            return Ok(new
            {
                isSuccess = true,
                message = "Registration successful!"
            });
        }
        catch (ConflictException)
        {
            return Conflict(new
            {
                isSuccess = false,
                errors = new[] { "This email is already in use" }
            });
        }
        catch (ValidationException ex)
        {
            return BadRequest(new
            {
                isSuccess = false,
                errors = new[] { ex.Message }
            });
        }
    }

    /// <summary>
    /// Log in an existing user
    /// </summary>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    isSuccess = false,
                    errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList()
                });
            }

            await _authService.LoginUserAsync(dto);

            return Ok(new
            {
                isSuccess = true,
                message = "Login successful!"
            });
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized(new
            {
                isSuccess = false,
                errors = new[] { "Invalid email or password" }
            });
        }
        catch (NotFoundException)
        {
            return NotFound(new
            {
                isSuccess = false,
                errors = new[] { $"User with email {dto.Email} not found" }
            });
        }
    }

    /// <summary>
    /// Log out the current user
    /// </summary>
    [HttpPost("logout")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Logout()
    {
        await _authService.Logout();
        return Ok(new
        {
            isSuccess = true,
            message = "Logout successful!"
        });
    }
}
